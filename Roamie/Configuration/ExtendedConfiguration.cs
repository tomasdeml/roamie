using System;
using Virtuoso.Miranda.Plugins.Infrastructure;

namespace Virtuoso.Roamie.Configuration
{
    [Serializable]
    public abstract class ExtendedConfiguration : PluginConfiguration
    {
        public ConfigurationStatus Status { get; set; }

        protected override void OnBeforeSerialization()
        {
            base.OnBeforeSerialization();

            if (Status != ConfigurationStatus.Deleted)
                Status = ConfigurationStatus.Persistent;
        }
    }
}
