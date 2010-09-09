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
using System.Collections.Generic;
using System.Text;
using Virtuoso.Miranda.Roamie.Native;
using System.ComponentModel;
using Virtuoso.Hyphen.Mini.Custom;
using System.Runtime.InteropServices;
using System.IO;
using Virtuoso.Miranda.Plugins.Infrastructure;
using System.Runtime.ConstrainedExecution;
using System.Threading;
using System.Reflection;
using Virtuoso.Miranda.Roamie.Properties;

namespace Virtuoso.Miranda.Roamie.Native
{
    internal sealed class ExternalDatabaseDriver : SafeHandle
    {
        #region Fields

        private delegate IntPtr DatabasePluginInfoPrototype(IntPtr reserved);

        private DatabaseLink databaseLink;
        private MirandaPluginInfoPrototype mirandaPluginInfo;
        private MirandaPluginInterfacesPrototype mirandaPluginInterfaces;

        private readonly bool IsEx;

        #endregion

        #region Constants

        private const string DbXSearchPattern = "*.dbx";
        private const string DbSearchPattern = "dbx_*.dll";

        private const string MirandaPluginInfoProc = "MirandaPluginInfo";
        private const string MirandaPluginInfoExProc = "MirandaPluginInfoEx";
        private const string MirandaPluginInterfacesProc = "MirandaPluginInterfaces";

        #endregion

        #region .ctors & .dctors

        private ExternalDatabaseDriver(bool ex) : base(IntPtr.Zero, true)
        {
            this.IsEx = ex;
            string driverPath = FindDriver();

            if (driverPath != null)
                InitializeDriver(driverPath);
            else
                throw new FileNotFoundException(Resources.ExceptionMsg_UnableToFindDbDriver);
        }

        private string FindDriver()
        {
            string[] paths = Directory.GetFiles(new FileInfo(Assembly.GetExecutingAssembly().Location).DirectoryName, DbXSearchPattern, SearchOption.TopDirectoryOnly); 

            if (paths.Length > 0)
                return paths[0];

            paths = Directory.GetFiles(MirandaEnvironment.MirandaPluginsFolderPath, DbSearchPattern, SearchOption.TopDirectoryOnly);

            if (paths.Length > 0)
                return paths[0];

            return null;            
        }

        private void InitializeDriver(string path)
        {
            handle = NativeMethods.LoadLibrary(path);

            if (handle != IntPtr.Zero)
            {
                ProbeDatabasePluginInfoExport();
                ProbePluginInfoExports();
                ProbeMirandaInterfacesExport();
            }
            else
                throw new FileLoadException(path);
        }

        private void ProbeMirandaInterfacesExport()
        {
            if (!IsEx)
                return;

            UIntPtr pProc = NativeMethods.GetProcAddress(handle, MirandaPluginInterfacesProc);

            if (pProc != UIntPtr.Zero)
                mirandaPluginInterfaces = (MirandaPluginInterfacesPrototype)Marshal.GetDelegateForFunctionPointer(Translate.ToHandle(pProc), typeof(MirandaPluginInterfacesPrototype));
            else
                throw new MissingMethodException("MirandaPluginInterfaces");
        }

        private void ProbePluginInfoExports()
        {
            UIntPtr pProc = NativeMethods.GetProcAddress(handle, IsEx ? MirandaPluginInfoExProc : MirandaPluginInfoProc);

            if (pProc != UIntPtr.Zero)
                mirandaPluginInfo = (MirandaPluginInfoPrototype)Marshal.GetDelegateForFunctionPointer(Translate.ToHandle(pProc), typeof(MirandaPluginInfoPrototype));
            else
                throw new MissingMethodException("MirandaPluginInfo[Ex]");
        }

        private void ProbeDatabasePluginInfoExport()
        {
            UIntPtr pProc = NativeMethods.GetProcAddress(handle, "DatabasePluginInfo");

            if (pProc != UIntPtr.Zero)
            {
                DatabasePluginInfoPrototype proc = (DatabasePluginInfoPrototype)Marshal.GetDelegateForFunctionPointer(Translate.ToHandle(pProc), typeof(DatabasePluginInfoPrototype));
                IntPtr pLink = proc(IntPtr.Zero);

                if (pLink != IntPtr.Zero)
                    databaseLink = (DatabaseLink)Marshal.PtrToStructure(pLink, typeof(DatabaseLink));
                else
                    throw new ArgumentException("DatabasePluginInfo");
            }
            else
                throw new MissingMethodException("DatabasePluginInfo");
        }

        public static ExternalDatabaseDriver Load(bool ex)
        {
            return new ExternalDatabaseDriver(ex);
        }

        #endregion        

        #region Properties

        public DatabaseLink DatabaseLink
        {
            get
            {
                if (IsInvalid)                     
                    throw new ObjectDisposedException("ExternalDatabaseDriver");

                return this.databaseLink;
            }
        }

        public MirandaPluginInfoPrototype MirandaPluginInfo
        {
            get
            {
                return mirandaPluginInfo;
            }
        }

        public MirandaPluginInterfacesPrototype MirandaPluginInterfaces
        {
            get
            {
                return mirandaPluginInterfaces;
            }
        }

        #endregion

        #region SafeHandle overrides

        public override bool IsInvalid
        {
            get { return handle == IntPtr.Zero; }
        }

        protected override bool ReleaseHandle()
        {
            try
            {
                if (!IsInvalid)
                    return NativeMethods.FreeLibrary(handle);
                else
                    return false;
            }
            finally
            {
                handle = IntPtr.Zero;
            }
        }

        #endregion
    }
}
