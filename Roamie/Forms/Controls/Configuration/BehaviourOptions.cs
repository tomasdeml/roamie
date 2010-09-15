using Virtuoso.Miranda.Plugins.Configuration.Forms.Controls;
using Virtuoso.Roamie.Roaming;

namespace Virtuoso.Roamie.Forms.Controls.Configuration
{
    internal sealed partial class BehaviourOptions : CategoryItemControl
    {
        public BehaviourOptions()
        {
            InitializeComponent();

            AllowSilentCHKBOX.CheckedChanged += SetControlDirtyHandler;
        }

        protected override bool OnShow(bool firstTime)
        {
            if (firstTime)
            {
                RoamingConfiguration config = RoamiePlugin.Singleton.RoamingContext.Configuration;

                AllowSilentCHKBOX.Checked = config.SilentStartup;

                FullSyncAfterThresholdCHBOX.Checked = config.FullSyncAfterThreshold;
                FullSyncAfterThresholdCHBOX.Text = "CHANGE TO FULLSYNC AFTER DELTA SIZE!!!";
            }

            return false;
        }

        protected override void Save()
        {
            RoamingConfiguration config = RoamiePlugin.Singleton.RoamingContext.Configuration;

            config.SilentStartup = AllowSilentCHKBOX.Checked;
            config.FullSyncAfterThreshold = FullSyncAfterThresholdCHBOX.Checked;
        }
    }
}

