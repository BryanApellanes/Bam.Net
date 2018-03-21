using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Bam.Net
{
    /// <summary>
    /// Using reflection, finds any custom attributes that
    /// may be addorning members of specified types.
    /// </summary>
    public class CustomAttributeTypeDescriptor
    {
        public CustomAttributeTypeDescriptor() { }
        public CustomAttributeTypeDescriptor(Type type)
        {
            Type = type;
            AttributeTypes = Analyze(type);
        }
        public Type Type { get; set; }
        public HashSet<Type> AttributeTypes
        {
            get;
            private set;
        }

        public static HashSet<Type> Analyze(Type type)
        {
            return new HashSet<Type>(GetMemberCustomAttributes(type));
        }

        private static IEnumerable<Type> GetMemberCustomAttributes(Type type)
        {
            object[] attributes = type.GetCustomAttributes(true);
            foreach(object attribute in attributes)
            {
                yield return attribute.GetType();
            }
            foreach(MethodInfo method in type.GetMethods())
            {
                foreach(object attribute in method.GetCustomAttributes(true))
                {
                    yield return attribute.GetType();
                }
            }
            foreach(PropertyInfo property in type.GetProperties())
            {
                foreach(object attribute in property.GetCustomAttributes(true))
                {
                    yield return attribute.GetType();
                }
            }
        }
    }
}
