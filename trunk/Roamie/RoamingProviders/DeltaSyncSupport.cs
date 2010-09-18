using Virtuoso.Roamie.Roaming;
using Virtuoso.Roamie.Roaming.Profiles;
using Virtuoso.Roamie.Roaming.DeltaSync;
using System.IO;

namespace Virtuoso.Roamie.RoamingProviders
{
    internal class DeltaSyncSupport : ProviderDecorator
    {
        #region Fields

        private const string DeltaExtension = ".delta.bin";

        private readonly IDeltaSyncEngine DeltaEngine; 

        #endregion

        #region .ctors

        public DeltaSyncSupport(DatabaseProvider provider) : base(provider)
        {
            DeltaEngine = DeltaSyncEngineFactory.GetEngine();
        }

        #endregion

        #region Properties

        private bool CanDeltaSync
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

            if (!CanDeltaSync)
                return;

            Context.State |= RoamingState.UploadDeltaOnly;

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

            string remoteDeltaPath = GetRemoteDeltaPath(profile);

            if (!CanDeltaSync)
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
