/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Services.AssemblyManagement.Data.Dao
{
    public class AssemblyDescriptorPagedQuery: PagedQuery<AssemblyDescriptorColumns, AssemblyDescriptor>
    { 
		public AssemblyDescriptorPagedQuery(AssemblyDescriptorColumns orderByColumn, AssemblyDescriptorQuery query, Database db = null) : base(orderByColumn, query, db) { }
    }
}