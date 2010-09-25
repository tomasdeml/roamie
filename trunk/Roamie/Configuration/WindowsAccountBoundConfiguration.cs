using System;
using System.Collections.Generic;
using System.Text;
using Virtuoso.Miranda.Plugins.Infrastructure;
using Virtuoso.Miranda.Plugins.Configuration;

namespace Virtuoso.Roamie.Configuration
{
    [Serializable]
    [ConfigurationOptions(RoamingConfiguration.Version, Encrypt = true, ProfileBound = false, Encryption = typeof(WindowsEncryption), Storage = typeof(IsolatedStorage))]
    public class WindowsAccountBoundConfiguration : RoamingConfiguration
    {
        internal static RoamingConfiguration Load()
        {
            return PluginConfiguration.Load<WindowsAccountBoundConfiguration>();
        }

        internal static void Delete()
        {
            var config = PluginConfiguration.GetDefaultConfiguration<WindowsAccountBoundConfiguration>();
            config.Save();
        }
    }
}
