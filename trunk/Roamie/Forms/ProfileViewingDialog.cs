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
using Virtuoso.Roamie.Roaming.Profiles;
using Virtuoso.Roamie.Properties;

namespace Virtuoso.Roamie.Forms
{
    internal sealed partial class ProfileViewingDialog : ProfileEditorDialog
    {
        #region .ctors

        private ProfileViewingDialog(RoamingProfile profile) : base(profile)
        {
            InitializeComponent();
        }

        public static void PresentModal(RoamingProfile profile)
        {
            if (profile == null) throw new ArgumentNullException("profile");

            using (ProfileViewingDialog dlg = new ProfileViewingDialog(profile))
                dlg.ShowDialog();
        }

        #endregion

        #region UI Handlers

        protected override Image GetDialogImage()
        {
            return Resources.Image_32x32_Profile;
        }

        private void ProfileEditor_Close(object sender, EventArgs e)
        {
            Close();
        }

        #endregion
    }
}

