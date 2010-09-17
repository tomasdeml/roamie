using System;
using System.IO;
using System.Net;
using Virtuoso.Roamie.Properties;
using Virtuoso.Roamie.Roaming.Profiles;

namespace Virtuoso.Roamie.RoamingProviders.Http
{
    internal class HttpSiteAdapter : ISiteAdapter
    {
        public bool FileExists(RoamingProfile profile, string remotePath)
        {
            try
            {
                HttpWebRequest req = HttpRequestFactory.CreateWebRequest(profile, new Uri(remotePath));

                using (HttpWebResponse resp = (HttpWebResponse)req.GetResponse())
                    return resp.StatusCode == HttpStatusCode.OK;
            }
            catch
            {
                return false;
            }
        }

        public bool PullFile(RoamingProfile profile, string remotePath, Stream outputStream)
        {
            HttpWebRequest request = HttpRequestFactory.CreateWebRequest(profile);

            if (!String.IsNullOrEmpty(profile.UserName))
                request.Credentials = new NetworkCredential(profile.UserName, profile.Password);

            using (HttpWebResponse response = (HttpWebResponse) request.GetResponse())
            {
                long? fileSize = response.ContentLength > 0 ? response.ContentLength : (long?) null;

                if (fileSize.GetValueOrDefault() <= 0)
                    return false;

                using (Stream remoteStream = response.GetResponseStream(),
                              downloadedStream = new MemoryStream())
                {
                    ProgressMediator.ChangeProgress(Resources.Text_UI_LogText_DownloadingDb,
                                                    SignificantProgress.Running);

                    var source = new UndisposableStream(remoteStream, fileSize);
                    var progressCallback = (fileSize != null
                                                ? (progress => ProgressMediator.ChangeProgress(null, progress))
                                                : (StreamUtility.ProgressCallback) null);

                    StreamUtility.CopyStream(source, downloadedStream, progressCallback);

                    ProgressMediator.ChangeProgress(Resources.Text_UI_LogText_CompressingEncrypting,
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
            throw new NotSupportedException();
        }

        public bool DeleteFile(RoamingProfile profile, string remotePath)
        {
            throw new NotSupportedException();
        }
    }
}
