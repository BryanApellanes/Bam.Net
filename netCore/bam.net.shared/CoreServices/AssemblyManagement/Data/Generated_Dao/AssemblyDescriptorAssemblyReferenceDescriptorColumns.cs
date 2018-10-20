using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.CoreServices.AssemblyManagement.Data.Dao
{
    public class AssemblyDescriptorAssemblyReferenceDescriptorColumns: QueryFilter<AssemblyDescriptorAssemblyReferenceDescriptorColumns>, IFilterToken
    {
        public AssemblyDescriptorAssemblyReferenceDescriptorColumns() { }
        public AssemblyDescriptorAssemblyReferenceDescriptorColumns(string columnName)
            : base(columnName)
        { }
		
		public AssemblyDescriptorAssemblyReferenceDescriptorColumns KeyColumn
		{
			get
			{
				return new AssemblyDescriptorAssemblyReferenceDescriptorColumns("Id");
			}
		}	

				
        public AssemblyDescriptorAssemblyReferenceDescriptorColumns Id
        {
            get
            {
                return new AssemblyDescriptorAssemblyReferenceDescriptorColumns("Id");
            }
        }
        public AssemblyDescriptorAssemblyReferenceDescriptorColumns Uuid
        {
            get
            {
                return new AssemblyDescriptorAssemblyReferenceDescriptorColumns("Uuid");
            }
        }

        public AssemblyDescriptorAssemblyReferenceDescriptorColumns AssemblyDescriptorId
        {
            get
            {
                return new AssemblyDescriptorAssemblyReferenceDescriptorColumns("AssemblyDescriptorId");
            }
        }
        public AssemblyDescriptorAssemblyReferenceDescriptorColumns AssemblyReferenceDescriptorId
        {
            get
            {
                return new AssemblyDescriptorAssemblyReferenceDescriptorColumns("AssemblyReferenceDescriptorId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(AssemblyDescriptorAssemblyReferenceDescriptor);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}