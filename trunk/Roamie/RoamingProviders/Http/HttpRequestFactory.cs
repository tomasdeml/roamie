using System;
using System.Net;
using Virtuoso.Roamie.Properties;
using Virtuoso.Roamie.Roaming.Profiles;

namespace Virtuoso.Roamie.RoamingProviders.Http
{
    internal static class HttpRequestFactory
    {
        public static HttpWebRequest CreateWebRequest(RoamingProfile profile)
        {
            return CreateWebRequest(profile, new Uri(profile.RemoteHost));
        }

        public static HttpWebRequest CreateWebRequest(RoamingProfile profile, Uri remoteUri)
        {
            HttpWebRequest request = WebRequest.Create(remoteUri) as HttpWebRequest;

            if (request == null)
                throw new FormatException(Resources.ExceptionMsg_RemoteUriNotSupported);

            return request;
        }
    }
}
