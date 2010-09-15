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

using System.IO;
using System.Diagnostics;
using Virtuoso.Roamie.Roaming;
using Virtuoso.Roamie.Roaming.Profiles;

namespace Virtuoso.Roamie.RoamingProviders
{
    public abstract class DatabaseProvider
    {
        #region Fields

        public const string RoamingExtension = "roaming.dat";

        #endregion

        #region Properties

        protected RoamingContext Context
        {
            get { return RoamiePlugin.Singleton.RoamingContext; }
        }        

        public abstract string Name { get; }

        public abstract bool CredentialsRequired { get; }

        public abstract ISiteAdapter Adapter
        {
            get;
        }

        #endregion

        #region Methods

        #region Abstract & Virtual

        public virtual void OnSelected() { }

        public virtual void NonSyncShutdown() { }

        public abstract void VerifyProfile(RoamingProfile profile);

        public abstract void SyncLocalDatabase(RoamingProfile profile);

        public abstract void SyncRemoteDatabase(RoamingProfile profile);

        protected virtual void RemoveLocalData(bool localDbRemoved) { }

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

            DatabaseProvider other = obj as DatabaseProvider;

            if (ReferenceEquals(other, null)) 
                return false;

            return GetHashCode() == other.GetHashCode();
        }

        protected void InitializeSafeProfilePath()
        {
            if (Path.GetFileName(Context.ProfilePath).ToLower().EndsWith(RoamingExtension)) 
                return;

            Trace.WriteLineIf(RoamiePlugin.TraceSwitch.TraceInfo, "Changing database file extension...", RoamiePlugin.TraceCategory);
            ChangeProfilePath(Path.ChangeExtension(Context.ProfilePath, RoamingExtension));
        }

        protected void ChangeProfilePath(string path)
        {
            Context.ProfilePath = path;
        }

        // TODO
#warning RoamingOrchestration must call this!

        protected void RemoveLocalDb()
        {
            try
            {
                bool removeDb = Context.IsInState(RoamingState.WipeLocalDbOnExit);

                if (removeDb)
                {
                    Trace.WriteLineIf(RoamiePlugin.TraceSwitch.TraceInfo, "PublicMode is active, preparing for local database removal...", RoamiePlugin.TraceCategory);

                    if (File.Exists(Context.ProfilePath))
                    {
                        File.Delete(Context.ProfilePath);
                        Trace.WriteLineIf(RoamiePlugin.TraceSwitch.TraceInfo, "Local database removed.", RoamiePlugin.TraceCategory);
                    }
                    else
                        Trace.WriteLineIf(RoamiePlugin.TraceSwitch.TraceWarning, "Local database no longer exist! Cannot remove local database.", RoamiePlugin.TraceCategory);                    
                }

                RemoveLocalData(removeDb);
            }
            catch { }
        }

        #endregion

        #endregion
    }
}
