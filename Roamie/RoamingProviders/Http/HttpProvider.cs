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
using System.Net;
using System.IO;
using System.Diagnostics;
using Virtuoso.Miranda.Plugins.Forms;
using System.Security.Cryptography;
using Virtuoso.Roamie.Properties;
using Virtuoso.Roamie.Roaming;
using Virtuoso.Roamie.Roaming.Profiles;

namespace Virtuoso.Roamie.RoamingProviders.Http
{
    internal class HttpProvider : DatabaseProvider, IDeltaAwareProvider
    {
        #region Fields

        private const string TraceCategory = "HttpProvider";

        #endregion

        #region .ctors

        public HttpProvider()
        {
            adapter = new HttpSiteAdapter();
        }

        #endregion

        #region Properties

        public override string Name
        {
            get { return "HTTP (read-only)"; }
        }

        public override bool CredentialsRequired
        {
            get { return false; }
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
            Context.State |= RoamingState.MirroringNotSupported;
            Context.State |= RoamingState.DiscardLocalChanges;

            if ((Context.State & RoamingState.WipeLocalDbOnExit) == RoamingState.WipeLocalDbOnExit)
                InformationDialog.PresentModal(Resources.Information_Caption_YourChangesWiilBeLost, Resources.Information_Formatable1_Text_YourChangesWillBeLost, Resources.Image_32x32_Profile);
        }

        public override void VerifyProfile(RoamingProfile profile)
        {
            try
            {
                HttpWebRequest request = HttpRequestFactory.CreateWebRequest(profile);

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    if (response.StatusCode != HttpStatusCode.OK || response.ContentType.ToLowerInvariant().StartsWith("text/htm"))
                        throw new SyncException(Resources.ExceptionMsg_SyncTestFailed_NotFound);
                }
            }
            catch (SyncException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new SyncException(Resources.ExceptionMsg_SyncTestFailed, e);
            }
        }

        protected override void PerformRemoteSiteSync(RoamingProfile profile)
        {
            // Base impl must not be called, http provider cannot upload files...
        }

        public override void NonSyncShutdown()
        {
            if ((Context.State & RoamingState.WipeLocalDbOnExit) != RoamingState.WipeLocalDbOnExit ||
                (Context.State & RoamingState.DiscardLocalChanges) != RoamingState.DiscardLocalChanges)
                InformationDialog.PresentModal(Resources.Information_Caption_CannotMirrorChanges, String.Format(Resources.Information_Formatable1_Text_CannotMirrorChanges, Path.GetFileName(Context.ProfilePath)), Resources.Image_32x32_Web);

            // Base impl must not be called, http provider cannot upload files...
        }

        #endregion
    }
}