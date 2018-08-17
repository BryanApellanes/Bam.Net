/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bam.Net.Data.Intersystems;

namespace Bam.Net.Data
{
    public class InterSystemsQuerySet : QuerySet
    {
        public InterSystemsQuerySet() : base()
        {
            Reset();
            TableNameFormatter = t => $"{TableNamePrefix}{t}";
            ColumnNameFormatter = c => c;
        }

        public override void Reset()
        {
            base.Reset();
            GoText = "\r\n";
        }

        public string TableNamePrefix { get; set; }

        public override SqlStringBuilder Where(IQueryFilter filter)
        {
            WhereFormat where = InterSystemsFormatProvider.GetWhereFormat(filter, StringBuilder, NextNumber);
            NextNumber = where.NextNumber;
            parameters.AddRange(where.Parameters);
            return this;
        }

        public override SqlStringBuilder Update(string tableName, params AssignValue[] values)
        {
            Builder.AppendFormat("UPDATE {0} ", TableNameFormatter(tableName));
            SetFormat set = InterSystemsFormatProvider.GetSetFormat(tableName, StringBuilder, NextNumber, values);
            NextNumber = set.NextNumber;
            parameters.AddRange(set.Parameters);
            return this;
        }

        public override SqlStringBuilder Id(string idAs)
        {
            Builder.AppendFormat("{0}SELECT LAST_IDENTITY() AS {1}", GoText, idAs);
            return this;
        }
    }
}
