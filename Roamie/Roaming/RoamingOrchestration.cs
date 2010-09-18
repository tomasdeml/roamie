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
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using Virtuoso.Roamie.Forms;
using Virtuoso.Roamie.Roaming.DeltaSync;
using Virtuoso.Roamie.RoamingProviders;
using Virtuoso.Roamie.Properties;

namespace Virtuoso.Roamie.Roaming
{
    internal static class RoamingOrchestration
    {
        #region Fields

        private static RoamingContext Context
        {
            get { return RoamiePlugin.Singleton.RoamingContext; }
        }

        #endregion

        #region Preparation

        /// <summary>
        /// Begins local synchronization. Called right after user selects a roaming profile.
        /// </summary>
        public static void SyncLocalSite()
        {
            try
            {
                SyncDialog.RunModal(DoSyncLocalSite, SyncOptions.Repeatable | SyncOptions.Silenceable);
            }
            catch
            {
                Context.State |= RoamingState.SyncErrorOccured;

                Context.DeactivateProfile();
                Context.RestoreProfilePath();

                throw;
            }
        }

        /// <summary>
        /// Synchronizes local database.
        /// </summary>
        private static object DoSyncLocalSite()
        {
            Context.ActiveProvider.SyncLocalSite(Context.ActiveProfile);
            return null;
        }

        #endregion

        #region Finale

        /// <summary>
        /// Performs remote database synchronization. Called during the unload.
        /// </summary>
        public static void SyncRemoteSite()
        {
            try
            {
                SyncDialog.RunModal(DoSyncRemoteSite, SyncOptions.Repeatable);
            }
            catch (Exception e)
            {
                Trace.WriteLineIf(RoamiePlugin.TraceSwitch.TraceError, "=== Synchronization failed ===",
                                  RoamiePlugin.TraceCategory);
                Trace.WriteLineIf(RoamiePlugin.TraceSwitch.TraceError, e.ToString(),
                                  RoamiePlugin.TraceCategory);
            }
        }

        private static object DoSyncRemoteSite()
        {
            DatabaseProvider provider = Context.ActiveProvider;

            provider.SyncRemoteSite(Context.ActiveProfile);
            provider.RemoveLocalSiteData();

            return null;
        }

        #endregion
    }
}
