/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.CoreServices.AccessControl.Data.Dao
{
    public class ResourcePagedQuery: PagedQuery<ResourceColumns, Resource>
    { 
		public ResourcePagedQuery(ResourceColumns orderByColumn, ResourceQuery query, Database db = null) : base(orderByColumn, query, db) { }
    }
}