using System;
using System.IO;
using System.Net;
using Virtuoso.Roamie.Roaming.Profiles;

namespace Virtuoso.Roamie.RoamingProviders.Http
{
    internal class HttpSiteAdapter : ISiteAdapter
    {
        public bool FileExists(RoamingProfile profile, string path)
        {
            HttpWebRequest req = HttpRequestFactory.CreateWebRequest(profile, new Uri(path));

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

        public Stream PullFile(RoamingProfile profile, string path)
        {
            HttpWebRequest req = HttpRequestFactory.CreateWebRequest(profile, new Uri(path));
            return req.GetResponse().GetResponseStream();
        }

        public void PushFile(RoamingProfile profile, string path, Stream sourceStream)
        {
            throw new NotSupportedException();
        }

        public bool DeleteFile(RoamingProfile profile, string path)
        {
            throw new NotSupportedException();
        }
    }
}
