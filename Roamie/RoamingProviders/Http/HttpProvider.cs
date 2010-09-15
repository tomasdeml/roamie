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
    internal class HttpProvider : PackingDatabaseProvider, IDeltaAwareProvider
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
                InformationDialog.PresentModal(Resources.Information_Caption_YourChangesWillBeLost, Resources.Information_Formatable1_Text_YourChangesWillBeLost, Resources.Image_32x32_Profile);
        }

        public override void VerifyProfile(RoamingProfile profile)
        {
            try
            {
                HttpWebRequest request = WebRequest.Create(profile.RemoteHost) as HttpWebRequest;

                if (request == null)
                    throw new FormatException(Resources.ExceptionMsg_RemoteUriNotSupported);

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
       
        public override void SyncLocalDatabase(RoamingProfile profile)
        {
            try
            {
                Trace.WriteLineIf(RoamiePlugin.TraceSwitch.TraceVerbose, "Synchronizing local database...", TraceCategory);
                HttpWebRequest request = HttpRequestFactory.CreateWebRequest(profile);

                if (!String.IsNullOrEmpty(profile.UserName))
                    request.Credentials = new NetworkCredential(profile.UserName, profile.Password);

                InitializeSafeProfilePath();

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (Stream remoteStream = response.GetResponseStream(),
                                  downloadedStream = new MemoryStream(response.ContentLength < 0 ? 2048 : (int)response.ContentLength),
                                  unprotectedStream = new MemoryStream((int)downloadedStream.Length * 2))
                    {
                        ProgressMediator.ChangeProgress(Resources.Text_UI_LogText_DownloadingDb, SignificantProgress.Running);
                        StreamUtility.CopyStream(new UndisposableStream(remoteStream, ((MemoryStream)downloadedStream).Capacity), downloadedStream, delegate(int _progress) { ProgressMediator.ChangeProgress(null, _progress); });

                        ProgressMediator.ChangeProgress(Resources.Text_UI_LogText_CompressingEncrypting, SignificantProgress.Running);
                        downloadedStream.Seek(0, SeekOrigin.Begin);

                        StreamUtility.DecryptAndDecompress(downloadedStream, unprotectedStream, profile.DatabasePassword);
                        unprotectedStream.Seek(0, SeekOrigin.Begin);

                        ProgressMediator.ChangeProgress(Resources.Text_UI_LogText_Saving, SignificantProgress.Running);
                        
                        using (FileStream localStream = new FileStream(Context.ProfilePath, FileMode.Create))
                            StreamUtility.CopyStream(unprotectedStream, localStream);
                    }
                }

                // Sync attached files
                base.SyncLocalDatabase(profile);

                Trace.WriteLineIf(RoamiePlugin.TraceSwitch.TraceInfo, "Local database synchronized.", TraceCategory);
                ProgressMediator.ChangeProgress(Resources.Text_UI_LogText_Completed, SignificantProgress.Complete);
            }
            catch (CryptographicException cE)
            {
                throw new SyncException(String.Format(Resources.ExceptionMsg_Formatable1_CryptoError, cE.Message), cE);
            }
            catch (WebException wE)
            {
                throw new SyncException(String.Format(Resources.ExceptionMsg_Formatable2_DownloadError, profile.RemoteHost, wE.Message), wE);
            }
            catch (Exception e)
            {
                throw new SyncException(String.Format(Resources.ExceptionMsg_Formatable1_GeneralDownloadError, e.Message), e);
            }
        }

        public override void SyncRemoteDatabase(RoamingProfile profile)
        {
            // Base impl must not be called, http provider cannot upload files...
        }

        public override void NonSyncShutdown()
        {
            if ((Context.State & RoamingState.WipeLocalDbOnExit) != RoamingState.WipeLocalDbOnExit ||
                (Context.State & RoamingState.DiscardLocalChanges) != RoamingState.DiscardLocalChanges)
                InformationDialog.PresentModal(Resources.Information_Caption_CannotMirrorChanges, String.Format(Resources.Information_Formatable1_Text_CannotMirrorChanges, Path.GetFileName(Context.ProfilePath)), Resources.Image_32x32_Web);

            RemoveLocalDb();

            // Base impl must not be called, http provider cannot upload files...
        }

        #endregion
    }
}