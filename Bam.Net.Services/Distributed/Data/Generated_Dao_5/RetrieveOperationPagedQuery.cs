/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Services.Distributed.Data.Dao
{
    public class RetrieveOperationPagedQuery: PagedQuery<RetrieveOperationColumns, RetrieveOperation>
    { 
		public RetrieveOperationPagedQuery(RetrieveOperationColumns orderByColumn, RetrieveOperationQuery query, Database db = null) : base(orderByColumn, query, db) { }
    }
}