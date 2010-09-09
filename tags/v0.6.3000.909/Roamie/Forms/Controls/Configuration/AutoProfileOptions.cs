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
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Virtuoso.Miranda.Plugins.Forms.Controls;
using Virtuoso.Miranda.Roamie.Properties;
using Virtuoso.Miranda.Plugins.Helpers;
using Virtuoso.Miranda.Plugins.Infrastructure;
using Virtuoso.Miranda.Roamie.Roaming.Profiles;
using Virtuoso.Miranda.Plugins.Forms;
using Virtuoso.Miranda.Plugins.Configuration.Forms.Controls;

namespace Virtuoso.Miranda.Roamie.Forms.Controls.Configuration
{
    internal sealed partial class AutoProfileOptions : CategoryItemControl
    {
        private MirandaBootConfiguration BootConfig;

        public AutoProfileOptions()
        {
            InitializeComponent();
        }

        protected override bool OnShow(bool firstTime)
        {
            if (BootConfig == null)
            {
                BootConfig = MirandaBootConfiguration.Load();

                if (BootConfig == null)
                {
                    InformationDialog.PresentModal(Resources.Information_Caption_UnableToLoadMirandaBootIni, Resources.Information_Text_UnableToLoadMirandaBootIni, Resources.Image_32x32_Favourite);
                    return true;
                }
            }

            if (!firstTime)
                return false;

            foreach (RoamingProfile profile in RoamiePlugin.Singleton.RoamingContext.Configuration.ProfileManager.Profiles)
                ProfileLBOX.Items.Add(profile.Name);

            if (ProfileLBOX.Items.Count > 0)
                ProfileLBOX.SelectedIndex = 0;
            else
            {
                InformationDialog.PresentModal(Resources.Information_Caption_NoProfilesFound, Resources.Information_Text_NoProfilesFound, Resources.Image_32x32_Favourite);
                return true;
            }

            if ((UseDefaultProfileCHBOX.Checked = SettingsGBOX.Enabled = DefaultProfileSECTION.Enabled =
                (BootConfig.IsValid && BootConfig.Profile.IsValid)))
            {
                ProfileLBOX.SelectedItem = BootConfig.Profile.Name;
                LocalRBTN.Checked = !(DownloadRBTN.Checked = BootConfig.StartupOption == StartupOption.DownloadDatabase);

                RemoveOnExitCHKBOX.Checked = BootConfig.PublicPc;
                DoNotPublishCHKBOX.Checked = BootConfig.SandboxMode;
            }

            AttachDirtiers();
            return false;
        }

        private void AttachDirtiers()
        {
            ProfileLBOX.SelectedIndexChanged += SetControlDirtyHandler;

            DownloadRBTN.CheckedChanged += SetControlDirtyHandler;
            UseDefaultProfileCHBOX.CheckedChanged += SetControlDirtyHandler;

            RemoveOnExitCHKBOX.CheckedChanged += SetControlDirtyHandler;
            DoNotPublishCHKBOX.CheckedChanged += SetControlDirtyHandler;
        }

        private void EnableDefaultProfileCHBOX_CheckedChanged(object sender, EventArgs e)
        {
            SettingsGBOX.Enabled = UseDefaultProfileCHBOX.Checked;
            DefaultProfileSECTION.Enabled = UseDefaultProfileCHBOX.Checked;

            IsDirty = true;
        }

        private void StartupOptionControl_CheckedChanged(object sender, EventArgs e)
        {
            if (LocalRBTN.Checked)
                RemoveOnExitCHKBOX.Enabled = RemoveOnExitCHKBOX.Checked = false;
            else
                RemoveOnExitCHKBOX.Enabled = true;
        }

        protected override void Save()
        {
            if (UseDefaultProfileCHBOX.Checked)
            {
                BootConfig.StartupOption = DownloadRBTN.Checked ? StartupOption.DownloadDatabase : StartupOption.UseLocalDatabase;

                BootConfig.Profile = RoamiePlugin.Singleton.RoamingContext.Configuration.ProfileManager.Profiles.Find((string)ProfileLBOX.SelectedItem);
                BootConfig.PublicPc = RemoveOnExitCHKBOX.Checked;
                BootConfig.SandboxMode = DoNotPublishCHKBOX.Checked;
            }
            else
                BootConfig.Reset();

            BootConfig.Save();

            if (MirandaContext.Initialized)
                MirandaContext.Current.ContactList.ShowBaloonTip(Resources.Information_Caption_ChangesSaved, Resources.Information_Text_MirandaBootChangesSaved, null, ToolTipIcon.Info, 10000);
        }
    }
}
