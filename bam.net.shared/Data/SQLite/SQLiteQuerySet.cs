/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bam.Net.Data
{
    public class SQLiteQuerySet: QuerySet
    {
        public override SqlStringBuilder Id(string idAs)
        {
            StringBuilder.AppendFormat("{0}SELECT last_insert_rowid() AS {1}", this.GoText, idAs);
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
            StringBuilder.AppendFormat("SELECT {0} FROM [{1}] ", cols, tableName);
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
