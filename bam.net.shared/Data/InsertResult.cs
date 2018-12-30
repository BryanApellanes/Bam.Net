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
    public class InsertResult: QueryResult
    {
        public InsertResult(object instance)
            : this(instance, "ID")
        {
        }

        public InsertResult(object instance, string idAs)
        {
            this.Value = instance;
            this.ColumnName = idAs;
        }
        
        public object Value { get; set; }
        public string ColumnName { get; private set; }

        public override void SetDataTable(DataTable table)
        {
            DataTable = table;            
            ((Dao)Value).IdValue = Convert.ToUInt64(DataTable.Rows[0][0]);
        }
    }
}
