using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.CoreServices.AssemblyManagement.Data.Dao
{
    public class AssemblyDescriptorProcessRuntimeDescriptorColumns: QueryFilter<AssemblyDescriptorProcessRuntimeDescriptorColumns>, IFilterToken
    {
        public AssemblyDescriptorProcessRuntimeDescriptorColumns() { }
        public AssemblyDescriptorProcessRuntimeDescriptorColumns(string columnName)
            : base(columnName)
        { }
		
		public AssemblyDescriptorProcessRuntimeDescriptorColumns KeyColumn
		{
			get
			{
				return new AssemblyDescriptorProcessRuntimeDescriptorColumns("Id");
			}
		}	

				
        public AssemblyDescriptorProcessRuntimeDescriptorColumns Id
        {
            get
            {
                return new AssemblyDescriptorProcessRuntimeDescriptorColumns("Id");
            }
        }
        public AssemblyDescriptorProcessRuntimeDescriptorColumns Uuid
        {
            get
            {
                return new AssemblyDescriptorProcessRuntimeDescriptorColumns("Uuid");
            }
        }

        public AssemblyDescriptorProcessRuntimeDescriptorColumns AssemblyDescriptorId
        {
            get
            {
                return new AssemblyDescriptorProcessRuntimeDescriptorColumns("AssemblyDescriptorId");
            }
        }
        public AssemblyDescriptorProcessRuntimeDescriptorColumns ProcessRuntimeDescriptorId
        {
            get
            {
                return new AssemblyDescriptorProcessRuntimeDescriptorColumns("ProcessRuntimeDescriptorId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(AssemblyDescriptorProcessRuntimeDescriptor);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}