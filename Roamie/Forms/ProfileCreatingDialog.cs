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
using System.Drawing;
using Virtuoso.Roamie.Forms.Controls;
using Virtuoso.Roamie.Roaming;
using Virtuoso.Roamie.Roaming.Profiles;
using Virtuoso.Roamie.Properties;
using Virtuoso.Roamie.Configuration;

namespace Virtuoso.Roamie.Forms
{
    internal sealed partial class ProfileCreatingDialog : ProfileEditorDialog
    {
        #region Fields

        private RoamingProfile CreatedProfile;

        #endregion

        #region .ctors

        private ProfileCreatingDialog()
        {
            InitializeComponent();
        }

        public static RoamingProfile PresentModal()
        {
            using (ProfileCreatingDialog dlg = new ProfileCreatingDialog())
            {
                dlg.ShowDialog();
                return dlg.CreatedProfile;
            }
        }

        #endregion

        #region UI Handlers

        protected override Image GetDialogImage()
        {
            return Resources.Image_32x32_Add2;
        }

        private void ProfileEditor_Save(object sender, ProfileEditor.SaveEventArgs e)
        {
            RoamingConfiguration configuration = RoamiePlugin.Singleton.RoamingContext.Configuration;

            ProfileCollection profiles = configuration.ProfileManager.Profiles;
            profiles.Add((CreatedProfile = e.Profile));

            configuration.Save();
            Close();
        }

        private void ProfileEditor_Cancel(object sender, EventArgs e)
        {
            Close();
        }

        #endregion
    }
}

