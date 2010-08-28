using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Virtuoso.Miranda.Plugins.Forms.Controls;
using Virtuoso.Miranda.Plugins.Configuration.Forms.Controls;
using Virtuoso.Miranda.Roamie.Roaming;
using Virtuoso.Miranda.Roamie.Roaming.DeltaSync;

namespace Virtuoso.Miranda.Roamie.Forms.Controls.Configuration
{
    internal sealed partial class BehaviourOptions : CategoryItemControl
    {
        public BehaviourOptions()
        {
            InitializeComponent();

            AllowSilentCHKBOX.CheckedChanged += SetControlDirtyHandler;
            FullSyncAfterThresholdCHBOX.CheckedChanged += SetControlDirtyHandler;
        }

        protected override bool OnShow(bool firstTime)
        {
            if (firstTime)
            {
                RoamingConfiguration config = RoamiePlugin.Singleton.RoamingContext.Configuration;

                AllowSilentCHKBOX.Checked = config.SilentStartup;

                FullSyncAfterThresholdCHBOX.Checked = config.FullSyncAfterThreshold;
                FullSyncAfterThresholdCHBOX.Text = String.Format(FullSyncAfterThresholdCHBOX.Text, DeltaSyncEngine.DeltaCountThreshold.ToString());
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

