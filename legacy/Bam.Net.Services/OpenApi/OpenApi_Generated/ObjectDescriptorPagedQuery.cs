/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Services.OpenApi
{
    public class ObjectDescriptorPagedQuery: PagedQuery<ObjectDescriptorColumns, ObjectDescriptor>
    { 
		public ObjectDescriptorPagedQuery(ObjectDescriptorColumns orderByColumn, ObjectDescriptorQuery query, Database db = null) : base(orderByColumn, query, db) { }
    }
}