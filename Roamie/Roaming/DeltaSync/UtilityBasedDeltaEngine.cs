﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Virtuoso.Roamie.Roaming.DeltaSync
{
    internal class UtilityBasedDeltaEngine : IDeltaSyncEngine
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

        #region .ctors

        public UtilityBasedDeltaEngine(IDeltaUtility deltaUtility)
        {
            DeltaUtility = deltaUtility;
        }

        #endregion

        #region Properties

        protected IDeltaUtility DeltaUtility
        {
            get; set;
        }

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
                DeltaUtility.CreatePatch(OriginalDatabasePath, WorkingDatabasePath, PatchPath);
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
                DeltaUtility.ApplyPatch(WorkingDatabasePath, PatchPath, PatchedDatabasePath);
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
