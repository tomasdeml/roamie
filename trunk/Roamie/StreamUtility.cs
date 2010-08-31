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
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;

namespace Virtuoso.Miranda.Roamie
{
    public static class StreamUtility
    {
        #region Fields

        private static readonly byte[] KeyGeneratorSalt = new byte[] { 12, 32, 64, 81, 33, 125, 35, 99, 113, 255 };

        #endregion

        #region Delegates

        public delegate void ProgressCallback(int progress);

        #endregion

        #region Copy methods

        public static void CopyStream(Stream source, Stream destination)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            if (destination == null)
                throw new ArgumentNullException("destination");

            int count;
            byte[] buffer = new byte[2048];

            while ((count = source.Read(buffer, 0, buffer.Length)) != 0)
                destination.Write(buffer, 0, count);
        }

        public static void CopyStream(Stream source, Stream destination, ProgressCallback callback)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            if (destination == null)
                throw new ArgumentNullException("destination");

            if (callback == null)
            {
                CopyStream(source, destination);
                return;
            }

            int count;
            byte[] buffer = new byte[2048];

            double percent = source.Length / 100d;
            double read = 0;

            while ((count = source.Read(buffer, 0, buffer.Length)) != 0)
            {
                read += count;
                destination.Write(buffer, 0, count);

                callback((int)(read / percent));
            }
        }

        public static byte[] ReadStream(Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException("stream");

            byte[] buffer = new byte[stream.Length];
            stream.Read(buffer, 0, buffer.Length);

            return buffer;
        }

        #endregion

        #region (D)Encryption methods

        public static void EncryptStream(Stream source, Stream destination, string key)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            if (destination == null)
                throw new ArgumentNullException("destination");

            if (String.IsNullOrEmpty(key))
                throw new ArgumentNullException("key");

            TripleDES tripleDes = TripleDES.Create();
            byte[] keyBytes;
            byte[] ivBytes;
            GetSecretBytes(tripleDes, key, out keyBytes, out ivBytes);

            // CryptoStream::Dispose() closes the underlying stream, which is what we don't want, so we use an unclosable stream decorator
            UndisposableStream permaDestination = new UndisposableStream(destination);

            using (ICryptoTransform transform = tripleDes.CreateEncryptor(keyBytes, ivBytes))
                using (CryptoStream cryptoDestination = new CryptoStream(permaDestination, transform, CryptoStreamMode.Write))
                    CopyStream(source, cryptoDestination);
        }

        public static void DecryptStream(Stream source, Stream destination, string key)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            if (destination == null)
                throw new ArgumentNullException("destination");

            if (String.IsNullOrEmpty(key))
                throw new ArgumentNullException("key");

            TripleDES tripleDes = TripleDES.Create();
            byte[] keyBytes;
            byte[] ivBytes;
            GetSecretBytes(tripleDes, key, out keyBytes, out ivBytes);

            // CryptoStream::Dispose() closes the underlying stream, which is what we don't want, so we use an unclosable stream decorator
            UndisposableStream permaSource = new UndisposableStream(source);

            using (ICryptoTransform transform = tripleDes.CreateDecryptor(keyBytes, ivBytes))
                using (CryptoStream cryptoSource = new CryptoStream(permaSource, transform, CryptoStreamMode.Read))
                    CopyStream(cryptoSource, destination);
        }

        private static void GetSecretBytes(SymmetricAlgorithm algorithm, string password, out byte[] keyBytes, out byte[] ivBytes)
        {
            Rfc2898DeriveBytes keyGenerator = new Rfc2898DeriveBytes(password, KeyGeneratorSalt, 20);
            keyBytes = keyGenerator.GetBytes(algorithm.LegalKeySizes[0].MaxSize / 8);

            keyGenerator.IterationCount = 10;
            ivBytes = keyGenerator.GetBytes(algorithm.LegalBlockSizes[0].MaxSize / 8);
        }

        #endregion

        #region (De)Compression methods

        public static void CompressAndEncrypt(Stream input, Stream output, string password)
        {
            if (input == null)
                throw new ArgumentNullException("input");

            if (!input.CanRead)
                throw new ArithmeticException("Invalid database stream.");

            if (output == null)
                throw new ArgumentNullException("output");

            if (!output.CanWrite)
                throw new ArgumentException("Invalid output stream.");

            if (String.IsNullOrEmpty(password))
                throw new ArgumentNullException("password");

            using (MemoryStream zippedStream = new MemoryStream(4096))
            {
                CompressStream(input, zippedStream);
                zippedStream.Seek(0, SeekOrigin.Begin);

                EncryptStream(zippedStream, output, password);
            }
        }

        public static void CompressStream(Stream source, Stream destination)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            if (destination == null)
                throw new ArgumentNullException("destination");

            using (GZipStream zipDestination = new GZipStream(destination, CompressionMode.Compress, true))
                CopyStream(source, zipDestination);
        }

        public static void DecryptAndDecompress(Stream input, Stream output, string password)
        {
            if (input == null)
                throw new ArgumentNullException("input");

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
                DecryptStream(input, decryptedStream, password);
                decryptedStream.Seek(0, SeekOrigin.Begin);

                DecompressStream(decryptedStream, output);
            }
        }

        public static void DecompressStream(Stream source, Stream destination)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            if (destination == null)
                throw new ArgumentNullException("destination");

            using (GZipStream zipSource = new GZipStream(source, CompressionMode.Decompress, true))
                CopyStream(zipSource, destination);
        }

        #endregion
    }
}