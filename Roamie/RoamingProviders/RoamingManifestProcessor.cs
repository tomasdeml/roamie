using Virtuoso.Roamie.Roaming;
using Virtuoso.Roamie.Roaming.Profiles;

namespace Virtuoso.Roamie.RoamingProviders
{
    internal class RoamingManifestProcessor : ProviderDecorator
    {
        #region Fields

        #endregion

        public RoamingManifestProcessor(Provider provider) : base(provider)
        {
        }

        public override void SyncLocalSite(RoamingProfile profile)
        {
            Context.Manifest = Manifest.Load(profile);
            base.SyncLocalSite(profile);
        }

        public override void SyncRemoteSite(RoamingProfile profile)
        {
            base.SyncRemoteSite(profile);

            if (!CanSyncRemoteSite)
                return;

            Context.Manifest.Publish(profile);
        }
    }
}
