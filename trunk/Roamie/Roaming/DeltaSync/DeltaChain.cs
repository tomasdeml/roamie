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
using System.IO;
using Virtuoso.Miranda.Roamie.Roaming.Profiles;
using Virtuoso.Miranda.Plugins.Infrastructure;
using Virtuoso.Miranda.Plugins;
using Virtuoso.Miranda.Roamie.Properties;
using System.Net;

namespace Virtuoso.Miranda.Roamie.Roaming.DeltaSync
{
    internal class DeltaChain
    {
        #region Constants

        private const string MS_DB_SETSAFETYMODE = "DB/SetSafetyMode";

        #endregion

        #region Properties

        private DeltaManifest manifest;
        public DeltaManifest Manifest
        {
            get { return manifest; }
        }

        #endregion

        #region .ctors

        public DeltaChain(DeltaManifest manifest)
        {
            if (manifest == null)
                throw new ArgumentNullException("manifest");

            this.manifest = manifest;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Merges all deltas with the database.
        /// </summary>
        /// <returns>TRUE if all deltas were applied successfully; FALSE if there were some entiries within deltas that failed to be applied.</returns>
        /// <exception cref="DeltaSyncException">Some deltas could not be applied.</exception>
        public bool MergeWithDatabase()
        {
            lock (Manifest)
            {
                if (Manifest.IsNew)
                    throw new InvalidOperationException();

                Callback setSafetyMode = ServiceManager.GetService(MS_DB_SETSAFETYMODE);
                setSafetyMode((UIntPtr)0, IntPtr.Zero);

                try 
                {
                    ISiteAdapter adapter = Manifest.AssociatedProfile.GetProvider().Adapter;
                    RoamingProfile profile = Manifest.AssociatedProfile;

                    Exception lastDeltaException = null;

                    bool completed = true;

                    int deltaNo = 0;
                    double deltaCountPercent = (double)Manifest.DeltaCount / 100D;                    

                    foreach (string deltaPath in Manifest.GetDeltaPaths())
                    {
                        deltaNo++;

                        try
                        {
                            using (Stream deltaStream = adapter.PullFile(profile, deltaPath))
                            {
                                ProgressMediator.ChangeProgress(String.Format(Resources.Text_UI_LogText_Formatable1_DownloadingDelta, deltaNo.ToString()), (int)((double)deltaNo / deltaCountPercent));
                                Delta delta = Delta.Deserialize(deltaStream, Manifest);

                                ProgressMediator.ChangeProgress(Resources.Text_UI_LogText_ApplyingDelta);
                                completed &= delta.Apply();
                            }
                        }
                        catch (WebException wE)
                        {
                            lastDeltaException = new DeltaSyncException(StringUtility.FormatExceptionMessage(Resources.ExceptionMsg_UnableToDownloadDelta, wE, false), wE);
                        }
                        catch (Exception e)
                        {
                            lastDeltaException = new DeltaSyncException(StringUtility.FormatExceptionMessage(Resources.ExceptionMsg_DeltaMergeFailed, e, false), e);
                        }
                    }

                    if (lastDeltaException != null)
                        throw lastDeltaException;
                                        
                    return completed;
                }                
                finally
                {
                    setSafetyMode((UIntPtr)1, IntPtr.Zero);

                    // Collect deserialized deltas
                    GC.Collect(0);
                }
            }
        }

        #endregion
    }
}
