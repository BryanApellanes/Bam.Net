/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Bam.Net.Data
{
    public class CountResult: QueryResult
    {
        public long Value
        {
            get;
            private set;
        }
        public override void SetDataTable(DataTable table)
        {
            Value = -1;
            this.DataTable = table;
            if (table.Rows.Count > 0 && table.Columns.Count > 0)
            {
                Value = Convert.ToInt64(table.Rows[0][0]);
            }
        }
    }
}
