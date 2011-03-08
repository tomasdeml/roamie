using System;
using Virtuoso.Miranda.Plugins.Infrastructure;

namespace Virtuoso.Roamie.Configuration
{
    [Serializable]
    [ConfigurationOptions(Version, Encrypt = true, ProfileBound = false, Encryption = typeof(MasterPasswordEncryption), Storage = typeof(DeletablePortableStorage))]
    [StorageOptions(FileName = "Roamie-Configuration.dat")]
    public class MirandaBoundConfiguration : RoamingConfiguration
    {
        internal static RoamingConfiguration Load()
        {
            return Load<MirandaBoundConfiguration>();
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
