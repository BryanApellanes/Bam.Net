using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.CoreServices.AssemblyManagement.Data.Dao
{
    public class AssemblyDescriptorColumns: QueryFilter<AssemblyDescriptorColumns>, IFilterToken
    {
        public AssemblyDescriptorColumns() { }
        public AssemblyDescriptorColumns(string columnName)
            : base(columnName)
        { }
		
		public AssemblyDescriptorColumns KeyColumn
		{
			get
			{
				return new AssemblyDescriptorColumns("Id");
			}
		}	

				
        public AssemblyDescriptorColumns Id
        {
            get
            {
                return new AssemblyDescriptorColumns("Id");
            }
        }
        public AssemblyDescriptorColumns Uuid
        {
            get
            {
                return new AssemblyDescriptorColumns("Uuid");
            }
        }
        public AssemblyDescriptorColumns Cuid
        {
            get
            {
                return new AssemblyDescriptorColumns("Cuid");
            }
        }
        public AssemblyDescriptorColumns Name
        {
            get
            {
                return new AssemblyDescriptorColumns("Name");
            }
        }
        public AssemblyDescriptorColumns FileHash
        {
            get
            {
                return new AssemblyDescriptorColumns("FileHash");
            }
        }
        public AssemblyDescriptorColumns AssemblyFullName
        {
            get
            {
                return new AssemblyDescriptorColumns("AssemblyFullName");
            }
        }
        public AssemblyDescriptorColumns Created
        {
            get
            {
                return new AssemblyDescriptorColumns("Created");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(AssemblyDescriptor);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}