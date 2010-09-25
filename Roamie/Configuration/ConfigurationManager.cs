using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.CompilerServices;
using Virtuoso.Miranda.Plugins.Infrastructure;
using Virtuoso.Roamie.Properties;

namespace Virtuoso.Roamie.Configuration
{
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
            WindowsAccountSettings.Load();

            PersistencyMode = WindowsAccountSettings.Singleton.ConfigurationPersistencyMode;
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

            WindowsAccountSettings.Singleton.ConfigurationPersistencyMode = mode;
            WindowsAccountSettings.Singleton.Save();
        }        

        private void LoadOrCreateConfiguration()
        {
            switch (PersistencyMode)
            {
                case ConfigurationPersistencyMode.Transient:
                    Configuration = new TransientConfiguration();
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
                    break;
                case ConfigurationPersistencyMode.WindowsAccount:
                    MirandaBoundConfiguration.Delete();
                    break;
            }
        }

        #endregion
    }
}
