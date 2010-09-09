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
using Virtuoso.Miranda.Plugins.Native;
using Virtuoso.Miranda.Plugins.Infrastructure;
using Virtuoso.Miranda.Plugins;
using System.Runtime.InteropServices;
using Virtuoso.Miranda.Roamie.Properties;
using System.Diagnostics;

namespace Virtuoso.Miranda.Roamie.Roaming.DeltaSync
{
    [Serializable]
    internal abstract class DeltaEntry
    {
        #region Fields

        private Guid contactToken;
        public Guid ContactToken
        {
            get { return contactToken; }
            set { contactToken = value; }
        }        

        private bool merged;
        public bool Merged
        {
            get { return merged; }
        }

        [NonSerialized]
        private bool omissible;
        public bool Omissible
        {
            get { return omissible; }
            set { omissible = value; }
        }

        [NonSerialized]
        private ContactInfo Contact;

        #endregion

        #region .ctors

        protected DeltaEntry(IntPtr contactHandle)
        {
            Guid? token = DeltaSyncEngine.GetContactToken(contactHandle);

            if (token == null)
                throw new DeltaSyncException(Resources.ExceptionMsg_ContactWithoutTokenFound);
            else
                contactToken = token.Value;
        }

        #endregion

        #region Methods

        public override int GetHashCode()
        {
            return contactToken.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj.GetType() != this.GetType())
                return false;

            return (obj.GetHashCode() == this.GetHashCode());
        }

        public ContactInfo GetContact()
        {
            return Contact ?? (Contact = DeltaSyncEngine.GetContactFromToken(contactToken));
        }

        public bool Merge()
        {
            if (merged)
                throw new InvalidOperationException();
                        
            merged = true;

            try
            {
                return PerformMerge();
            }
            catch (Exception e)
            {
                GlobalEvents.ChangeProgress(GlobalEvents.FormatExceptionMessage(Resources.ExceptionMsg_DeltaMergeFailed, e));
                return false;
            }
        }

        protected abstract bool PerformMerge();

        #endregion
    }
}
