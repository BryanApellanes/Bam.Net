/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.DaoRef
{
    public class LeftRightPagedQuery: PagedQuery<LeftRightColumns, LeftRight>
    { 
		public LeftRightPagedQuery(LeftRightColumns orderByColumn, LeftRightQuery query, Database db = null) : base(orderByColumn, query, db) { }
    }
}