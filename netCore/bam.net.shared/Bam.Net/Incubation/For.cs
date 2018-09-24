using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Incubation
{
    public static class For<I>
    {
        public static Incubator Use<T>()
        {
            return new FluentIncubationContext<I>().Use<T>();
        }

        public static Incubator Use(object instance)
        {
            return new FluentIncubationContext<I>().Use(instance);
        }
    }
}
