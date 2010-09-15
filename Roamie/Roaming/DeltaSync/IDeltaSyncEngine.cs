using System;
using System.IO;

namespace Virtuoso.Roamie.Roaming.DeltaSync
{
    interface IDeltaSyncEngine : IDisposable
    {
        void Initialize(string databasePath);
        Stream CreateDelta();
        void ApplyDelta(Stream patchStream);
    }
}
