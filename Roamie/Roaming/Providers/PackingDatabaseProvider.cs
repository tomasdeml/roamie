﻿/***********************************************************************************\
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
using Virtuoso.Miranda.Roamie.Roaming.Profiles;
using Virtuoso.Miranda.Roamie.Roaming.Packing;
using Virtuoso.Miranda.Roamie.Properties;
using System.Diagnostics;
using System.IO;

namespace Virtuoso.Miranda.Roamie.Roaming.Providers
{
    public abstract class PackingDatabaseProvider : DatabaseProvider
    {
        #region Fields

        private PackingContainer Container;

        #endregion

        #region .ctors

        protected PackingDatabaseProvider() { }

        #endregion       

        #region Impl

        /// <summary>
        /// Retrieves and deploys files packed with the database. Must be called AFTER your synchonization implementation.
        /// </summary>
        /// <param name="profile">Roaming profile.</param>
        public override void SyncLocalDatabase(RoamingProfile profile)
        {
            try
            {
                using (PackingContainer container = (Container = PackingContainer.Load(profile)))
                    Container.Deploy();

                if (Container.Files.Count > 0)
                    GC.Collect(0);
            }
            catch
            {
                ProgressMediator.ChangeProgress(Resources.Text_UI_LogText_CannotGetAttachedContainer);
                Container = new PackingContainer(profile);
            }
        }

        /// <summary>
        /// Publishes packed files if neccessary. Must be called AFTER your synchronization implementation.
        /// </summary>
        /// <param name="profile">Roaming profile.</param>
        public override void SyncRemoteDatabase(RoamingProfile profile)
        {
            SyncAttachedContainer();
        }

        /// <summary>
        /// Publishes packed files if neccessary. Must be called AFTER your synchronization implementation.
        /// </summary>
        /// <param name="profile">Roaming profile.</param>
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
            if (Container == null || !Container.IsDirty)
                return;

            try
            {
                using (PackingContainer container = Container)
                    container.Publish();

                Trace.WriteLineIf(RoamiePlugin.TraceSwitch.TraceInfo, "Attached files synchronization completed.", RoamiePlugin.TraceCategory);
            }
            catch (Exception e)
            {
                Trace.WriteLineIf(RoamiePlugin.TraceSwitch.TraceInfo, "Attached files synchronization failed. " + e.ToString(), RoamiePlugin.TraceCategory);
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
