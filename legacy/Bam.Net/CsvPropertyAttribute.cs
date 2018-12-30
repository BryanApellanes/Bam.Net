using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net
{
    [AttributeUsage(AttributeTargets.Property)]
    public class CsvPropertyAttribute: Attribute
    {
        public int Sequence { get; set; }

        public static Func<Type, PropertyInfo[]> Getter(bool includeAllProperties = true)
        {
            return includeAllProperties ?
            (Func<Type, PropertyInfo[]>)((type) =>
            {
                List<PropertyInfo> csvProps = GetCsvProperties(type);
                List<PropertyInfo> props = new List<PropertyInfo>();
                props.AddRange(type.GetProperties().Where(pi => !pi.HasCustomAttributeOfType<CsvPropertyAttribute>()));
                props.Sort((x, y) => x.Name.CompareTo(y.Name));
                csvProps.AddRange(props);
                return csvProps.ToArray();
            })
            :
            (Func<Type, PropertyInfo[]>)((type) => GetCsvProperties(type).ToArray());
        }

        private static List<PropertyInfo> GetCsvProperties(Type type)
        {
            List<PropertyInfo> csvProps = new List<PropertyInfo>();
            csvProps.AddRange(type.GetProperties().Where(pi => pi.HasCustomAttributeOfType<CsvPropertyAttribute>()));
            csvProps.Sort((x, y) => x.GetCustomAttributeOfType<CsvPropertyAttribute>().Sequence.CompareTo(y.GetCustomAttributeOfType<CsvPropertyAttribute>().Sequence));
            return csvProps;
        }
    }
}
