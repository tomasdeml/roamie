using Virtuoso.Roamie.Roaming.DeltaSync;
using Virtuoso.Roamie.Roaming.Profiles;
using Virtuoso.Roamie.Properties;

namespace Virtuoso.Roamie.RoamingProviders
{
    internal class LegacyDeltaManifestCheck : ProviderDecorator
    {
        #region Constants

        private const string LegacyManifestExtension = ".dbm.bin";

        #endregion

        #region .ctors

        public LegacyDeltaManifestCheck(Provider provider)
            : base(provider)
        {
        } 

        #endregion

        #region Methods

        public override void SyncLocalSite(RoamingProfile profile)
        {
            string remoteLegacyManifestPath = GetRemoteLegacyManifestPath(profile);

            if (Adapter.FileExists(profile, remoteLegacyManifestPath))
                throw new DeltaSyncException(Resources.ExceptionMsg_LegacyManifestFound);

            base.SyncLocalSite(profile);
        }

        private static string GetRemoteLegacyManifestPath(RoamingProfile profile)
        {
            return profile.RemoteHost + LegacyManifestExtension;
        }

        #endregion
    }
}