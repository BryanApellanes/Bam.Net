using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Data.Dynamic.Data.Dao
{
    public class DynamicTypeDescriptorColumns: QueryFilter<DynamicTypeDescriptorColumns>, IFilterToken
    {
        public DynamicTypeDescriptorColumns() { }
        public DynamicTypeDescriptorColumns(string columnName)
            : base(columnName)
        { }
		
		public DynamicTypeDescriptorColumns KeyColumn
		{
			get
			{
				return new DynamicTypeDescriptorColumns("Id");
			}
		}	

				
        public DynamicTypeDescriptorColumns Id
        {
            get
            {
                return new DynamicTypeDescriptorColumns("Id");
            }
        }
        public DynamicTypeDescriptorColumns Uuid
        {
            get
            {
                return new DynamicTypeDescriptorColumns("Uuid");
            }
        }
        public DynamicTypeDescriptorColumns Cuid
        {
            get
            {
                return new DynamicTypeDescriptorColumns("Cuid");
            }
        }
        public DynamicTypeDescriptorColumns TypeName
        {
            get
            {
                return new DynamicTypeDescriptorColumns("TypeName");
            }
        }
        public DynamicTypeDescriptorColumns Created
        {
            get
            {
                return new DynamicTypeDescriptorColumns("Created");
            }
        }

        public DynamicTypeDescriptorColumns DynamicNamespaceDescriptorId
        {
            get
            {
                return new DynamicTypeDescriptorColumns("DynamicNamespaceDescriptorId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(DynamicTypeDescriptor);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}