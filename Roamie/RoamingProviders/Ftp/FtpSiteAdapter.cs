using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using Virtuoso.Roamie.Properties;
using Virtuoso.Roamie.Roaming.Profiles;

namespace Virtuoso.Roamie.RoamingProviders.Ftp
{
    internal class FtpSiteAdapter : ISiteAdapter
    {
        [DebuggerHidden]
        public bool FileExists(RoamingProfile profile, string remotePath)
        {
            FtpWebRequest req = FtpRequestFactory.CreateRequest(WebRequestMethods.Ftp.DownloadFile, profile, new Uri(remotePath));

            try
            {
                using (FtpWebResponse resp = (FtpWebResponse)req.GetResponse())
                {
                    bool exists = (resp.StatusCode == FtpStatusCode.OpeningData || resp.StatusCode == FtpStatusCode.DataAlreadyOpen);
                    return exists;
                }
            }
            catch (Exception)
            {
                // TODO Log
                return false;
            }
        }

        public bool PullFile(RoamingProfile profile, string remotePath, Stream outputStream)
        {
            FtpWebRequest ftpFileRequest = FtpRequestFactory.CreateRequest(WebRequestMethods.Ftp.DownloadFile, profile,
                                                                           new Uri(remotePath));
            int? fileSize = FtpRequestFactory.GetFileSize(ftpFileRequest);

            if (fileSize.GetValueOrDefault() <= 0)
            {
                // TODO Log
                //Trace.WriteLineIf(RoamiePlugin.TraceSwitch.TraceError, "The FTP SIZE response says the remote file length is 0 bytes. This indicates the file is corrupted. Aborting download.", "Roamie");
                return false;
            }

            using (FtpWebResponse ftpFileResponse = (FtpWebResponse) ftpFileRequest.GetResponse())
            {
                using (Stream remoteStream = ftpFileResponse.GetResponseStream(), 
                       downloadedStream = new MemoryStream())
                {
                    ProgressMediator.ChangeProgress(Resources.Text_UI_LogText_DownloadingDb, SignificantProgress.Running);

                    var source = new UndisposableStream(remoteStream, fileSize);
                    var progressCallback = (fileSize != null
                                                ? (progress => ProgressMediator.ChangeProgress(null, progress))
                                                : (StreamUtility.ProgressCallback) null);

                    StreamUtility.CopyStream(source, downloadedStream, progressCallback);

                    ProgressMediator.ChangeProgress(Resources.Text_UI_LogText_DecryptingDecompressing,
                                                    SignificantProgress.Running);
                    downloadedStream.Seek(0, SeekOrigin.Begin);
                    StreamUtility.DecryptAndDecompress(downloadedStream, outputStream, profile.DatabasePassword);
                    outputStream.Seek(0, SeekOrigin.Begin);
                }
            }

            ProgressMediator.ChangeProgress(Resources.Text_UI_LogText_Completed, SignificantProgress.Complete);
            return true;
        }

        public void PushFile(RoamingProfile profile, Stream sourceStream, string remotePath, bool reliable)
        {
            Uri remoteUri = new Uri(remotePath);
            Uri tempRemoteUri = (reliable ? new Uri(remotePath + ".tmp") : remoteUri); 

            // First upload new database as *.tmp file (to preserve original database if the transfer fails)
            FtpWebRequest ftpRequest = FtpRequestFactory.CreateRequest(WebRequestMethods.Ftp.UploadFile, profile,
                                                                       tempRemoteUri);

            using (Stream remoteStream = ftpRequest.GetRequestStream(),
                          protectedStream = new MemoryStream())
            {
                ProgressMediator.ChangeProgress(Resources.Text_UI_LogText_CompressingEncrypting,
                                                SignificantProgress.Running);
                StreamUtility.CompressAndEncrypt(sourceStream, protectedStream, profile.DatabasePassword);

                ProgressMediator.ChangeProgress(Resources.Text_UI_LogText_Uploading, SignificantProgress.Stopped);
                protectedStream.Seek(0, SeekOrigin.Begin);
                StreamUtility.CopyStream(protectedStream, remoteStream,
                                         progress => ProgressMediator.ChangeProgress(null, progress));
            }

            GetAndVerifyFtpResponse(ftpRequest, FtpStatusCode.ClosingData, true);

            if (reliable)
            {
                ProgressMediator.ChangeProgress(Resources.Text_UI_LogText_Finishing, SignificantProgress.Running);

                // Now delete any previous existing *.dat profiles
                DeleteFile(profile, remoteUri);

                // Lastly rename *.tmp to *.dat
                ftpRequest = FtpRequestFactory.CreateRequest(WebRequestMethods.Ftp.Rename, profile, tempRemoteUri);
                ftpRequest.RenameTo = remoteUri.Segments[remoteUri.Segments.Length - 1];
                GetAndVerifyFtpResponse(ftpRequest, FtpStatusCode.FileActionOK, true);
            }

            ProgressMediator.ChangeProgress(Resources.Text_UI_LogText_Completed, SignificantProgress.Complete);
        }

        private static bool GetAndVerifyFtpResponse(FtpWebRequest ftpRequest, FtpStatusCode expectedStatus, bool throwException)
        {
            try
            {
                using (FtpWebResponse ftpResponse = (FtpWebResponse)ftpRequest.GetResponse())
                {
                    if (ftpResponse.StatusCode == expectedStatus)
                        return true;

                    string message = String.Format(Resources.ExceptionMsg_Formatable2_UnexpectedResponse,
                                                   ftpResponse.StatusCode, expectedStatus);
                    // TODO Log
                    //Trace.WriteLineIf(RoamiePlugin.TraceSwitch.TraceError, message, TraceCategory);

                    if (throwException)
                        throw new WebException(message);

                    return false;
                }
            }
            catch (Exception)
            {
                if (throwException)
                    throw;

                return false;
            }
        }

        public bool DeleteFile(RoamingProfile profile, string remotePath)
        {
            return DeleteFile(profile, new Uri(remotePath));
        }

        private bool DeleteFile(RoamingProfile profile, Uri remotePath)
        {
            FtpWebRequest req = FtpRequestFactory.CreateRequest(WebRequestMethods.Ftp.DeleteFile, profile, remotePath);
            return GetAndVerifyFtpResponse(req, FtpStatusCode.FileActionOK, false);
        }
    }
}
