using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.CoreServices
{
    public static class For
    {
        public static FluentServiceRegistryContext<I> Type<I>()
        {
            return new FluentServiceRegistryContext<I>();
        }
    }
}
