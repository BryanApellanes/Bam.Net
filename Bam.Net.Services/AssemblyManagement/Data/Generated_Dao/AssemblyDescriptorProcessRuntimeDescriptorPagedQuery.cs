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
    public class AssemblyDescriptorProcessRuntimeDescriptorPagedQuery: PagedQuery<AssemblyDescriptorProcessRuntimeDescriptorColumns, AssemblyDescriptorProcessRuntimeDescriptor>
    { 
		public AssemblyDescriptorProcessRuntimeDescriptorPagedQuery(AssemblyDescriptorProcessRuntimeDescriptorColumns orderByColumn, AssemblyDescriptorProcessRuntimeDescriptorQuery query, Database db = null) : base(orderByColumn, query, db) { }
    }
}