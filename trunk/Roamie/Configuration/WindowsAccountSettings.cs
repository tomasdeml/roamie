using System;
using System.Collections.Generic;
using System.Text;
using Virtuoso.Miranda.Plugins.Infrastructure;
using Virtuoso.Miranda.Plugins.Configuration;

namespace Virtuoso.Roamie.Configuration
{
    [Serializable]
    [ConfigurationOptions("1.0.0.0", Encrypt = false, ProfileBound = false, Storage = typeof(IsolatedStorage))]
    public class WindowsAccountSettings : PluginConfiguration
    {
        public static WindowsAccountSettings Singleton
        {
            get;
            private set;
        }

        public ConfigurationPersistencyMode ConfigurationPersistencyMode
        {
            get;
            set;
        }

        private WindowsAccountSettings() {}

        public static void Load()
        {
            Singleton = PluginConfiguration.Load<WindowsAccountSettings>();
        }
    }
}
