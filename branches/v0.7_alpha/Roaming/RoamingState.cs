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

namespace Virtuoso.Miranda.Roamie.Roaming
{
    [Flags]
    public enum RoamingState
    {
        /// <summary>
        /// Roaming is disabled.
        /// </summary>
        Disabled = 1,
        
        /// <summary>
        /// Roaming is enabled.
        /// </summary>
        Active = 2,

        /// <summary>
        /// Roaming is disabled, because the local DB is loaded.
        /// </summary>
        LocalDbInUse = 4,
        
        /// <summary>
        /// Roamed DB should be deleted on exit.
        /// </summary>
        WipeLocalDbOnExit = 8,

        /// <summary>
        /// The changes made to the local DB should be discarded on exit.
        /// </summary>
        DiscardLocalChanges = 16,
                
        /// <summary>
        /// New DB was created and loaded, roaming may be disabled.
        /// </summary>
        CreateNewDb = 32,

        /// <summary>
        /// An error occured during a synchronization.
        /// </summary>
        SyncErrorOccured = 64,

        /// <summary>
        /// Active DB provider cannot mirror local changes.
        /// </summary>
        MirroringNotSupported = 128,

        /// <summary>
        /// Delta engine is being utilized.
        /// </summary>
        DeltaSyncEngineLoaded = 256,
        
        /// <summary>
        /// Deltas should be applied, if exist.
        /// </summary>
        ApplyNeccessaryDeltas = 512,

        /// <summary>
        /// A change incompatible with the Delta engine occured.
        /// </summary>
        DeltaIncompatibleChangeOccured = 1024,

        /// <summary>
        /// The full synchronization is preferred.
        /// </summary>
        PreferFullSync = 2048,
    }
}
