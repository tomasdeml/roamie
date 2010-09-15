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
using Virtuoso.Miranda.Plugins.Helpers;
using Virtuoso.Miranda.Plugins.Infrastructure;
using System.IO;
using System.Diagnostics;
using Virtuoso.Roamie.Roaming.Profiles;

namespace Virtuoso.Roamie
{
    internal enum StartupOption
    {
        DownloadDatabase = 0,
        UseLocalDatabase = 1
    }

    internal class MirandaBootConfiguration
    {
        #region Fields

        private const string RoamieCategory = "Roamie";
        private const string ProfileKey = "Profile";
        private const string StartupOptionKey = "StartupOption";
        private const string PublicPcKey = "PublicPc";
        private const string SandboxModeKey = "SandboxMode";
        
        private readonly IniStructure IniStructure;

        #endregion

        #region .ctors

        private MirandaBootConfiguration()
        {
            IniStructure = IniStructure.ReadIni(MirandaEnvironment.MirandaBootIniPath);

            if (IniStructure == null) 
                return;

            string[] keyNames = IniStructure.GetKeys(RoamieCategory);

            foreach (string keyName in keyNames)
            {
                int intValue;

                switch (keyName)
                {
                    case StartupOptionKey:
                        StartupOption =
                            (StartupOption)
                            Enum.Parse(typeof (StartupOption), IniStructure.GetValue(RoamieCategory, StartupOptionKey));
                        break;
                    case ProfileKey:
                        Profile =
                            RoamiePlugin.Singleton.RoamingContext.Configuration.ProfileManager.Profiles.Find(
                                IniStructure.GetValue(RoamieCategory, ProfileKey));
                        break;
                    case PublicPcKey:
                        if (Int32.TryParse(IniStructure.GetValue(RoamieCategory, PublicPcKey), out intValue))
                            PublicPc = Convert.ToBoolean(intValue);
                        break;
                    case SandboxModeKey:
                        if (Int32.TryParse(IniStructure.GetValue(RoamieCategory, SandboxModeKey), out intValue))
                            SandboxMode = Convert.ToBoolean(intValue);
                        break;
                }
            }
        }

        public static MirandaBootConfiguration Load()
        {
            try
            {
                return File.Exists(MirandaEnvironment.MirandaBootIniPath) ? new MirandaBootConfiguration() : null;
            }
            catch (Exception e)
            {
                Trace.WriteLineIf(RoamiePlugin.TraceSwitch.TraceError, StringUtility.FormatExceptionMessage("Error while loading MirandaBoot.ini file. ", e));
                return null;
            }
        }

        #endregion

        #region Properties

        public RoamingProfile Profile { get; set; }

        public StartupOption StartupOption { get; set; }

        public bool PublicPc { get; set; }

        public bool SandboxMode { get; set; }

        public bool IsValid
        {
            get
            {
                return Profile != null;
            }
        }

        #endregion

        #region Methods

        public void Save()
        {
            if (IsValid)
            {
                IniStructure.DeleteCategory(RoamieCategory);
                IniStructure.AddCategory(RoamieCategory);

                IniStructure.AddValue(RoamieCategory, StartupOptionKey, ((int)StartupOption).ToString());
                IniStructure.AddValue(RoamieCategory, ProfileKey, Profile.Name);
                IniStructure.AddValue(RoamieCategory, PublicPcKey, Convert.ToInt32(PublicPc).ToString());
                IniStructure.AddValue(RoamieCategory, SandboxModeKey, Convert.ToInt32(SandboxMode).ToString());
            }

            IniStructure.WriteIni(IniStructure, MirandaEnvironment.MirandaBootIniPath);
        }

        public void Reset()
        {
            Profile = null;
            IniStructure.DeleteCategory(RoamieCategory);
        }

        #endregion
    }
}
