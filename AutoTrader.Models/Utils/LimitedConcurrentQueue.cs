using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutoTrader.Models.Utils
{
    public class LimitedConcurrentQueue<T> : ConcurrentQueue<T>
    {
        private Timer _capacityLimitTimer;

        public LimitedConcurrentQueue(int capacity)
        {
            _capacityLimitTimer = new Timer(FlattenQueue, capacity, 0, 1000 * 60 * 10 /* 10 minutes */);
        }

        private void FlattenQueue(object state)
        {
            var capacity = (int)state;

            while (Count > capacity)
            {
                TryDequeue(out T result);
            }
        }
    }
}