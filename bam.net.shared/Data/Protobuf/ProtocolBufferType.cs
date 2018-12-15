using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.CoreServices.ProtoBuf
{
    public class ProtocolBufferType
    {
        public ProtocolBufferType(Type type, IPropertyNumberer propertyNumberer, Func<PropertyInfo, bool> propertyFilter)
        {
            TypeName = type.Name;
            List<ProtocolBufferProperty> props = new List<ProtocolBufferProperty>();
            type.GetProperties().Where(propertyFilter).Each(new { List = props, Type = type }, (ctx, p) =>
            {
                ctx.List.Add(new ProtocolBufferProperty(p, propertyNumberer.GetNumber(ctx.Type, p)));
            });
            Properties = props.ToArray();
        }

        public string TypeName { get; set; }
        public ProtocolBufferProperty[] Properties { get; set; }
    }
}
