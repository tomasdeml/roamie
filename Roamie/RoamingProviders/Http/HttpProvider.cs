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
using System.Net;
using Virtuoso.Roamie.Properties;
using Virtuoso.Roamie.Roaming.Profiles;

namespace Virtuoso.Roamie.RoamingProviders.Http
{
    internal class HttpProvider : Provider
    {
        #region .ctors

        public HttpProvider()
        {
            adapter = new HttpSiteAdapter();
        }

        #endregion

        #region Properties

        public override string Name
        {
            get { return "HTTP (read-only)"; }
        }

        public override bool CredentialsRequired
        {
            get { return false; }
        }

        private readonly ISiteAdapter adapter;
        public override ISiteAdapter Adapter
        {
            get { return adapter; }
        }

        #endregion

        #region Methods

        public override void VerifyProfile(RoamingProfile profile)
        {
            try
            {
                HttpWebRequest request = HttpRequestFactory.CreateWebRequest(profile);

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    if (response.StatusCode != HttpStatusCode.OK || response.ContentType.ToLowerInvariant().StartsWith("text/htm"))
                        throw new SyncException(Resources.ExceptionMsg_SyncTestFailed_NotFound);
                }
            }
            catch (SyncException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new SyncException(Resources.ExceptionMsg_SyncTestFailed, e);
            }
        }

        #endregion
    }
}