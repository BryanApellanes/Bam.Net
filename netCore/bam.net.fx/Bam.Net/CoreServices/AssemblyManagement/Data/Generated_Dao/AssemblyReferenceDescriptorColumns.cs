using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.CoreServices.AssemblyManagement.Data.Dao
{
    public class AssemblyReferenceDescriptorColumns: QueryFilter<AssemblyReferenceDescriptorColumns>, IFilterToken
    {
        public AssemblyReferenceDescriptorColumns() { }
        public AssemblyReferenceDescriptorColumns(string columnName)
            : base(columnName)
        { }
		
		public AssemblyReferenceDescriptorColumns KeyColumn
		{
			get
			{
				return new AssemblyReferenceDescriptorColumns("Id");
			}
		}	

				
        public AssemblyReferenceDescriptorColumns Id
        {
            get
            {
                return new AssemblyReferenceDescriptorColumns("Id");
            }
        }
        public AssemblyReferenceDescriptorColumns Uuid
        {
            get
            {
                return new AssemblyReferenceDescriptorColumns("Uuid");
            }
        }
        public AssemblyReferenceDescriptorColumns Cuid
        {
            get
            {
                return new AssemblyReferenceDescriptorColumns("Cuid");
            }
        }
        public AssemblyReferenceDescriptorColumns ReferencerHash
        {
            get
            {
                return new AssemblyReferenceDescriptorColumns("ReferencerHash");
            }
        }
        public AssemblyReferenceDescriptorColumns ReferencedHash
        {
            get
            {
                return new AssemblyReferenceDescriptorColumns("ReferencedHash");
            }
        }
        public AssemblyReferenceDescriptorColumns Created
        {
            get
            {
                return new AssemblyReferenceDescriptorColumns("Created");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(AssemblyReferenceDescriptor);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}