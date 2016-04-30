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
    public static class Asking
    {
        public static FluentIncubationContext<I> For<I>()
        {
            return new FluentIncubationContext<I>();
        }
    }
}
