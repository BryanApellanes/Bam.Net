using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Bam.Net
{
    public class EventSubscription
    {
        public string EventName { get; set; }
        public Delegate Delegate { get; set; }
        public FieldInfo FieldInfo { get; set; }
        public EventInfo EventInfo { get; set; }

        public object Invoke(params object[] args)
        {
            return Delegate.DynamicInvoke(args);
        }
    }
}
