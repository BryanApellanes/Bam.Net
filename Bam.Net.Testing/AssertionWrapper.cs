/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Bam.Net.Testing
{
    public class AssertionWrapper
    {
        public AssertionWrapper(Because because, object wrapped, string name = null)
        {
            this.Value = wrapped;
            this.Because = because;
            this.Name = string.IsNullOrEmpty(name) ? "the value": name;
        }

        public string Name { get; set; }
        public object Value { get; private set; }
        public Type TypeOfValue
        {
            get
            {
                return Value.GetType();
            }
        }
        protected Because Because { get; set; }

        public void IsA(Type type)
        {
            Because.ItsTrue("the {0} is a {0}"._Format(Name, type.Name), Value.GetType() == type, "the object under test is NOT a {0}"._Format(type.Name));
        }
        
        public void Equals(object obj)
        {
            Because.ItsTrue("{0} .Equals({1})"._Format(Name, obj), Value.Equals(obj), "{0} did not .Equals({1})"._Format(Name, obj));
        }

        public AssertionWrapper Prop(string propertyName)
        {
            AssertionWrapper result = new AssertionWrapper(Because, null, "{0}.{1}"._Format(TypeOfValue.Name, propertyName));
            PropertyInfo prop = TypeOfValue.GetProperty(propertyName);
            if(prop != null)
            {
                result.Value = prop.GetValue(Value, null);
            }

            return result;
        }
    }
}
