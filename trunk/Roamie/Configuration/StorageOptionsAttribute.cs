using System;
using System.Collections.Generic;
using System.Text;

namespace Virtuoso.Roamie.Configuration
{
    // TODO Move to Hyphen

    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class StorageOptionsAttribute : Attribute
    {
        public string FileName
        {
            get;
            set;
        }

        public StorageOptionsAttribute()
        {
        }
    }
}
