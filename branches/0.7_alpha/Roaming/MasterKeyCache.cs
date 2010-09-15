using System;
using Virtuoso.Miranda.Plugins.Infrastructure;
using Virtuoso.Miranda.Plugins.Configuration;

namespace Virtuoso.Roamie.Roaming
{
    [Serializable]
    [ConfigurationOptions("1.0.0.0", Encrypt = true, ProfileBound = false, Encryption = typeof(WindowsEncryption), Storage = typeof(IsolatedStorage))]
    internal sealed class MasterKeyCache : PluginConfiguration
    {
        private string key;
        public string Key
        {
            get { return key; }
            set { key = value; }
        }
    }
}
