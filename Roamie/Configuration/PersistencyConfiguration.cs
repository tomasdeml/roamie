using System;
using Virtuoso.Miranda.Plugins.Infrastructure;
using Virtuoso.Miranda.Plugins.Configuration;

namespace Virtuoso.Roamie.Configuration
{
    [Serializable]
    [ConfigurationOptions("1.0.0.0", Encrypt = false, ProfileBound = false, Storage = typeof(IsolatedStorage))]
    public class PersistencyConfiguration : PluginConfiguration
    {
        public static PersistencyConfiguration Singleton
        {
            get;
            private set;
        }

        public ConfigurationPersistencyMode ConfigurationPersistencyMode
        {
            get;
            set;
        }

        private PersistencyConfiguration() {}

        public static void Load()
        {
            Singleton = Load<PersistencyConfiguration>();
        }
    }
}
