using System;
using System.Collections.Generic;

namespace Virtuoso.Roamie.Roaming.Provisioning
{
    [Serializable]
    internal class ContentCollection : IList<Content>
    {
        #region Properties

        private readonly List<Content> List;

        [NonSerialized]
        private bool isDirty;
        public bool IsDirty
        {
            get { return isDirty; }
        }

        #endregion

        #region .ctors

        public ContentCollection()
        {
            this.List = new List<Content>(1);
            this.isDirty = false;
        }

        private void MarkDirty()
        {
            isDirty = true;
        }

        #endregion

        #region IList<AttachedFile> Members

        public int IndexOf(Content item)
        {
            return List.IndexOf(item);
        }

        public void Insert(int index, Content item)
        {
            MarkDirty();
            List.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            MarkDirty();
            List.RemoveAt(index);
        }

        public Content this[int index]
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

        public void Add(Content item)
        {
            MarkDirty();
            List.Add(item);
        }

        public void Clear()
        {
            MarkDirty();
            List.Clear();
        }

        public bool Contains(Content item)
        {
            return List.Contains(item);
        }

        public void CopyTo(Content[] array, int arrayIndex)
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

        public bool Remove(Content item)
        {
            MarkDirty();
            return List.Remove(item);
        }

        #endregion

        #region IEnumerable<AttachedFile> Members

        public IEnumerator<Content> GetEnumerator()
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
