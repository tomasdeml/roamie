using System;
using System.Collections.Generic;
using System.Text;
using Virtuoso.Miranda.Plugins.Configuration;
using Virtuoso.Miranda.Plugins.Infrastructure;
using System.IO;

namespace Virtuoso.Roamie.Configuration
{
    // TODO Move to Hyphen

    public class MemoryStorage : IStorage
    {
        #region Fields

        private static readonly Dictionary<Type, byte[]> Storage = new Dictionary<Type, byte[]>();

        #endregion

        #region Methods

        public bool Exists(Type configType, ConfigurationOptionsAttribute options)
        {
            return Storage.ContainsKey(configType);
        }

        public Stream OpenRead(Type configType, ConfigurationOptionsAttribute options)
        {
            if (!Exists(configType, options))
                throw new FileNotFoundException();

            return new MemoryStream(Storage[configType]);
        }

        public Stream OpenWrite(Type configType, ConfigurationOptionsAttribute options)
        {
            ObservableMemoryStream stream = new ObservableMemoryStream(configType);
            stream.Disposed += WriteStream_Disposed;

            return stream;
        }

        private static void WriteStream_Disposed(object sender, ObservableMemoryStream.DisposedEventArgs e)
        {
            Storage[(Type)e.Token] = e.Buffer;
        }

        public void Dispose()
        {
            Storage.Clear();
        } 

        #endregion
    }
}
