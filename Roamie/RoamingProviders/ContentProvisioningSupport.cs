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
using System.Diagnostics;
using System.IO;
using Virtuoso.Roamie.Properties;
using Virtuoso.Roamie.Roaming;
using Virtuoso.Roamie.Roaming.Profiles;
using Virtuoso.Roamie.Roaming.Provisioning;

namespace Virtuoso.Roamie.RoamingProviders
{
    internal class ContentProvisioningSupport : ProviderDecorator
    {
        #region Fields

        private ProvisioningContainer Container;

        #endregion

        #region .ctors

        public ContentProvisioningSupport(DatabaseProvider provider) : base(provider)
        {
        }

        #endregion

        #region Methods

        public override void  SyncLocalSite(RoamingProfile profile)
        {
            base.SyncLocalSite(profile);

            try
            {
                Container = ProvisioningContainer.Load(profile);
                Container.Deploy();
            }
            catch
            {
                ProgressMediator.ChangeProgress(Resources.Text_UI_LogText_CannotGetAttachedContainer);
                Container = new ProvisioningContainer(profile);
            }
            finally
            {
                if (Container != null)
                    Container.Dispose();
            }
        }

        public override void SyncRemoteSite(RoamingProfile profile)
        {
            base.SyncRemoteSite(profile);

            if (Container == null || Container.Contents.Count == 0)
                return;

            try
            {
                Container.Publish();
                Trace.WriteLineIf(RoamiePlugin.TraceSwitch.TraceInfo, "Attached files synchronization completed.", RoamiePlugin.TraceCategory);
            }
            catch (Exception e)
            {
                Trace.WriteLineIf(RoamiePlugin.TraceSwitch.TraceInfo, "Attached files synchronization failed. " + e.ToString(), RoamiePlugin.TraceCategory);
            }
            finally
            {
                Container.Dispose();
            }
        }

        public override void RemoveLocalSiteData()
        {
            base.RemoveLocalSiteData();

            foreach (Content file in Container.Contents)
                if (File.Exists(file.Path))
                    File.Delete(file.Path);
        }

        #endregion
    }
}
