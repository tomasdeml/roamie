using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using Virtuoso.Roamie.Roaming.Profiles;

namespace Virtuoso.Roamie.RoamingProviders.Ftp
{
    internal class FtpSiteAdapter : ISiteAdapter
    {
        [DebuggerHidden]
        public bool FileExists(RoamingProfile profile, string path)
        {
            FtpWebRequest req = FtpRequestFactory.CreateRequest(WebRequestMethods.Ftp.DownloadFile, profile, new Uri(path));

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

        public Stream PullFile(RoamingProfile profile, string path)
        {
            FtpWebRequest req = FtpRequestFactory.CreateRequest(WebRequestMethods.Ftp.DownloadFile, profile, new Uri(path));
            return req.GetResponse().GetResponseStream();
        }

        public void PushFile(RoamingProfile profile, string path, Stream sourceStream)
        {
            FtpWebRequest req = FtpRequestFactory.CreateRequest(WebRequestMethods.Ftp.UploadFile, profile, new Uri(path));

            using (Stream remoteStream = req.GetRequestStream())
                StreamUtility.CopyStream(sourceStream, remoteStream);
        }

        public bool DeleteFile(RoamingProfile profile, string path)
        {
            FtpWebRequest req = FtpRequestFactory.CreateRequest(WebRequestMethods.Ftp.DeleteFile, profile, new Uri(path));

            using (FtpWebResponse resp = (FtpWebResponse)req.GetResponse())
                return resp.StatusCode == FtpStatusCode.FileActionOK;
        }
    }
}
