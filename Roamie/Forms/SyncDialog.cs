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
using System.Media;
using Virtuoso.Miranda.Roamie.Roaming;
using Virtuoso.Miranda.Roamie.Properties;
using System.Diagnostics;
using Virtuoso.Miranda.Roamie.Roaming.Profiles;

namespace Virtuoso.Miranda.Roamie.Forms
{
    #region Delegates

    public delegate object SyncOperator();

    #endregion

    public sealed partial class SyncDialog : Form
    {
        #region Enums

        [Flags]
        public enum SyncOptions : byte
        {
            Repeatable = 1,
            Unrepeatable = 2,
            IgnoreErrors = 4,
            NoThrow = 8,
            SilenceEligible = 16,
        }

        #endregion

        #region Fields

        private delegate void InvokeDelegate(object state);

        private readonly SyncOperator SyncOperator;
        private StringBuilder LogBuilder = new StringBuilder(50);

        private volatile SyncOptions Options;

        private Exception Error;
        private object Result;

        #endregion

        #region .ctors

        private SyncDialog(SyncOperator syncOperator, SyncOptions options)
        {
            if (syncOperator == null) 
                throw new ArgumentNullException("syncOperator");

            if ((options & SyncOptions.SilenceEligible) == SyncOptions.SilenceEligible && !RoamiePlugin.Singleton.RoamingContext.Configuration.SilentStartup)
                options &= ~SyncOptions.SilenceEligible;

            this.SyncOperator = syncOperator;
            this.Options = options;

            InitializeComponent();
        }

        public static object RunModal(SyncOperator syncOperator)
        {
            return RunModal(syncOperator, SyncOptions.Unrepeatable);
        }

        internal static object RunModal(SyncOperator syncOperator, SyncOptions options)
        {
            using (SyncDialog dlg = new SyncDialog(syncOperator, options))
            {
                if ((dlg.Options & SyncOptions.SilenceEligible) == SyncOptions.SilenceEligible)
                    dlg.ShowHide(false);

                dlg.ShowDialog();

                if ((options & SyncOptions.NoThrow) != SyncOptions.NoThrow && dlg.Error != null)
                    throw dlg.Error;
                else
                    return dlg.Result;
            }
        }

        public static void TestModal(RoamingProfile profile)
        {
            if (profile == null)
                throw new ArgumentNullException("profile");

            try
            {
                RunModal(delegate
                {
                    profile.GetProvider().TestSync(profile);
                    return null;
                });

                MessageBox.Show(Resources.Text_UI_LogText_Completed, Resources.Text_UI_LogText_Completed, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch { }
        }

        #endregion

        #region UI Handlers

        private void SyncDialog_KeyDown(object sender, KeyEventArgs e)
        {
            if ((TryAgainBTN.Visible || ControlBox) && (e.KeyCode == Keys.Escape || e.KeyCode == Keys.Enter))
                TryAgainBTN.PerformClick();
        }

        private void TryAgainBTN_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SilentStatusIcon_MouseClick(object sender, MouseEventArgs e)
        {
            ShowHide(true);
        }

        private void ShowHide(bool show)
        {
            if (show)
                Opacity = 1D;
            else
                Opacity = 0;

            SilentStatusIcon.Visible = !show;
        }

        #endregion

        #region Worker handlers

        private void SyncDialog_Shown(object sender, EventArgs e)
        {
            EnableControls(false);

            GlobalEvents.ProgressChange += DisplayProgressChange;
            Worker.RunWorkerAsync();
        }

        private void DisplayProgressChange(string message, int progress)
        {
            Worker.ReportProgress(progress, message);
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            DisplayProgressChange(Resources.Text_UI_LogText_Initializing, (int)GlobalEvents.SignificantProgress.Running);
            e.Result = SyncOperator();
        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            switch ((GlobalEvents.SignificantProgress)e.ProgressPercentage)
            {
                case GlobalEvents.SignificantProgress.Running:
                    ProgressPBAR.Style = ProgressBarStyle.Marquee;
                    break;
                case GlobalEvents.SignificantProgress.NoChange:
                    break;
                case GlobalEvents.SignificantProgress.Stopped:
                    ProgressPBAR.Style = ProgressBarStyle.Continuous;
                    ProgressPBAR.Value = ProgressPBAR.Minimum;
                    break;
                case GlobalEvents.SignificantProgress.Complete:
                    ProgressPBAR.Style = ProgressBarStyle.Continuous;
                    ProgressPBAR.Value = ProgressPBAR.Maximum;
                    break;
                default:
                    if (e.ProgressPercentage < ProgressPBAR.Minimum || e.ProgressPercentage > ProgressPBAR.Maximum)
                    {
                        Debug.Fail("Invalid percentage.");
                        return;
                    }

                    ProgressPBAR.Style = ProgressBarStyle.Continuous;
                    ProgressPBAR.Value = e.ProgressPercentage;

                    break;
            }

            if (e.UserState != null)
            {
                LogBuilder.Append((string)e.UserState + Environment.NewLine);

                LogTBOX.Clear();
                LogTBOX.AppendText(LogBuilder.ToString());

                LogTBOX.ScrollToCaret();
                Trace.WriteLineIf(RoamiePlugin.TraceSwitch.TraceVerbose, e.UserState, RoamiePlugin.TraceCategory);
            }
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            GlobalEvents.ProgressChange -= DisplayProgressChange;

            if (e.Error == null)
            {
                Result = e.Result;
                Error = null;

                Close();
            }
            else if ((Options & SyncOptions.IgnoreErrors) != SyncOptions.IgnoreErrors)
            {
                Error = e.Error;
                Trace.WriteLineIf(RoamiePlugin.TraceSwitch.TraceError, GlobalEvents.FormatExceptionMessage("Sync error: ", e.Error), RoamiePlugin.TraceCategory);

                LogBuilder = new StringBuilder(50);
                Worker_ProgressChanged(this, new ProgressChangedEventArgs((int)GlobalEvents.SignificantProgress.Complete, String.Format("{0}{1}{2}", Resources.Text_UI_LogText_SyncFailed, Environment.NewLine, e.Error.Message)));

                SystemSounds.Hand.Play();
                ShowHide(true);

                if (InvokeRequired)
                    Invoke(new InvokeDelegate(EnableControls), true);
                else
                    EnableControls(true);
            }
        }

        private void EnableControls(object state)
        {
            bool value = (bool)state;

            ControlBox = value && (Options & SyncOptions.Unrepeatable) == SyncOptions.Unrepeatable;
            TryAgainBTN.Visible = value && (Options & SyncOptions.Repeatable) == SyncOptions.Repeatable;

            LogTBOX.ScrollBars = value ? ScrollBars.Vertical : ScrollBars.None;
        }

        #endregion      
    }
}