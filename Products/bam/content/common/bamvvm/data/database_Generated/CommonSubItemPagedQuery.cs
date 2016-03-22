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
    public class CommonSubItemPagedQuery: PagedQuery<CommonSubItemColumns, CommonSubItem>
    { 
		public CommonSubItemPagedQuery(CommonSubItemColumns orderByColumn, CommonSubItemQuery query, Database db = null) : base(orderByColumn, query, db) { }
    }
}