using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.CoreServices.ProtoBuf
{
    public class ProtocolBufferProperty
    {
        public ProtocolBufferProperty(PropertyInfo property, int propertyNumberid)
        {
            TypeName = GetPrimitiveType(property);
            PropertyName = property.Name;
            PropertyNumberId = propertyNumberid;
            IsRepeated = property.IsEnumerable();
        }

        public string TypeName { get; set; }
        public string PropertyName { get; set; }
        public int PropertyNumberId { get; set; }
        public bool IsRepeated { get; set; }
        public static string GetPrimitiveType(PropertyInfo property)
        {
            Type propType = property.PropertyType;
            if(propType == typeof(int) || propType == typeof(int?))
            {
                return "int32";
            }
            if(propType == typeof(long) || propType == typeof(long?))
            {
                return "int64";
            }
            if(propType == typeof(bool) || propType == typeof(bool?))
            {
                return "bool";
            }
            if(propType == typeof(string))
            {
                return "string";
            }
            return "bytes";
        }   
    }
}
