/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data.MySql
{
    /// <summary>
    /// Provides MySql specific expression formatting.
    /// It may make sense to put this class into the database.ServiceProvider
    /// container, especially when moving on to implement 
    /// support for other databases.  
    /// </summary>
    internal class MySqlFormatProvider
    {
        public static SetFormat GetSetFormat(string tableName, StringBuilder stringBuilder, int? startNumber, params AssignValue[] values)
        {
            SetFormat set = new SetFormat
            {
                ColumnNameFormatter = (c) => c,
                ParameterPrefix = "@"
            };
            foreach (AssignValue value in values)
            {
                set.AddAssignment(value);
            }

            set.StartNumber = startNumber;
            stringBuilder.Append(set.Parse());
            return set;
        }

        public static WhereFormat GetWhereFormat(IQueryFilter filter, StringBuilder stringBuilder, int? startNumber)
        {
            WhereFormat where = new WhereFormat(filter);
            where.ColumnNameFormatter = (c) => c;
            where.ParameterPrefix = "@";
            where.StartNumber = startNumber;
            stringBuilder.Append(where.Parse());
            return where;
        }

        public static WhereFormat GetWhereFormat(AssignValue filter, StringBuilder stringBuilder, int? startNumber)
        {
            WhereFormat where = new WhereFormat();
            where.ColumnNameFormatter = (c) => c;
            where.ParameterPrefix = "@";
            where.StartNumber = startNumber;
            where.AddAssignment(filter);
            stringBuilder.Append(where.Parse());
            return where;
        }
    }
}
