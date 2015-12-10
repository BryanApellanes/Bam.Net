/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Data;
using Naizari.Extensions;

namespace Naizari.Data
{
    public class DbSelectParameter
    {
        public DbSelectParameter(Enum property, object value)
            : this(property.ToString(), value)
        { }

        protected object value;

        public DbSelectParameter(string columnOrProperty, object value)
        {
            if (string.IsNullOrEmpty(columnOrProperty))
                throw new ArgumentException("columnOrProperty can't be empty");

            this.ColumnName = columnOrProperty;
            this.parameterName = columnOrProperty + "_" + StringExtensions.RandomString(4, false, false);
            if (value == null)
                this.value = DBNull.Value;
            else
                this.value = value;
        }

        public string ColumnName
        {
            get;
            private set;
        }

        public DbParameter CreateParameter(DatabaseAgent agent)
        {
            return agent.CreateParameter(ParameterName, this.value);
        }

        public DaoParameter DbParameter
        {
            get
            {
                return new DaoParameter(ParameterName, Value);
            }
        }

        string parameterName;
        public string ParameterName
        {
            get
            {
                //if (string.IsNullOrEmpty(parameterName))
                //    return ColumnName;

                return parameterName;
            }
            set
            {
                parameterName = value;
            }
        }

        public object Value
        {
            get { return this.value; }
            internal set { this.value = value; }
        }

        public override string ToString()
        {
            return string.Format(" {0} = @{1} ", ColumnName, ParameterName);
        }

    }
}
