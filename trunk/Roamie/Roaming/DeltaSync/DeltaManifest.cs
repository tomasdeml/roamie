/***********************************************************************************\
 * Virtuoso.Miranda.Roamie (Roamie)                                                *
 * A Miranda plugin providing a remote database synchronization features.          *
 * Copyright (C) 2006-2007 Virtuoso                                                *
 *                    deml.tomas@seznam.cz                                         *
 *                                                                                 *
 * This program is free software; you can redistribute it and/or                   *
 * modify it under the terms of the GNU General Public License                     *
 * as published by the Free Software Foundation; either version 2                  *
 * of the License, or (at your option) any later version.                          *
 *                                                                                 *
 * This program is distributed in the hope that it will be useful,                 *
 * but WITHOUT ANY WARRANTY; without even the implied warranty of                  *
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the                   *
 * GNU General Public License for more details.                                    *
 *                                                                                 *
 * You should have received a copy of the GNU General Public License               *
 * along with this program; if not, write to the Free Software                     *
 * Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA. *
\***********************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Virtuoso.Miranda.Roamie.Roaming.DeltaSync;
using System.Runtime.Serialization;
using Virtuoso.Miranda.Roamie.Properties;
using Virtuoso.Miranda.Roamie.Roaming.Profiles;
using Virtuoso.Miranda.Roamie.Roaming.Providers;

namespace Virtuoso.Miranda.Roamie.Roaming.DeltaSync
{
    [Serializable]
    internal class DeltaManifest
    {
        #region Fields

        #region Non-serialized fields
                
        private const string ManifestSuffix = "dbm.bin";

        protected virtual Version SupportedPackageVersion
        {
            get
            {
                return new Version(1, 0, 0, 0);
            }
        }

        [NonSerialized]
        private RoamingProfile associatedProfile;
        public RoamingProfile AssociatedProfile
        {
            get { return associatedProfile; }
        }

        [NonSerialized]
        private bool isNew;
        public bool IsNew
        {
            get { return isNew; }
        }

        #endregion

        #region Serialized fields

        private int deltaCount;
        public int DeltaCount
        {
            get { return deltaCount; }
            set { deltaCount = value; }
        }

        private Version packageVersion;
        public Version PackageVersion
        {
            get { return packageVersion; }
            set { packageVersion = value; }
        }

        private Guid databaseToken;
        public Guid DatabaseToken
        {
            get { return databaseToken; }
            set { databaseToken = value; }
        }

        #endregion

        #endregion

        #region .ctors

        public DeltaManifest(RoamingProfile profile, Guid databaseToken)
        {
            if (profile == null)
                throw new ArgumentNullException("profile");

            if (databaseToken == Guid.Empty)
                throw new ArgumentNullException("databaseToken");

            this.associatedProfile = profile;
            this.databaseToken = databaseToken;
            this.packageVersion = SupportedPackageVersion;

            // Do not touch
            this.isNew = true;
        }        

        #endregion

        #region Methods

        public void Serialize(Stream destination)
        {
            if (destination == null) 
                throw new ArgumentNullException("destination");

            if (!destination.CanWrite) 
                throw new ArgumentException();

            using (MemoryStream encryptedStream = new MemoryStream(4092))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(encryptedStream, this);

                encryptedStream.Seek(0, SeekOrigin.Begin);
                StreamUtility.CompressAndEncrypt(encryptedStream, destination, AssociatedProfile.DatabasePassword);
            }
        }

        public static DeltaManifest Deserialize(Stream source, RoamingProfile profile)
        {
            if (source == null) 
                throw new ArgumentNullException("source");

            if (profile == null)
                throw new ArgumentNullException("profile");

            if (!source.CanRead) 
                throw new ArgumentException();

            using (MemoryStream decryptedStream = new MemoryStream(8192))
            {
                StreamUtility.DecryptAndDecompress(source, decryptedStream, profile.DatabasePassword);
                decryptedStream.Seek(0, SeekOrigin.Begin);

                BinaryFormatter formatter = new BinaryFormatter();
                DeltaManifest manifest = (DeltaManifest)formatter.Deserialize(decryptedStream);

                manifest.associatedProfile = profile;
                return manifest;
            }
        }        

        public void Validate()
        {
            if (DeltaSyncEngine.GetDatabaseToken() != DatabaseToken)
                throw new DbTokenMismatchException();
        }

        public static string GetManifestPath(RoamingProfile profile)
        {
            if (profile == null)
                throw new ArgumentNullException("profile");

            return String.Format("{0}.{1}", profile.RemoteHost, ManifestSuffix);
        }

        public IEnumerable<string> GetDeltaPaths()
        {
            for (int i = 1; i <= DeltaCount; i++)
                yield return Delta.GetPathForDelta(this, i);
        }

        public void Remove()
        {
            if (IsNew)
                return;

            ISiteAdapter adapter = AssociatedProfile.GetProvider().Adapter;
            RoamingProfile profile = AssociatedProfile;

            adapter.DeleteFile(AssociatedProfile, GetManifestPath(profile));

            foreach (string deltaPath in GetDeltaPaths())
                adapter.DeleteFile(profile, deltaPath);

            deltaCount = 0;
        }

        #endregion
    }
}
