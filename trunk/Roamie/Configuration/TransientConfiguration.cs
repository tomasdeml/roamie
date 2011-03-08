using System;
using Virtuoso.Miranda.Plugins.Infrastructure;

namespace Virtuoso.Roamie.Configuration
{
    [Serializable]
    [ConfigurationOptions(Version, Encrypt = false, ProfileBound = false, Storage = typeof(MemoryStorage))]
    public class TransientConfiguration : RoamingConfiguration
    {
    }
}
