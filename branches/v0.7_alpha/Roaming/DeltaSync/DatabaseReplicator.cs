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
using Virtuoso.Miranda.Plugins.Infrastructure;
using Virtuoso.Miranda.Roamie.Properties;
using System.Windows.Forms;

namespace Virtuoso.Miranda.Roamie.Roaming.DeltaSync
{
    partial class DeltaSyncEngine
    {
        internal class DatabaseReplicator
        {
            #region Fields

            private const string PhotonService_IsSchemaContact = "Photon/S_IsSchemaContact";
            private const string UnknownContact = "(unknown contact)";

            private readonly DeltaSyncEngine Engine;
            private bool Initialized;

            #endregion

            #region .ctors

            public DatabaseReplicator(DeltaSyncEngine engine)
            {
                if (engine == null)
                    throw new ArgumentNullException("engine");

                this.Engine = engine;
            }

            #endregion

            #region Methods

            public void BeginReplication()
            {
                if (Initialized)
                    return;

                Initialized = true;
                MirandaContext.Current.ModulesLoaded += SetUpReplication;
            }

            private void SetUpReplication(object sender, EventArgs e)
            {
                MirandaContext.Current.ModulesLoaded -= SetUpReplication;
                HookDbEvents();
            }

            public void DisableReplication()
            {
                UnhookDbEvents();
            }

            private void HookDbEvents()
            {
                MirandaDatabase db = MirandaContext.Current.MirandaDatabase;

                db.ContactAdded += HandleContactChange;
                db.ContactDeleted += HandleContactChange;
                db.ContactSettingChanged += HandleContactSettingChange;
            }

            private void UnhookDbEvents()
            {
                MirandaDatabase db = MirandaContext.Current.MirandaDatabase;

                db.ContactAdded -= HandleContactChange;
                db.ContactDeleted -= HandleContactChange;
                db.ContactSettingChanged -= HandleContactSettingChange;
            }

            #endregion

            #region Handlers
                        
            private bool HandleContactChange(object sender, MirandaContactEventArgs e)
            {
                if (!e.ContactInfo.DisplayName.Equals(UnknownContact, StringComparison.OrdinalIgnoreCase) &&
                    (!ServiceManager.ServiceExists(PhotonService_IsSchemaContact) || 
                    MirandaContext.Current.CallService(PhotonService_IsSchemaContact, e.ContactInfo.MirandaHandle, IntPtr.Zero) != 0))
                {
                    UnhookDbEvents();
                    RoamiePlugin.Singleton.RoamingContext.State |= RoamingState.DeltaIncompatibleChangeOccured;

                    NotifyAboutIncompatibleDbChange();
                }

                return EventResult.HonourEventChain;
            }

            private void NotifyAboutIncompatibleDbChange()
            {
                MirandaContext.Current.ContactList.ShowBaloonTip(Resources.Balloon_Title_DeltaWarning, Resources.Text_UI_RoamingStatus_DeltaIncompatibleChange, null, ToolTipIcon.Warning, 10000);
            }

            private bool HandleContactSettingChange(object sender, MirandaContactSettingEventArgs e)
            {
                if (e.ValueType != DatabaseSettingType.Blob || e.ValueType == DatabaseSettingType.Deleted)
                {
                    Delta delta = Engine.CurrentDelta;

                    lock (delta)
                    {
                        if (DeltaSyncEngine.GetContactToken(e.ContactInfo.MirandaHandle) != null)
                            delta.Entries.AddLast(new DeltaSettingEntry(e.ContactInfo.MirandaHandle, e.SettingName, e.SettingOwner, e.Value, e.ValueType));
                    }
                }

                return false;
            }

            #endregion
        }
    }
}