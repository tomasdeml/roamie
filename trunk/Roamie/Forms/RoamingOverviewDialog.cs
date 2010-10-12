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
using Virtuoso.Miranda.Plugins.Forms;
using Virtuoso.Miranda.Plugins.Configuration.Forms;
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

        private static RoamingContext RoamingContext
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
            DisplayRoamingInfo();
            IndicateState();
        }

        private void DisplayRoamingInfo()
        {
            Initializing = true;
            RoamingContext context = RoamingContext;

            if (context.ActiveProfile == null || context.IsInState(RoamingState.Disabled))
            {
                RoamingProfileLBTN.Text = Resources.Label_UI_RoamingStatusDialog_NoProfile;
                SyncActionCHBOX.Enabled =
                    PublicModeCHBOX.Enabled = PreferFullSyncCHBOX.Enabled = RoamingProfileLBTN.Enabled = false;
            }
            else
            {
                InitializeCheckBoxes();
            }

            Initializing = false;
        }

        private void IndicateState()
        {
            IndicateThisComputerState();
            IndicateSyncState();
        }

        private void InitializeCheckBoxes()
        {
            RoamingContext context = RoamingContext;

            SyncActionCHBOX.Enabled = !context.IsInState(RoamingState.RemoteSyncNotSupported);
            SyncActionCHBOX.Checked = !context.IsInState(RoamingState.DiscardLocalChanges);

            bool remoteProfileIsMaster = !context.LocalProfileIsMaster;

            PublicModeCHBOX.Enabled = remoteProfileIsMaster;
            PublicModeCHBOX.Checked = context.IsInState(RoamingState.RemoveLocalCopyOnExit);

            PreferFullSyncCHBOX.Enabled = remoteProfileIsMaster && SyncActionCHBOX.Checked;
            PreferFullSyncCHBOX.Checked = context.IsInState(RoamingState.ForceFullSync);
        }

        private void SyncActionCHBOX_CheckedChanged(object sender, EventArgs e)
        {
            if (Initializing)
                return;

            PreferFullSyncCHBOX.Enabled = (SyncActionCHBOX.Enabled && SyncActionCHBOX.Checked &&
                                           !RoamingContext.IsInState(RoamingState.LocalProfileLoaded, RoamingState.NewProfileCreated));

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

        private void MoreOptionsLINK_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ConfigurationDialog.Present(false, RoamiePlugin.Singleton);
        }

        #endregion

        #region Indicators

        private void IndicateSyncState()
        {
            bool localProfileIsMaster = RoamingContext.LocalProfileIsMaster;

            if (RoamingContext.IsInState(RoamingState.Disabled))
                SyncStatusPBOX.Image = Resources.Image_32x32_Block;
            else if (RoamingContext.IsInState(RoamingState.DiscardLocalChanges))
                SyncStatusPBOX.Image = localProfileIsMaster ? Resources.Image_32x32_Block : Resources.Image_32x32_Left;
            else if (localProfileIsMaster)
                SyncStatusPBOX.Image = Resources.Image_32x32_Right;
            else if (RoamingContext.IsInState(RoamingState.ForceFullSync))
                SyncStatusPBOX.Image = Resources.Image_32x32_Sync;
            else
                SyncStatusPBOX.Image = Resources.Image_32x32_SyncDelta;
        }

        private void IndicateThisComputerState()
        {
            if (RoamingContext.IsInState(RoamingState.RemoveLocalCopyOnExit))
            {
                ThisComputerOverlayPBOX.Visible = true;
                ThisComputerOverlayPBOX.Image = Resources.Image_16x16_Delete2;
            }
            else
                ThisComputerOverlayPBOX.Visible = false;
        }

        #endregion
    }
}