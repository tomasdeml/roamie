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
using System.Net;
using System.IO;
using System.Diagnostics;
using Virtuoso.Miranda.Roamie.Roaming;
using Virtuoso.Miranda.Roamie.Forms;
using Virtuoso.Miranda.Roamie.Roaming.Profiles;
using Virtuoso.Miranda.Roamie.Properties;
using System.Security.Cryptography;
using Virtuoso.Miranda.Roamie.Roaming.DeltaSync;
using Virtuoso.Miranda.Plugins.Infrastructure;
using Virtuoso.Miranda.Plugins.Forms;

namespace Virtuoso.Miranda.Roamie.Roaming.Providers
{
    internal class FtpProvider : PackingDatabaseProvider, IDeltaAwareProvider, ISiteAdapter
    {
        #region Fields

        private const string TraceCategory = "FtpProvider";

        #endregion

        #region .ctors

        public FtpProvider()
        {
            Trace.WriteLineIf(RoamiePlugin.TraceSwitch.TraceVerbose, "Ftp Provider loaded.", TraceCategory);
        }

        #endregion

        #region Properties

        public override string Name
        {
            get { return "File Transfer Protocol"; }
        }

        public override bool CredentialsRequired
        {
            get { return true; }
        }

        public override ISiteAdapter Adapter
        {
            get { return this; }
        }

        #endregion

        #region Methods

        public override void OnSelected()
        {
            if (Context.Configuration.UseProxy)
                InformationDialog.PresentModal(Resources.Information_Caption_ProxyNotSupported, String.Format(Resources.Information_Formatable1_Text_ProxyNotSupported, Environment.NewLine), Resources.Image_32x32_Web);
        }

        #endregion

        #region Sync Methods

        #region Request factories

        protected static FtpWebRequest CreateRequest(string method, RoamingProfile profile)
        {
            return CreateRequest(method, profile, new Uri(profile.RemoteHost));
        }

        protected static FtpWebRequest CreateRequest(string method, RoamingProfile profile, Uri remoteAddress)
        {
            FtpWebRequest ftpRequest = WebRequest.Create(remoteAddress) as FtpWebRequest;

            if (ftpRequest == null)
                throw new FormatException(Resources.ExceptionMsg_RemoteUriNotSupported);

            ftpRequest.Credentials = new NetworkCredential(profile.UserName, profile.Password);
            ftpRequest.UseBinary = true;
            ftpRequest.Proxy = null;
            ftpRequest.Method = method;
            ftpRequest.UsePassive = true;
            ftpRequest.KeepAlive = false;
            ftpRequest.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.NoCacheNoStore);

            Trace.WriteLineIf(RoamiePlugin.TraceSwitch.TraceVerbose, "Ftp request created: " + String.Format("Url = '{0}', Method = '{1}', UsePassive = '{2}', UserName = '{3}', CachePolicy = '{4}'.", remoteAddress, method, ftpRequest.UsePassive.ToString(), profile.UserName, ftpRequest.CachePolicy.ToString()), TraceCategory);
            return ftpRequest;
        }

        protected static FtpWebRequest CloneRequest(string newMethod, FtpWebRequest request)
        {
            if (String.IsNullOrEmpty(newMethod))
                throw new ArgumentNullException("newMethod");

            if (request == null)
                throw new ArgumentNullException("request");

            FtpWebRequest ftpRequest = WebRequest.Create(request.RequestUri) as FtpWebRequest;

            if (ftpRequest == null)
                throw new FormatException(Resources.ExceptionMsg_RemoteUriNotSupported);

            ftpRequest.Credentials = request.Credentials;
            ftpRequest.UseBinary = request.UseBinary;
            ftpRequest.Proxy = request.Proxy;
            ftpRequest.Method = newMethod;
            ftpRequest.UsePassive = request.UsePassive;
            ftpRequest.CachePolicy = request.CachePolicy;

            return ftpRequest;
        }

        protected static FtpWebRequest CreateTestRequest(RoamingProfile profile)
        {
            RoamingProfile testProfile = new RoamingProfile(profile.Name, profile.Description, new Uri(profile.RemoteHost).GetComponents(UriComponents.SchemeAndServer, UriFormat.SafeUnescaped), profile.UserName, profile.Password, profile.DatabasePassword, profile.RoamingProvider);        
            return CreateRequest(WebRequestMethods.Ftp.PrintWorkingDirectory, testProfile);
        }

        protected static int? GetFileSize(FtpWebRequest ftpFileRequest)
        {
            try
            {
                FtpWebRequest ftpSizeRequest = CloneRequest(WebRequestMethods.Ftp.GetFileSize, ftpFileRequest);

                using (FtpWebResponse ftpSizeResponse = (FtpWebResponse)ftpSizeRequest.GetResponse())
                    return (int)ftpSizeResponse.ContentLength;
            }
            catch (Exception e)
            {
                Trace.WriteLineIf(RoamiePlugin.TraceSwitch.TraceWarning, StringUtility.FormatExceptionMessage("The target FTP server does not support the SIZE command, progress notifications will not be available.", e));
                return null;
            }
        }

        #endregion

        public override void TestSync(RoamingProfile profile)
        {
            try
            {
                Trace.WriteLineIf(RoamiePlugin.TraceSwitch.TraceVerbose, "Testing roaming profile...", TraceCategory);
                FtpWebRequest ftpRequest = CreateTestRequest(profile);

                using (FtpWebResponse ftpResponse = (FtpWebResponse)ftpRequest.GetResponse())
                {
                    Trace.WriteLineIf(RoamiePlugin.TraceSwitch.TraceVerbose, String.Format("Ftp request response is: '{0}' ({1}).", ftpResponse.StatusCode, ftpResponse.StatusDescription), TraceCategory);

                    if (ftpResponse.StatusCode != FtpStatusCode.PathnameCreated)
                    {
                        Trace.WriteLineIf(RoamiePlugin.TraceSwitch.TraceError, String.Format("Unexpected ftp request response. Expected '{0}', but got '{1}'. Check profile settings. Throwing...", FtpStatusCode.PathnameCreated.ToString(), ftpResponse.StatusCode), TraceCategory);
                        throw new SyncException(Resources.ExceptionMsg_SyncTestFailed);
                    }
                    else
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

        public override void SyncLocalDatabase(RoamingProfile profile)
        {
            try
            {               
                Trace.WriteLineIf(RoamiePlugin.TraceSwitch.TraceVerbose, "Synchronizing local database...", TraceCategory);
                InitializeSafeProfilePath();

                FtpWebRequest ftpFileRequest = CreateRequest(WebRequestMethods.Ftp.DownloadFile, profile);                              

                using (FtpWebResponse ftpFileResponse = (FtpWebResponse)ftpFileRequest.GetResponse())
                {
                    int? fileSize = GetFileSize(ftpFileRequest);

                    using (Stream remoteStream = ftpFileResponse.GetResponseStream(),
                        downloadedStream = new MemoryStream(fileSize == null ? 10240 : fileSize.Value), unprotectedStream = new MemoryStream((int)downloadedStream.Length * 2))
                    {
                        if (fileSize != null && fileSize.Value <= 0)
                        {
                            Trace.WriteLineIf(RoamiePlugin.TraceSwitch.TraceError, "The FTP SIZE response says the remote file length is 0 bytes. This indicates the database is corrupted. Aborting synchronization.", TraceCategory);
                            throw new SyncException(Resources.ExceptionMsg_InvalidRemoteDatabase);
                        }

                        ProgressMediator.ChangeProgress(Resources.Text_UI_LogText_DownloadingDb, SignificantProgress.Running);
                        
                        // Show percentage progress ONLY if we have successfully obtained the file size, otherwise show marquee
                        StreamUtility.CopyStream(new UndisposableStream(remoteStream, ((MemoryStream)downloadedStream).Capacity), downloadedStream, 
                            fileSize != null ? delegate(int _progress) { ProgressMediator.ChangeProgress(null, _progress); } : (StreamUtility.ProgressCallback)null);

                        ProgressMediator.ChangeProgress(Resources.Text_UI_LogText_DecryptingDecompressing, SignificantProgress.Running);
                        downloadedStream.Seek(0, SeekOrigin.Begin);
                        StreamUtility.DecryptAndDecompress(downloadedStream, unprotectedStream, profile.DatabasePassword);

                        ProgressMediator.ChangeProgress(Resources.Text_UI_LogText_Saving);
                        unprotectedStream.Seek(0, SeekOrigin.Begin);

                        using (Stream localStream = new FileStream(Context.ProfilePath, FileMode.Create))
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
            try
            {
                Trace.WriteLineIf(RoamiePlugin.TraceSwitch.TraceVerbose, "Synchronizing remote database...", TraceCategory);

                if ((Context.State & RoamingState.DiscardLocalChanges) == RoamingState.DiscardLocalChanges)
                {
                    Trace.WriteLineIf(RoamiePlugin.TraceSwitch.TraceInfo, "Sandbox mode is active, no synchronization required.", TraceCategory);
                    return;
                }

                Uri remoteUri = new Uri(profile.RemoteHost);
                Uri tempRemoteUri = new Uri(profile.RemoteHost + ".tmp");

                FtpWebRequest ftpRequest = CreateRequest(WebRequestMethods.Ftp.UploadFile, profile, tempRemoteUri);

                using (Stream localStream = new FileStream(Context.ProfilePath, FileMode.Open),                    
                    protectedStream = new MemoryStream((int)localStream.Length / 2),
                    remoteStream = ftpRequest.GetRequestStream())
                {
                    ProgressMediator.ChangeProgress(Resources.Text_UI_LogText_CompressingEncrypting, SignificantProgress.Running);
                    StreamUtility.CompressAndEncrypt(localStream, protectedStream, profile.DatabasePassword);

                    ProgressMediator.ChangeProgress(Resources.Text_UI_LogText_Uploading, SignificantProgress.Stopped);
                    protectedStream.Seek(0, SeekOrigin.Begin);
                    StreamUtility.CopyStream(protectedStream, remoteStream, delegate(int _progress) { ProgressMediator.ChangeProgress(null, _progress); });              
                }

                GetAndVerifyFtpResponse(ftpRequest, FtpStatusCode.ClosingData);
                ProgressMediator.ChangeProgress(Resources.Text_UI_LogText_Finishing, SignificantProgress.Running);

                try
                {
                    ftpRequest = CreateRequest(WebRequestMethods.Ftp.DeleteFile, profile, remoteUri);
                    GetAndVerifyFtpResponse(ftpRequest, FtpStatusCode.FileActionOK);
                }
                // Ignore exception when deleting a non-existent file
                catch { }

                ftpRequest = CreateRequest(WebRequestMethods.Ftp.Rename, profile, tempRemoteUri);
                ftpRequest.RenameTo = remoteUri.Segments[remoteUri.Segments.Length - 1];
                GetAndVerifyFtpResponse(ftpRequest, FtpStatusCode.FileActionOK);

                // Sync attached files
                base.SyncRemoteDatabase(profile);

                RemoveLocalDb();
                ProgressMediator.ChangeProgress(Resources.Text_UI_LogText_Completed, SignificantProgress.Complete);
            }
            catch (WebException wE)
            {
                throw new SyncException(String.Format(Resources.ExceptionMsg_Formatable2_UploadError, profile.RemoteHost, wE.Message), wE);
            }
            catch (Exception e)
            {
                throw new SyncException(String.Format(Resources.ExceptionMsg_Formatable1_GeneralUploadError, e.Message), e);
            }
        }

        private static void GetAndVerifyFtpResponse(FtpWebRequest ftpRequest, FtpStatusCode expectedStatus)
        {
            using (FtpWebResponse ftpResponse = (FtpWebResponse)ftpRequest.GetResponse())
            {
                if (ftpResponse.StatusCode != expectedStatus)
                {
                    string message = String.Format(Resources.ExceptionMsg_Formatable2_UnexpectedResponse, ftpResponse.StatusCode.ToString(), expectedStatus.ToString());
                    Trace.WriteLineIf(RoamiePlugin.TraceSwitch.TraceError, message, TraceCategory);

                    throw new SyncException(message);
                }
            }
        }        

        public override void NonSyncShutdown()
        {
            Trace.WriteLineIf(RoamiePlugin.TraceSwitch.TraceInfo, "NonSyncShutdown requested => no synchronization is required.", TraceCategory);

            base.NonSyncShutdown();
            RemoveLocalDb();
        }

        #endregion        

        #region ISiteAdapter

        [DebuggerHidden]
        bool ISiteAdapter.FileExists(RoamingProfile profile, string path)
        {
            FtpWebRequest req = CreateRequest(WebRequestMethods.Ftp.DownloadFile, profile, new Uri(path));

            try
            {
                using (FtpWebResponse resp = (FtpWebResponse)req.GetResponse())
                {
                    bool exists = (resp.StatusCode == FtpStatusCode.OpeningData || resp.StatusCode == FtpStatusCode.DataAlreadyOpen);
                    Debug.Assert(exists);

                    return exists;
                }
            }
            catch
            {
                return false;
            }
        }

        Stream ISiteAdapter.PullFile(RoamingProfile profile, string path)
        {
            FtpWebRequest req = CreateRequest(WebRequestMethods.Ftp.DownloadFile, profile, new Uri(path));
            return req.GetResponse().GetResponseStream();
        }

        void ISiteAdapter.PushFile(RoamingProfile profile, string path, Stream sourceStream)
        {
           FtpWebRequest req = CreateRequest(WebRequestMethods.Ftp.UploadFile, profile, new Uri(path));

           using (Stream remoteStream = req.GetRequestStream())
               StreamUtility.CopyStream(sourceStream, remoteStream);
        }

        bool ISiteAdapter.DeleteFile(RoamingProfile profile, string path)
        {
            FtpWebRequest req = CreateRequest(WebRequestMethods.Ftp.DeleteFile, profile, new Uri(path));

            using (FtpWebResponse resp = (FtpWebResponse)req.GetResponse())
                return resp.StatusCode == FtpStatusCode.FileActionOK;
        }

        #endregion
    }
}
