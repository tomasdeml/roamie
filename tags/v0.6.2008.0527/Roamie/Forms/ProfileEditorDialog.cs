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
using Virtuoso.Miranda.Roamie.Forms.Controls;

namespace Virtuoso.Miranda.Roamie.Forms
{
    internal partial class ProfileEditorDialog : Form
    {
        #region Fields

        private RoamingProfile Profile;

        #endregion

        #region .ctors

        protected ProfileEditorDialog()
        {
            InitializeComponent();
        }

        protected ProfileEditorDialog(RoamingProfile profile) : this()
        {
            this.Profile = profile;
        }

        #endregion

        #region UI Handlers

        protected override void OnLoad(EventArgs e)
        {
            if (Profile != null)
                ProfileEditor.LoadProfile(Profile);

            categoryItemHeader1.Image = GetDialogImage();
            categoryItemHeader1.HeaderText = Text;

            base.OnLoad(e);
        }

        protected virtual Image GetDialogImage()
        {
            return Resources.Image_32x32_Edit;
        }

        private void ProfileEditor_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    e.Handled = true;
                    ProfileEditor.ClickOk();
                    break;
                case Keys.Escape:
                    e.Handled = true;
                    ProfileEditor.ClickCancel();
                    break;
            }
        }

        #endregion
    }
}