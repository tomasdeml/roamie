using Virtuoso.Roamie.Roaming.Profiles;

namespace Virtuoso.Roamie.RoamingProviders
{
    internal abstract class ProviderDecorator : DatabaseProvider
    {
        #region Fields

        protected DatabaseProvider Provider { get; private set; }

        #endregion

        #region .ctors

        protected ProviderDecorator(DatabaseProvider provider)
        {
            Provider = provider;
        }

        #endregion

        #region Properties

        public override string Name
        {
            get { return Provider.Name; }
        }

        public override bool CredentialsRequired
        {
            get { return Provider.CredentialsRequired; }
        }

        public override ISiteAdapter Adapter
        {
            get { return Provider.Adapter; }
        }

        #endregion

        #region Methods

        public override void VerifyProfile(RoamingProfile profile)
        {
            Provider.VerifyProfile(profile);
        }

        public override void NonSyncShutdown()
        {
            Provider.NonSyncShutdown();
        }

        public override void RemoveLocalSiteData()
        {
            Provider.RemoveLocalSiteData();
        }

        public override void SyncLocalSite(RoamingProfile profile)
        {
            Provider.SyncLocalSite(profile);
        }

        public override void SyncRemoteSite(RoamingProfile profile)
        {
            Provider.SyncRemoteSite(profile);
        }

        #endregion
    }
}
