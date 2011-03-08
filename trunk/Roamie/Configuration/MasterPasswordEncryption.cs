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
using System.Security.Cryptography;
using System.Windows.Forms;
using Virtuoso.Miranda.Plugins.Configuration;
using Virtuoso.Roamie.Forms;
using Virtuoso.Roamie.Properties;

namespace Virtuoso.Roamie.Configuration
{
    internal sealed class MasterPasswordEncryption : PortableEncryption
    {
        #region Fields

        private static string Key;

        #endregion			
        
        #region Encryption

        protected override string PromptForKey(bool decrypting)
        {
            if (!String.IsNullOrEmpty(Key))
                return Key;

            Key = MasterPasswordDialog.Prompt(decrypting);
            return Key;
        }

        public override byte[] Decrypt(byte[] data)
        {
            try
            {
                byte[] decryptedData = base.Decrypt(data);
                return decryptedData;
            }
            catch (CryptographicException e)
            {
                Key = null;

                MessageBox.Show(Resources.MsgBox_Text_InvalidMasterKey, Resources.MsgBox_Title_InvalidMasterKey, MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw new OperationCanceledException(Resources.MsgBox_Text_InvalidMasterKey, e);
            }
        }

        #endregion
    }
}
