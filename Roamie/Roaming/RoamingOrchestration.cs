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
using Virtuoso.Miranda.Roamie.Roaming.Providers;
using Virtuoso.Miranda.Roamie.Forms;
using System.Windows.Forms;
using System.Diagnostics;
using Virtuoso.Miranda.Roamie.Properties;
using Virtuoso.Miranda.Roamie.Roaming.DeltaSync;
using System.Threading;
using Virtuoso.Miranda.Roamie.Roaming.Packing;
using Virtuoso.Miranda.Plugins.Infrastructure;
using System.Media;

namespace Virtuoso.Miranda.Roamie.Roaming
{
    internal static class RoamingOrchestration
    {
        #region Properties

        private static RoamingContext _context;
        private static RoamingContext Context
        {
            get
            {
                return _context ?? (_context = RoamiePlugin.Singleton.RoamingContext);
            }
        }

        #endregion

        public static class Arrangement
        {
            /// <summary>
            /// Begins local synchronization. Called right after a user selects a roaming profile.
            /// </summary>
            public static void PerformLocalSynchronization()
            {
                try
                {
                    SyncDialog.RunModal(delegate
                    {
                        MethodInvoker syncChain = new MethodInvoker(SyncLocalDb) +
                                                  new MethodInvoker(DecideOnDeltaExistence);
                        syncChain();

                        Trace.WriteLineIf(RoamiePlugin.TraceSwitch.TraceInfo, "Synchronization completed.", RoamiePlugin.TraceCategory);
                        return null;
                    }, SyncDialog.SyncOptions.Repeatable | SyncDialog.SyncOptions.SilenceEligible);
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
                Context.ActiveProvider.SyncLocalDatabase(Context.ActiveProfile);
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
            /// Loads the Delta engine. Called after the user selects a roaming profile and local database is synchronized.
            /// </summary>
            public static void LoadDeltaEngine()
            {
                if (!(Context.ActiveProvider is IDeltaAwareProvider))
                    return;

                Context.DeltaEngine = new DeltaSyncEngine(Context.ActiveProfile);
                SyncDialog.RunModal(DeltaEngineInitializer, SyncDialog.SyncOptions.SilenceEligible | SyncDialog.SyncOptions.Unrepeatable | SyncDialog.SyncOptions.NoThrow);
            }

            private static object DeltaEngineInitializer()
            {
                bool engineLoaded = InitializeDeltaEngine();

                try
                {
                    if (engineLoaded)
                    {
                        // Set during the Arrangement when a provider is delta aware
                        if (Context.IsInState(RoamingState.ApplyNeccessaryDeltas))
                            if (!Context.DeltaEngine.ApplyDeltas())
                                SystemSounds.Exclamation.Play();
                    }
                }
                catch (Exception e)
                {
                    string message = String.Format(Resources.MsgBox_Formatable2Text_ErrorWhileApplyingDeltas, Environment.NewLine, e.Message);
                    Trace.WriteLineIf(RoamiePlugin.TraceSwitch.TraceError, RoamiePlugin.TraceCategory, message);

                    throw new DeltaSyncException(message, e);
                }
                finally
                {
                    Context.DeltaEngine.BeginReplication();
                }

                return null;
            }

            private static bool InitializeDeltaEngine()
            {
                bool loadEngine;

                try
                {
                    Context.DeltaEngine.Initialize();
                    loadEngine = true;
                }
                catch (DbTokenMismatchException)
                {
                    loadEngine = HandleDbTokenMismatch();
                }
                catch (Exception e)
                {
                    // Unknown error during engine init, mark it unloaded
                    Context.State &= ~RoamingState.DeltaSyncEngineLoaded;
                    throw new DeltaSyncException(Resources.ExceptionMsg_UnableToInitDeltaEngine + e.Message, e);
                }

                return loadEngine;
            }

            private static bool HandleDbTokenMismatch()
            {
                using (DbTokenMismatchDialog dlg = new DbTokenMismatchDialog())
                {
                    if (DialogResult.Cancel == dlg.ShowDialog())
                    {
                        lock (Context.SyncObject)
                            Context.DeactivateProfile();

                        return false;
                    }
                    else
                    {
                        if (Context.DeltaEngine.LegacyManifest != null)
                            Context.DeltaEngine.LegacyManifest.Remove();

                        return true;
                    }
                }
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
                    Context.DeltaEngine.FinalizeDelta();
            }

            /// <summary>
            /// Performs remote database synchronization. Called during the unload.
            /// </summary>
            public static void PerformRemoteSync()
            {
                MethodInvoker syncChain = null;

                if (Context.IsInState(RoamingState.DiscardLocalChanges))
                {
                    syncChain = delegate { RoamiePlugin.Singleton.RoamingContext.ActiveProvider.NonSyncShutdown(); };
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

                // Workaround for Clist_modern, otherwise we get an Access Violation exception...
                Thread syncThread = new Thread(RemoteSyncPerformer);

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
                Context.ActiveProvider.SyncRemoteDatabase(Context.ActiveProfile);
                Trace.WriteLineIf(RoamiePlugin.TraceSwitch.TraceInfo, "Synchronization completed.", RoamiePlugin.TraceCategory);
            }

            /// <summary>
            /// Performs delta synchronization.
            /// </summary>
            private static void DeltaSyncRemoteDb()
            {
                Context.DeltaEngine.PublishDelta();
                Context.ActiveProvider.NonSyncShutdown();

                Trace.WriteLineIf(RoamiePlugin.TraceSwitch.TraceInfo, "Delta synchronization completed.", RoamiePlugin.TraceCategory);
            }

            /// <summary>
            /// Performs full synchronization and removes deltas.
            /// </summary>
            private static void FullSyncRemoteDbAndRemoveDeltas()
            {
                FullSyncRemoteDb();

                GlobalEvents.ChangeProgress(Resources.Text_UI_LogText_RemovingDeltas, GlobalEvents.SignificantProgress.Running);
                Context.DeltaEngine.DeltaManifest.Remove();
            }
        }
    }
}
