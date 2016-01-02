/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.DaoRef
{
    public class RightPagedQuery: PagedQuery<RightColumns, Right>
    { 
		public RightPagedQuery(RightColumns orderByColumn, RightQuery query, Database db = null) : base(orderByColumn, query, db) { }
    }
}