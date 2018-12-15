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

        public Incubator Use(object instance)
        {
            return Returns(instance);
        }

        public Incubator Returns(object instance)
        {
            Incubator inc = Incubator ?? new Incubator();
            inc.Set(typeof(I), () => instance);
            return inc;
        }

        public Incubator Use<T>()
        {
            return Returns<T>();
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
            inc.Set(typeof(I), () => inc.Construct(typeof(T)));
            return inc;
        }

        public Incubator Use<T>(Func<T> instanciator)
        {
            return Returns<T>(instanciator);
        }

        public Incubator Returns<T>(Func<T> instanciator)
        {
            Incubator inc = Incubator ?? new Incubator();
            inc.Set(typeof(I), instanciator, false);
            return inc;
        }

        public Incubator Use<T>(Func<Incubator, T> instanciator)
        {
            return Returns<T>(instanciator);
        }

        public Incubator Returns<T>(Func<Incubator, T> instanciator)
        {
            Incubator inc = Incubator ?? new Incubator();
            inc.Set(typeof(I), () => instanciator(inc));
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
