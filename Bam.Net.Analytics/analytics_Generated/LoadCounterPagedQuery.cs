/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Analytics
{
    public class LoadCounterPagedQuery: PagedQuery<LoadCounterColumns, LoadCounter>
    { 
		public LoadCounterPagedQuery(LoadCounterColumns orderByColumn, LoadCounterQuery query, Database db = null) : base(orderByColumn, query, db) { }
    }
}