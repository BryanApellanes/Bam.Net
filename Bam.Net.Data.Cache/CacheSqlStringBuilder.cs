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
using Bam.Net.Data.Cache;

namespace Bam.Net.Data
{
    public class CacheSqlStringBuilder : SchemaWriter
    {
        public CacheSqlStringBuilder(): base()
        {
            Reset();
            TableNameFormatter = t => $"{TableNamePrefix}{t}";
            ColumnNameFormatter = c => c;
            SelectStar = true;
        }

        public string TableNamePrefix { get; set; }

        public static void Register(Incubator incubator)
        {
            CacheSqlStringBuilder builder = new CacheSqlStringBuilder();
            incubator.Set(typeof(SqlStringBuilder), builder);
            incubator.Set<SqlStringBuilder>(builder);
        }

        public override SqlStringBuilder Where(IQueryFilter filter)
        {
            WhereFormat where = CacheFormatProvider.GetWhereFormat(filter, StringBuilder, NextNumber);
            NextNumber = where.NextNumber;
            parameters.AddRange(where.Parameters);
            return this;
        }

        public override SqlStringBuilder Update(string tableName, params AssignValue[] values)
        {
            Builder.AppendFormat("UPDATE {0} ", TableNameFormatter(tableName));
            SetFormat set = CacheFormatProvider.GetSetFormat(tableName, StringBuilder, NextNumber, values);
            NextNumber = set.NextNumber;
            parameters.AddRange(set.Parameters);
            return this;
        }

        public override string GetKeyColumnDefinition(KeyColumnAttribute keyColumn)
        {
            return string.Format(KeyColumnFormat, GetColumnDefinition(keyColumn));
        }

        public override SqlStringBuilder Select<T>()
        {
            return Select(TableNameFormatter(Dao.TableName(typeof(T))), SelectStar ? "*" : ColumnAttribute.GetColumns(typeof(T)).ToDelimited(c => ColumnNameFormatter(c.Name)));
        }

        //public override SqlStringBuilder Select(string tableName, params string[] columnNames)
        //{
        //    base.Select(tableName, columnNames);
        //    //if (!tableName.StartsWith(TableNamePrefix))
        //    //{
        //    //    tableName = TableNameFormatter(tableName);
        //    //}
        //    return SelectTop(-1, tableName, columnNames);
        //}

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
