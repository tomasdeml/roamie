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
using System.IO;
using System.Diagnostics;
using Virtuoso.Miranda.Plugins.Configuration.Forms;
using Virtuoso.Roamie.Roaming;
using Virtuoso.Roamie.Roaming.Profiles;
using Virtuoso.Roamie.RoamingProviders;
using Virtuoso.Roamie.Properties;

namespace Virtuoso.Roamie.Forms
{
    internal sealed partial class StartupDialog : Form
    {
        #region Fields

        private readonly bool FirstTime;

        #endregion

        #region .ctors

        private StartupDialog(bool firstTime)
        {
            InitializeComponent();

            FirstTime = firstTime;

            UseLocalRBTN.Enabled = LocalPBOX.Enabled = !firstTime;
            CreateNewRBTN.Enabled = NewPBOX.Enabled = firstTime;

            CreateNewRBTN.Checked = firstTime;
            UseLocalRBTN.Checked = !firstTime;
        }

        public static void PresentModal(bool firstTime)
        {
            using (StartupDialog dlg = new StartupDialog(firstTime))
            {
                bool handled = false;

                if (!firstTime)
                    handled = ApplyBootConfiguration(dlg, false);
                else
                    Trace.WriteLineIf(RoamiePlugin.TraceSwitch.TraceInfo, "MirandaBoot.ini contains no Roamie default settings.", RoamiePlugin.TraceCategory);

                if (!handled)
                    dlg.ShowDialog();
            }
        }

        private static bool ApplyBootConfiguration(StartupDialog dlg, bool handled)
        {
            MirandaBootConfiguration bootConfig = MirandaBootConfiguration.Load();

            if (bootConfig != null && bootConfig.IsValid)
            {
                switch (bootConfig.StartupOption)
                {
                    case StartupOption.DownloadDatabase:
                        dlg.DownloadExistingRBTN.Checked = true;
                        dlg.PublicComputerCHBOX.Checked = bootConfig.PublicPc;
                        dlg.SandboxModeCHBOX.Checked = bootConfig.SandboxMode;
                        break;
                    case StartupOption.UseLocalDatabase:
                        dlg.UseLocalRBTN.Checked = true;
                        dlg.RoamLocalOnExitCHBOX.Checked = !bootConfig.SandboxMode;
                        break;
                }

                Trace.WriteLineIf(RoamiePlugin.TraceSwitch.TraceInfo, "MirandaBoot.ini contains Roamie default settings => using these settings...", RoamiePlugin.TraceCategory);
                handled = dlg.ProcessSelection(bootConfig.Profile);
            }
            else
                Trace.WriteLineIf(RoamiePlugin.TraceSwitch.TraceInfo, "MirandaBoot.ini contains invalid Roamie settings.", RoamiePlugin.TraceCategory);

            return handled;
        }

        #endregion

        #region Helpers

        private bool ProcessSelection(RoamingProfile preselectedProfile)
        {
            RoamingState selectedRoamingState = GatherSelection();

            RoamingContext context = RoamiePlugin.Singleton.RoamingContext;
            context.State = selectedRoamingState;
            
            if (context.IsInState(RoamingState.Active))
            {
                RoamingProfile profile = (preselectedProfile ?? ProfileSelectionDialog.PresentModal());

                if (profile == null)
                {
                    context.State = RoamingState.Disabled;
                    return false;
                }
                
                context.ActivateProfile(profile);

                if (!context.IsInState(RoamingState.LocalDbInUse))
                {
                    try
                    {
                        if (context.Configuration.SilentStartup)
                            Opacity = 0;

                        RoamingOrchestration.Preparation.PerformLocalSynchronization();
                    }
                    catch
                    {
                        Opacity = 1D;
                        return false;
                    }
                }
            }

            return true;
        }

        private RoamingState GatherSelection()
        {
            RoamingState selectedRoamingState;

            if (DownloadExistingRBTN.Checked)
            {
                selectedRoamingState = RoamingState.Active;

                if (PublicComputerCHBOX.Checked)
                    selectedRoamingState |= RoamingState.WipeLocalDbOnExit;

                if (SandboxModeCHBOX.Checked)
                    selectedRoamingState |= RoamingState.DiscardLocalChanges;
            }
            else if (UseLocalRBTN.Checked)
            {
                selectedRoamingState = RoamingState.LocalDbInUse;

                if (RoamLocalOnExitCHBOX.Checked)
                    selectedRoamingState |= RoamingState.Active | RoamingState.DeltaIncompatibleChangeOccured | RoamingState.PreferFullSync;
                else
                    selectedRoamingState |= RoamingState.DiscardLocalChanges;
            }
            else /*CreateNewRBTN.Checked*/
            {
                selectedRoamingState = RoamingState.CreateNewDb | RoamingState.LocalDbInUse;

                if (RoamNewOnExitCHBOX.Checked)
                    selectedRoamingState |= RoamingState.Active;
                else
                    selectedRoamingState |= RoamingState.DiscardLocalChanges;
            }

            return selectedRoamingState;
        }

        #endregion

        #region UI Handlers

        private void StartupDialog_Load(object sender, EventArgs e)
        {
            string profilePath = RoamiePlugin.Singleton.RoamingContext.ProfilePath;

            // Previously roamed db selected...
            if (profilePath.EndsWith(DatabaseProvider.RoamingExtension) && UseLocalRBTN.Enabled)
            {
                UseLocalRBTN.Checked = true;
                RoamLocalOnExitCHBOX.Checked = true;
            }

            Text += Path.GetFileNameWithoutExtension(profilePath);
            Activate();
        }

        private void OkBTN_Click(object sender, EventArgs e)
        {
            if (ProcessSelection(null))
                Close();
        }

        private void OptionsLINK_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ConfigurationDialog.Present(true, RoamiePlugin.Singleton);
        }

        private void RadioBtn_Checked(object sender, EventArgs e)
        {
            RadioButton btn = (RadioButton)sender;

            string name = btn.Name;
            bool firstGroup = false, secondGroup = false, thirdGroup = false;

            if (name == DownloadExistingRBTN.Name)
                firstGroup = true;
            else if (name == UseLocalRBTN.Name)
                secondGroup = true;
            else if (name == CreateNewRBTN.Name)
                thirdGroup = true;

            PublicComputerCHBOX.Enabled = firstGroup;
            SandboxModeCHBOX.Enabled = firstGroup;

            RoamLocalOnExitCHBOX.Enabled = secondGroup;
            RoamNewOnExitCHBOX.Enabled = thirdGroup;

            OkBTN.Enabled = true;
            OkBTN.Focus();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            RoamingContext context = RoamiePlugin.Singleton.RoamingContext;

            if (context.IsInState(RoamingState.Disabled) || context.IsInState(RoamingState.SyncErrorOccured))
            {
                context.DeactivateProfile();
                context.RestoreProfilePath();

                if (FirstTime)
                {
                    CreateNewRBTN.Checked = true;
                    RoamNewOnExitCHBOX.Checked = false;
                }
                else
                {
                    UseLocalRBTN.Checked = true;
                    RoamLocalOnExitCHBOX.Checked = false;
                }

                ProcessSelection(null);
            }

            Settings.Default.Save();
            base.OnFormClosing(e);
        }

        #endregion
    }
}