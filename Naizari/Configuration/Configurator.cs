/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Reflection;
using System.Data;
using System.Data.SqlClient;
using KLGates.Data.Access;

namespace KLGates.Configuration
{
    public class Configurator
    {
        Dictionary<string, object> properties = new Dictionary<string, object>();

        public object this[string propertyName]
        {
            get { return properties[propertyName]; }
            set { properties[propertyName] = value; }
        }

        SqlConnectionStringBuilder connectionString = new SqlConnectionStringBuilder();
        public SqlConnectionStringBuilder ConnectionString
        {
            get { return connectionString; }
        }

        public void LoadConfigFromDb(string tableName)
        {
            DatabaseUtility db = DatabaseUtility.GetInstance(connectionString.ConnectionString);
            throw new NotImplementedException("this is not implemented");

        }

        public void SetProperties(object target)
        {
            foreach (string propertyName in properties.Keys)
            {
                Type targetType = target.GetType();
                foreach (PropertyInfo property in targetType.GetProperties())
                {
                    if (propertyName.Equals(property.Name))
                    {
                        property.SetValue(target, properties[propertyName], null);
                    }

                    if (propertyName.Equals(targetType.Name + "." + property.Name))
                    {
                        property.SetValue(target, properties[propertyName], null);
                    }
                }
            }
        }
    }
}
