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
using Virtuoso.Miranda.Roamie.Roaming.Profiles;

namespace Virtuoso.Miranda.Roamie.Forms.Controls
{
    partial class ProfileEditor
    {
        internal sealed class SaveEventArgs : EventArgs
        {
            #region Fields

            private readonly RoamingProfile profile, originalProfile;
            private readonly ProfileEditor.EditingMode commitMode;

            public RoamingProfile OriginalProfile
            {
                get { return originalProfile; }
            } 

            public ProfileEditor.EditingMode CommitMode
            {
                get { return commitMode; }
            }

            public RoamingProfile Profile
            {
                get { return profile; }
            }

            #endregion

            #region .ctors

            public SaveEventArgs(RoamingProfile profile, RoamingProfile originalProfile, ProfileEditor.EditingMode mode)
            {
                if (profile == null) throw new ArgumentNullException("profile");

                this.profile = profile;
                this.originalProfile = originalProfile;
                this.commitMode = mode;
            }

            #endregion
        }
    }
}
