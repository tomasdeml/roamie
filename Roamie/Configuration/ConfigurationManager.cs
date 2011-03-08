using System.Runtime.CompilerServices;

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
            if (MirandaBoundConfiguration.Exists())
                PersistencyMode = ConfigurationPersistencyMode.MirandaInstallation;
            else if (WindowsAccountBoundConfiguration.Load().Status == ConfigurationStatus.Persistent)
                PersistencyMode = ConfigurationPersistencyMode.WindowsAccount;
            else if (TransientConfigurationMarker.Load().Status == ConfigurationStatus.Persistent)
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

            Configuration.Status = ConfigurationStatus.Persistent;
            Configuration.Save();

            DeletePreviousConfiguration();
        }        

        private void LoadOrCreateConfiguration()
        {
            switch (PersistencyMode)
            {
                case ConfigurationPersistencyMode.Transient:
                    Configuration = new TransientConfiguration();
                    new TransientConfigurationMarker().Save();
                    break;
                case ConfigurationPersistencyMode.WindowsAccount:
                    Configuration = WindowsAccountBoundConfiguration.Load();
                    break;
                case ConfigurationPersistencyMode.MirandaInstallation:
                    Configuration = MirandaBoundConfiguration.Load();
                    break;
            }
        }

        private void DeletePreviousConfiguration()
        {
            switch (PersistencyMode)
            {
                case ConfigurationPersistencyMode.Transient:
                    WindowsAccountBoundConfiguration.Delete();
                    MirandaBoundConfiguration.Delete();
                    break;
                case ConfigurationPersistencyMode.MirandaInstallation:
                    WindowsAccountBoundConfiguration.Delete();
                    TransientConfigurationMarker.Delete();
                    break;
                case ConfigurationPersistencyMode.WindowsAccount:
                    MirandaBoundConfiguration.Delete();
                    TransientConfigurationMarker.Delete();
                    break;
            }
        }

        #endregion
    }
}
