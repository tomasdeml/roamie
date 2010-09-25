﻿using System;
using System.Collections.Generic;
using System.Text;
using Virtuoso.Miranda.Plugins.Configuration;
using System.IO;
using Virtuoso.Miranda.Plugins.Infrastructure;

namespace Virtuoso.Roamie.Configuration
{
    // TODO Move to Hyphen

    public class DeletablePortableStorage : PortableStorage
    {
        protected override string GetFileName(Type configType, ConfigurationOptionsAttribute options)
        {
            return GetFileName(configType);   
        }

        private static string GetFileName(Type configType)
        {
            if (!configType.IsDefined(typeof(StorageOptionsAttribute), false))
                throw new NotSupportedException();

            var storageOptions = (StorageOptionsAttribute)configType.GetCustomAttributes(typeof(StorageOptionsAttribute), false)[0];

            if (String.IsNullOrEmpty(storageOptions.FileName))
                throw new NotSupportedException();

            return storageOptions.FileName;
        }

        public static void Delete(Type configType)
        {
            string configDirectory = Path.Combine(MirandaEnvironment.MirandaFolderPath, "Configuration");
            string path = Path.Combine(configDirectory, GetFileName(configType));

            if (File.Exists(path))
                File.Delete(path);
        }
    }
}
