/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Bam.Net;

namespace Bam.Net.Data
{
    /// <summary>
    /// Used to define a Column in a Table in Schema.
    /// </summary>
    public class ColumnAttribute: Attribute
    {
        public ColumnAttribute()
        {
            this.Name = "".RandomString(5);
        }

        public string Table { get; set; }
        public string Name { get; set; }
       
        public string DbDataType { get; set; }
        public string MaxLength { get; set; }
        public virtual bool AllowNull { get; set; }

        public override string ToString()
        {
            string maxLength = string.IsNullOrEmpty(MaxLength) ? "" : string.Format("({0})", MaxLength);
            return string.Format("\"{0}\" {1}{2}{3}", Name, DbDataType, maxLength, AllowNull ? "" : " NOT NULL");
        }

        public string ToString(SchemaWriter builder)
        {
            return builder.GetColumnDefinition(this);
        }

        public static ColumnAttribute[] GetColumns(object instance)
        {
            return GetColumns(instance.GetType());
        }

        public static ColumnAttribute[] GetColumns(Type type)
        {
            List<ColumnAttribute> attributes = GetAttributes<ColumnAttribute>(type);

            return attributes.ToArray();
        }

        public static ForeignKeyAttribute[] GetForeignKeys(object instance)
        {
            return GetForeignKeys(instance.GetType());   
        }

        public static ForeignKeyAttribute[] GetForeignKeys(Type type)
        {
            List<ForeignKeyAttribute> fks = GetAttributes<ForeignKeyAttribute>(type);

            return fks.ToArray();
        }

        private static List<T> GetAttributes<T>(Type type) where T : Attribute
        {
            PropertyInfo[] properties = type.GetProperties();
            List<T> attributes = new List<T>();
            foreach (PropertyInfo prop in properties)
            {
                if (prop.HasCustomAttributeOfType<T>(out T attr))
                {
                    attributes.Add(attr);
                }
            }
            return attributes;
        }
    }
}
