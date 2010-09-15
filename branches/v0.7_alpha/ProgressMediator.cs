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

using System.Diagnostics;

namespace Virtuoso.Miranda.Roamie
{
    internal static class ProgressMediator
    {
        #region Events

        public delegate void LogPipe(string message, int progress);
        public static event LogPipe ProgressChange;

        #endregion

        #region Methods

        public static void ChangeProgress(string message)
        {
            ChangeProgress(message, SignificantProgress.NoChange);
        }

        public static void ChangeProgress(string message, SignificantProgress progress)
        {
            ChangeProgress(message, (int)progress);
        }

        public static void ChangeProgress(string message, int progress)
        {
            if (ProgressChange != null)
                ProgressChange(message, progress);

            Trace.WriteLineIf(RoamiePlugin.TraceSwitch.TraceVerbose, message);
        }

        #endregion
    }

    internal enum SignificantProgress
    {
        Running = -1,
        NoChange = -2,
        Stopped = -3,
        Complete = -4,
    }
}
