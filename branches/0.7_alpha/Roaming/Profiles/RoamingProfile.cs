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
using Virtuoso.Roamie.RoamingProviders;

namespace Virtuoso.Roamie.Roaming.Profiles
{
    [Serializable]
    public class RoamingProfile
    {
        #region Properties

        private string name, description, remoteHost, userName, password, databasePassword, roamingProvider;
        private bool versioned;
        private bool preferFullSync;        
        
        internal Dictionary<string, string> additionalProperties;

        public string RoamingProvider
        {
            get { return roamingProvider; }
        }
        public string Description
        {
            get { return description; }
        }
        public string Name
        {
            get { return name; }
        }       
        public string DatabasePassword
        {
            get { return databasePassword; }
        }
        public string Password
        {
            get { return password; }
        }
        public string UserName
        {
            get { return userName; }
        }
        public string RemoteHost
        {
            get { return remoteHost; }
        }
        public Dictionary<string, string> AdditionalProperties
        {
            get { return additionalProperties; }
        }
        public bool Versioned
        {
            get { return versioned; }
            set { versioned = value; }
        }
        public bool PreferFullSync
        {
            get { return preferFullSync; }
            set { preferFullSync = value; }
        }

        #endregion

        #region .ctors

        public RoamingProfile(string name, string description, string remoteHost, string userName, string password, string databasePassword, string roamingProvider)
        {
            if (String.IsNullOrEmpty(roamingProvider) || String.IsNullOrEmpty(name) || String.IsNullOrEmpty(remoteHost) || String.IsNullOrEmpty(databasePassword))
                throw new ArgumentNullException();

            this.roamingProvider = roamingProvider;
            this.name = name;
            this.description = description;
            this.remoteHost = remoteHost;
            this.userName = userName;
            this.password = password;
            this.databasePassword = databasePassword;
            this.additionalProperties = new Dictionary<string, string>();
        }

        #endregion

        #region Methods

        public override int GetHashCode()
        {
            return name.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (object.ReferenceEquals(obj, null)) 
                return false;

            RoamingProfile other = obj as RoamingProfile;

            if (object.ReferenceEquals(other, null)) 
                return false;

            return other.GetHashCode() == GetHashCode();
        }

        public bool IsValid
        {
            get
            {
                return RoamiePlugin.Singleton.RoamingContext.Configuration.ProfileManager.Profiles.Contains(this);
            }
        }

        public DatabaseProvider GetProvider()
        {
            DatabaseProvider provider = null;
            RoamiePlugin.Singleton.RoamingContext.DatabaseProviders.TryGetValue(roamingProvider, out provider);

            return provider;
        }

        #endregion
    }
}
