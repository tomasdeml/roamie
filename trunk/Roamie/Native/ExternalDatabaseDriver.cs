/***********************************************************************************\
 * Virtuoso.Miranda.Roamie (Roamie)                                                *
 * A Miranda plugin providing a remote database synchronization features.          *
 * Copyright (C) 2006-2007 Virtuoso                                                *
 *                    deml.tomas@seznam.cz                                         *
 *                                                                                 *
 * This program is free software; you can redistribute it and/or                   *
 * modify it under the terms of the GNU General Public License                     *
 * as published by the Free Software Foundation; either version 2                  *
 * of the License, or (at your option) any later version.                          *
 *                                                                                 *
 * This program is distributed in the hope that it will be useful,                 *
 * but WITHOUT ANY WARRANTY; without even the implied warranty of                  *
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the                   *
 * GNU General Public License for more details.                                    *
 *                                                                                 *
 * You should have received a copy of the GNU General Public License               *
 * along with this program; if not, write to the Free Software                     *
 * Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA. *
\***********************************************************************************/

using System;
using Virtuoso.Hyphen.Mini.Custom;
using System.Runtime.InteropServices;
using System.IO;
using Virtuoso.Miranda.Plugins.Infrastructure;
using Microsoft.Win32.SafeHandles;
using Virtuoso.Roamie.Properties;

namespace Virtuoso.Roamie.Native
{
    internal sealed class ExternalDatabaseDriver : SafeHandleZeroOrMinusOneIsInvalid
    {
        #region Constants

        private const string DbXSearchPattern = "*.dbx";
        private const string DbSearchPattern = "dbx_*.dll";

        private const string MirandaPluginInfoProc = "MirandaPluginInfo";
        private const string MirandaPluginInfoExProc = "MirandaPluginInfoEx";
        private const string MirandaPluginInterfacesProc = "MirandaPluginInterfaces";
        private const string DatabasePluginInfoProc = "DatabasePluginInfo";

        #endregion

        #region Fields

        private DatabaseLink databaseLink;

        private readonly bool IsPostV07Build20Api;

        #endregion

        #region Properties

        public DatabaseLink DatabaseLink
        {
            get
            {
                if (IsInvalid)
                    throw new ObjectDisposedException("ExternalDatabaseDriver");

                return databaseLink;
            }
        }

        public MirandaPluginInfoPrototype MirandaPluginInfo { get; private set; }

        public MirandaPluginInterfacesPrototype MirandaPluginInterfaces { get; private set; }

        #endregion

        #region .ctors & .dctors

        private ExternalDatabaseDriver(bool isPostV07Build20Api) : base(true)
        {
            IsPostV07Build20Api = isPostV07Build20Api;
            string driverPath = FindDriver();

            if (driverPath != null)
                InitializeDriver(driverPath);
            else
                throw new FileNotFoundException(Resources.ExceptionMsg_UnableToFindDbDriver);
        }

        private static string FindDriver()
        {
            string[] paths = Directory.GetFiles(MirandaEnvironment.MirandaPluginsFolderPath, DbXSearchPattern,
                                                SearchOption.TopDirectoryOnly);

            if (paths.Length > 0)
                return paths[0];

            paths = Directory.GetFiles(MirandaEnvironment.MirandaPluginsFolderPath, DbSearchPattern,
                                       SearchOption.TopDirectoryOnly);

            return paths.Length > 0 ? paths[0] : null;
        }

        private void InitializeDriver(string path)
        {
            handle = NativeMethods.LoadLibrary(path);

            if (handle == IntPtr.Zero)
                throw new FileLoadException(path);
            
            ProbeDatabasePluginInfoExport();
            ProbePluginInfoExports();
            ProbeMirandaInterfacesExport();
        }

        private void ProbeMirandaInterfacesExport()
        {
            if (!IsPostV07Build20Api)
                return;

            UIntPtr pProc = NativeMethods.GetProcAddress(handle, MirandaPluginInterfacesProc);

            if (pProc != UIntPtr.Zero)
                MirandaPluginInterfaces = (MirandaPluginInterfacesPrototype)
                    Marshal.GetDelegateForFunctionPointer(Translate.ToHandle(pProc),
                                                          typeof (MirandaPluginInterfacesPrototype));
            else
                throw new MissingMethodException("MirandaPluginInterfaces");
        }

        private void ProbePluginInfoExports()
        {
            UIntPtr pProc = NativeMethods.GetProcAddress(handle,
                                                         IsPostV07Build20Api ? MirandaPluginInfoExProc : MirandaPluginInfoProc);

            if (pProc != UIntPtr.Zero)
                MirandaPluginInfo = (MirandaPluginInfoPrototype)
                    Marshal.GetDelegateForFunctionPointer(Translate.ToHandle(pProc), typeof (MirandaPluginInfoPrototype));
            else
                throw new MissingMethodException("MirandaPluginInfo[Ex]");
        }

        private void ProbeDatabasePluginInfoExport()
        {
            UIntPtr pProc = NativeMethods.GetProcAddress(handle, DatabasePluginInfoProc);

            if (pProc == UIntPtr.Zero)
                throw new MissingMethodException(DatabasePluginInfoProc);

            DatabasePluginInfoPrototype proc = (DatabasePluginInfoPrototype)
                Marshal.GetDelegateForFunctionPointer(Translate.ToHandle(pProc), typeof (DatabasePluginInfoPrototype));

            IntPtr pDbLink = proc(IntPtr.Zero);

            if (pDbLink != IntPtr.Zero)
                databaseLink = (DatabaseLink) Marshal.PtrToStructure(pDbLink, typeof (DatabaseLink));
            else
                throw new ArgumentException(DatabasePluginInfoProc);
        }

        public static ExternalDatabaseDriver Load(bool isPostV07Build20Api)
        {
            return new ExternalDatabaseDriver(isPostV07Build20Api);
        }

        #endregion      

        #region SafeHandle overrides

        protected override bool ReleaseHandle()
        {
            try
            {
                return !IsInvalid && NativeMethods.FreeLibrary(handle);
            }
            finally
            {
                handle = IntPtr.Zero;
            }
        }

        #endregion
    }
}
