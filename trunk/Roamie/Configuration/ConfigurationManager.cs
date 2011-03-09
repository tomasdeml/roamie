using System.Runtime.CompilerServices;
using Virtuoso.Miranda.Plugins.Infrastructure;

namespace Virtuoso.Roamie.Configuration
{
    public enum ConfigurationStatus
    {
        NewTransient,
        Persistent,
        Deleted
    }

    public enum ConfigurationPersistencyMode
    {
        Undefined,
        Transient,
        WindowsAccount,
        MirandaInstallation
    }

    internal class ConfigurationManager
    {
        #region Fields

        #endregion

        #region .ctors

        private ConfigurationManager()
        {
            if (PluginConfiguration.Exists(typeof(MirandaBoundConfiguration)))
                PersistencyMode = ConfigurationPersistencyMode.MirandaInstallation;
            else if (PluginConfiguration.Exists(typeof(WindowsAccountBoundConfiguration)))
                PersistencyMode = ConfigurationPersistencyMode.WindowsAccount;
            else if (PluginConfiguration.Exists(typeof(TransientConfigurationMarker)))
                PersistencyMode = ConfigurationPersistencyMode.Transient;
            else
                PersistencyMode = ConfigurationPersistencyMode.Undefined;

            LoadOrCreateConfiguration();
        }

        #endregion

        #region Properties

        private static ConfigurationManager singleton;
        public static ConfigurationManager Singleton
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return singleton ?? (singleton = new ConfigurationManager());
            }
        }

        public RoamingConfiguration Configuration
        {
            get;
            private set;
        }            

        public ConfigurationPersistencyMode PersistencyMode
        {
            get;
            private set;
        }

        public bool WasPersistencyModeDefined
        {
            get
            {
                return PersistencyMode != ConfigurationPersistencyMode.Undefined;
            }
        }

        #endregion

        #region Methods

        public void SetPersistencyMode(ConfigurationPersistencyMode mode)
        {
            if (PersistencyMode == mode)
                return;            

            RoamingConfiguration prevConfig = Configuration;
            PersistencyMode = mode;            

            LoadOrCreateConfiguration();

            if (prevConfig != null)
                prevConfig.CopyTo(Configuration);

            Configuration.Save();
            DeletePreviousConfiguration();
        }        

        private void LoadOrCreateConfiguration()
        {
            switch (PersistencyMode)
            {
                case ConfigurationPersistencyMode.Transient:
                    Configuration = new TransientConfiguration();
                    TransientConfigurationMarker.Create();
                    break;
                case ConfigurationPersistencyMode.WindowsAccount:
                    Configuration = PluginConfiguration.Load<WindowsAccountBoundConfiguration>();
                    break;
                case ConfigurationPersistencyMode.MirandaInstallation:
                    Configuration = PluginConfiguration.Load<MirandaBoundConfiguration>();
                    break;
            }
        }

        private void DeletePreviousConfiguration()
        {
            switch (PersistencyMode)
            {
                case ConfigurationPersistencyMode.Transient:
                    PluginConfiguration.Delete(typeof (WindowsAccountBoundConfiguration));
                    PluginConfiguration.Delete(typeof (MirandaBoundConfiguration));
                    break;
                case ConfigurationPersistencyMode.MirandaInstallation:
                    PluginConfiguration.Delete(typeof (WindowsAccountBoundConfiguration));
                    PluginConfiguration.Delete(typeof (TransientConfigurationMarker));
                    break;
                case ConfigurationPersistencyMode.WindowsAccount:
                    PluginConfiguration.Delete(typeof (MirandaBoundConfiguration));
                    PluginConfiguration.Delete(typeof (TransientConfigurationMarker));
                    break;
            }
        }

        #endregion
    }
}
