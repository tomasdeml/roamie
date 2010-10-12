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
using System.Net;
using Virtuoso.Miranda.Plugins.Infrastructure;
using Virtuoso.Roamie.Roaming.Profiles;
using Virtuoso.Roamie.RoamingProviders;

namespace Virtuoso.Roamie.Configuration
{
    [Serializable]
    public abstract class RoamingConfiguration : PluginConfiguration
    {
        #region Fields

        internal const string Version = "1.0.0.0";

        internal Dictionary<string, ConfigurationValuesDictionary> ConfigurationValues;        

        #endregion

        #region .ctors

        protected RoamingConfiguration()
        {
            ConfigurationValues = new Dictionary<string, ConfigurationValuesDictionary>(1);
            ProfileManager = new ProfileManager();
        }

        #endregion

        #region Properties

        public ProfileManager ProfileManager
        {
            get;
            private set;
        }

        public bool SilentMode
        {
            get;
            set;
        }

        public bool UseProxy
        {
            get;
            set;
        }

        public bool AuthenticateToProxy
        {
            get;
            set;
        }

        public WebProxy Proxy
        {
            get;
            set;
        }

        public bool FullSyncAfterThreshold
        {
            get;
            set;
        }

        #endregion

        #region Methods

        public ConfigurationValuesDictionary GetProviderConfiguration(DatabaseProvider provider)
        {
            if (provider == null) 
                throw new ArgumentNullException("provider");

            ConfigurationValuesDictionary retValue;
            string key = provider.GetType().FullName;
            
            if (!ConfigurationValues.TryGetValue(key, out retValue))
            {
                retValue = new ConfigurationValuesDictionary();
                ConfigurationValues[key] = retValue;
            }

            return retValue;
        }

        public void CopyTo(RoamingConfiguration newConfig)
        {
            newConfig.AuthenticateToProxy = AuthenticateToProxy;
            newConfig.ConfigurationValues = ConfigurationValues;
            newConfig.FullSyncAfterThreshold = FullSyncAfterThreshold;
            newConfig.IsDirty = IsDirty;
            newConfig.ProfileManager = ProfileManager;
            newConfig.Proxy = Proxy;
            newConfig.SilentMode = SilentMode;
            newConfig.UseProxy = UseProxy;
        }

        #endregion        
    }
}
