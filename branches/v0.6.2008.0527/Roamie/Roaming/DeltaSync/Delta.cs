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
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Virtuoso.Miranda.Plugins.Infrastructure;
using Virtuoso.Miranda.Plugins;
using System.Diagnostics;

namespace Virtuoso.Miranda.Roamie.Roaming.DeltaSync
{
    [Serializable]
    internal class Delta
    {
        #region Constants

        private const string DeltaSuffix = "dbd.bin";

        private const string MS_DB_EVENT_ADD = "DB/Event/Add";
        private const string MS_DB_EVENT_MARKREAD = "DB/Event/MarkRead";
        private const string MS_DB_EVENT_FINDFIRSTUNREAD = "DB/Event/FindFirstUnread";

        #endregion

        #region Properties

        private LinkedList<DeltaEntry> entries;
        public LinkedList<DeltaEntry> Entries
        {
            get { return entries; }
            set { entries = value; }
        }

        private DateTime timestamp;
        public DateTime Timestamp
        {
            get { return timestamp; }
            set { timestamp = value; }
        }

        private bool applied;
        public bool Applied
        {
            get { return applied; }
        }

        private DeltaManifest manifest;
        public DeltaManifest Manifest
        {
            get { return manifest; }
        }

        #endregion

        #region .ctors

        public Delta(DeltaManifest manifest) : this(manifest, null) { }

        public Delta(DeltaManifest manifest, LinkedList<DeltaEntry> deltaEntries)
        {
            if (manifest == null)
                throw new ArgumentNullException("manifest");

            if (deltaEntries == null)
                this.entries = new LinkedList<DeltaEntry>();
            else
                this.entries = deltaEntries;

            this.manifest = manifest;
            this.timestamp = DateTime.Now;
        }

        #endregion

        #region Methods

        public void Serialize(Stream destination)
        {
            if (destination == null)
                throw new ArgumentNullException("destination");

            if (!destination.CanWrite)
                throw new ArgumentException();

            if (Manifest == null)
                throw new InvalidOperationException();

            using (MemoryStream encryptedStream = new MemoryStream(4096))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(encryptedStream, this);

                encryptedStream.Seek(0, SeekOrigin.Begin);
                SecureStreamCompactor.CompressAndEncrypt(encryptedStream, destination, Manifest.AssociatedProfile.DatabasePassword);
            }
        }

        public static Delta Deserialize(Stream source, DeltaManifest manifest)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            if (manifest == null)
                throw new ArgumentNullException("manifest");

            if (!source.CanRead)
                throw new ArgumentException();

            using (MemoryStream decryptedStream = new MemoryStream(8192))
            {
                SecureStreamCompactor.DecryptAndDecompress(source, decryptedStream, manifest.AssociatedProfile.DatabasePassword);
                decryptedStream.Seek(0, SeekOrigin.Begin);

                BinaryFormatter formatter = new BinaryFormatter();
                Delta delta = (Delta)formatter.Deserialize(decryptedStream);

                delta.manifest = manifest;
                return delta;
            }
        }

        public static string GetPathForDelta(DeltaManifest manifest, int deltaIndex)
        {
            if (manifest == null)
                throw new ArgumentNullException("manifest");

            return String.Format("{0}.d{1}.{2}", manifest.AssociatedProfile.RemoteHost, deltaIndex.ToString("000"), DeltaSuffix);
        }

        /// <summary>
        /// Applies the delta on the database.
        /// </summary>
        /// <returns>TRUE if the delta was completely applied; FALSE if some entries failed to be applied.</returns>
        public bool Apply()
        {
            if (applied)
                throw new InvalidOperationException();

            List<IntPtr> affectedContactHandles = new List<IntPtr>(5);
            bool completed = true;

            foreach (DeltaEntry entry in entries)
            {
                bool merged = entry.Merge();
                completed &= merged;

                Debug.Assert(merged);
                IntPtr contactHandle = entry.GetContact().MirandaHandle;

                if (!affectedContactHandles.Contains(contactHandle) && contactHandle != ContactInfo.MeNeutral.MirandaHandle)
                    affectedContactHandles.Add(contactHandle);
            }

            foreach (IntPtr affectedContactHandle in affectedContactHandles)
                MarkAllEventsAsRead(affectedContactHandle);

            applied = true;
            return completed;
        }

        private void MarkAllEventsAsRead(IntPtr contactHandle)
        {
            Callback markAsRead = ServiceManager.GetService(MS_DB_EVENT_MARKREAD);
            Callback findFirstUnread = ServiceManager.GetService(MS_DB_EVENT_FINDFIRSTUNREAD);

            IntPtr hEvent = IntPtr.Zero;
            UIntPtr hContact = Translate.ToHandle(contactHandle);

            while ((hEvent = (IntPtr)findFirstUnread(hContact, IntPtr.Zero)) != IntPtr.Zero)
                markAsRead(hContact, hEvent);
        }

        #endregion
    }
}
