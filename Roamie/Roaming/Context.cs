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
using System.Diagnostics;
using System.Net;
using Virtuoso.Roamie.Roaming.Profiles;
using Virtuoso.Roamie.RoamingProviders;
using Virtuoso.Roamie.RoamingProviders.Ftp;
using Virtuoso.Roamie.RoamingProviders.Http;
using Virtuoso.Roamie.Configuration;

namespace Virtuoso.Roamie.Roaming
{
    public sealed class Context
    {
        #region Fields

        private string InitialProfilePath;

        #endregion

        #region Properties

        public object SyncObject { get; private set; }

        public Dictionary<string, Provider> DatabaseProviders { get; private set; }

        public RoamingProfile ActiveProfile { get; private set; }

        public RoamingConfiguration Configuration
        {
            get
            {
                return ConfigurationManager.Singleton.Configuration;
            }
        }

        public Provider ActiveProvider { get; private set; }

        public RoamingState State { get; set; }

        public string ProfilePath { get; internal set; }

        public bool LocalProfileIsMaster
        {
            get { return IsInState(RoamingState.LocalProfileLoaded, RoamingState.NewProfileCreated); }
        }

        public Manifest Manifest { get; set; }

        #endregion

        #region .ctors

        internal Context(string profilePath)
        {
            SyncObject = new object();

            Initalize(profilePath);            
            InitializeProxySettings();
            InitializeProviders();
        }

        private void Initalize(string profilePath)
        {
            if (String.IsNullOrEmpty(profilePath))
                throw new ArgumentNullException("profilePath");

            InitialProfilePath = ProfilePath = profilePath;
            State = RoamingState.Disabled;
            DatabaseProviders = new Dictionary<string, Provider>(1);
        }

        internal void InitializeProxySettings()
        {
            if (Configuration.UseProxy)
                WebRequest.DefaultWebProxy = Configuration.Proxy ?? WebRequest.GetSystemWebProxy();
        }

        private void InitializeProviders()
        {
            Provider provider = new FtpProvider();
            provider = AddCommonDecorators(provider);
            DatabaseProviders[provider.Name] = provider;

            provider = new HttpProvider();
            provider = AddCommonDecorators(provider);
            provider = new OneWaySynchronization(provider);
            DatabaseProviders[provider.Name] = provider;
        }

        private Provider AddCommonDecorators(Provider provider)
        {
            provider = new LegacyDeltaManifestCheck(provider);
            provider = new RoamingManifestProcessor(provider);
            provider = new DeltaSyncSupport(provider);
            provider = new ContentProvisioningSupport(provider);

            return provider;
        }

        #endregion

        #region Methods

        internal void ActivateProfile(RoamingProfile profile)
        {
            if (profile == null)
                throw new ArgumentNullException("profile");

            try
            {
                Trace.WriteLineIf(RoamiePlugin.TraceSwitch.TraceVerbose, "Activating roaming profile: " + profile.Name, RoamiePlugin.TraceCategory);

                ActiveProfile = profile;
                ActiveProvider = DatabaseProviders[profile.RoamingProvider];
                ActiveProvider.OnSelected();

                if (profile.PreferFullSync)
                    State |= RoamingState.ForceFullSync;
            }
            catch (Exception e)
            {
                Trace.WriteLineIf(RoamiePlugin.TraceSwitch.TraceError, StringUtility.FormatExceptionMessage("Error while activating a roaming profile: " + profile.Name, e), RoamiePlugin.TraceCategory);
                DeactivateProfile();
            }
        }

        internal void DeactivateProfile()
        {
            lock (SyncObject)
            {
                Trace.WriteLineIf(RoamiePlugin.TraceSwitch.TraceInfo, "Deactivating the roaming profile. No synchronization will be performed!", RoamiePlugin.TraceCategory);

                ActiveProfile = null;
                ActiveProvider = null;

                State = RoamingState.Disabled;
                State |= RoamingState.LocalProfileLoaded;
                State |= RoamingState.DiscardLocalChanges;
            }
        }

        internal void RestoreProfilePath()
        {
            ProfilePath = InitialProfilePath;
        }

        public bool IsInState(params RoamingState[] statesInQuestion)
        {
            foreach (RoamingState state in statesInQuestion)
                if (IsInState(state))
                    return true;

            return false;
        }

        public bool IsInState(RoamingState stateInQuestion)
        {
            return (State & stateInQuestion) == stateInQuestion;
        }

        #endregion
    }
}
