/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Bam.Net.Schema.Org
{
    public abstract class DataType
    {
        public DataType()
        {
            this.Name = "DataType";
        }
        public string Name { get; set; }

        public static implicit operator DateTime(DataType type)
        {
            return type.Value<DateTime>();
        }
        
        public static implicit operator DataType(DateTime dateTime)
        {
            Date dt = new Date();
            dt.Value<DateTime>(dateTime);
            return dt;
        }
        public virtual T Value<T>(object value = null)
        {
            Type type = this.GetType();
            PropertyInfo property = type.GetProperty("Value");
            if (value != null)
            {
                property.SetValue(this, value, null);
            }

            return (T)property.GetValue(this, null);
        }

        public static T GetDataType<T>(string typeName) where T: DataType, new()
        {
            object inst = GetDataType(typeName);
            return (T)inst;
        }

        public static object GetDataType(string typeName)
        {
            Type type = Type.GetType(typeName);
            if (type == null)
            {
                type = Type.GetType(string.Format(string.Format("{0}.{1}", typeof(DataType).Namespace, typeName)));
            }
            ConstructorInfo ctor = type.GetConstructor(Type.EmptyTypes);
            object inst = ctor.Invoke(null);
            return inst;
        }

        public static Type GetTypeOfDataType(string typeName)
        {
            object dataType = GetDataType(typeName);
            Type result = null;
            if (dataType != null)
            {
                result = dataType.GetType();
            }

            return result;
        }
    }
}
