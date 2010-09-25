using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Virtuoso.Roamie.Configuration
{
    internal class ObservableMemoryStream : MemoryStream
    {
        #region Nested types

        public class DisposedEventArgs : EventArgs
        {
            public object Token
            {
                get;
                set;
            }

            public byte[] Buffer
            {
                get;
                set;
            }
        }
 
        #endregion

        #region Fields

        private readonly object Token;

        #endregion

        #region Events

        public event EventHandler<DisposedEventArgs> Disposed; 

        #endregion

        #region .ctors

        public ObservableMemoryStream(object token)
        {
            Token = token;
        }

        #endregion

        #region Properties
        
        #endregion

        #region Methods

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing && Disposed != null)
                Disposed(this, new DisposedEventArgs { Buffer = ToArray(), Token = this.Token });
        } 

        #endregion
    }
}
