using System;
using System.IO;

namespace Virtuoso.Roamie.Roaming.DeltaSync
{
    class MicrosoftPatchDeltaSyncEngine : IDeltaSyncEngine
    {
        #region Fields

        private const string OriginalDatabaseExtension = ".tmp";
        private const string PatchExtension = ".patch.tmp";

        private string WorkingDatabasePath;
        private string OriginalDatabasePath;
        private string PatchPath;

        #endregion

        public void Initialize(string databasePath)
        {
            WorkingDatabasePath = databasePath;
            OriginalDatabasePath = databasePath + OriginalDatabaseExtension;
            PatchPath = databasePath + PatchExtension;

            //File.Copy(WorkingDatabasePath, OriginalDatabasePath, true);
        }

        public Stream CreateDelta()
        {
            try
            {
                MicrosoftPatchApi.CreatePatch(OriginalDatabasePath, WorkingDatabasePath, PatchPath);
                return File.OpenRead(PatchPath);
            }
            catch (Exception e)
            {
                // TODO
                throw;
            }
        }

        public void ApplyDelta(Stream patchStream)
        {
            try
            {
                using (Stream localPatchStream = File.Create(PatchPath))
                    StreamUtility.CopyStream(patchStream, localPatchStream);

                MicrosoftPatchApi.ApplyPatch(WorkingDatabasePath, PatchPath, OriginalDatabasePath);
                File.Delete(WorkingDatabasePath);
                File.Move(OriginalDatabasePath, WorkingDatabasePath);
            }
            catch (Exception)
            {
                // TODO
                throw;
            }
        }

        public void Dispose()
        {
            if (File.Exists(PatchPath))
                File.Delete(PatchPath);
            
            if (File.Exists(OriginalDatabasePath))
                File.Delete(OriginalDatabasePath);
        }
    }
}
