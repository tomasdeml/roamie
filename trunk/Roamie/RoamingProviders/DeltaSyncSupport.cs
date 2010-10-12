using Virtuoso.Roamie.Roaming;
using Virtuoso.Roamie.Roaming.Profiles;
using Virtuoso.Roamie.Roaming.DeltaSync;
using System.IO;

namespace Virtuoso.Roamie.RoamingProviders
{
    internal class DeltaSyncSupport : ProviderDecorator
    {
        #region Fields

        private const string DeltaExtension = "-d.bin";

        private readonly IDeltaSyncEngine DeltaEngine; 

        #endregion

        #region .ctors

        public DeltaSyncSupport(Provider provider) : base(provider)
        {
            DeltaEngine = DeltaSyncEngineFactory.GetEngine();
        }

        #endregion

        #region Properties

        private bool UseDeltaSync
        {
            get
            {
                return !Context.IsInState(RoamingState.ForceFullSync);
            }
        }

        #endregion

        public override void SyncLocalSite(RoamingProfile profile)
        {
            base.SyncLocalSite(profile);

            DeltaEngine.Initialize(Context.ProfilePath);
            string remoteDeltaPath = GetRemoteDeltaPath(profile);

            if (!Adapter.FileExists(profile, remoteDeltaPath))
                return;

            using (Stream deltaStream = DeltaEngine.CreateLocalDeltaFile())
                Adapter.PullFile(profile, remoteDeltaPath, deltaStream);

            DeltaEngine.ApplyDelta();
        }

        public override void SyncRemoteSite(RoamingProfile profile)
        {
            base.SyncRemoteSite(profile);

            if (!CanSyncRemoteSite)
                return;

            string remoteDeltaPath = GetRemoteDeltaPath(profile);

            if (!UseDeltaSync)
            {
                Adapter.DeleteFile(profile, remoteDeltaPath);
                return;
            }

            ProgressMediator.ChangeProgress("Computing delta, this may take a while...", SignificantProgress.Running);

            using (Stream deltaStream = DeltaEngine.ComputeDelta())
                Adapter.PushFile(profile, deltaStream, remoteDeltaPath, false);
        }

        private static string GetRemoteDeltaPath(RoamingProfile profile)
        {
            return profile.RemoteHost + DeltaExtension;
        }

        public override void RemoveLocalSiteData()
        {
            base.RemoveLocalSiteData();
            DeltaEngine.Dispose();
        }
    }
}
