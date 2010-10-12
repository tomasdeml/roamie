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
using System.Drawing;
using System.Windows.Forms;
using Virtuoso.Roamie.Roaming.Profiles;
using Virtuoso.Roamie.RoamingProviders;
using Virtuoso.Roamie.Properties;

namespace Virtuoso.Roamie.Forms.Controls
{
    internal partial class ProfileEditor : UserControl
    {
        #region Delegates & Events
        
        private delegate void InvokeWithProfile(ref RoamingProfile profile);
        private delegate bool InvokeAndVerify();

        public event EventHandler<SaveEventArgs> Save;
        public event EventHandler Cancel;
        public event EventHandler Close;

        #endregion

        #region Fields & Enums

        public enum EditingMode
        {
            ViewProfile,
            CreateProfile,
            EditProfile,
        }

        private EditingMode mode;
        private RoamingProfile LoadedProfile;        

        #endregion

        #region .ctors

        public ProfileEditor()
        {
            InitializeComponent();
        }

        private void ProfileEditor_Load(object sender, EventArgs e)
        {
            ReloadProviders();
        }

        #endregion

        #region Profile handling

        public void ReloadProviders()
        {
            DatabaseProviderLBOX.Items.Clear();

            if (RoamiePlugin.Singleton != null)
            {                                
                List<DatabaseProvider> providers = new List<DatabaseProvider>(2);

                foreach (DatabaseProvider provider in RoamiePlugin.Singleton.RoamingContext.DatabaseProviders.Values)
                    providers.Add(provider);

                DatabaseProviderLBOX.DisplayMember = "Name";
                DatabaseProviderLBOX.DataSource = providers;
                
                if (DatabaseProviderLBOX.Items.Count > 0)
                    DatabaseProviderLBOX.SelectedIndex = 0;
            }
        }

        public bool CheckValues()
        {
            if (InvokeRequired)
                return (bool)Invoke(new InvokeAndVerify(DoCheckValues));
            else
                return DoCheckValues();
        }

        private bool DoCheckValues()
        {
            ErrorProvider.Clear();

            if (String.IsNullOrEmpty(ProfileNameTBOX.Text))
            {
                ProfileNameTBOX.BannerForeColor = Color.Red;
                return false;
            }
            else
            {
                RoamingProfile profile = null;

                if ((profile = RoamiePlugin.Singleton.RoamingContext.Configuration.ProfileManager.Profiles.Find(delegate(RoamingProfile _profile)
                {
                    return _profile.Name == ProfileNameTBOX.Text;
                })) != null && profile != LoadedProfile)
                {
                    ErrorProvider.SetError(ProfileNameTBOX, Resources.Label_UI_ProfileEditor_NameAlreadyExists);
                    return false;
                }
            }

            DatabaseProvider provider = (DatabaseProvider)DatabaseProviderLBOX.SelectedItem;

            if (DatabaseProviderLBOX.SelectedItem == null)
            {
                ErrorProvider.SetError(DatabaseProviderLBOX, Resources.Label_UI_ProfileEditor_NoDbProviderSelected);
                return false;
            }

            if (String.IsNullOrEmpty(DescriptionTBOX.Text))
            {
                DescriptionTBOX.BannerForeColor = Color.Red;
                return false;
            }

            if (String.IsNullOrEmpty(RemoteAddressTBOX.Text))
            {
                RemoteAddressTBOX.BannerForeColor = Color.Red;
                return false;
            }

            if (provider.CredentialsRequired)
            {
                if (String.IsNullOrEmpty(LoginNameTBOX.Text))
                {
                    LoginNameTBOX.BannerForeColor = Color.Red;
                    return false;
                }

                if (String.IsNullOrEmpty(LoginPasswordTBOX.Text))
                {
                    LoginPasswordTBOX.BannerForeColor = Color.Red;
                    return false;
                }
            }

            if (String.IsNullOrEmpty(DatabasePasswordTBOX.Text))
            {
                DatabasePasswordTBOX.BannerForeColor = Color.Red;
                return false;
            }

            return true;
        }

        public RoamingProfile GetProfile()
        {
            RoamingProfile profile = null;

            if (InvokeRequired)
                Invoke(new InvokeWithProfile(GetProfileMethod), new object[] { profile });
            else
                GetProfileMethod(ref profile);

            return profile;
        }

        private void GetProfileMethod(ref RoamingProfile profile)
        {
            if (CheckValues())
            {
                profile = new RoamingProfile(ProfileNameTBOX.Text, DescriptionTBOX.Text, RemoteAddressTBOX.Text, LoginNameTBOX.Text, LoginPasswordTBOX.Text, DatabasePasswordTBOX.Text, ((DatabaseProvider)DatabaseProviderLBOX.SelectedItem).Name);
                profile.PreferFullSync = PreferFullSync.Checked;
            }
        }        

        public void LoadProfile(RoamingProfile profile)
        {
            if (profile == null) throw new ArgumentNullException("profile");

            if (InvokeRequired)
                Invoke(new InvokeWithProfile(LoadProfileMethod), new object[] { profile });
            else
                LoadProfileMethod(ref profile);
        }

        private void LoadProfileMethod(ref RoamingProfile profile)
        {
            ProfileNameTBOX.Text = profile.Name;
            DescriptionTBOX.Text = profile.Description;
            RemoteAddressTBOX.Text = profile.RemoteHost;
            LoginNameTBOX.Text = profile.UserName;
            LoginPasswordTBOX.Text = profile.Password;
            DatabasePasswordTBOX.Text = profile.DatabasePassword;
            DatabaseProviderLBOX.SelectedItem = profile.GetProvider();
            PreferFullSync.Checked = profile.PreferFullSync;
            
            LoadedProfile = profile;
        }

        public void Clear()
        {
            ErrorProvider.Clear();

            ProfileNameTBOX.Clear();
            DescriptionTBOX.Clear();
            RemoteAddressTBOX.Clear();
            LoginNameTBOX.Clear();
            LoginPasswordTBOX.Clear();
            DatabasePasswordTBOX.Clear();

            ReloadProviders();

            if (DatabaseProviderLBOX.Items.Count > 0)
                DatabaseProviderLBOX.SelectedIndex = 0;
        }

        public void ClickOk()
        {            
            ActionBTN.PerformClick();
        }

        public void ClickCancel()
        {
            if (mode != EditingMode.ViewProfile)
                CancelBTN.PerformClick();
        }

        #endregion        

        #region Properties

        public EditingMode Mode
        {
            get
            {
                return mode;
            }
            set
            {
                if (!Enum.IsDefined(typeof(EditingMode), value)) throw new ArgumentOutOfRangeException();

                mode = value;
                bool editing = mode != EditingMode.ViewProfile;

                CancelBTN.Visible = editing;
                UploadPathExamplesLink.Visible = editing;

                ProfileNameTBOX.ReadOnly = DescriptionTBOX.ReadOnly = RemoteAddressTBOX.ReadOnly = LoginNameTBOX.ReadOnly =
                    LoginPasswordTBOX.ReadOnly = DatabasePasswordTBOX.ReadOnly = !editing;

                DatabaseProviderLBOX.Enabled = PreferFullSync.Enabled = editing;
            }
        }
                
        #endregion

        #region UI Handlers

        private void TestBTN_Click(object sender, EventArgs e)
        {
            if (CheckValues())
                SyncDialog.TestModal(GetProfile());
        }

        private void ActionBTN_Click(object sender, EventArgs e)
        {
            if (CheckValues() && mode != EditingMode.ViewProfile && Save != null)
                Save(this, new SaveEventArgs(GetProfile(), LoadedProfile, mode));
            else if (mode == EditingMode.ViewProfile && Close != null)
                Close(this, EventArgs.Empty);
        }

        private void CancelBTN_Click(object sender, EventArgs e)
        {
            if (mode != EditingMode.ViewProfile && Cancel != null)
                Cancel(this, EventArgs.Empty);
        }

        private void Control_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    e.Handled = true;
                    ActionBTN.PerformClick();
                    break;
                case Keys.Escape:
                    e.Handled = true;
                    ParentForm.Close();
                    break;
            }
        }

        private void UploadPathExamplesLINK_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            HelpToolTip.Show(String.Format(Resources.Balloon_Text_Examples_NL1, Environment.NewLine), RemoteAddressTBOX, 0, 0);
        }

        #endregion        
    }
}
