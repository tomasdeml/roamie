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
using Virtuoso.Roamie.Roaming.Packing;
using Virtuoso.Roamie.Roaming.Profiles;

namespace Virtuoso.Roamie.RoamingProviders
{
    public abstract class PackingDatabaseProvider : DatabaseProvider
    {
        #region Fields

        private PackingContainer Container;

        #endregion
 
        #region Methods

        /// <summary>
        /// Retrieves and deploys files packed with the database. Must be called AFTER your synchonization implementation.
        /// </summary>
        /// <param name="profile">Roaming profile.</param>
        protected override void PerformLocalSiteSync(RoamingProfile profile)
        {
            try
            {
                Container = PackingContainer.Load(profile);
                Container.Deploy();
            }
            catch
            {
                ProgressMediator.ChangeProgress(Resources.Text_UI_LogText_CannotGetAttachedContainer);
                Container = new PackingContainer(profile);
            }
            finally
            {
                if (Container != null)
                    Container.Dispose();
            }
        }

        /// <summary>
        /// Publishes packed files if neccessary. Must be called AFTER your synchronization implementation.
        /// </summary>
        /// <param name="profile">Roaming profile.</param>
        protected override void PerformRemoteSiteSync(RoamingProfile profile)
        {
            SyncAttachedContainer();
        }

        /// <summary>
        /// Publishes packed files if neccessary. Must be called AFTER your synchronization implementation.
        /// </summary>
        public override void NonSyncShutdown()
        {
            if (!Context.IsInState(RoamingState.DiscardLocalChanges))
                SyncAttachedContainer();
        }

        /// <summary>
        /// Uploads attached files when neccessary.
        /// </summary>
        private void SyncAttachedContainer()
        {
            if (Container == null)
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
            
        /// <summary>
        /// Removes deployed files if required.
        /// </summary>
        /// <param name="localDbRemoved"></param>
        protected override void RemoveLocalData(bool localDbRemoved)
        {
            if (!localDbRemoved)
                return;

            foreach (PackedFile file in Container.Files)
                if (File.Exists(file.Path))
                    File.Delete(file.Path);
        }

        #endregion
    }
}
