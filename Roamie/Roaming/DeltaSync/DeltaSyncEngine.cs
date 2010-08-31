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
using Virtuoso.Miranda.Roamie.Roaming.Profiles;
using Virtuoso.Miranda.Roamie.Roaming.Providers;
using System.IO;
using Virtuoso.Miranda.Plugins.Infrastructure;
using Virtuoso.Miranda.Plugins;
using Virtuoso.Miranda.Plugins.Native;
using System.Runtime.InteropServices;
using Virtuoso.Miranda.Plugins.Helpers;
using Virtuoso.Miranda.Roamie.Properties;
using System.Diagnostics;

namespace Virtuoso.Miranda.Roamie.Roaming.DeltaSync
{
    // TODO Extract interface

    /// <summary>
    /// Represents the Delta sync engine.
    /// </summary>
    /// <remarks>Codename: Synch</remarks>
    internal partial class DeltaSyncEngine
    {
        #region Fields

        #region Constants

        private const string RS_CONTACT_TOKEN = "ContactToken";
        private const string RS_DB_TOKEN = "DbToken";

        private const string MS_DB_CONTACT_GETCOUNT = "DB/Contact/GetCount",
                            MS_DB_CONTACT_FINDFIRST = "DB/Contact/FindFirst",
                            MS_DB_CONTACT_FINDNEXT = "DB/Contact/FindNext";

        private const string MS_DB_EVENT_FINDLAST = "DB/Event/FindLast",
                             MS_DB_EVENT_FINDFIRST = "DB/Event/FindFirst",
                             MS_DB_EVENT_FINDNEXT = "DB/Event/FindNext",
                             MS_DB_EVENT_GETBLOBSIZE = "DB/Event/GetBlobSize",
                             MS_DB_EVENT_GET = "DB/Event/Get";

        #endregion

        private static readonly Dictionary<IntPtr, Guid> TokenCache = new Dictionary<IntPtr, Guid>(10);
        public const int DeltaCountThreshold = 20;

        #endregion

        #region .ctors

        public DeltaSyncEngine(RoamingProfile profile)
        {
            if (profile == null)
                throw new ArgumentNullException("profile");

            this.profile = profile;

            // Do not touch
            RoamiePlugin.Singleton.RoamingContext.State |= RoamingState.DeltaSyncEngineLoaded;
        }

        #endregion

        #region Properties

        private Snapshot DatabaseSnapshot;
        private DatabaseReplicator Replicator;
        private DeltaManifest deltaManifest;
        private DeltaChain DeltaChain;
        private Delta CurrentDelta;

        private RoamingProfile profile;
        public RoamingProfile Profile
        {
            get { return profile; }
        }

        public bool Initialized
        {
            get { return deltaManifest != null; }
        }

        private DeltaManifest legacyManifest;
        public DeltaManifest LegacyManifest
        {
            get { return legacyManifest; }
        }

        public DeltaManifest DeltaManifest
        {
            get { return deltaManifest; }
        }

        public bool DeltaMergeRecommended
        {
            get
            {
                if (deltaManifest == null)
                    return false;
                else
                    return deltaManifest.DeltaCount >= DeltaCountThreshold;
            }
        }

        #endregion

        #region Tokenization stuff

        #region Db

        private void TokenizeDatabase()
        {
            Guid token = GetDatabaseToken(false);

            if (token == Guid.Empty)
            {
                FlagDatabaseDirty();
                ContactInfo.MeNeutral.WriteSetting(RS_DB_TOKEN, RoamiePlugin.Singleton, Guid.NewGuid().ToString(), DatabaseSettingType.AsciiString);
            }
        }

        private static void FlagDatabaseDirty()
        {
            RoamiePlugin.Singleton.RoamingContext.State |= RoamingState.DeltaIncompatibleChangeOccured;
        }

        public static Guid GetDatabaseToken()
        {
            return GetDatabaseToken(true);
        }

        private static Guid GetDatabaseToken(bool throwIfNone)
        {
            object token = ContactInfo.MeNeutral.ReadSetting(RS_DB_TOKEN, RoamiePlugin.Singleton, DatabaseSettingType.AsciiString);

            if (token != null)
                return new Guid((string)token);
            else if (throwIfNone)
                throw new DeltaSyncException();
            else
                return Guid.Empty;
        }

        #endregion

        #region Contacts

        private static void TokenizeContacts()
        {
            foreach (ContactInfo contactInfo in MirandaContext.Current.MirandaDatabase.GetContacts(true))
                TokenizeContact(contactInfo);
        }

        private static void TokenizeContact(ContactInfo contactInfo)
        {
            if (contactInfo == null)
                throw new ArgumentNullException("contactInfo");

            Guid? token = GetContactToken(contactInfo.MirandaHandle);

            if (token == null)
            {
                FlagDatabaseDirty();
                contactInfo.WriteSetting(RS_CONTACT_TOKEN, RoamiePlugin.Singleton, Guid.NewGuid().ToString(), DatabaseSettingType.AsciiString);
            }
        }

        public static Guid? GetContactToken(IntPtr contactHandle)
        {
            if (TokenCache.ContainsKey(contactHandle))
                return TokenCache[contactHandle];

            object token = ContactInfo.ReadSetting(contactHandle, RS_CONTACT_TOKEN, RoamiePlugin.Singleton, DatabaseSettingType.AsciiString);

            if (token is string)
            {
                Guid guid = new Guid(token.ToString());
                TokenCache[contactHandle] = guid;

                return guid;
            }
            else
                return null;
        }

        public static ContactInfo GetContactFromToken(Guid token)
        {
            IntPtr? handle = GetContactHandleFromToken(token);

            if (handle == null)
                throw new DeltaSyncException(Resources.ExceptionMsg_ContactWithoutTokenFound);
            else
                return new ContactInfo(handle.Value);
        }

        private static IntPtr? GetContactHandleFromToken(Guid token)
        {
            IntPtr meHandle = ContactInfo.MeNeutral.MirandaHandle;

            // Check the Self contact
            Guid? contactToken = GetContactToken(meHandle);

            if (contactToken.GetValueOrDefault() == token)
                return meHandle;

            // Now check other contacts
            foreach (IntPtr contactHandle in MirandaContext.Current.MirandaDatabase.GetContactHandles())
            {
                contactToken = GetContactToken(contactHandle);

                if (contactToken.GetValueOrDefault() == token)
                    return contactHandle;
            }

            return null;
        }

        #endregion

        #endregion

        #region Event replication stuff

#if DEBUG
        private unsafe void SimulateNewEvent()
        {
            DBEVENTINFO info = new DBEVENTINFO(0, IntPtr.Zero);
            info.Module = Translate.ToHandle("ICQ", StringEncoding.Ansi).IntPtr;
            UnmanagedStringHandle str = new UnmanagedStringHandle("aěščřšžšž", StringEncoding.MirandaDefault);
            info.BlobSize = (uint)str.Size;
            info.BlobPtr = str;
            info.EventType = 0;
            info.Flags = 0;
            info.Timestamp = Utilities.GetTimestamp();

            IntPtr result = new IntPtr(MirandaContext.Current.CallService("DB/Event/Add", Translate.ToHandle(MirandaContext.Current.MirandaDatabase.FindContact("177147220", ContactInfoProperty.UniqueID, StringEncoding.Ansi).MirandaHandle), new UnmanagedStructHandle<DBEVENTINFO>(ref info)));
        }
#endif

        private unsafe void GatherNewEvents(LinkedList<DeltaEntry> entries)
        {
            MirandaContext context = MirandaContext.Current;

            foreach (KeyValuePair<IntPtr, IntPtr> contactEventPair in DatabaseSnapshot.EventHandles)
            {
                UIntPtr wEventHandle = Translate.ToHandle(contactEventPair.Value);
                InteropBuffer buffer = InteropBufferPool.AcquireBuffer();

                try
                {
                    while (AdvanceToNextEvent(ref wEventHandle, contactEventPair.Key))
                    {
                        int blobSize = context.CallService(MS_DB_EVENT_GETBLOBSIZE, wEventHandle, IntPtr.Zero);

                        if (blobSize == -1)
                            continue;

                        PrepareBlobBuffer(ref buffer, blobSize);
                        DBEVENTINFO dbEventInfo = new DBEVENTINFO(blobSize, buffer.IntPtr);

                        if (context.CallService(MS_DB_EVENT_GET, wEventHandle, new IntPtr(&dbEventInfo)) != 0)
                            continue;

                        byte[] blob = new byte[blobSize];
                        Marshal.Copy(buffer.IntPtr, blob, 0, blob.Length);

                        DeltaEventEntry entry = DeltaEventEntry.TryCreate(contactEventPair.Key, dbEventInfo, blob);

                        if (entry == null)
                            continue;

                        entries.AddLast(entry);
                    }
                }
                finally
                {
                    if (buffer.Locked)
                        buffer.Unlock();

                    InteropBufferPool.ReleaseBuffer(buffer);
                }
            }
        }

        private static bool AdvanceToNextEvent(ref UIntPtr hEvent, IntPtr contactHandle)
        {
            if (hEvent == UIntPtr.Zero)
                hEvent = (UIntPtr)MirandaContext.Current.CallService(MS_DB_EVENT_FINDFIRST, Translate.ToHandle(contactHandle), IntPtr.Zero);
            else
                hEvent = (UIntPtr)MirandaContext.Current.CallService(MS_DB_EVENT_FINDNEXT, hEvent, IntPtr.Zero);

            return hEvent != UIntPtr.Zero;
        }

        private static void PrepareBlobBuffer(ref InteropBuffer buffer, int blobSize)
        {
            if (buffer.Size < blobSize)
            {
                if (buffer.Locked)
                    buffer.Unlock();

                InteropBufferPool.ReleaseBuffer(buffer);
                buffer = InteropBufferPool.AcquireBuffer(blobSize);
            }

            buffer.Lock();
        }

        #endregion

        #region Initialization stuff

        /// <summary>
        /// Initializes the engine and loads and validates the manifest.
        /// </summary>
        /// <exception cref="DeltaSyncException">Manifest validation failed.</exception>
        public void Initialize()
        {
            ProgressMediator.ChangeProgress(Resources.Text_UI_LogText_TokenizingDb, SignificantProgress.Running);

            TokenizeDatabase();
            TokenizeContacts();

            ProgressMediator.ChangeProgress(Resources.Text_UI_LogText_LoadingManifest, SignificantProgress.Running);
            LoadOrCreateManifest();

            // code may not be executed from this point on because of validation exceptions
        }

        private void LoadOrCreateManifest()
        {
            ISiteAdapter adapter = profile.GetProvider().Adapter;

            if (!adapter.FileExists(profile, profile.RemoteHost))
                FlagDatabaseDirty();

            string packagePath = DeltaManifest.GetManifestPath(profile);

            if (!adapter.FileExists(profile, packagePath))
                deltaManifest = new DeltaManifest(profile, GetDatabaseToken());
            else
                using (Stream manifestStream = adapter.PullFile(profile, packagePath))
                    deltaManifest = DeltaManifest.Deserialize(manifestStream, profile);

            try
            {
                if (!deltaManifest.IsNew)
                {
                    try
                    {
                        deltaManifest.Validate();
                        DeltaChain = new DeltaChain(deltaManifest);
                    }
                    // The manifest may belong to another db...
                    catch (DeltaSyncException)
                    {
                        // Store the old manifest
                        legacyManifest = deltaManifest;

                        // Create new manifest
                        deltaManifest = new DeltaManifest(profile, GetDatabaseToken());

                        // Inform the caller
                        throw;
                    }
                }
            }
            finally
            {
                // MUST run always, even if DeltaManifest.IsNew == TRUE
                CurrentDelta = new Delta(deltaManifest);
            }
        }

        #endregion

        #region Snapshot stuff

        public void BeginReplication()
        {
            if (!Initialized)
                throw new InvalidOperationException();

            if (RoamiePlugin.Singleton.RoamingContext.IsInState(RoamingState.DeltaIncompatibleChangeOccured))
                return;

            Trace.WriteLineIf(RoamiePlugin.TraceSwitch.TraceInfo, RoamiePlugin.TraceCategory, "DeltaSync engine (Synch) initialized");

            Replicator = new DatabaseReplicator(this);
            Replicator.BeginReplication();

            DatabaseSnapshot = new Snapshot();
            Dictionary<IntPtr, IntPtr> lastHandles = DatabaseSnapshot.EventHandles;

            Callback findLast = ServiceManager.GetService(MS_DB_EVENT_FINDLAST);

            foreach (IntPtr contactHandle in GetContactHandles())
                lastHandles[contactHandle] = (IntPtr)findLast(Translate.ToHandle(contactHandle), IntPtr.Zero);
        }

        private static IEnumerable<IntPtr> GetContactHandles()
        {
            MirandaContext context = MirandaContext.Current;
            Callback findNext = ServiceManager.GetService(MS_DB_CONTACT_FINDNEXT);

            IntPtr handle = (IntPtr)context.CallService(MS_DB_CONTACT_FINDFIRST);

            do
            {
                if (handle != IntPtr.Zero)
                    yield return handle;
            }
            while ((handle = (IntPtr)findNext(Translate.ToHandle(handle), IntPtr.Zero)) != IntPtr.Zero);
        }

        public unsafe void FinalizeDelta()
        {
            if (!Initialized)
                throw new InvalidOperationException();

            if (RoamiePlugin.Singleton.RoamingContext.IsInState(RoamingState.DeltaIncompatibleChangeOccured))
                return;

            Replicator.DisableReplication();
            LinkedList<DeltaEntry> entries = CurrentDelta.Entries;

            bool omissibleFound = InspectOmissibleSettings(entries);
            GatherNewEvents(entries);

            if (omissibleFound)
            {
                LinkedList<DeltaEntry> entriesToPublish = new LinkedList<DeltaEntry>();

                foreach (DeltaEntry entry in entries)
                    if (!entry.Omissible)
                        entriesToPublish.AddLast(entry);

                CurrentDelta.Entries = entriesToPublish;
            }
        }

        private static bool InspectOmissibleSettings(LinkedList<DeltaEntry> entries)
        {
            bool omissibleFound = false;

            foreach (DeltaEntry entry in entries)
            {
                DeltaSettingEntriesFilter.InspectIfOmissible(entry);
                omissibleFound |= entry.Omissible;
            }

            return omissibleFound | DeltaSettingEntriesFilter.CheckForDuplicitEntries(entries);
        }

        #endregion

        #region Delta stuff

        public bool ApplyDeltas()
        {
            if (!Initialized)
                throw new InvalidOperationException();

            try
            {
                if (!deltaManifest.IsNew)
                    return DeltaChain.MergeWithDatabase();
                else
                    return true;
            }
            finally
            {
                if (DeltaMergeRecommended && RoamiePlugin.Singleton.RoamingContext.Configuration.FullSyncAfterThreshold)
                    RoamiePlugin.Singleton.RoamingContext.State |= RoamingState.PreferFullSync;
            }
        }

        public void PublishDelta()
        {
            if (!Initialized)
                throw new InvalidOperationException();

            lock (CurrentDelta)
            {
                if (CurrentDelta.Entries.Count == 0)
                {
                    ProgressMediator.ChangeProgress(Resources.Text_UI_LogText_NoDeltaSyncRequired, SignificantProgress.Complete);
                    return;
                }
                else
                    ProgressMediator.ChangeProgress(Resources.Text_UI_LogText_DeltaSyncRequired, SignificantProgress.Running);

                deltaManifest.DeltaCount++;

                RoamingProfile profile = Profile;
                ISiteAdapter adapter = Profile.GetProvider().Adapter;

                PublishDelta(profile, adapter);
                PublishDeltaManifest(profile, adapter);

                ProgressMediator.ChangeProgress(Resources.Text_UI_LogText_Completed, SignificantProgress.Complete);
            }
        }

        private void PublishDelta(RoamingProfile profile, ISiteAdapter adapter)
        {
            using (MemoryStream deltaStream = new MemoryStream(4096))
            {
                ProgressMediator.ChangeProgress(Resources.Text_UI_LogText_PublishingDelta, 25);

                CurrentDelta.Serialize(deltaStream);
                deltaStream.Seek(0, SeekOrigin.Begin);
                
                adapter.PushFile(profile, Delta.GetPathForDelta(deltaManifest, deltaManifest.DeltaCount), deltaStream);
                ProgressMediator.ChangeProgress(null, 50);
            }
        }

        private void PublishDeltaManifest(RoamingProfile profile, ISiteAdapter adapter)
        {
            using (MemoryStream manifestStream = new MemoryStream(4096))
            {
                ProgressMediator.ChangeProgress(Resources.Text_UI_LogText_PublishingManifest, 75);

                deltaManifest.Serialize(manifestStream);
                manifestStream.Seek(0, SeekOrigin.Begin);

                adapter.PushFile(profile, DeltaManifest.GetManifestPath(deltaManifest.AssociatedProfile), manifestStream);
                ProgressMediator.ChangeProgress(null, 100);
            }
        }

        #endregion
    }
}
