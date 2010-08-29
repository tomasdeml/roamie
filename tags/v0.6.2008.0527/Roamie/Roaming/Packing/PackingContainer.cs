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
using Virtuoso.Miranda.Roamie.Roaming.Profiles;
using Virtuoso.Miranda.Roamie.Roaming.Providers;
using Virtuoso.Miranda.Roamie.Properties;

namespace Virtuoso.Miranda.Roamie.Roaming.Packing
{
    [Serializable]
    internal sealed class PackingContainer : IDisposable
    {
        #region Fields

        private const string ContainerSuffix = "dbpc.bin";

        #endregion

        #region .ctors

        public PackingContainer(RoamingProfile profile)
        {
            if (profile == null)
                throw new ArgumentNullException("profile");

            this.profile = profile;
            this.files = new FileCollection();
        }

        ~PackingContainer()
        {
            Dispose();
        }

        #endregion

        #region Properties

        public bool IsDirty
        {
            get { return files.IsDirty; }
        }

        [NonSerialized]
        private RoamingProfile profile;
        public RoamingProfile Profile
        {
            get { return profile; }
        }

        private FileCollection files;
        public FileCollection Files
        {
            get { return files; }
        }
                       
        #endregion

        #region Methods

        private void Serialize(Stream destination)
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
                SecureStreamCompactor.CompressAndEncrypt(encryptedStream, destination, RoamiePlugin.Singleton.RoamingContext.ActiveProfile.DatabasePassword);
            }
        }

        private static PackingContainer Deserialize(Stream source, RoamingProfile profile)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            if (profile == null)
                throw new ArgumentNullException("profile");

            if (!source.CanRead)
                throw new ArgumentException();

            using (MemoryStream decryptedStream = new MemoryStream(8192))
            {
                SecureStreamCompactor.DecryptAndDecompress(source, decryptedStream, profile.DatabasePassword);
                decryptedStream.Seek(0, SeekOrigin.Begin);

                BinaryFormatter formatter = new BinaryFormatter();
                PackingContainer container = (PackingContainer)formatter.Deserialize(decryptedStream);

                return container;
            }
        }

        private static string GetContainerPath(RoamingProfile profile)
        {
            if (profile == null)
                throw new ArgumentNullException("profile");

            return String.Format("{0}.{1}", profile.RemoteHost, ContainerSuffix);
        }

        public static PackingContainer Load(RoamingProfile profile)
        {
            if (profile == null)
                throw new ArgumentNullException("profile");

            ISiteAdapter adapter = profile.GetProvider().Adapter;
            string containerPath = GetContainerPath(profile);

            if (!adapter.FileExists(profile, containerPath))
                return new PackingContainer(profile);
            else
            {
                using (Stream containerStream = adapter.PullFile(profile, containerPath))
                {
                    PackingContainer container = Deserialize(containerStream, profile);
                    container.profile = profile;

                    return container;
                }
            }
        }

        public void Deploy()
        {
            foreach (PackedFile file in files)
            {
                string path = file.Path;

                if (File.Exists(path))
                    File.Delete(path);

                File.WriteAllBytes(path, file.Stream.ToArray());
            }
        }

        public void Publish()
        {
            List<PackedFile> invalidFiles = new List<PackedFile>(1);

            foreach (PackedFile file in files)
            {
                try
                {
                    file.Prepare();
                }
                catch
                {
                    invalidFiles.Add(file);
                }
            }

            foreach (PackedFile file in invalidFiles)
                files.Remove(file);

            ISiteAdapter adapter = profile.GetProvider().Adapter;

            using (MemoryStream containerStream = new MemoryStream(4096))
            {
                Serialize(containerStream);
                containerStream.Seek(0, SeekOrigin.Begin);

                GlobalEvents.ChangeProgress(Resources.Text_UI_LogText_PublishingDelta, GlobalEvents.SignificantProgress.Running);
                adapter.PushFile(profile, GetContainerPath(profile), containerStream);
            }
        }

        public void Dispose()
        {
            foreach (PackedFile file in files)
                file.Dispose();
        }

        #endregion
    }
}
