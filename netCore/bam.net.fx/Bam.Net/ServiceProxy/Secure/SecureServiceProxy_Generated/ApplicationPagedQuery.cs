/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.ServiceProxy.Secure
{
    public class ApplicationPagedQuery: PagedQuery<ApplicationColumns, Application>
    { 
		public ApplicationPagedQuery(ApplicationColumns orderByColumn, ApplicationQuery query, Database db = null) : base(orderByColumn, query, db) { }
    }
}