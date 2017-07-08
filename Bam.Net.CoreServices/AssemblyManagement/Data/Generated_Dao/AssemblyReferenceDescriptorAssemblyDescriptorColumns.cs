using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.CoreServices.AssemblyManagement.Data.Dao
{
    public class AssemblyReferenceDescriptorAssemblyDescriptorColumns: QueryFilter<AssemblyReferenceDescriptorAssemblyDescriptorColumns>, IFilterToken
    {
        public AssemblyReferenceDescriptorAssemblyDescriptorColumns() { }
        public AssemblyReferenceDescriptorAssemblyDescriptorColumns(string columnName)
            : base(columnName)
        { }
		
		public AssemblyReferenceDescriptorAssemblyDescriptorColumns KeyColumn
		{
			get
			{
				return new AssemblyReferenceDescriptorAssemblyDescriptorColumns("Id");
			}
		}	

				
        public AssemblyReferenceDescriptorAssemblyDescriptorColumns Id
        {
            get
            {
                return new AssemblyReferenceDescriptorAssemblyDescriptorColumns("Id");
            }
        }
        public AssemblyReferenceDescriptorAssemblyDescriptorColumns Uuid
        {
            get
            {
                return new AssemblyReferenceDescriptorAssemblyDescriptorColumns("Uuid");
            }
        }

        public AssemblyReferenceDescriptorAssemblyDescriptorColumns AssemblyReferenceDescriptorId
        {
            get
            {
                return new AssemblyReferenceDescriptorAssemblyDescriptorColumns("AssemblyReferenceDescriptorId");
            }
        }
        public AssemblyReferenceDescriptorAssemblyDescriptorColumns AssemblyDescriptorId
        {
            get
            {
                return new AssemblyReferenceDescriptorAssemblyDescriptorColumns("AssemblyDescriptorId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(AssemblyReferenceDescriptorAssemblyDescriptor);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}