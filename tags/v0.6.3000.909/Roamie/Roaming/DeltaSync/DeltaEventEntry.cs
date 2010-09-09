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
using Virtuoso.Miranda.Plugins.Infrastructure;
using Virtuoso.Miranda.Plugins.Native;
using Virtuoso.Miranda.Roamie.Properties;
using System.Runtime.InteropServices;

namespace Virtuoso.Miranda.Roamie.Roaming.DeltaSync
{
    [Serializable]
    internal class DeltaEventEntry : DeltaEntry
    {
        #region Constants

        private const string MS_DB_EVENT_ADD = "DB/Event/Add";

        #endregion

        #region Properties

        private DBEVENTINFO eventInfo;
        public DBEVENTINFO EventInfo
        {
            get { return eventInfo; }
            set { eventInfo = value; }
        }

        private byte[] eventBlob;
        public byte[] EventBlob
        {
            get { return eventBlob; }
            set { eventBlob = value; }
        }

        private string eventModule;
        public string EventModule
        {
            get { return eventModule; }
            set { eventModule = value; }
        }

        #endregion

        #region .ctors

        private DeltaEventEntry(IntPtr contactHandle, DBEVENTINFO dbEventInfo, byte[] eventBlob) : base(contactHandle)
        {
            EventInfo = dbEventInfo;
            EventBlob = eventBlob;
            EventModule = Translate.ToString(dbEventInfo.Module, StringEncoding.Ansi);
        }

        public static DeltaEventEntry TryCreate(IntPtr contactHandle, DBEVENTINFO dbEventInfo, byte[] eventBlob)
        {
            try
            {
                return new DeltaEventEntry(contactHandle, dbEventInfo, eventBlob);
            }
            catch
            {
                return null;
            }
        }

        #endregion

        #region Methods

        public override int GetHashCode()
        {
            return base.GetHashCode() + eventInfo.GetHashCode() + eventModule.GetHashCode();
        }

        protected override unsafe bool PerformMerge()
        {
            ContactInfo contactInfo = GetContact();

            IntPtr blobPtr = Marshal.AllocHGlobal(eventBlob.Length);
            UnmanagedStringHandle moduleStrPtr = new UnmanagedStringHandle(eventModule, StringEncoding.Ansi);

            try
            {
                Marshal.Copy(eventBlob, 0, blobPtr, eventBlob.Length);

                DBEVENTINFO eventInfo = this.eventInfo;
                eventInfo.Module = moduleStrPtr.IntPtr;
                eventInfo.Flags |= (uint)DatabaseEventProperties.Read;
                eventInfo.BlobSize = (uint)eventBlob.Length;
                eventInfo.BlobPtr = blobPtr;

                int eventHandle = MirandaContext.Current.CallService(MS_DB_EVENT_ADD, Translate.ToHandle(contactInfo.MirandaHandle), new IntPtr(&eventInfo));
                return (eventHandle != 0);                    
            }
            finally
            {
                if (blobPtr != IntPtr.Zero)
                    Marshal.FreeHGlobal(blobPtr);

                moduleStrPtr.Free();
            }
        }        

        #endregion
    }
}
