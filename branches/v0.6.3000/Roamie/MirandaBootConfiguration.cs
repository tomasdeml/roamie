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
using Virtuoso.Miranda.Plugins.Helpers;
using Virtuoso.Miranda.Plugins.Infrastructure;
using Virtuoso.Miranda.Roamie.Roaming.Profiles;
using Virtuoso.Miranda.Roamie.Roaming;
using System.IO;
using System.Diagnostics;

namespace Virtuoso.Miranda.Roamie
{
    internal enum StartupOption : int
    {
        DownloadDatabase = 0,
        UseLocalDatabase = 1
    }

    internal sealed class MirandaBootConfiguration
    {
        #region Fields

        private const string RoamieCategory = "Roamie", 
            ProfileKey = "Profile",
            StartupOptionKey = "StartupOption", 
            PublicPcKey = "PublicPc", 
            SandboxModeKey = "SandboxMode";

        private readonly IniStructure Ini;

        private RoamingProfile profile;       
        private StartupOption startupOption;        
        private bool publicPc, sandboxMode;

        #endregion

        #region .ctors

        private MirandaBootConfiguration()
        {
            Ini = IniStructure.ReadIni(MirandaEnvironment.MirandaBootIniPath);
            RoamingContext context = RoamiePlugin.Singleton.RoamingContext;
            
            if (Ini != null)
            {
                string[] keyNames = Ini.GetKeys(RoamieCategory);
                int temp;
               
                foreach (string keyName in keyNames)
                {                 
                    switch (keyName)
                    {
                        case StartupOptionKey:
                            startupOption = (Roamie.StartupOption)Enum.Parse(typeof(Roamie.StartupOption), Ini.GetValue(RoamieCategory, StartupOptionKey));
                            break;
                        case ProfileKey:
                            profile = context.Configuration.ProfileManager.Profiles.Find(Ini.GetValue(RoamieCategory, ProfileKey));
                            break;
                        case PublicPcKey:
                            if (Int32.TryParse(Ini.GetValue(RoamieCategory, PublicPcKey), out temp))
                                publicPc = Convert.ToBoolean(temp);
                            break;
                        case SandboxModeKey:
                            if (Int32.TryParse(Ini.GetValue(RoamieCategory, SandboxModeKey), out temp))
                                sandboxMode = Convert.ToBoolean(temp);
                            break;
                    }
                }
            }
        }

        public static MirandaBootConfiguration Load()
        {
            try
            {
                if (!File.Exists(MirandaEnvironment.MirandaBootIniPath))
                    return null;
                else
                    return new MirandaBootConfiguration();
            }
            catch (Exception e)
            {
                Trace.WriteLineIf(RoamiePlugin.TraceSwitch.TraceError, GlobalEvents.FormatExceptionMessage("Error while loading MirandaBoot.ini file. ", e));
                return null;
            }
        }

        #endregion

        #region Properties

        public RoamingProfile Profile
        {
            get { return profile; }
            set { profile = value; }
        }

        public StartupOption StartupOption
        {
            get { return startupOption; }
            set { startupOption = value; }
        }

        public bool PublicPc
        {
            get { return publicPc; }
            set { publicPc = value; }
        }

        public bool SandboxMode
        {
            get { return sandboxMode; }
            set { sandboxMode = value; }
        }

        public bool IsValid
        {
            get
            {
                return profile != null;
            }
        }

        #endregion

        #region Methods

        public void Save()
        {
            if (IsValid)
            {
                Ini.DeleteCategory(RoamieCategory);
                Ini.AddCategory(RoamieCategory);

                Ini.AddValue(RoamieCategory, StartupOptionKey, ((int)startupOption).ToString());
                Ini.AddValue(RoamieCategory, ProfileKey, profile.Name);
                Ini.AddValue(RoamieCategory, PublicPcKey, Convert.ToInt32(publicPc).ToString());
                Ini.AddValue(RoamieCategory, SandboxModeKey, Convert.ToInt32(sandboxMode).ToString());
            }

            IniStructure.WriteIni(Ini, MirandaEnvironment.MirandaBootIniPath);
        }

        public void Reset()
        {
            profile = null;
            Ini.DeleteCategory(RoamieCategory);
        }

        #endregion
    }
}
