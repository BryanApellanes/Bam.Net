using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Incubation
{
    /// <summary>
    /// Fluent entry point to FluentIncubationContext
    /// </summary>
    public static class Requesting
    {
        public static FluentIncubationContext<I> A<I>()
        {
            return new FluentIncubationContext<I>();
        }

        /// <summary>
        /// Bind the specified type I ( same as Bind )
        /// </summary>
        /// <typeparam name="I"></typeparam>
        /// <param name="incubator"></param>
        /// <returns></returns>
        public static FluentIncubationContext<I> AskingFor<I>(this Incubator incubator)
        {
            return new FluentIncubationContext<I>(incubator);
        }

        public static FluentIncubationContext<I> For<I>(this Incubator incubator)
        {
            return new FluentIncubationContext<I>(incubator);
        }
        
        public static FluentCtorContext<I> ForCtor<I>(this Incubator incubator, string parameterName)
        {
            return new FluentCtorContext<I>(incubator, parameterName);
        }

        /// <summary>
        /// Bind the specified type I ( same as AskingFor )
        /// </summary>
        /// <typeparam name="I"></typeparam>
        /// <param name="incubator"></param>
        /// <returns></returns>
        public static FluentIncubationContext<I> Bind<I>(this Incubator incubator)
        {
            return new FluentIncubationContext<I>(incubator);
        }
    }
}
