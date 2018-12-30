using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.CoreServices
{
    /// <summary>
    /// A little bit of over engineering 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class MaximumLimitEnforcer<T>
    {
        public const string LimitReachedMessageFormat = "Maximum limit reached: {0}";
        public abstract int GetMaximumLimit();
        public abstract int GetThrottledValue();
        public abstract T LimitNotReachedAction();
        public abstract T LimitReachedAction();
        public T Execute() 
        {
            if (GetThrottledValue() > GetMaximumLimit())
            {
                return LimitReachedAction();
            }
            else
            {
                return LimitNotReachedAction();
            }
        }
    }
}
