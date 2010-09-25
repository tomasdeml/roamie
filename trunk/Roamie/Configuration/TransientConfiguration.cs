using System;
using System.Collections.Generic;
using System.Text;
using Virtuoso.Miranda.Plugins.Infrastructure;

namespace Virtuoso.Roamie.Configuration
{
    [Serializable]
    [ConfigurationOptions(RoamingConfiguration.Version, Encrypt = false, ProfileBound = false, Storage = typeof(MemoryStorage))]
    public class TransientConfiguration : RoamingConfiguration
    {
    }
}
