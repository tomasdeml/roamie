using System;
using System.IO;

namespace Virtuoso.Miranda.Roamie
{
    internal class UndisposableStream : Stream
    {
        #region Fields

        private readonly Stream BaseStream;
        private readonly long? CustomLength;

        #endregion

        #region .ctors

        public UndisposableStream(Stream stream) : this(stream, null) { }

        public UndisposableStream(Stream stream, long? length)
        {
            if (stream == null) 
                throw new ArgumentNullException("stream");

            if (length != null && length <= 0) 
                throw new ArgumentOutOfRangeException("length");

            BaseStream = stream;
            CustomLength = length;

            GC.SuppressFinalize(this);
        }

        #endregion

        #region Properties

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
            get { return CustomLength == null ? BaseStream.Length : CustomLength.Value; }
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

        #endregion

        #region Methods

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
            // We do not want to dispose the stream here
        }

        // TODO Use everywhere!!!
        public void ForceDispose()
        {
            BaseStream.Dispose();
        }

        #endregion
    }
}