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

namespace Virtuoso.Miranda.Roamie.Roaming.DeltaSync
{
    internal static class DeltaSettingEntriesFilter
    {
        private static string[] OmissibleSettingOwners = new string[] { "UserOnline" };

        public static void InspectIfOmissible(DeltaEntry entry)
        {
            if (entry == null)
                throw new ArgumentNullException("entry");

            DeltaSettingEntry settingEntry = entry as DeltaSettingEntry;

            if (settingEntry != null)
                settingEntry.Omissible = Array.IndexOf<string>(OmissibleSettingOwners, settingEntry.Owner) != -1;
        }

        public static bool CheckForDuplicitEntries(LinkedList<DeltaEntry> entries)
        {
            if (entries == null)
                throw new ArgumentNullException("entries");

            bool duplicitFound = false;

            List<DeltaEntry> entriesList = new List<DeltaEntry>(entries);            
            Dictionary<DeltaEntry, byte> hashList = new Dictionary<DeltaEntry, byte>(entries.Count / 2);

            for (int i = entries.Count - 1; i >= 0; i--)
            {
                DeltaEntry entry1 = entriesList[i];

                if (entry1.Omissible)
                    continue;

                if (!hashList.ContainsKey(entry1))
                    hashList.Add(entry1, 0);
                else
                    entry1.Omissible = true;
            }

            return duplicitFound;
        }
    }
}
