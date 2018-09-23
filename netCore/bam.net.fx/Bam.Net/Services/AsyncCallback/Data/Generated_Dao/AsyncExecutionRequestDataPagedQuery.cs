/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Services.AsyncCallback.Data.Dao
{
    public class AsyncExecutionRequestDataPagedQuery: PagedQuery<AsyncExecutionRequestDataColumns, AsyncExecutionRequestData>
    { 
		public AsyncExecutionRequestDataPagedQuery(AsyncExecutionRequestDataColumns orderByColumn, AsyncExecutionRequestDataQuery query, Database db = null) : base(orderByColumn, query, db) { }
    }
}