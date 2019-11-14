using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpydotNet.GLGraphics
{
    public class ThreadBoundDisposable : IDisposable
    {
        private bool disposed = false;

        ~ThreadBoundDisposable()
        {
            if (!disposed)
            {
                Dispose(false);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Dispose(bool disposing)
        {
            if (!disposed)
            {
                GCUnmanagedFinalize();
                disposed = true;
            }
        }

        protected virtual void GCUnmanagedFinalize()
        {

        }
    }
}
