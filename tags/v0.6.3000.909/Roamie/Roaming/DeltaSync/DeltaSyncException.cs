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
using System.Runtime.Serialization;
using Virtuoso.Miranda.Roamie.Properties;

namespace Virtuoso.Miranda.Roamie.Roaming.DeltaSync
{
    [Serializable]
    internal class DeltaSyncException : SyncException
    {
        public DeltaSyncException() : base(Resources.ExceptionMsg_DeltaSyncFailed) { }
        public DeltaSyncException(string message) : base(message) { }
        public DeltaSyncException(string message, Exception inner) : base(message, inner) { }
        protected DeltaSyncException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}