using System;
using Virtuoso.Miranda.Plugins.Infrastructure;
using Virtuoso.Miranda.Plugins.Configuration;

namespace Virtuoso.Roamie.Configuration
{
    [Serializable]
    [ConfigurationOptions(Version, Encrypt = true, ProfileBound = false, Encryption = typeof(WindowsEncryption), Storage = typeof(IsolatedStorage))]
    public class WindowsAccountBoundConfiguration : RoamingConfiguration
    {
        internal static RoamingConfiguration Load()
        {
            return Load<WindowsAccountBoundConfiguration>();
        }

        internal static void Delete()
        {
            var config = GetDefaultConfiguration<WindowsAccountBoundConfiguration>();
            config.Status = ConfigurationStatus.Deleted;
            config.Save();
        }
    }
}
