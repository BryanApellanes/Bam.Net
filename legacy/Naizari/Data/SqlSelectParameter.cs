/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace Naizari.Data
{
    public class SqlSelectParameter: DbSelectParameter
    {
        public SqlSelectParameter(Enum property, object value)
            : base(property, value)
        { }

        public SqlSelectParameter(string columnOrProperty, object value)
            : base(columnOrProperty, value)
        { }

        public SqlParameter SqlParameter
        {
            get
            {
                SqlParameter p = new SqlParameter("@" + ParameterName, this.value);
                return p;
            }

        }
    }
}
