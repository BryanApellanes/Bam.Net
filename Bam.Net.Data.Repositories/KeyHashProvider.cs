using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data.Repositories
{
    internal static class KeyHashProvider
    {
        public static string[] GetCompositeKeyProperties(Type type)
        {
            List<string> props = type.GetPropertiesWithAttributeOfType<CompositeKeyAttribute>().Select(pi => pi.Name).ToList();
            Args.ThrowIf(props.Count == 0, "No properties addorned with CompositeKeyAttribute defined on type ({0})", type.Name);
            props.Sort();
            return props.ToArray();
        }

        internal static int GetIntKeyHash(object instance, string propertyDelimiter, string[] compositeKeyProperties)
        {
            Args.ThrowIfNull(instance);
            Args.ThrowIf(compositeKeyProperties.Length == 0, $"No CompositeKeyProperties defined on type ({instance.GetType().Name})");
            return string.Join(propertyDelimiter, compositeKeyProperties.Select(prop => instance.Property(prop))).ToSha256Int();
        }

        internal static long GetLongKeyHash(object instance, string propertyDelimiter, string[] compositeKeyProperties)
        {
            Args.ThrowIfNull(instance);
            Args.ThrowIf(compositeKeyProperties.Length == 0, $"No CompositeKeyProperties defined on type ({instance.GetType().Name})");
            return string.Join(propertyDelimiter, compositeKeyProperties.Select(prop => instance.Property(prop))).ToSha256Long();
        }

        internal static ulong GetULongKeyHash(object instance, string propertyDelimiter, string[] compositeKeyProperties)
        {
            Args.ThrowIfNull(instance);
            Args.ThrowIf(compositeKeyProperties.Length == 0, $"No CompositeKeyProperties defined on type ({instance.GetType().Name})");
            return string.Join(propertyDelimiter, compositeKeyProperties.Select(prop => instance.Property(prop))).ToSha256ULong();
        }
    }

}
