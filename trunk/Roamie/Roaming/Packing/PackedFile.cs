using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Virtuoso.Miranda.Plugins.Infrastructure;

namespace Virtuoso.Miranda.Roamie.Roaming.Packing
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
            
            this.relativePath = path.Substring(MirandaEnvironment.MirandaFolderPath.Length + 1);
        }

        ~PackedFile()
        {
            Dispose();
        }

        #endregion

        #region Properties

        [NonSerialized]
        private string path;
        public string Path
        {
            get
            {
                if (path == null)
                    path = System.IO.Path.Combine(MirandaEnvironment.MirandaFolderPath, relativePath);

                return path; 
            }
        }

        private string relativePath;
        public string RelativePath
        {
            get { return relativePath; }
            set { relativePath = value; }
        }

        private MemoryStream stream;
        public MemoryStream Stream
        {
            get { return stream; }
        }

        #endregion

        #region Methods

        public override int GetHashCode()
        {
            return relativePath.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is PackedFile))
                return false;

            return this.GetHashCode() == obj.GetHashCode();
        }

        public void Prepare()
        {            
            this.stream = new MemoryStream(File.ReadAllBytes(Path));
        }

        public void Dispose()
        {
            if (stream != null)
            {
                stream.Dispose();
                stream = null;
            }
        }

        #endregion
    }
}
