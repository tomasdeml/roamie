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
using Virtuoso.Miranda.Plugins.Infrastructure;
using System.Diagnostics;

namespace Virtuoso.Miranda.Roamie.Roaming.DeltaSync
{
    [Serializable, DebuggerDisplay("Name = {Name}, Owner = {Owner}, Value = {Value}")]
    internal class DeltaSettingEntry : DeltaEntry
    {
        #region Properties

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string owner;
        public string Owner
        {
            get { return owner; }
            set { owner = value; }
        }

        private object value;
        public object Value
        {
            get { return value; }
            set { this.value = value; }
        }

        private DatabaseSettingType valueType;
        public DatabaseSettingType ValueType
        {
            get { return valueType; }
            set { valueType = value; }
        }

        #endregion

        #region .ctors

        public DeltaSettingEntry(IntPtr contactHandle, string name, string owner, object value, DatabaseSettingType valueType) : base(contactHandle)
        {
            if (String.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");

            this.name = name;
            this.owner = owner;
            this.value = value;
            this.valueType = valueType;
        }

        #endregion

        #region Methods

        public override int GetHashCode()
        {
            return base.GetHashCode() + name.GetHashCode() + owner.GetHashCode() + valueType.GetHashCode();
        }

        protected override bool PerformMerge()
        {
            ContactInfo contactInfo = GetContact();

            if (ValueType != DatabaseSettingType.Deleted)
                return contactInfo.WriteSetting(Name, Owner, Value, ValueType);
            else
                return contactInfo.DeleteSetting(Name, Owner);
        }

        #endregion
    }
}
