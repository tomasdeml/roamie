using System;
using Virtuoso.Miranda.Plugins.Configuration;
using Virtuoso.Miranda.Plugins.Infrastructure;

namespace Virtuoso.Roamie.Configuration
{
    [Serializable]
    [ConfigurationOptions("1.0.0.0", Encrypt = false, ProfileBound = false, Storage = typeof(IsolatedStorage))]
    public class TransientConfigurationMarker : ExtendedConfiguration
    {
        internal static TransientConfigurationMarker Load()
        {
            return Load<TransientConfigurationMarker>();
        }

        internal static void Delete()
        {
            var config = GetDefaultConfiguration<TransientConfigurationMarker>();
            config.Status = ConfigurationStatus.Deleted;
            config.Save();
        }
    }
}