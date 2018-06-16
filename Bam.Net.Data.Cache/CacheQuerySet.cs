/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bam.Net.Data.Cache;

namespace Bam.Net.Data
{
    public class CacheQuerySet : QuerySet
    {
        public CacheQuerySet() : base()
        {
            Reset();
            TableNameFormatter = t => $"{TableNamePrefix}{t}";
            ColumnNameFormatter = c => c;
        }

        public string TableNamePrefix { get; set; }

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
    }
}
