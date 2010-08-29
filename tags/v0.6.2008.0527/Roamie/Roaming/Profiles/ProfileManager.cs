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

using System.Diagnostics;
using Virtuoso.Miranda.Roamie.Roaming.Providers;

namespace Virtuoso.Miranda.Roamie.Roaming.Profiles
{
    [Serializable]
    public sealed class ProfileManager
    {
        #region Fields

        internal readonly ProfileCollection profiles;
        public ProfileCollection Profiles
        {
            get
            {
                return profiles;
            }
        } 

        #endregion

        #region .ctors

        internal ProfileManager()
        {
            profiles = new ProfileCollection();
        }

        #endregion

        #region Methods

        public void VerifyProfiles()
        {
            List<RoamingProfile> invalidProfiles = new List<RoamingProfile>(1);
            Dictionary<string, DatabaseProvider> providers = RoamiePlugin.Singleton.RoamingContext.DatabaseProviders;

            foreach (RoamingProfile profile in profiles)
                if (!providers.ContainsKey(profile.RoamingProvider))
                    invalidProfiles.Add(profile);

            foreach (RoamingProfile profile in invalidProfiles)
            {
                Trace.WriteLineIf(RoamiePlugin.TraceSwitch.TraceWarning, "Invalid roaming profile detected, removing it.", RoamiePlugin.TraceCategory);
                profiles.Remove(profile);
            }
        }

        #endregion
    }
}
