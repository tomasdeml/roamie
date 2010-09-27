using System;
using Virtuoso.Miranda.Plugins.Infrastructure;
using Virtuoso.Roamie.Roaming;

namespace Virtuoso.Roamie.Configuration
{
    [Serializable]
    [ConfigurationOptions(RoamingConfiguration.Version, Encrypt = true, ProfileBound = false, Encryption = typeof(MasterPasswordEncryption), Storage = typeof(DeletablePortableStorage))]
    [StorageOptions(FileName = "RoamingConfiguration.dat")]
    public class MirandaBoundConfiguration : RoamingConfiguration
    {
        internal static RoamingConfiguration Load()
        {
            return PluginConfiguration.Load<MirandaBoundConfiguration>();
        }

        internal static void Delete()
        {
            DeletablePortableStorage.Delete(typeof (MirandaBoundConfiguration));
        }

        internal static bool Exists()
        {
            return DeletablePortableStorage.Exists(typeof (MirandaBoundConfiguration));
        }
    }
}
