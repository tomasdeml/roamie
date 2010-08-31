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
using System.Text;
using Virtuoso.Miranda.Roamie.Roaming.Profiles;
using Virtuoso.Miranda.Roamie.Forms;
using System.Net;
using System.IO;
using Virtuoso.Miranda.Roamie.Properties;
using Virtuoso.Miranda.Roamie.Roaming;
using System.Diagnostics;
using Virtuoso.Miranda.Plugins.Forms;
using System.Security.Cryptography;

namespace Virtuoso.Miranda.Roamie.Roaming.Providers
{
    internal partial class HttpProvider : PackingDatabaseProvider, IDeltaAwareProvider, ISiteAdapter
    {
        #region Fields

        private const string TraceCategory = "HttpProvider";

        #endregion

        #region .ctors

        public HttpProvider() { }

        #endregion

        #region Properties

        public override string Name
        {
            get { return "Http Download-only"; }
        }

        public override bool CredentialsRequired
        {
            get { return false; }
        }

        public override ISiteAdapter Adapter
        {
            get { return this; }
        }

        #endregion

        #region Methods

        public override void OnSelected()
        {
            Context.State |= RoamingState.MirroringNotSupported;
            Context.State |= RoamingState.DiscardLocalChanges;

            if ((Context.State & RoamingState.WipeRoamedDbOnExit) == RoamingState.WipeRoamedDbOnExit)
                InformationDialog.PresentModal(Resources.Information_Caption_YourChangesWiilBeLost, Resources.Information_Formatable1_Text_YourChangesWillBeLost, Resources.Image_32x32_Profile);
        }

        public override void TestSync(RoamingProfile profile)
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

        public override void NonSyncShutdown()
        {
            if ((Context.State & RoamingState.WipeRoamedDbOnExit) != RoamingState.WipeRoamedDbOnExit ||
                (Context.State & RoamingState.DiscardLocalChanges) != RoamingState.DiscardLocalChanges)
                InformationDialog.PresentModal(Resources.Information_Caption_CannotMirrorChanges, String.Format(Resources.Information_Formatable1_Text_CannotMirrorChanges, Path.GetFileName(Context.ProfilePath)), Resources.Image_32x32_Web);

            RemoveLocalDb();

            // Base impl must not be called, http provider cannot upload files...
        }

        public override void SyncRemoteDatabase(RoamingProfile profile)
        {
            // Base impl must not be called, http provider cannot upload files...
        }

        public override void SyncLocalDatabase(RoamingProfile profile)
        {
            try
            {
                Trace.WriteLineIf(RoamiePlugin.TraceSwitch.TraceVerbose, "Synchronizing local database...", TraceCategory);
                HttpWebRequest request = CreateWebRequest(profile);

                if (!String.IsNullOrEmpty(profile.UserName))
                    request.Credentials = new NetworkCredential(profile.UserName, profile.Password);

                InitializeSafeProfilePath();

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (Stream remoteStream = response.GetResponseStream(),
                        downloadedStream = new MemoryStream(response.ContentLength < 0 ? 2048 : (int)response.ContentLength),
                        unprotectedStream = new MemoryStream((int)downloadedStream.Length * 2))
                    {
                        GlobalEvents.ChangeProgress(Resources.Text_UI_LogText_DownloadingDb, GlobalEvents.SignificantProgress.Running);
                        Streaming.CopyStream(new Streaming.CustomizableStream(remoteStream, ((MemoryStream)downloadedStream).Capacity), downloadedStream, delegate(int _progress) { GlobalEvents.ChangeProgress(null, _progress); });

                        GlobalEvents.ChangeProgress(Resources.Text_UI_LogText_CompressingEncrypting, GlobalEvents.SignificantProgress.Running);
                        downloadedStream.Seek(0, SeekOrigin.Begin);

                        SecureStreamCompactor.DecryptAndDecompress(downloadedStream, unprotectedStream, profile.DatabasePassword);
                        unprotectedStream.Seek(0, SeekOrigin.Begin);

                        GlobalEvents.ChangeProgress(Resources.Text_UI_LogText_Saving, GlobalEvents.SignificantProgress.Running);
                        using (FileStream localStream = new FileStream(Context.ProfilePath, FileMode.Create))
                            Streaming.CopyStream(unprotectedStream, localStream);
                    }
                }

                // Sync attached files
                base.SyncLocalDatabase(profile);

                Trace.WriteLineIf(RoamiePlugin.TraceSwitch.TraceInfo, "Local database synchronized.", TraceCategory);
                GlobalEvents.ChangeProgress(Resources.Text_UI_LogText_Completed, GlobalEvents.SignificantProgress.Complete);
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

        private static HttpWebRequest CreateWebRequest(RoamingProfile profile)
        {
            return CreateWebRequest(profile, new Uri(profile.RemoteHost));
        }

        private static HttpWebRequest CreateWebRequest(RoamingProfile profile, Uri remoteUri)
        {
            HttpWebRequest request = WebRequest.Create(remoteUri) as HttpWebRequest;

            if (request == null)
                throw new FormatException(Resources.ExceptionMsg_RemoteUriNotSupported);

            return request;
        }

        #endregion

        #region ISiteAdapter

        bool ISiteAdapter.FileExists(RoamingProfile profile, string path)
        {
            HttpWebRequest req = CreateWebRequest(profile, new Uri(path));

            try
            {
                using (HttpWebResponse resp = (HttpWebResponse)req.GetResponse())
                    return resp.StatusCode == HttpStatusCode.OK;
            }
            catch
            {
                return false;
            }
        }

        Stream ISiteAdapter.PullFile(RoamingProfile profile, string path)
        {
            HttpWebRequest req = CreateWebRequest(profile, new Uri(path));
            return req.GetResponse().GetResponseStream();
        }

        void ISiteAdapter.PushFile(RoamingProfile profile, string path, Stream sourceStream)
        {
            throw new NotSupportedException();
        }

        bool ISiteAdapter.DeleteFile(RoamingProfile profile, string path)
        {
            throw new NotSupportedException();
        }

        #endregion
    }
}
