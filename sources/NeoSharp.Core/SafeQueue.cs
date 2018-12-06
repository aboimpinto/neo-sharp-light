using System.Collections.Concurrent;
using System.Linq;
using System.Threading;

namespace NeoSharp.Core
{
    public class SafeQueue<T> 
    {
        private readonly ConcurrentQueue<T> safeList = new ConcurrentQueue<T>();

        private readonly ReaderWriterLockSlim readerWriteLockSlim = new ReaderWriterLockSlim();
        private readonly AutoResetEvent waitForQueueToChangeEvent = new AutoResetEvent(false);

        public int Count => this.safeList.Count;

        public void Enqueue(T item)
        {
            this.readerWriteLockSlim.EnterWriteLock();
            this.safeList.Enqueue(item);
            this.readerWriteLockSlim.ExitWriteLock();

            this.waitForQueueToChangeEvent.Set();
        }

        public T Dequeue()
        {
            if (!this.safeList.Any()) return default(T);

            this.readerWriteLockSlim.EnterWriteLock();
            this.safeList.TryDequeue(out var dequeueItem);
            this.readerWriteLockSlim.ExitWriteLock();

            return dequeueItem;
        }

        public void WaitForQueueToChange()
        {
            this.waitForQueueToChangeEvent.WaitOne();
        }
    }
}