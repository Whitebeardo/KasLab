using System.Collections.Generic;
using System.Threading;

namespace KasLab.Tasks
{
    public class SyncQueue<T>
    {
        private readonly Queue<T> _queue;

        private readonly object _lock;

        public int Count
        {
            get
            {
                lock (this._lock)
                {
                    return this._queue.Count;
                }
            }
        }

        public SyncQueue()
        {
            this._queue = new Queue<T>();
            this._lock = new object();
        }

        public void Push(T item)
        {
            lock (this._lock)
            {
                this._queue.Enqueue(item);
            }
        }

        public T Pop()
        {
                while (this.Count == 0)
                {
                    Thread.Sleep(500);
                }
                lock (this._lock)
                {
                    return this._queue.Dequeue();
                }
        }
    }
}
