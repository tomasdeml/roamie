using System;
using System.Collections.Generic;
using System.Text;
using Virtuoso.Miranda.Plugins.Infrastructure;
using Virtuoso.Roamie.Roaming;
using Virtuoso.Miranda.Plugins.Configuration;

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
            DeletablePortableStorage.Delete(typeof(MirandaBoundConfiguration));
        }
    }
}
