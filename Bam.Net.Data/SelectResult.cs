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
    public class SelectResult : QueryResult
    {
        public SelectResult()
        {

        }

        public object Value { get; set; }

        #region IHasDataTable Members
        
        public override void SetDataTable(DataTable table)
        {
            DataTable = table;
        }

        #endregion
    }
}
