/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bryan.Common.Data
{
    public class CommonItemPagedQuery: PagedQuery<CommonItemColumns, CommonItem>
    { 
		public CommonItemPagedQuery(CommonItemColumns orderByColumn, CommonItemQuery query, Database db = null) : base(orderByColumn, query, db) { }
    }
}