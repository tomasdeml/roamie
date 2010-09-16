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

        public static class Preparation
        {
            /// <summary>
            /// Begins local synchronization. Called right after user selects a roaming profile.
            /// </summary>
            public static void PerformLocalSynchronization()
            {
                try
                {
                    SyncDialog.RunModal(() =>
                    {
                        MethodInvoker syncChain = new MethodInvoker(SyncLocalDb) +
                                                  DecideOnDeltaExistence;
                        syncChain();

                        Trace.WriteLineIf(RoamiePlugin.TraceSwitch.TraceInfo,
                                          "Synchronization completed.",
                                          RoamiePlugin.TraceCategory);
                        return null;
                    }, SyncOptions.Repeatable | SyncOptions.Silenceable);
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
            private static void SyncLocalDb()
            {
                Context.ActiveProvider.SyncLocalSite(Context.ActiveProfile);
            }

            /// <summary>
            /// Decides whether delta engine should apply deltas.
            /// </summary>
            private static void DecideOnDeltaExistence()
            {
                if (Context.ActiveProvider is IDeltaAwareProvider)
                    Context.State |= RoamingState.ApplyNeccessaryDeltas;
            }
        }

        public static class Performance
        {
            /// <summary>
            /// Loads the Delta engine. 
            /// Called after the user selects a roaming profile and local database is synchronized.
            /// </summary>
            public static void LoadDeltaEngine()
            {
                if (!(Context.ActiveProvider is IDeltaAwareProvider))
                    return;

                DeltaSyncEngineFactory.GetEngine().Initialize(Context.ProfilePath);
                SyncDialog.RunModal(DeltaEngineInitializer, SyncOptions.Silenceable | SyncOptions.Unrepeatable | SyncOptions.NoThrow);
            }

            private static object DeltaEngineInitializer()
            {
                try
                {
                    // Set during the Arrangement when a provider is delta aware
                    if (Context.IsInState(RoamingState.ApplyNeccessaryDeltas))
                    {
                        // TODO
                    }
                }
                catch (Exception e)
                {
                    string message = String.Format(Resources.MsgBox_Formatable2Text_ErrorWhileApplyingDeltas, Environment.NewLine, e.Message);
                    Trace.WriteLineIf(RoamiePlugin.TraceSwitch.TraceError, RoamiePlugin.TraceCategory, message);

                    throw new DeltaSyncException(message, e);
                }

                return null;
            }
        }

        public static class Finale
        {
            /// <summary>
            /// Finalizes the delta. Called right at the begin of unload.
            /// </summary>
            public static void FinalizeDeltaEngine()
            {
                if (Context.IsInState(RoamingState.DeltaSyncEngineLoaded) && !Context.IsInState(RoamingState.DiscardLocalChanges))
                {
                    // TODO Finalize Delta
                }
            }

            /// <summary>
            /// Performs remote database synchronization. Called during the unload.
            /// </summary>
            public static void PerformRemoteSync()
            {
                DatabaseProvider databaseProvider = RoamiePlugin.Singleton.RoamingContext.ActiveProvider;
                MethodInvoker syncChain;

                if (Context.IsInState(RoamingState.DiscardLocalChanges))
                {
                    syncChain = () => databaseProvider.NonSyncShutdown();
                    Trace.WriteLineIf(RoamiePlugin.TraceSwitch.TraceInfo, "Sandbox mode active, no synchronization required => shutting down the database provider...", RoamiePlugin.TraceCategory);
                }
                else
                {
                    // Full sync is default
                    syncChain = FullSyncRemoteDb;
                    Trace.WriteLineIf(RoamiePlugin.TraceSwitch.TraceInfo, "Sandbox mode inactive, synchronization required => starting...", RoamiePlugin.TraceCategory);

                    // Engine loaded
                    if (Context.IsInState(RoamingState.DeltaSyncEngineLoaded))
                    {
                        if (Context.IsInState(RoamingState.DeltaIncompatibleChangeOccured) || Context.IsInState(RoamingState.PreferFullSync))
                            syncChain = FullSyncRemoteDbAndRemoveDeltas;
                        else
                            syncChain = DeltaSyncRemoteDb;
                    }
                }

                syncChain += databaseProvider.RemoveLocalDb;

                // Workaround for Clist_modern, otherwise we get an Access Violation exception...
                Thread syncThread = new Thread(RemoteSyncPerformer) {IsBackground = false};
                syncThread.SetApartmentState(ApartmentState.STA);

                syncThread.Start(syncChain);
                syncThread.Join();
            }

            private static void RemoteSyncPerformer(object syncChain)
            {
                SyncDialog.RunModal(delegate
                {
                    try
                    {
                        ((MethodInvoker)syncChain)();
                    }
                    catch (Exception e)
                    {
                        Trace.WriteLineIf(RoamiePlugin.TraceSwitch.TraceError, "=== Synchronization failed ===", RoamiePlugin.TraceCategory);
                        Trace.WriteLineIf(RoamiePlugin.TraceSwitch.TraceError, e.ToString(), RoamiePlugin.TraceCategory);
                    }

                    return null;
                });
            }

            /// <summary>
            /// Performs full synchronization.
            /// </summary>
            private static void FullSyncRemoteDb()
            {
                Context.ActiveProvider.SyncRemoteSite(Context.ActiveProfile);
                Trace.WriteLineIf(RoamiePlugin.TraceSwitch.TraceInfo, "Synchronization completed.", RoamiePlugin.TraceCategory);
            }

            /// <summary>
            /// Performs delta synchronization.
            /// </summary>
            private static void DeltaSyncRemoteDb()
            {
                // TODO
                //DeltaSyncEngineFactory.GetEngine().CreateDelta();
                Context.ActiveProvider.NonSyncShutdown();

                Trace.WriteLineIf(RoamiePlugin.TraceSwitch.TraceInfo, "Delta synchronization completed.", RoamiePlugin.TraceCategory);
            }

            /// <summary>
            /// Performs full synchronization and removes deltas.
            /// </summary>
            private static void FullSyncRemoteDbAndRemoveDeltas()
            {
                FullSyncRemoteDb();

                ProgressMediator.ChangeProgress(Resources.Text_UI_LogText_RemovingDeltas, SignificantProgress.Running);
                // TODO
                //Context.DeltaEngine.DeltaManifest.Remove();
            }
        }
    }
}
