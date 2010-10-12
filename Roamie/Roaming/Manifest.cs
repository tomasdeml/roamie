using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Virtuoso.Roamie.Properties;
using Virtuoso.Roamie.Roaming.Profiles;
using Virtuoso.Roamie.RoamingProviders;

namespace Virtuoso.Roamie.Roaming
{
    [Serializable]
    public class Manifest
    {
        #region Fields

        private const string ManifestExtension = "-m.bin";

        #endregion

        #region Properties

        public Version Version { get; set; }

        #endregion

        #region Methods

        public static Manifest Load(RoamingProfile profile)
        {
            if (profile == null)
                throw new ArgumentNullException("profile");

            ISiteAdapter adapter = profile.GetProvider().Adapter;
            string remoteManifestPath = GetRemoteManifestPath(profile);

            if (!adapter.FileExists(profile, remoteManifestPath))
                return new Manifest();

            using (MemoryStream containerStream = new MemoryStream())
            {
                adapter.PullFile(profile, remoteManifestPath, containerStream);

                BinaryFormatter formatter = new BinaryFormatter();
                Manifest container = (Manifest)formatter.Deserialize(containerStream);

                return container;
            }
        }

        public void Publish(RoamingProfile profile)
        {
            ISiteAdapter adapter = profile.GetProvider().Adapter;

            using (MemoryStream containerStream = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(containerStream, this);
                containerStream.Seek(0, SeekOrigin.Begin);

                ProgressMediator.ChangeProgress(Resources.Text_UI_LogText_PublishingContent, SignificantProgress.Running);
                adapter.PushFile(profile, containerStream, GetRemoteManifestPath(profile), true);
            }
        }

        private static string GetRemoteManifestPath(RoamingProfile profile)
        {
            return profile.RemoteHost + ManifestExtension;
        }

        #endregion
    }
}
