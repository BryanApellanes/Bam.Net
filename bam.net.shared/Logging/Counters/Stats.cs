using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Logging.Counters
{
    public class Stats
    {
        public string Name { get; set; }
        public virtual object Value { get; set; }

        static ConcurrentDictionary<string, Stats> _stats = new ConcurrentDictionary<string, Stats>();
        
        public static Timer Start(string name)
        {
            Timer timer = GetStats<Timer>(name, 0);
            timer.StartTime = new Instant();
            return timer;
        }

        public static Timer End(Timer timer, Action<Timer> endHandler = null)
        {
            End(timer.Name);
            timer.End();
            if(endHandler != null)
            {
                Task.Run(() => endHandler(timer));
            }
            return timer;
        }

        public static Timer End(string name)
        {
            Timer timer = GetStats<Timer>(name, 0);
            timer.End();
            if(!_stats.TryRemove(name, out Stats value))
            {
                Log.TraceInfo("Failed to remove timer {0}", name);
            }
            return timer;
        }

        public static Counter Increment(string name)
        {
            return GetCounter(name).Increment();
        }

        public static Counter Decrement(string name)
        {
            return GetCounter(name).Decrement();
        }

        public static Counter Count(string name)
        {
            return GetCounter(name, 0);
        }

        public static Counter Count(string name, long setValueTo)
        {
            Counter counter = GetCounter(name);
            counter.Value = setValueTo;
            return counter;
        }
        
        public static Counter Count(string name, Func<long> countReader)
        {
            Counter counter = GetCounter(name);
            counter.CountReader = countReader;
            return counter;
        }

        public static Counter Diff(string name, Func<Counter> counter)
        {
            return Diff(name, counter());
        }

        public static Counter Diff(string name, Counter counter)
        {
            Counter named = GetCounter(name);
            return named.Diff(counter);
        }

        public static Counter GetCounter(string name, long initialValue = 1)
        {
            return GetStats<Counter>(name, initialValue);
        }
        
        private static T GetStats<T>(string name, object initialValue) where T : Stats, new()
        { 
            if (!_stats.ContainsKey(name))
            {
                T stats = new T { Name = name, Value = initialValue };
                _stats.TryAdd(name, stats);
                return stats;
            }
            else if (_stats.TryGetValue(name, out Stats value))
            {
                T stats = (T)value;
                return stats;
            }
            else
            {
                Log.TraceInfo("Failed to get stats instance of type {0} named {1}", typeof(T).Name, name);
                return new T { Value = initialValue };
            }
        }
    }
}
