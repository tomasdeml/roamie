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
using Virtuoso.Miranda.Roamie.Roaming;
using Virtuoso.Miranda.Plugins.Infrastructure;
using Virtuoso.Miranda.Plugins.Forms;
using Virtuoso.Miranda.Plugins.Configuration.Forms;
using System.Diagnostics;

namespace Virtuoso.Miranda.Roamie.Forms
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

        #region UI Handlers

        private void RoamingStatusDialog_Load(object sender, EventArgs e)
        {
            PrivateState = RoamiePlugin.Singleton.RoamingContext.State;

            if (DisplayRoamingInfos())
            {
                IndicateWipeOnExit();
                IndicateMirroringDisabled();
                IndicateMirroringNotSupported();
                IndicateDeltaSync();
            }
        }

        private static ListViewItem CreateHighlightedItem(string text)
        {
            return CreateHighlightedItem(text, TraceEventType.Verbose);
        }

        private static ListViewItem CreateHighlightedItem(string text, TraceEventType level)
        {
            ListViewItem item = new ListViewItem(text);
            item.Font = new Font(item.Font, FontStyle.Bold);

            switch (level)
            {
                case TraceEventType.Information:
                    item.ForeColor = Color.Green;
                    break;
                case TraceEventType.Warning:
                    item.ForeColor = Color.OrangeRed;
                    break;
                case TraceEventType.Error:
                    item.ForeColor = Color.Red;
                    break;
            }

            return item;
        }

        private bool DisplayRoamingInfos()
        {
            try
            {
                Initializing = true;
                RoamingContext context = RoamiePlugin.Singleton.RoamingContext;

                IndicateRoamingActive();
                IndicateLocalDbInUse();

                if (context.ActiveProfile == null || context.IsInState(RoamingState.Disabled))
                {
                    RoamingProfileLBTN.Text = Resources.Label_UI_RoamingStatusDialog_NoProfile;

                    SyncActionCHBOX.Text = Resources.Label_UI_RoamingStatusDialog_OnExitCHBOX_NoAction;
                    SyncActionCHBOX.Enabled = PublicModeCHBOX.Enabled = PreferFullSyncCHBOX.Enabled = OkBTN.Enabled = false;

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
            RoamingContext context = RoamiePlugin.Singleton.RoamingContext;

            RoamingProfileLBTN.Text = context.ActiveProfile.Name;
            RoamingProfileLBTN.LinkArea = new LinkArea(0, RoamingProfileLBTN.Text.Length);

            PublicModeCHBOX.Enabled = (PrivateState & RoamingState.LocalDbInUse) != RoamingState.LocalDbInUse;
            PublicModeCHBOX.Checked = (PrivateState & RoamingState.WipeLocalDbOnExit) == RoamingState.WipeLocalDbOnExit;
            
            SyncActionCHBOX.Enabled = (PrivateState & RoamingState.MirroringNotSupported) != RoamingState.MirroringNotSupported;
            SyncActionCHBOX.Checked = (PrivateState & RoamingState.DiscardLocalChanges) != RoamingState.DiscardLocalChanges;

            PreferFullSyncCHBOX.Checked = (PrivateState & RoamingState.PreferFullSync) == RoamingState.PreferFullSync;
        }

        private void OnExitCHBOX_CheckedChanged(object sender, EventArgs e)
        {
            PreferFullSyncCHBOX.Enabled = (SyncActionCHBOX.Enabled && SyncActionCHBOX.Checked && (PrivateState & RoamingState.DeltaIncompatibleChangeOccured) != RoamingState.DeltaIncompatibleChangeOccured);

            if (Initializing)
                return;            

            if (SyncActionCHBOX.Checked)
                PrivateState &= ~RoamingState.DiscardLocalChanges;
            else
                PrivateState |= RoamingState.DiscardLocalChanges;
        }

        private void PublicModeCHBOX_CheckedChanged(object sender, EventArgs e)
        {
            if (Initializing)
                return;

            if (PublicModeCHBOX.Checked)
                PrivateState |= RoamingState.WipeLocalDbOnExit;
            else
                PrivateState &= ~RoamingState.WipeLocalDbOnExit;
        }

        private void ForceFullSyncCHBOX_CheckedChanged(object sender, EventArgs e)
        {
            if (Initializing)
                return;

            if (PreferFullSyncCHBOX.Checked)
                PrivateState |= RoamingState.PreferFullSync;
            else
                PrivateState &= ~RoamingState.PreferFullSync;
        }

        private void RoamingProfileLBTN_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ProfileViewingDialog.PresentModal(RoamiePlugin.Singleton.RoamingContext.ActiveProfile);
        }

        private void OkBTN_Click(object sender, EventArgs e)
        {
            RoamiePlugin.Singleton.RoamingContext.State = PrivateState;
            Close();
        }

        private void CancelBTN_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void OptionsBTN_Click(object sender, EventArgs e)
        {
            ConfigurationDialog.Present(false, RoamiePlugin.Singleton);
        }

        #region Indicators

        private void IndicateDeltaSync()
        {
            if ((PrivateState & RoamingState.DeltaSyncEngineLoaded) == RoamingState.DeltaSyncEngineLoaded)
            {
                if ((PrivateState & RoamingState.PreferFullSync) == RoamingState.PreferFullSync)
                    StatusLVIEW.Items.Add(CreateHighlightedItem(Resources.Text_UI_RoamingStatus_ForceFullSync));

                if ((PrivateState & RoamingState.DeltaIncompatibleChangeOccured) != RoamingState.DeltaIncompatibleChangeOccured)
                {
                    StatusLVIEW.Items.Add(CreateHighlightedItem(Resources.Text_UI_RoamingStatus_DeltaSync, TraceEventType.Information));
                    Virtuoso.Miranda.Roamie.Roaming.DeltaSync.DeltaSyncEngine engine = RoamiePlugin.Singleton.RoamingContext.DeltaEngine;

                    if (engine.DeltaMergeRecommended)
                        StatusLVIEW.Items.Add(CreateHighlightedItem(String.Format(Resources.Text_Formatable1_UI_RoamingStatus_DeltaMergeRecommended, engine.DeltaManifest.DeltaCount.ToString()), TraceEventType.Warning));
                }
                else
                    StatusLVIEW.Items.Add(CreateHighlightedItem(Resources.Text_UI_RoamingStatus_DeltaIncompatibleChange, TraceEventType.Warning));
            }
        }

        private void IndicateMirroringNotSupported()
        {
            if ((PrivateState & RoamingState.MirroringNotSupported) == RoamingState.MirroringNotSupported)
                StatusLVIEW.Items.Add(CreateHighlightedItem(Resources.Text_UI_RoamingStatus_DownloadOnlySession, TraceEventType.Warning));
        }

        private void IndicateMirroringDisabled()
        {
            if ((PrivateState & RoamingState.DiscardLocalChanges) == RoamingState.DiscardLocalChanges)
                StatusLVIEW.Items.Add(CreateHighlightedItem(Resources.Text_UI_RoamingStatus_SandboxMode, TraceEventType.Warning));
            else
                StatusLVIEW.Items.Add(Resources.Text_UI_RoamingStatus_NonSandboxMode);
        }

        private void IndicateWipeOnExit()
        {
            if ((PrivateState & RoamingState.WipeLocalDbOnExit) == RoamingState.WipeLocalDbOnExit)
                StatusLVIEW.Items.Add(CreateHighlightedItem(Resources.Text_UI_RoamingStatus_PublicMode));
            else
                StatusLVIEW.Items.Add(Resources.Text_UI_RoamingStatus_NonPublicMode);
        }

        private void IndicateLocalDbInUse()
        {
            if ((PrivateState & RoamingState.LocalDbInUse) == RoamingState.LocalDbInUse)
            {
                StatusLVIEW.Items.Add(new ListViewItem(new string[] { Resources.Text_UI_RoamingStatus_Local, 
                    Resources.Text_UI_RoamingStatus_Local }));

                if ((PrivateState & RoamingState.Active) == RoamingState.Active)
                    StatusLVIEW.Items.Add(Resources.Text_UI_RoamingStatus_LocalRoaming);
            }
            else if ((PrivateState & RoamingState.Active) == RoamingState.Active)
                StatusLVIEW.Items.Add(Resources.Text_UI_RoamingStatus_Roaming);
        }

        private void IndicateRoamingActive()
        {
            if ((PrivateState & RoamingState.Active) == RoamingState.Active)
                StatusPBOX.Image = Resources.Image_32x32_Running;
            else
                StatusPBOX.Image = Resources.Image_32x32_Stopped;
        }

        #endregion

        #endregion
    }
}