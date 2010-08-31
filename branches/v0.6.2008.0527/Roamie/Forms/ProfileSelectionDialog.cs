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
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Virtuoso.Miranda.Roamie.Properties;
using Virtuoso.Miranda.Roamie.Roaming.Profiles;
using Virtuoso.Miranda.Plugins.Forms;
using Virtuoso.Miranda.Plugins.Configuration.Forms;

namespace Virtuoso.Miranda.Roamie.Forms
{
    public sealed partial class ProfileSelectionDialog : Form
    {
        #region Fields

        private RoamingProfile SelectedProfile;

        #endregion

        #region .ctors

        private ProfileSelectionDialog()
        {
            InitializeComponent();
        }

        public static RoamingProfile PresentModal()
        {
            using (ProfileSelectionDialog dlg = new ProfileSelectionDialog())
            {
                dlg.ShowDialog();
                return dlg.SelectedProfile;
            }
        }

        #endregion

        #region UI Handlers

        private void RoamingProfileSelectionDialog_Load(object sender, EventArgs e)
        {
            ProfilesLVIEW.ShowNewProfileItem(true);
        }

        private void ManageProfilesLBTN_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ConfigurationDialog.Present(true, RoamiePlugin.Singleton, ConfigurationDialog.CreatePath(RoamiePlugin.Singleton, Resources.Config_RoamingProfiles, Resources.Config_RoamingProfiles_Manage));
            ProfilesLVIEW.RefreshProfiles();

            OkBTN.Enabled = false;
        }        

        private void ProfilesLVIEW_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            OkBTN.Enabled = e.IsSelected;
        }

        private void OkBTN_Click(object sender, EventArgs e)
        {
            SelectedProfile = ProfilesLVIEW.SelectedProfile;

            if (SelectedProfile == null)
                ProfilesLVIEW_NewProfileRequested(ProfilesLVIEW, EventArgs.Empty);
            else
                Close();
        }        

        private void ProfilesLVIEW_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (ProfilesLVIEW.SelectedItems.Count > 0)
                OkBTN.PerformClick();
        }              

        private void ProfilesLVIEW_NewProfileRequested(object sender, EventArgs e)
        {
            SelectedProfile = ProfileCreatingDialog.PresentModal();

            if (SelectedProfile != null)
                Close();
        }

        #endregion
    }
}