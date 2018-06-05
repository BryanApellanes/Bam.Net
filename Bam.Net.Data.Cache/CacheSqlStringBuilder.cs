/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Bam.Net.Incubation;
using Bam.Net;
using Bam.Net.Data;

namespace Bam.Net.Data
{
    public class CacheSqlStringBuilder : SchemaWriter
    {
        public static void Register(Incubator incubator)
        {
            CacheSqlStringBuilder builder = new CacheSqlStringBuilder();
            incubator.Set(typeof(SqlStringBuilder), builder);
            incubator.Set<SqlStringBuilder>(builder);
        }
        public override string GetKeyColumnDefinition(KeyColumnAttribute keyColumn)
        {
            return string.Format(KeyColumnFormat, GetColumnDefinition(keyColumn));
        }
        public override string GetColumnDefinition(ColumnAttribute column)
        {
            string max = string.Format("({0})", column.MaxLength);
            string type = column.DbDataType.ToLowerInvariant();

            if (type.Equals("bigint") ||
                type.Equals("int") ||
                type.Equals("datetime") ||
                type.Equals("bit"))
            {
                max = string.Empty;
            }
            else if (type.Equals("decimal"))
            {
                max = string.Format("({0}, 2)", column.MaxLength);
            }

            return string.Format("\"{0}\" {1}{2}{3}", column.Name, column.DbDataType, max, column.AllowNull ? "" : " NOT NULL");
        }
    }
}
