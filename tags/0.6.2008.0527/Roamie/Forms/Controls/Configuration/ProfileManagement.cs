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
using Virtuoso.Miranda.Roamie.Roaming.Profiles;
using Virtuoso.Miranda.Roamie.Properties;
using Virtuoso.Miranda.Plugins.Configuration.Forms.Controls;

namespace Virtuoso.Miranda.Roamie.Forms.Controls.Configuration
{
    internal sealed partial class ProfileManagement : CategoryItemControl
    {
        #region .ctors

        public ProfileManagement()
        {
            InitializeComponent();
        }

        #endregion

        #region UI Handlers

        protected override bool OnShow(bool firstTime)
        {
            if (!firstTime)
                ProfilesLVIEW.RefreshProfiles();

            return false;
        }

        private void ProfilesLVIEW_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            EditProfileBTN.Enabled = DeleteProfileBTN.Enabled = (e.IsSelected && !ProfilesLVIEW.ActiveProfileSelected);
        }

        private void DeleteProfileBTN_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show(Resources.MsgBox_Text_ProfileRemovalConfirmation, Resources.MsgBox_Title_ProfileRemovalConfirmation, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) && ProfilesLVIEW.SelectedProfile != null)
            {
                RoamiePlugin.Singleton.RoamingContext.Configuration.ProfileManager.Profiles.Remove(ProfilesLVIEW.SelectedProfile);
                EditProfileBTN.Enabled = DeleteProfileBTN.Enabled = false;

                ProfilesLVIEW.RefreshProfiles();
            }
        }        

        private void EditProfileBTN_Click(object sender, EventArgs e)
        {
            if (ProfilesLVIEW.SelectedProfile != null && ProfileEditingDialog.PresentModal(ProfilesLVIEW.SelectedProfile))
            {
                EditProfileBTN.Enabled = DeleteProfileBTN.Enabled = false;
                ProfilesLVIEW.RefreshProfiles();
            }
        }

        private void ProfilesLVIEW_DoubleClick(object sender, EventArgs e)
        {
            if (ProfilesLVIEW.SelectedProfile != null)
            {
                if (ProfilesLVIEW.SelectedProfile == RoamiePlugin.Singleton.RoamingContext.ActiveProfile)
                    ProfileViewingDialog.PresentModal(ProfilesLVIEW.SelectedProfile);
                else
                    EditProfileBTN.PerformClick();
            }
        }

        private void ProfilesCMENU_Opening(object sender, CancelEventArgs e)
        {
            RoamingProfile profile = ProfilesLVIEW.SelectedProfile;

            if (profile == null)
                e.Cancel = true;
            else
                EditProfileToolStripMenuItem.Enabled = DeleteProfileToolStripMenuItem.Enabled = 
                    !ProfilesLVIEW.ActiveProfileSelected;
        }

        private void ProfilesCMENU_ItemClick(object sender, EventArgs e)
        {
            if (Object.ReferenceEquals(sender, ViewProfileToolStripMenuItem))
                ProfileViewingDialog.PresentModal(ProfilesLVIEW.SelectedProfile);
            else if (Object.ReferenceEquals(sender, TestToolStripMenuItem))
                SyncDialog.TestModal(ProfilesLVIEW.SelectedProfile);
            else if (Object.ReferenceEquals(sender, EditProfileToolStripMenuItem))
                EditProfileBTN.PerformClick();
            else if (Object.ReferenceEquals(sender, DeleteProfileToolStripMenuItem))
                DeleteProfileBTN.PerformClick();
        }

        #endregion        
    }
}
