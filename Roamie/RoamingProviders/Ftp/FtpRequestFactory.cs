using System;
using System.Diagnostics;
using System.Net;
using System.Net.Cache;
using Virtuoso.Roamie.Properties;
using Virtuoso.Roamie.Roaming.Profiles;

namespace Virtuoso.Roamie.RoamingProviders.Ftp
{
    internal static class FtpRequestFactory
    {
        public static FtpWebRequest CreateRequest(string method, RoamingProfile profile)
        {
            return CreateRequest(method, profile, new Uri(profile.RemoteHost));
        }

        public static FtpWebRequest CreateRequest(string method, RoamingProfile profile, Uri remoteAddress)
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
            ftpRequest.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);

            Trace.WriteLineIf(RoamiePlugin.TraceSwitch.TraceVerbose, "Ftp request created: " + String.Format("Url = '{0}', Method = '{1}', UsePassive = '{2}', UserName = '{3}', CachePolicy = '{4}'.", remoteAddress, method, ftpRequest.UsePassive, profile.UserName, ftpRequest.CachePolicy), FtpProvider.TraceCategory);
            return ftpRequest;
        }

        public static FtpWebRequest CloneRequest(string newMethod, FtpWebRequest request)
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

        public static FtpWebRequest CreateTestRequest(RoamingProfile profile)
        {
            RoamingProfile testProfile = new RoamingProfile(profile.Name, profile.Description, new Uri(profile.RemoteHost).GetComponents(UriComponents.SchemeAndServer, UriFormat.SafeUnescaped), profile.UserName, profile.Password, profile.DatabasePassword, profile.RoamingProvider);        
            return CreateRequest(WebRequestMethods.Ftp.PrintWorkingDirectory, testProfile);
        }

        public static int? GetFileSize(FtpWebRequest ftpFileRequest)
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
    }
}
