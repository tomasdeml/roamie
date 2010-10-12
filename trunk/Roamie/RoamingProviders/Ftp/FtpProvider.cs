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
using System.Diagnostics;
using System.Net;
using Virtuoso.Miranda.Plugins.Forms;
using Virtuoso.Roamie.Properties;
using Virtuoso.Roamie.Roaming.Profiles;

namespace Virtuoso.Roamie.RoamingProviders.Ftp
{
    internal class FtpProvider : Provider
    {
        #region Fields

        public const string TraceCategory = "FtpProvider";

        #endregion

        #region .ctors

        public FtpProvider()
        {
            adapter = new FtpSiteAdapter();
            Trace.WriteLineIf(RoamiePlugin.TraceSwitch.TraceVerbose, "Ftp Provider loaded.", TraceCategory);
        }

        #endregion

        #region Properties

        public override string Name
        {
            get { return "FTP"; }
        }

        public override bool CredentialsRequired
        {
            get { return true; }
        }

        private readonly ISiteAdapter adapter;
        public override ISiteAdapter Adapter
        {
            get { return adapter; }
        }

        #endregion

        #region Methods

        public override void OnSelected()
        {
            if (Context.Configuration.UseProxy)
                InformationDialog.PresentModal(Resources.Information_Caption_ProxyNotSupported, String.Format(Resources.Information_Formatable1_Text_ProxyNotSupported, Environment.NewLine), Resources.Image_32x32_Web);
        }

        public override void VerifyProfile(RoamingProfile profile)
        {
            try
            {
                Trace.WriteLineIf(RoamiePlugin.TraceSwitch.TraceVerbose, "Testing roaming profile...", TraceCategory);
                FtpWebRequest ftpRequest = FtpRequestFactory.CreateTestRequest(profile);

                using (FtpWebResponse ftpResponse = (FtpWebResponse)ftpRequest.GetResponse())
                {
                    Trace.WriteLineIf(RoamiePlugin.TraceSwitch.TraceVerbose, String.Format("Ftp request response is: '{0}' ({1}).", ftpResponse.StatusCode, ftpResponse.StatusDescription), TraceCategory);

                    if (ftpResponse.StatusCode != FtpStatusCode.PathnameCreated)
                    {
                        Trace.WriteLineIf(RoamiePlugin.TraceSwitch.TraceError, String.Format("Unexpected ftp request response. Expected '{0}', but got '{1}'. Check profile settings. Throwing...", FtpStatusCode.PathnameCreated.ToString(), ftpResponse.StatusCode), TraceCategory);
                        throw new SyncException(Resources.ExceptionMsg_SyncTestFailed);
                    }
                    
                    Trace.WriteLineIf(RoamiePlugin.TraceSwitch.TraceVerbose, "Sync test successful.", TraceCategory);
                }
            }
            catch (SyncException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new SyncException(e.Message, e);
            }
        }

        #endregion
    }
}
