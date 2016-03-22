/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Automation.Data
{
    public class DeferredJobDataPagedQuery: PagedQuery<DeferredJobDataColumns, DeferredJobData>
    { 
		public DeferredJobDataPagedQuery(DeferredJobDataColumns orderByColumn, DeferredJobDataQuery query, Database db = null) : base(orderByColumn, query, db) { }
    }
}