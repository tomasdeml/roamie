using System;
using System.IO;

namespace Virtuoso.Roamie.Roaming.DeltaSync
{
    class MicrosoftPatchDeltaSyncEngine : IDeltaSyncEngine
    {
        #region Fields

        private const string OriginalDatabaseExtension = ".tmp";
        private const string PatchedDatabaseExtension = ".patched.tmp";
        private const string PatchExtension = ".patch.tmp";

        private string WorkingDatabasePath;
        private string OriginalDatabasePath;
        private string PatchedDatabasePath;
        private string PatchPath;

        #endregion

        public void Initialize(string databasePath)
        {
            WorkingDatabasePath = databasePath;
            OriginalDatabasePath = databasePath + OriginalDatabaseExtension;
            PatchedDatabasePath = databasePath + PatchedDatabaseExtension;
            PatchPath = databasePath + PatchExtension;

            File.Copy(WorkingDatabasePath, OriginalDatabasePath, true);
        }

        public Stream ComputeDelta()
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

        public Stream CreateLocalDeltaFile()
        {
            return File.Create(PatchPath);
        }

        public void ApplyDelta()
        {
            try
            {
                MicrosoftPatchApi.ApplyPatch(WorkingDatabasePath, PatchPath, PatchedDatabasePath);
                File.Delete(WorkingDatabasePath);
                File.Move(PatchedDatabasePath, WorkingDatabasePath);
            }
            catch (Exception)
            {
                // TODO
                throw;
            }
            finally
            {
                if (File.Exists(PatchPath))
                    File.Delete(PatchPath);
            }
        }

        public void Dispose()
        {
            if (File.Exists(PatchPath))
                File.Delete(PatchPath);

            if (File.Exists(PatchedDatabasePath))
                File.Delete(PatchedDatabasePath);
            
            if (File.Exists(OriginalDatabasePath))
                File.Delete(OriginalDatabasePath);
        }
    }
}
