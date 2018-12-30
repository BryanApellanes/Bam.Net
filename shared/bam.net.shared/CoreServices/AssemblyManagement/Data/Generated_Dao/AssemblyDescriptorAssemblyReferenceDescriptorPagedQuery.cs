/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.CoreServices.AssemblyManagement.Data.Dao
{
    public class AssemblyDescriptorAssemblyReferenceDescriptorPagedQuery: PagedQuery<AssemblyDescriptorAssemblyReferenceDescriptorColumns, AssemblyDescriptorAssemblyReferenceDescriptor>
    { 
		public AssemblyDescriptorAssemblyReferenceDescriptorPagedQuery(AssemblyDescriptorAssemblyReferenceDescriptorColumns orderByColumn, AssemblyDescriptorAssemblyReferenceDescriptorQuery query, Database db = null) : base(orderByColumn, query, db) { }
    }
}