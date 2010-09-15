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
using Virtuoso.Miranda.Plugins.Configuration;
using Virtuoso.Roamie.Roaming.Profiles;
using Virtuoso.Roamie.RoamingProviders;

namespace Virtuoso.Roamie.Roaming
{
    [Serializable, ConfigurationOptions("0.2.0.0", Encrypt = true, ProfileBound = false, Encryption = typeof(MasterKeyEncryption), Storage = typeof(PortableStorage))]
    public class RoamingConfiguration : PluginConfiguration
    {
        #region Fields

        internal readonly Dictionary<string, ConfigurationValuesDictionary> ConfigurationValues;

        private readonly ProfileManager profileManager;
        public ProfileManager ProfileManager
        {
            get { return profileManager; }
        }

        #endregion

        #region .ctors

        public RoamingConfiguration()
        {
            ConfigurationValues = new Dictionary<string, ConfigurationValuesDictionary>(1);
            profileManager = new ProfileManager();
        }

        #endregion

        #region Methods

        public ConfigurationValuesDictionary GetProviderConfiguration(DatabaseProvider provider)
        {
            if (provider == null) 
                throw new ArgumentNullException("provider");

            string hash = provider.GetType().FullName;

            ConfigurationValuesDictionary retValue = null;

            if (ConfigurationValues.ContainsKey(hash))
                retValue = ConfigurationValues[hash];
            else
            {
                retValue = new ConfigurationValuesDictionary();
                ConfigurationValues[hash] = retValue;
            }

            return retValue;
        }

        #endregion

        #region Properties

        private bool silentStartup;
        public bool SilentStartup
        {
            get { return silentStartup; }
            set { silentStartup = value; }
        }

        private bool useProxy;
        public bool UseProxy
        {
            get { return useProxy; }
            set { useProxy = value; }
        }

        private bool authenticateToProxy;
        public bool AuthenticateToProxy
        {
            get { return authenticateToProxy; }
            set { authenticateToProxy = value; }
        }

        private WebProxy proxy;
        public WebProxy Proxy
        {
            get { return proxy; }
            set { proxy = value; }
        }

        private bool fullSyncAfterThreshold;
        public bool FullSyncAfterThreshold
        {
            get { return fullSyncAfterThreshold; }
            set { fullSyncAfterThreshold = value; }
        }

        #endregion
    }
}
