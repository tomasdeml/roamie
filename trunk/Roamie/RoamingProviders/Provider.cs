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
using System.IO;
using System.Diagnostics;
using Virtuoso.Roamie.Roaming;
using Virtuoso.Roamie.Roaming.Profiles;

namespace Virtuoso.Roamie.RoamingProviders
{
    public abstract class Provider
    {
        #region Fields

        public const string RoamingExtension = "roaming.dat";

        #endregion

        #region Properties

        protected Context Context
        {
            get { return RoamiePlugin.Singleton.RoamingContext; }
        }        

        public abstract string Name { get; }

        public abstract bool CredentialsRequired { get; }

        public abstract ISiteAdapter Adapter
        {
            get;
        }

        protected bool CanSyncRemoteSite
        {
            get { return !Context.IsInState(RoamingState.DiscardLocalChanges); }
        }

        #endregion

        #region Methods

        public virtual void OnSelected() { }

        public abstract void VerifyProfile(RoamingProfile profile);

        public virtual void SyncLocalSite(RoamingProfile profile)
        {
            InitializeSafeProfilePath();
            PerformLocalSiteSync(profile);
        }

        private void PerformLocalSiteSync(RoamingProfile profile)
        {
            using (Stream dbStream = File.Create(Context.ProfilePath))
            {
                // TODO
                if (!Adapter.PullFile(profile, profile.RemoteHost, dbStream))
                    throw new Exception();
            }
        }

        public virtual void SyncRemoteSite(RoamingProfile profile)
        {
            if (!CanSyncRemoteSite)
            {
                // TODO Log
                //Trace.WriteLineIf(RoamiePlugin.TraceSwitch.TraceInfo, "Sandbox mode is active, no synchronization required.", "");
                NonSyncShutdown();
            }
            else if (Context.IsInState(RoamingState.ForceFullSync))
            {
                PerformRemoteSiteSync(profile);
            }
        }

        private void PerformRemoteSiteSync(RoamingProfile profile)
        {
            using (Stream dbStream = File.OpenRead(Context.ProfilePath))
                Adapter.PushFile(profile, dbStream, profile.RemoteHost, true);
        }

        public virtual void NonSyncShutdown() { }

        public virtual void RemoveLocalSiteData()
        {
            bool removeDb = Context.IsInState(RoamingState.RemoveLocalCopyOnExit);

            if (!removeDb)
                return;

            try
            {
                Trace.WriteLineIf(RoamiePlugin.TraceSwitch.TraceInfo,
                                  "PublicMode is active, preparing for local database removal...",
                                  RoamiePlugin.TraceCategory);

                if (File.Exists(Context.ProfilePath))
                {
                    File.Delete(Context.ProfilePath);
                    Trace.WriteLineIf(RoamiePlugin.TraceSwitch.TraceInfo, "Local database removed.",
                                      RoamiePlugin.TraceCategory);
                }
                else
                    Trace.WriteLineIf(RoamiePlugin.TraceSwitch.TraceWarning,
                                      "Local database no longer exist! Cannot remove local database.",
                                      RoamiePlugin.TraceCategory);

                RemoveLocalData();
            }
            catch (Exception)
            {
                // TODO Log
            }
        }

        protected virtual void RemoveLocalData()
        {
        }

        #endregion

        #region Etc.

        public override int GetHashCode()
        {
            return GetType().FullName.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null) 
                return false;

            Provider other = obj as Provider;

            if (ReferenceEquals(other, null)) 
                return false;

            return GetHashCode() == other.GetHashCode();
        }

        protected void InitializeSafeProfilePath()
        {
            return;

#warning TODO

            if (Path.GetFileName(Context.ProfilePath).ToLower().EndsWith(RoamingExtension)) 
                return;

            Trace.WriteLineIf(RoamiePlugin.TraceSwitch.TraceInfo, "Changing database file extension...", RoamiePlugin.TraceCategory);
            //ChangeProfilePath(Path.ChangeExtension(Context.ProfilePath, RoamingExtension));
            ChangeProfilePath(Path.Combine(Path.GetDirectoryName(Context.ProfilePath), "Roaming_" + Path.GetFileName(Context.ProfilePath)));
        }

        protected void ChangeProfilePath(string path)
        {
            Context.ProfilePath = path;
        }

        #endregion
    }
}
