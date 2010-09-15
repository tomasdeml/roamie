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
using System.IO;
using System.Net;
using System.Security.Cryptography;
using Virtuoso.Miranda.Plugins.Forms;
using Virtuoso.Roamie.Properties;
using Virtuoso.Roamie.Roaming;
using Virtuoso.Roamie.Roaming.Profiles;

namespace Virtuoso.Roamie.RoamingProviders.Ftp
{
    internal class FtpProvider : PackingDatabaseProvider, IDeltaAwareProvider
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

        public override void SyncLocalDatabase(RoamingProfile profile)
        {
            try
            {               
                Trace.WriteLineIf(RoamiePlugin.TraceSwitch.TraceVerbose, "Synchronizing local database...", TraceCategory);
                InitializeSafeProfilePath();

                FtpWebRequest ftpFileRequest = FtpRequestFactory.CreateRequest(WebRequestMethods.Ftp.DownloadFile, profile);                              

                using (FtpWebResponse ftpFileResponse = (FtpWebResponse)ftpFileRequest.GetResponse())
                {
                    int? fileSize = FtpRequestFactory.GetFileSize(ftpFileRequest);

                    using (Stream remoteStream = ftpFileResponse.GetResponseStream(),
                        downloadedStream = new MemoryStream(fileSize == null ? 10240 : fileSize.Value), 
                        unprotectedStream = new MemoryStream((int)downloadedStream.Length * 2))
                    {
                        if (fileSize != null && fileSize.Value <= 0)
                        {
                            Trace.WriteLineIf(RoamiePlugin.TraceSwitch.TraceError, "The FTP SIZE response says the remote file length is 0 bytes. This indicates the database is corrupted. Aborting synchronization.", TraceCategory);
                            throw new SyncException(Resources.ExceptionMsg_InvalidRemoteDatabase);
                        }

                        ProgressMediator.ChangeProgress(Resources.Text_UI_LogText_DownloadingDb, SignificantProgress.Running);
                        
                        var source = new UndisposableStream(remoteStream, ((MemoryStream)downloadedStream).Capacity);
                        var progressCallback = (fileSize != null ? (progress => ProgressMediator.ChangeProgress(null, progress)) : (StreamUtility.ProgressCallback)null);

                        StreamUtility.CopyStream(source, downloadedStream, progressCallback);

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

                // First upload new database as *.tmp file (to preserve original database if the transfer fails)
                FtpWebRequest ftpRequest = FtpRequestFactory.CreateRequest(WebRequestMethods.Ftp.UploadFile, profile,
                                                                           tempRemoteUri);

                using (Stream localStream = new FileStream(Context.ProfilePath, FileMode.Open),                    
                    protectedStream = new MemoryStream((int)localStream.Length / 2),
                    remoteStream = ftpRequest.GetRequestStream())
                {
                    ProgressMediator.ChangeProgress(Resources.Text_UI_LogText_CompressingEncrypting, SignificantProgress.Running);
                    StreamUtility.CompressAndEncrypt(localStream, remoteStream, profile.DatabasePassword);

                    ProgressMediator.ChangeProgress(Resources.Text_UI_LogText_Uploading, SignificantProgress.Stopped);
                    protectedStream.Seek(0, SeekOrigin.Begin);
                    StreamUtility.CopyStream(protectedStream, remoteStream,
                                             progress => ProgressMediator.ChangeProgress(null, progress));
                }

                GetAndVerifyFtpResponse(ftpRequest, FtpStatusCode.ClosingData);
                ProgressMediator.ChangeProgress(Resources.Text_UI_LogText_Finishing, SignificantProgress.Running);

                // Now delete any previous existing *.dat profiles
                try
                {
                    ftpRequest = FtpRequestFactory.CreateRequest(WebRequestMethods.Ftp.DeleteFile, profile, remoteUri);
                    GetAndVerifyFtpResponse(ftpRequest, FtpStatusCode.FileActionOK);
                }
                // Ignore exception when deleting a non-existent file
                catch { }

                // Lastly rename *.tmp to *.dat
                ftpRequest = FtpRequestFactory.CreateRequest(WebRequestMethods.Ftp.Rename, profile, tempRemoteUri);
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
                if (ftpResponse.StatusCode == expectedStatus)
                    return;

                string message = String.Format(Resources.ExceptionMsg_Formatable2_UnexpectedResponse,
                                               ftpResponse.StatusCode, expectedStatus);
                Trace.WriteLineIf(RoamiePlugin.TraceSwitch.TraceError, message, TraceCategory);

                throw new SyncException(message);
            }
        }        

        public override void NonSyncShutdown()
        {
            Trace.WriteLineIf(RoamiePlugin.TraceSwitch.TraceInfo, "NonSyncShutdown requested => no synchronization is required.", TraceCategory);

            base.NonSyncShutdown();
            RemoveLocalDb();
        }

        #endregion
    }
}
