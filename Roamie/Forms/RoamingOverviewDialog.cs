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
using System.Windows.Forms;
using Virtuoso.Miranda.Plugins.Forms;
using Virtuoso.Miranda.Plugins.Configuration.Forms;
using System.Diagnostics;
using Virtuoso.Roamie.Roaming;
using Virtuoso.Roamie.Properties;

namespace Virtuoso.Roamie.Forms
{
    internal sealed partial class RoamingOverviewDialog : SingletonDialog
    {
        #region Fields

        private bool Initializing;
        private RoamingState PrivateState;

        #endregion

        #region .ctors

        private RoamingOverviewDialog()
        {
            InitializeComponent();
        }

        #endregion

        #region Properties

        private RoamingContext RoamingContext
        {
            get
            {
                return RoamiePlugin.Singleton.RoamingContext;
            }
        }

        #endregion

        #region UI Handlers

        private void RoamingStatusDialog_Load(object sender, EventArgs e)
        {
            if (DisplayRoamingInfos())
            {
                IndicateState();
            }
        }

        private void IndicateState()
        {
            IndicateWipeOnExit();
            IndicateMirroringDisabled();
            IndicateMirroringNotSupported();
            IndicateDeltaSync();
        }

        private bool DisplayRoamingInfos()
        {
            try
            {
                Initializing = true;
                RoamingContext context = RoamingContext;

                IndicateLocalDbInUse();

                if (context.ActiveProfile == null || context.IsInState(RoamingState.Disabled))
                {
                    RoamingProfileLBTN.Text = Resources.Label_UI_RoamingStatusDialog_NoProfile;
                    SyncActionCHBOX.Enabled = PublicModeCHBOX.Enabled = PreferFullSyncCHBOX.Enabled = false;

                    return false;
                }
                else
                {
                    InitializeCheckBoxes();
                    return true;
                }
            }
            finally
            {
                Initializing = false;
            }
        }

        private void InitializeCheckBoxes()
        {
            RoamingContext context = RoamingContext;

            RoamingProfileLBTN.Text = context.ActiveProfile.Name;
            RoamingProfileLBTN.LinkArea = new LinkArea(0, RoamingProfileLBTN.Text.Length);

            PublicModeCHBOX.Enabled = !context.IsInState(RoamingState.LocalProfileLoaded);
            PublicModeCHBOX.Checked = context.IsInState(RoamingState.RemoveLocalCopyOnExit);
            
            SyncActionCHBOX.Enabled = !context.IsInState(RoamingState.RemoteSyncNotSupported);
            SyncActionCHBOX.Checked = !context.IsInState(RoamingState.DiscardLocalChanges);

            PreferFullSyncCHBOX.Checked = context.IsInState(RoamingState.ForceFullSync);
        }

        private void SyncActionCHBOX_CheckedChanged(object sender, EventArgs e)
        {
            PreferFullSyncCHBOX.Enabled = (SyncActionCHBOX.Enabled && SyncActionCHBOX.Checked);

            if (Initializing)
                return;            

            if (SyncActionCHBOX.Checked)
                RoamingContext.State &= ~RoamingState.DiscardLocalChanges;
            else
                RoamingContext.State |= RoamingState.DiscardLocalChanges;

            IndicateState();
        }

        private void PublicModeCHBOX_CheckedChanged(object sender, EventArgs e)
        {
            if (Initializing)
                return;

            if (PublicModeCHBOX.Checked)
                RoamingContext.State |= RoamingState.RemoveLocalCopyOnExit;
            else
                RoamingContext.State &= ~RoamingState.RemoveLocalCopyOnExit;

            IndicateState();
        }

        private void ForceFullSyncCHBOX_CheckedChanged(object sender, EventArgs e)
        {
            if (Initializing)
                return;

            if (PreferFullSyncCHBOX.Checked)
                RoamingContext.State |= RoamingState.ForceFullSync;
            else
                RoamingContext.State &= ~RoamingState.ForceFullSync;

            IndicateState();
        }

        private void RoamingProfileLBTN_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ProfileViewingDialog.PresentModal(RoamiePlugin.Singleton.RoamingContext.ActiveProfile);
        }
        
        private void OptionsBTN_Click(object sender, EventArgs e)
        {
            ConfigurationDialog.Present(false, RoamiePlugin.Singleton);
        }

        #region Indicators

        private void IndicateDeltaSync()
        {
            if (RoamingContext.IsInState(RoamingState.Disabled))
                SyncStatusPBOX.Image = Resources.Image_32x32_Disabled;
            else if (RoamingContext.IsInState(RoamingState.ForceFullSync))
                SyncStatusPBOX.Image = Resources.Image_32x32_Sync;
            else
                SyncStatusPBOX.Image = Resources.Image_32x32_SyncDelta;
       }

        private void IndicateMirroringNotSupported()
        {
          /*  if ((PrivateState & RoamingState.RemoteSyncNotSupported) == RoamingState.RemoteSyncNotSupported)
                StatusLVIEW.Items.Add(CreateHighlightedItem(Resources.Text_UI_RoamingStatus_DownloadOnlySession, TraceEventType.Warning));
        */}

        private void IndicateMirroringDisabled()
        {
            /*if (RoamingContext.IsInState(RoamingState.DiscardLocalChanges))
                StatusLVIEW.Items.Add(CreateHighlightedItem(Resources.Text_UI_RoamingStatus_SandboxMode, TraceEventType.Warning));
            else
                StatusLVIEW.Items.Add(Resources.Text_UI_RoamingStatus_NonSandboxMode);
       */ }

        private void IndicateWipeOnExit()
        {
            if (RoamingContext.IsInState(RoamingState.RemoveLocalCopyOnExit))
                ThisComputerOverlayPBOX.Visible = true;
            else
                ThisComputerOverlayPBOX.Visible = false;
        }

        private void IndicateLocalDbInUse()
        {
          /*  if ((PrivateState & RoamingState.LocalProfileLoaded) == RoamingState.LocalProfileLoaded)
            {
                StatusLVIEW.Items.Add(new ListViewItem(new string[] { Resources.Text_UI_RoamingStatus_Local, 
                    Resources.Text_UI_RoamingStatus_Local }));

                if ((PrivateState & RoamingState.Active) == RoamingState.Active)
                    StatusLVIEW.Items.Add(Resources.Text_UI_RoamingStatus_LocalRoaming);
            }
            else if ((PrivateState & RoamingState.Active) == RoamingState.Active)
                StatusLVIEW.Items.Add(Resources.Text_UI_RoamingStatus_Roaming);
       */ }

        #endregion

        private void MoreOptionsLINK_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ConfigurationDialog.Present(false, RoamiePlugin.Singleton);
        }

        #endregion
    }
}