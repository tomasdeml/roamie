using System;
using System.Collections.Generic;
using System.Text;

namespace Virtuoso.Miranda.Roamie.Roaming.Packing
{
    [Serializable]
    internal class FileCollection : IList<PackedFile>
    {
        #region Properties

        private readonly List<PackedFile> List;

        [NonSerialized]
        private bool isDirty;
        public bool IsDirty
        {
            get { return isDirty; }
        }

        #endregion

        #region .ctors

        public FileCollection()
        {
            this.List = new List<PackedFile>(1);
            this.isDirty = false;
        }

        private void MarkDirty()
        {
            isDirty = true;
        }

        #endregion

        #region IList<AttachedFile> Members

        public int IndexOf(PackedFile item)
        {
            return List.IndexOf(item);
        }

        public void Insert(int index, PackedFile item)
        {
            MarkDirty();
            List.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            MarkDirty();
            List.RemoveAt(index);
        }

        public PackedFile this[int index]
        {
            get
            {
                return List[index];
            }
            set
            {
                MarkDirty();
                List[index] = value;
            }
        }

        #endregion

        #region ICollection<AttachedFile> Members

        public void Add(PackedFile item)
        {
            MarkDirty();
            List.Add(item);
        }

        public void Clear()
        {
            MarkDirty();
            List.Clear();
        }

        public bool Contains(PackedFile item)
        {
            return List.Contains(item);
        }

        public void CopyTo(PackedFile[] array, int arrayIndex)
        {
            List.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return List.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(PackedFile item)
        {
            MarkDirty();
            return List.Remove(item);
        }

        #endregion

        #region IEnumerable<AttachedFile> Members

        public IEnumerator<PackedFile> GetEnumerator()
        {
            return List.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return List.GetEnumerator();
        }

        #endregion
    }
}
