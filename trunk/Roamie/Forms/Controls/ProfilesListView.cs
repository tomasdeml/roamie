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
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using Virtuoso.Roamie.Properties;
using Virtuoso.Roamie.Roaming.Profiles;

namespace Virtuoso.Roamie.Forms.Controls
{
    internal class ProfilesListView : ListView
    {
        #region Fields

        private const string ProfileIconName = "Profile";
        private const string NewProfileIconName = "NewProfile";
        private const string ActiveProfileIconName = "ActiveProfile";

        private ListViewItem NewProfileItem;

        private ColumnHeader NameCOLUMN;
        private ColumnHeader DescriptionCOLUMN;

        private ImageList ImageList;
        private ToolTip InfoToolTip;
        private IContainer components;

        private bool doNotMaskNewProfileItem;

        #endregion

        #region Events

        public event EventHandler NewProfileRequested;

        #endregion

        #region .ctors

        public ProfilesListView()
        {
            InitializeComponent();

            InitializeImageList();
            InitializeSpecialItems();
                                 
            RefreshProfiles();
        }

        private void InitializeSpecialItems()
        {
            NewProfileItem = new ListViewItem(Resources.Label_UI_ProfileListView_NewProfile, NewProfileIconName);
            ListViewItem.ListViewSubItem NewProfileItemSub1 = new ListViewItem.ListViewSubItem(NewProfileItem, Resources.Label_UI_ProfileListView_NewProfile_Description);
            NewProfileItemSub1.ForeColor = SystemColors.ControlDark;
            NewProfileItem.SubItems.Add(NewProfileItemSub1);
        }

        private void InitializeImageList()
        {
            ImageList = new ImageList();
            ImageList.ColorDepth = ColorDepth.Depth32Bit;
            ImageList.ImageSize = new Size(32, 32);
            ImageList.Images.Add(ProfileIconName, Resources.Image_32x32_Profile);
            ImageList.Images.Add(NewProfileIconName, Resources.Image_32x32_Add2);
            ImageList.Images.Add(ActiveProfileIconName, Resources.Image_32x32_Active);
            LargeImageList = ImageList;
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.NameCOLUMN = new System.Windows.Forms.ColumnHeader();
            this.DescriptionCOLUMN = new System.Windows.Forms.ColumnHeader();
            this.InfoToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // InfoToolTip
            // 
            this.InfoToolTip.Active = false;
            this.InfoToolTip.AutoPopDelay = 10000;
            this.InfoToolTip.InitialDelay = 500;
            this.InfoToolTip.ReshowDelay = 100;
            // 
            // ProfilesListView
            // 
            this.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.NameCOLUMN,
            this.DescriptionCOLUMN});
            this.MultiSelect = false;
            this.TileSize = new System.Drawing.Size(300, 60);
            this.View = System.Windows.Forms.View.Tile;
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ProfilesListView_MouseMove);
            this.ResumeLayout(false);

        }

        #endregion

        #region Methods

        public void RefreshProfiles()
        {
            RoamiePlugin singleton = RoamiePlugin.Singleton;

            if (singleton != null)
            {
                try
                {
                    BeginUpdate();

                    bool restoreNewProfileItem = Items.Contains(NewProfileItem);
                    Items.Clear();

                    ProfileManager manager = singleton.RoamingContext.Configuration.ProfileManager;
                    manager.VerifyProfiles();

                    ProfileCollection profiles = manager.Profiles;

                    if (profiles.Count > 0)
                    {
                        foreach (RoamingProfile profile in profiles)
                        {
                            ListViewItem item = new ListViewItem(profile.Name, profile.Equals(RoamiePlugin.Singleton.RoamingContext.ActiveProfile) ? ActiveProfileIconName : ProfileIconName);
                            item.Tag = profile;

                            ListViewItem.ListViewSubItem subItem1 = new ListViewItem.ListViewSubItem(item, profile.Description);
                            subItem1.ForeColor = SystemColors.ControlDark;
                            item.SubItems.Add(subItem1);

                            ListViewItem.ListViewSubItem subItem2 = new ListViewItem.ListViewSubItem(item, profile.RemoteHost);
                            subItem2.ForeColor = SystemColors.ControlDark;
                            item.SubItems.Add(subItem2);

                            Items.Add(item);
                        }
                    }

                    ShowNewProfileItem(restoreNewProfileItem);
                }
                finally
                {
                    Sort();
                    EndUpdate();
                }                
            }
        }

        public void ShowNewProfileItem(bool value)
        {
            if (Items.Contains(NewProfileItem) && !value)
                Items.Remove(NewProfileItem);
            else if (!Items.Contains(NewProfileItem) && value)
                Items.Add(NewProfileItem);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing)
                ImageList.Dispose();
        }

        #endregion

        #region Properties

        [Browsable(false)]
        public RoamingProfile SelectedProfile
        {
            get
            {
                if (SelectedItems.Count > 0 && SelectedItems[0] != NewProfileItem)
                    return (RoamingProfile)SelectedItems[0].Tag;
                else
                    return null;
            }
        }

        [Browsable(false)]
        public bool ActiveProfileSelected
        {
            get
            {
                return SelectedItems.Count != 0 &&
                    ((RoamingProfile)SelectedItems[0].Tag).Equals(RoamiePlugin.Singleton.RoamingContext.ActiveProfile);
            }
        }

        [Browsable(true), DefaultValue(false)]
        public bool DoNotMaskNewProfileItem
        {
            get
            {
                return doNotMaskNewProfileItem;
            }
            set
            {
                doNotMaskNewProfileItem = value;
            }
        }

        [Browsable(true), DefaultValue(false)]
        public bool ShowInfoTips
        {
            get
            {
                return InfoToolTip.Active;
            }
            set
            {
                InfoToolTip.Active = value;
            }
        }

        #endregion

        #region UI Handlers

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            if (SelectedItems.Count > 0 && SelectedItems[0] == NewProfileItem)
            {
                if (NewProfileRequested != null)
                    NewProfileRequested(this, EventArgs.Empty);
            }
            else
                base.OnMouseDoubleClick(e);
        }               

        protected override void OnItemSelectionChanged(ListViewItemSelectionChangedEventArgs e)
        {
            if (SelectedItems.Count > 0 && SelectedItems[0] == NewProfileItem && !doNotMaskNewProfileItem)
                return;

            base.OnItemSelectionChanged(e);
        }     
        
        private void ProfilesListView_MouseMove(object sender, MouseEventArgs e)
        {
            ListViewItem item = GetItemAt(e.X, e.Y);

            if (item != null)
            {
                if (item != NewProfileItem)
                {
                    RoamingProfile profile = (RoamingProfile)item.Tag;

                    string tooltip = String.Format(Resources.Balloon_Text_ProfileToolTip_NL1, Environment.NewLine, String.IsNullOrEmpty(profile.UserName) ? Resources.Text_UI_Anonymous : profile.UserName, profile.RoamingProvider, profile.RemoteHost);

                    if (profile.PreferFullSync)
                        tooltip = String.Format("{0}{1}{1}{2}", tooltip, Environment.NewLine, Resources.Text_UI_RoamingStatus_ForceFullSync);

                    InfoToolTip.ToolTipTitle = profile.Name;
                    InfoToolTip.Show(String.Intern(tooltip), this, e.X + 20, e.Y);
                }
            }
            else
                InfoToolTip.Hide(this);
        }

        #endregion        
    }
}
