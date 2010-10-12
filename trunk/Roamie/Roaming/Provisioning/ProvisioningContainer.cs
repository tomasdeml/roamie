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
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Virtuoso.Roamie.Properties;
using Virtuoso.Roamie.Roaming.Profiles;
using Virtuoso.Roamie.RoamingProviders;

namespace Virtuoso.Roamie.Roaming.Provisioning
{
    [Serializable]
    internal sealed class ProvisioningContainer : IDisposable
    {
        #region Fields

        private const string ContainerSuffix = "-cnt.bin";

        #endregion

        #region .ctors

        public ProvisioningContainer(RoamingProfile profile)
        {
            if (profile == null)
                throw new ArgumentNullException("profile");

            this.profile = profile;
            Contents = new ContentCollection();
        }

        ~ProvisioningContainer()
        {
            Dispose();
        }

        #endregion

        #region Properties

        public bool IsDirty
        {
            get { return Contents.IsDirty; }
        }

        [NonSerialized]
        private RoamingProfile profile;
        public RoamingProfile Profile
        {
            get { return profile; }
        }

        public ContentCollection Contents { get; private set; }

        #endregion

        #region Methods

        private static string GetContainerPath(RoamingProfile profile)
        {
            if (profile == null)
                throw new ArgumentNullException("profile");

            return String.Format("{0}.{1}", profile.RemoteHost, ContainerSuffix);
        }

        public static ProvisioningContainer Load(RoamingProfile profile)
        {
            if (profile == null)
                throw new ArgumentNullException("profile");

            ISiteAdapter adapter = profile.GetProvider().Adapter;
            string containerPath = GetContainerPath(profile);

            if (!adapter.FileExists(profile, containerPath))
                return new ProvisioningContainer(profile);

            using (MemoryStream containerStream = new MemoryStream())
            {
                adapter.PullFile(profile, containerPath, containerStream);

                BinaryFormatter formatter = new BinaryFormatter();
                ProvisioningContainer container = (ProvisioningContainer)formatter.Deserialize(containerStream);
                container.profile = profile;

                return container;
            }
        }

        public void Deploy()
        {
            foreach (Content content in Contents)
                File.WriteAllBytes(content.Path, content.Stream.ToArray());
        }

        public void Publish()
        {
            List<Content> invalidContent = new List<Content>(1);

            foreach (Content content in Contents)
            {
                try
                {
                    content.Prepare();
                }
                catch
                {
                    invalidContent.Add(content);
                }
            }

            foreach (Content content in invalidContent)
                Contents.Remove(content);

            ISiteAdapter adapter = profile.GetProvider().Adapter;

            using (MemoryStream containerStream = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(containerStream, this);
                containerStream.Seek(0, SeekOrigin.Begin);

                ProgressMediator.ChangeProgress(Resources.Text_UI_LogText_PublishingContent, SignificantProgress.Running);
                adapter.PushFile(profile, containerStream, GetContainerPath(profile), true);
            }
        }

        public void Dispose()
        {
            foreach (Content file in Contents)
                file.Dispose();
        }

        #endregion
    }
}
