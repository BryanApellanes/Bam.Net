/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bam.Net.Data;
using Bam.Net.Data;

namespace Bam.Net.Data.Tests
{
    public class TableColumns: QueryFilter<TableColumns>, IFilterToken
    {
        public TableColumns() { }
        public TableColumns(string columnName)
            : base(columnName)
        { }
        public TableColumns TestOne
        {
            get
            {
                return new TableColumns("TestOne");
            }
        }

        public TableColumns ColumnOne
        {
            get
            {
                return new TableColumns("ColumnOne");
            }
        }

        public TableColumns ColumnTwo
        {
            get
            {
                return new TableColumns("ColumnTwo");
            }
        }

        public string Operator { get; set; }

        public override string ToString()
        {
            return this.Operator;
        }
    }
}
