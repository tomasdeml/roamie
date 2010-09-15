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
using System.Diagnostics;
using System.Reflection;
using Virtuoso.Miranda.Plugins.Infrastructure;
using Virtuoso.Miranda.Plugins.Configuration.Forms.Controls;
using Virtuoso.Roamie.Properties;

namespace Virtuoso.Roamie.Forms.Controls
{
    public partial class AboutInformation : CategoryItemControl
    {
        #region .ctors

        private AboutInformation()
        {
            InitializeComponent();
        }

        #endregion

        #region UI Handlers

        protected override bool OnShow(bool firstTime)
        {
            if (firstTime)
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                string newLine = Environment.NewLine;

                VersionLABEL.Text = ((AssemblyCopyrightAttribute)
                     assembly.GetCustomAttributes(typeof (AssemblyCopyrightAttribute), false)[0]).Copyright;
                VersionLABEL.Text += newLine + String.Format("v{0}", assembly.GetName().Version);

                HyphenVersionLABEL.Text = ((AssemblyCopyrightAttribute)
                     typeof (MirandaEnvironment).Assembly.GetCustomAttributes(typeof (AssemblyCopyrightAttribute), false)[0]).Copyright;
                HyphenVersionLABEL.Text += newLine + String.Format("v{0}", MirandaEnvironment.HyphenVersion);
                FxVersionLABEL.Text += newLine + String.Format("v{0}", Environment.Version);
            }

            return false;
        }

        private void VirtuosoLINK_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Process mailProcess = new Process();

                mailProcess.StartInfo.UseShellExecute = true;
                mailProcess.StartInfo.FileName = "mailto:deml.tomas@seznam.cz";
                mailProcess.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Resources.MsgBox_Text_CannotOpenEmailClient + ex.Message, Resources.MsgBox_Title_CannotOpenEmailClient, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }       

        private void HomepageLBTN_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Process mailProcess = new Process();

                mailProcess.StartInfo.UseShellExecute = true;
                mailProcess.StartInfo.FileName = RoamiePlugin.Singleton.HomePage.ToString();
                mailProcess.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Resources.MsgBox_Text_CannotOpenEmailClient + ex.Message, Resources.MsgBox_Title_CannotOpenEmailClient, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion
    }
}

