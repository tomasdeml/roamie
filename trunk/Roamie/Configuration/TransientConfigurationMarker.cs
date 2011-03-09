using System;
using Virtuoso.Miranda.Plugins.Configuration;
using Virtuoso.Miranda.Plugins.Infrastructure;

namespace Virtuoso.Roamie.Configuration
{
    [Serializable]
    [ConfigurationOptions("1.0.0.0", Encrypt = false, ProfileBound = false, Storage = typeof(IsolatedStorage))]
    public class TransientConfigurationMarker : RoamingConfiguration
    {
        internal static void Create()
        {
            new TransientConfigurationMarker().Save();
        }
    }
}