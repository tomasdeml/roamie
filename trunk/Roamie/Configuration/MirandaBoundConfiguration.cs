using System;
using Virtuoso.Miranda.Plugins.Configuration;
using Virtuoso.Miranda.Plugins.Infrastructure;

namespace Virtuoso.Roamie.Configuration
{
    [Serializable]
    [ConfigurationOptions(Version, StaticFileName = "Roamie-Configuration.dat", Encrypt = true, ProfileBound = false, Encryption = typeof(MasterPasswordEncryption), Storage = typeof(PortableStorage))]
    public class MirandaBoundConfiguration : RoamingConfiguration
    {
    }
}
