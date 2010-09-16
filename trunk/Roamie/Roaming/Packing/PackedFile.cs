using System;
using System.IO;
using Virtuoso.Miranda.Plugins.Infrastructure;

namespace Virtuoso.Roamie.Roaming.Packing
{
    [Serializable]
    internal class PackedFile : IDisposable
    {
        #region .ctors

        public PackedFile(string path)
        {
            if (String.IsNullOrEmpty(path))
                throw new ArgumentNullException("path");

            if (!path.StartsWith(MirandaEnvironment.MirandaFolderPath))
                throw new ArgumentOutOfRangeException("path");
            
            RelativePath = path.Substring(MirandaEnvironment.MirandaFolderPath.Length + 1);
        }

        ~PackedFile()
        {
            Dispose(false);
        }

        #endregion

        #region Properties

        [NonSerialized]
        private string path;
        public string Path
        {
            get { return path ?? (path = System.IO.Path.Combine(MirandaEnvironment.MirandaFolderPath, RelativePath)); }
        }

        public string RelativePath { get; set; }

        public MemoryStream Stream { get; private set; }

        #endregion

        #region Methods

        public override int GetHashCode()
        {
            return RelativePath.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is PackedFile))
                return false;

            return GetHashCode() == obj.GetHashCode();
        }

        public void Prepare()
        {            
            Stream = new MemoryStream(File.ReadAllBytes(Path));
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected void Dispose(bool disposing)
        {
            if (!disposing || Stream == null)
                return;

            Stream.Dispose();
            Stream = null;
        }

        #endregion
    }
}
