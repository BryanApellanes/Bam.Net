using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Incubation
{
    public class FluentIncubationContext<I>
    {
        public FluentIncubationContext()
        {
        }
        public FluentIncubationContext(Incubator incubator)
        {
            this.Incubator = incubator;
        }

        /// <summary>
        /// Specify the return type T for the specified 
        /// type I ( same as To )
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Incubator Returns<T>()
        {
            Incubator inc = Incubator ?? new Incubator();
            inc.Set(typeof(I), inc.Construct(typeof(T)));
            return inc;
        }
        /// <summary>
        /// Specify the return type T for the specified 
        /// type I ( same as Returns )
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Incubator To<T>()
        {
            return Returns<T>();
        }
        protected Incubator Incubator
        {
            get;
            set;
        }
    }
}
