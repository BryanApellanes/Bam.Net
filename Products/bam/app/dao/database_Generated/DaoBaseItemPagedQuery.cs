/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Data
{
    public class DaoBaseItemPagedQuery: PagedQuery<DaoBaseItemColumns, DaoBaseItem>
    { 
		public DaoBaseItemPagedQuery(DaoBaseItemColumns orderByColumn, DaoBaseItemQuery query, Database db = null) : base(orderByColumn, query, db) { }
    }
}