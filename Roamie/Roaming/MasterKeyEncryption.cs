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
using Virtuoso.Miranda.Plugins.Infrastructure;
using Virtuoso.Roamie.Properties;

// TODO Get rid of and generate password

namespace Virtuoso.Roamie.Roaming
{
    internal sealed class MasterKeyEncryption : PortableEncryption
    {
        #region Fields

        private static string Key;

        #endregion			
        
        #region Encryption

        protected override string PromptForKey(bool decrypting)
        {
            if (!String.IsNullOrEmpty(Key) || TryLoadKey())
                return Key;

            Key = Forms.MasterKeyDialog.Prompt(decrypting);

            if (!decrypting)
                CacheKey();

            return Key;
        }

        public override byte[] Decrypt(byte[] data)
        {
            try
            {
                byte[] decryptedData = base.Decrypt(data);
                CacheKey();

                return decryptedData;
            }
            catch (CryptographicException e)
            {
                MessageBox.Show(Resources.MsgBox_Text_InvalidMasterKey, Resources.MsgBox_Title_InvalidMasterKey, MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw new OperationCanceledException(Resources.MsgBox_Text_InvalidMasterKey, e);
            }
        }

        #endregion			
        
        #region MasterKey Cache

        private bool TryLoadKey()
        {
            Key = PluginConfiguration.Load<MasterKeyCache>().Key;
            return !String.IsNullOrEmpty(Key);
        }

        private void CacheKey()
        {
            MasterKeyCache cache = new MasterKeyCache();
            cache.Key = Key;

            cache.Save();
        }        

        #endregion			
    }
}
