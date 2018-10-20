/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bam.Net.Data.MySql;

namespace Bam.Net.Data
{
    public class MySqlQuerySet : QuerySet
    {
        public MySqlQuerySet()
            : base()
        {
            this.GoText = ";\r\n";
            this.TableNameFormatter = (s) => "`{0}`"._Format(s);
            this.ColumnNameFormatter = (s) => s;
        }

        public override SqlStringBuilder Id(string idAs)
        {
            Builder.AppendFormat("{0}SELECT last_insert_id() AS {1}", this.GoText, idAs);
            return this;
        }

        public override SqlStringBuilder Where(IQueryFilter filter)
        {
            WhereFormat where = MySqlFormatProvider.GetWhereFormat(filter, StringBuilder, NextNumber);
            NextNumber = where.NextNumber;
            parameters.AddRange(where.Parameters);
            return this;
        }

        public override SqlStringBuilder Update(string tableName, params AssignValue[] values)
        {
            Builder.AppendFormat("UPDATE {0} ", TableNameFormatter(tableName));
            SetFormat set = MySqlFormatProvider.GetSetFormat(tableName, StringBuilder, NextNumber, values);
            NextNumber = set.NextNumber;
            parameters.AddRange(set.Parameters);
            return this;
        }


        public int Limit
        {
            get;
            set;
        }

        public override SqlStringBuilder SelectTop(int topCount, string tableName, params string[] columnNames)
        {
            this.Limit = topCount;

            if (columnNames.Length == 0)
            {
                columnNames = new string[] { "*" };
            }

            string cols = columnNames.ToDelimited(s => string.Format("{0}", s));
            StringBuilder.AppendFormat("SELECT {0} FROM `{1}` ", cols, tableName);
            return this;
        }

        public override void Execute(Database db)
        {
            if (Limit > 0)
            {
                Go();
            }
            base.Execute(db);
        }

        public override SqlStringBuilder Go()
        {
            if (this.Limit > 0)
            {
                StringBuilder.AppendFormat(" Limit {0} ", this.Limit);
            }

            base.Go();
            this.Limit = -1;
            return this;
        }
    }
}
