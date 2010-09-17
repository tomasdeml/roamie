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
using System.IO;

namespace Virtuoso.Miranda.Roamie
{
    public static class SecureStreamCompactor
    {
        public static void CompressAndEncrypt(Stream input, Stream output, string password)
        {
            if (input == null)
                throw new ArgumentNullException("databaseStream");

            if (!input.CanRead) 
                throw new ArithmeticException("Invalid database stream.");

            if (output == null) 
                throw new ArgumentNullException("output");

            if (!output.CanWrite) 
                throw new ArgumentNullException("Invalid output stream.");

            if (String.IsNullOrEmpty(password))
                throw new ArgumentNullException("password");

            using (MemoryStream zippedStream = new MemoryStream(4096))
            {
                Streaming.CompressStream(input, zippedStream);
                zippedStream.Seek(0, SeekOrigin.Begin);

                Streaming.EncryptStream(zippedStream, output, password);
            }
        }

        public static void DecryptAndDecompress(Stream input, Stream output, string password)
        {
            if (input == null) 
                throw new ArgumentNullException("databaseStream");

            if (!input.CanRead) 
                throw new ArithmeticException("Invalid database stream.");

            if (output == null) 
                throw new ArgumentNullException("output");

            if (!output.CanWrite) 
                throw new ArithmeticException("Invalid output stream.");

            if (String.IsNullOrEmpty(password)) 
                throw new ArgumentNullException("password");

            using (MemoryStream decryptedStream = new MemoryStream(8192))
            {
                Streaming.DecryptStream(input, decryptedStream, password);
                decryptedStream.Seek(0, SeekOrigin.Begin);

                Streaming.DecompressStream(decryptedStream, output);
            }
        }
    }
}