using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data.Repositories
{
    public static class WrapperExtensions
    {
        public static T Wrap<T>(this object instance, DaoRepository repo)
        {
            return (T)repo.Wrap(typeof(T), instance);
        }
    }
}
