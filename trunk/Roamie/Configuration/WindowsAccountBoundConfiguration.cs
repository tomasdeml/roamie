using System;
using Virtuoso.Miranda.Plugins.Infrastructure;
using Virtuoso.Miranda.Plugins.Configuration;

namespace Virtuoso.Roamie.Configuration
{
    [Serializable]
    [ConfigurationOptions(Version, Encrypt = true, ProfileBound = false, Encryption = typeof(WindowsEncryption), Storage = typeof(IsolatedStorage))]
    public class WindowsAccountBoundConfiguration : RoamingConfiguration
    {
    }
}
