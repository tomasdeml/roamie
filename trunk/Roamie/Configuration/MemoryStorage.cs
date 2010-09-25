using System;
using System.Collections.Generic;
using System.Text;
using Virtuoso.Miranda.Plugins.Configuration;
using Virtuoso.Miranda.Plugins.Infrastructure;
using System.IO;

namespace Virtuoso.Roamie.Configuration
{
    public class MemoryStorage : IStorage
    {
        public bool Exists(Type configType, ConfigurationOptionsAttribute options)
        {
            throw new NotImplementedException();
        }

        public Stream OpenRead(Type configType, ConfigurationOptionsAttribute options)
        {
            throw new NotImplementedException();
        }

        public Stream OpenWrite(Type configType, ConfigurationOptionsAttribute options)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
