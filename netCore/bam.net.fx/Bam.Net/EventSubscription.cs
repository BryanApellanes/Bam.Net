using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Bam.Net
{
    /// <summary>
    /// A descriptor for the constituent pieces of an
    /// in process EventSubscription.  Helps manage
    /// events and subscribed handlers. 
    /// </summary>
    public class EventSubscription
    {
        public string EventName { get; set; }
        public Delegate Delegate { get; set; }
        public FieldInfo FieldInfo { get; set; }
        public EventInfo EventInfo { get; set; }

        public virtual object Invoke(params object[] args)
        {
            return Delegate.DynamicInvoke(args);
        }

        public override int GetHashCode()
        {
            return this.GetHashCode(EventName, Delegate);
        }

        public override bool Equals(object obj)
        {
            EventSubscription compareTo = obj as EventSubscription;
            if(compareTo == null)
            {
                return false;
            }
            return compareTo.GetHashCode() == GetHashCode();
        }

        public bool Equals(EventHandler handler)
        {
            return Delegate.Equals(handler);
        }

        public static EventSubscription FromEventHandler(string eventName, EventHandler handler)
        {
            return new EventSubscription { EventName = eventName, Delegate = handler };
        }

        public static EventSubscription FromEventHandler(EventHandler handler)
        {
            return new EventSubscription { Delegate = handler };
        }
    }
}
