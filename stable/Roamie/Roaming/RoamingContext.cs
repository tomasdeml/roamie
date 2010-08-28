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
using Virtuoso.Miranda.Roamie.Forms;
using Virtuoso.Miranda.Roamie.Roaming.Profiles;
using System.Diagnostics;
using Virtuoso.Miranda.Roamie.Roaming.Providers;
using Virtuoso.Miranda.Roamie.Roaming.DeltaSync;
using System.Net;
using Virtuoso.Miranda.Roamie.Roaming.Packing;

namespace Virtuoso.Miranda.Roamie.Roaming
{
    public sealed class RoamingContext
    {
        #region Properties

        private readonly object syncObject = new object();
        private string profilePath;
        private string initialProfilePath;
        private RoamingState state;
        private DatabaseProvider activeProvider;
        private RoamingConfiguration configuration;
        private RoamingProfile activeProfile;
        private Dictionary<string, DatabaseProvider> databaseProviders;
        private DeltaSyncEngine deltaEngine;

        public object SyncObject
        {
            get
            {
                return syncObject;
            }
        }

        public Dictionary<string, DatabaseProvider> DatabaseProviders
        {
            get { return databaseProviders; }
        }

        public RoamingProfile ActiveProfile
        {
            get { return activeProfile; }
        }

        public RoamingConfiguration Configuration
        {
            get { return configuration; }

            internal set
            {
                if (value == null)
                    throw new ArgumentNullException("value"); configuration = value;
            }
        }

        public DatabaseProvider ActiveProvider
        {
            get { return activeProvider; }
        }

        public RoamingState State
        {
            get { return state; }
            set { state = value; }
        }

        public string ProfilePath
        {
            get { return profilePath; }
            internal set
            {
                lock (SyncObject)
                {
                    if (String.IsNullOrEmpty(value))
                        throw new ArgumentNullException("ProfilePath");

                    profilePath = value;
                }
            }
        }

        internal DeltaSyncEngine DeltaEngine
        {
            get { return deltaEngine; }
            set { deltaEngine = value; }
        }                

        #endregion

        #region .ctors

        internal RoamingContext(string profilePath)
        {
            if (String.IsNullOrEmpty(profilePath))
                throw new ArgumentNullException("profilePath");

            Initalize(profilePath);            
            InitializeProxySettings();
            InitializeProviders();
        }

        internal void InitializeProxySettings()
        {
            if (Configuration.UseProxy)
                WebRequest.DefaultWebProxy = (IWebProxy)Configuration.Proxy ?? WebRequest.GetSystemWebProxy();
        }

        private void InitializeProviders()
        {
            DatabaseProvider[] providers = { new FtpProvider(), new HttpProvider() };

            foreach (DatabaseProvider provider in providers)
                databaseProviders.Add(provider.Name, provider);
        }

        private void Initalize(string profilePath)
        {
            this.initialProfilePath = this.profilePath = profilePath;
            this.state = RoamingState.RoamingDisabled;
            this.configuration = RoamingConfiguration.Load<RoamingConfiguration>();
            this.databaseProviders = new Dictionary<string, DatabaseProvider>(1);
        }

        #endregion

        #region Methods

        internal void ActivateProfile(RoamingProfile profile)
        {
            if (profile == null)
                throw new ArgumentNullException("profile");

            try
            {
                Trace.WriteLineIf(RoamiePlugin.TraceSwitch.TraceVerbose, "Activating a roaming profile: " + profile.Name, RoamiePlugin.TraceCategory);

                activeProfile = profile;
                activeProvider = databaseProviders[profile.RoamingProvider];
                activeProvider.OnSelected();

                if (profile.PreferFullSync)
                    state |= RoamingState.PreferFullSync;
            }
            catch (Exception e)
            {
                Trace.WriteLineIf(RoamiePlugin.TraceSwitch.TraceError, GlobalEvents.FormatExceptionMessage("Error while activating a roaming profile: " + profile.Name, e), RoamiePlugin.TraceCategory);
                DeactivateProfile();
            }
        }

        internal void DeactivateProfile()
        {
            lock (SyncObject)
            {
                Trace.WriteLineIf(RoamiePlugin.TraceSwitch.TraceInfo, "Deactivating the roaming profile. No synchronization will be performed!", RoamiePlugin.TraceCategory);

                activeProfile = null;
                activeProvider = null;

                state = RoamingState.RoamingDisabled;
                state |= RoamingState.LocalDbInUse;
                state |= RoamingState.DiscardLocalChanges;
            }
        }

        internal void RestoreConfiguration()
        {
            configuration = RoamingConfiguration.Load<RoamingConfiguration>();
        }

        internal void RestoreProfilePath()
        {
            profilePath = initialProfilePath;
        }

        public bool IsInState(RoamingState state)
        {
            return (this.state & state) == state;
        }

        #endregion
    }
}
