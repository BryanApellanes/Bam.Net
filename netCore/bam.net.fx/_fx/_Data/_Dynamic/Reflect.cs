using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data.Dynamic
{
    /// <summary>
    /// A convenience class for accessing common ReflectionExtensions.
    /// This is specifically to allow dynamic instances to use the
    /// reflection extension methods in a non extension sort of way with
    /// less typing.
    /// </summary>
    public static partial class Reflect
    {
        /// <summary>
        /// Combines the current instance with the specified toMerge values
        /// creating a new type with all the properties of each and value 
        /// set to the last one in
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="toMerge"></param>
        /// <returns></returns>
        public static object Combine(this object instance, params object[] toMerge)
        {
            return Extensions.Combine(instance, toMerge);
        }
    }
}
