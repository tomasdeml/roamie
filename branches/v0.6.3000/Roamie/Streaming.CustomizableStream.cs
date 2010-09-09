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
using System.IO;

namespace Virtuoso.Miranda.Roamie
{
    partial class Streaming
    {
        public class CustomizableStream : Stream
        {
            #region Fields

            private readonly Stream BaseStream;
            private readonly long CustomLength;

            #endregion

            #region .ctors

            public CustomizableStream(Stream stream) : this(stream, -1) { }

            public CustomizableStream(Stream stream, long length)
            {
                if (stream == null) throw new ArgumentNullException("stream");
                if (length != -1 && length <= 0) throw new ArgumentOutOfRangeException("length");

                BaseStream = stream;
                CustomLength = length;
            }

            #endregion

            #region Impls

            public override bool CanRead
            {
                get { return BaseStream.CanRead; }
            }

            public override bool CanSeek
            {
                get { return BaseStream.CanSeek; }
            }

            public override bool CanWrite
            {
                get { return BaseStream.CanWrite; }
            }

            public override void Flush()
            {
                BaseStream.Flush();
            }

            public override long Length
            {
                get { return CustomLength == -1 ? BaseStream.Length : CustomLength; }
            }

            public override long Position
            {
                get
                {
                    return BaseStream.Position;
                }
                set
                {
                    BaseStream.Position = value;
                }
            }

            public override int Read(byte[] buffer, int offset, int count)
            {
                return BaseStream.Read(buffer, offset, count);
            }

            public override long Seek(long offset, SeekOrigin origin)
            {
                return BaseStream.Seek(offset, origin);
            }

            public override void SetLength(long value)
            {
                BaseStream.SetLength(value);
            }

            public override void Write(byte[] buffer, int offset, int count)
            {
                BaseStream.Write(buffer, offset, count);
            }

            protected override void Dispose(bool disposing)
            {
            }

            #endregion
        }
    }
}
