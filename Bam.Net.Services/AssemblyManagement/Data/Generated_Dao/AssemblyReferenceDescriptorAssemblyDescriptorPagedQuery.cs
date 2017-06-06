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
    public class AssemblyReferenceDescriptorAssemblyDescriptorPagedQuery: PagedQuery<AssemblyReferenceDescriptorAssemblyDescriptorColumns, AssemblyReferenceDescriptorAssemblyDescriptor>
    { 
		public AssemblyReferenceDescriptorAssemblyDescriptorPagedQuery(AssemblyReferenceDescriptorAssemblyDescriptorColumns orderByColumn, AssemblyReferenceDescriptorAssemblyDescriptorQuery query, Database db = null) : base(orderByColumn, query, db) { }
    }
}