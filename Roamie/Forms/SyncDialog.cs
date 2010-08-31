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

    #region Enums

    [Flags]
    public enum SyncOptions
    {
        Repeatable = 1,
        Unrepeatable = 2,
        IgnoreErrors = 4,
        NoThrow = 8,
        SilenceEligible = 16,
    }

    #endregion

    public sealed partial class SyncDialog : Form
    {
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

            if ((options & SyncOptions.SilenceEligible) == SyncOptions.SilenceEligible && 
                !RoamiePlugin.Singleton.RoamingContext.Configuration.SilentStartup)
                options &= ~SyncOptions.SilenceEligible;

            SyncOperator = syncOperator;
            Options = options;

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
                    dlg.ToggleVisibility(false);

                dlg.ShowDialog();

                if (dlg.Error != null && (options & SyncOptions.NoThrow) != SyncOptions.NoThrow)
                    throw dlg.Error;
                
                return dlg.Result;
            }
        }

        // TODO Into separate class!
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
            catch (Exception)
            { }
        }

        #endregion

        #region UI Handlers

        private void SyncDialog_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Escape || e.KeyCode == Keys.Enter) && (TryAgainBTN.Visible || ControlBox))
                TryAgainBTN.PerformClick();
        }

        private void TryAgainBTN_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SilentStatusIcon_MouseClick(object sender, MouseEventArgs e)
        {
            ToggleVisibility(true);
        }

        private void ToggleVisibility(bool visible)
        {
            Opacity = visible ? 1D : 0;
            SilentStatusIcon.Visible = !visible;
        }

        #endregion

        #region Worker handlers

        private void SyncDialog_Shown(object sender, EventArgs e)
        {
            EnableControls(false);

            ProgressMediator.ProgressChange += DisplayProgressChange;
            Worker.RunWorkerAsync();
        }

        private void DisplayProgressChange(string message, int progress)
        {
            Worker.ReportProgress(progress, message);
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            DisplayProgressChange(Resources.Text_UI_LogText_Initializing, (int)SignificantProgress.Running);
            e.Result = SyncOperator();
        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            switch ((SignificantProgress)e.ProgressPercentage)
            {
                case SignificantProgress.Running:
                    ProgressPBAR.Style = ProgressBarStyle.Marquee;
                    break;
                case SignificantProgress.NoChange:
                    break;
                case SignificantProgress.Stopped:
                    ProgressPBAR.Style = ProgressBarStyle.Continuous;
                    ProgressPBAR.Value = ProgressPBAR.Minimum;
                    break;
                case SignificantProgress.Complete:
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

            if (e.UserState == null) 
                return;

            LogBuilder.Append((string)e.UserState + Environment.NewLine);

            LogTBOX.Clear();
            LogTBOX.AppendText(LogBuilder.ToString());

            LogTBOX.ScrollToCaret();
            Trace.WriteLineIf(RoamiePlugin.TraceSwitch.TraceVerbose, e.UserState, RoamiePlugin.TraceCategory);
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ProgressMediator.ProgressChange -= DisplayProgressChange;

            if (e.Error == null)
            {
                Result = e.Result;
                Error = null;

                Close();
            }
            else if ((Options & SyncOptions.IgnoreErrors) != SyncOptions.IgnoreErrors)
            {
                Error = e.Error;
                Trace.WriteLineIf(RoamiePlugin.TraceSwitch.TraceError, StringUtility.FormatExceptionMessage("Sync error: ", e.Error), RoamiePlugin.TraceCategory);

                LogBuilder = new StringBuilder(50);
                Worker_ProgressChanged(this, new ProgressChangedEventArgs((int)SignificantProgress.Complete, String.Format("{0}{1}{2}", Resources.Text_UI_LogText_SyncFailed, Environment.NewLine, e.Error.Message)));

                SystemSounds.Hand.Play();
                ToggleVisibility(true);

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