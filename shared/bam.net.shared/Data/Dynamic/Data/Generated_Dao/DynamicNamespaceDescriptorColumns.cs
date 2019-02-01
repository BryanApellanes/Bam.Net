using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Data.Dynamic.Data.Dao
{
    public class DynamicNamespaceDescriptorColumns: QueryFilter<DynamicNamespaceDescriptorColumns>, IFilterToken
    {
        public DynamicNamespaceDescriptorColumns() { }
        public DynamicNamespaceDescriptorColumns(string columnName)
            : base(columnName)
        { }
		
		public DynamicNamespaceDescriptorColumns KeyColumn
		{
			get
			{
				return new DynamicNamespaceDescriptorColumns("Id");
			}
		}	

				
        public DynamicNamespaceDescriptorColumns Id
        {
            get
            {
                return new DynamicNamespaceDescriptorColumns("Id");
            }
        }
        public DynamicNamespaceDescriptorColumns Uuid
        {
            get
            {
                return new DynamicNamespaceDescriptorColumns("Uuid");
            }
        }
        public DynamicNamespaceDescriptorColumns Cuid
        {
            get
            {
                return new DynamicNamespaceDescriptorColumns("Cuid");
            }
        }
        public DynamicNamespaceDescriptorColumns Namespace
        {
            get
            {
                return new DynamicNamespaceDescriptorColumns("Namespace");
            }
        }
        public DynamicNamespaceDescriptorColumns Key
        {
            get
            {
                return new DynamicNamespaceDescriptorColumns("Key");
            }
        }
        public DynamicNamespaceDescriptorColumns CreatedBy
        {
            get
            {
                return new DynamicNamespaceDescriptorColumns("CreatedBy");
            }
        }
        public DynamicNamespaceDescriptorColumns ModifiedBy
        {
            get
            {
                return new DynamicNamespaceDescriptorColumns("ModifiedBy");
            }
        }
        public DynamicNamespaceDescriptorColumns Modified
        {
            get
            {
                return new DynamicNamespaceDescriptorColumns("Modified");
            }
        }
        public DynamicNamespaceDescriptorColumns Deleted
        {
            get
            {
                return new DynamicNamespaceDescriptorColumns("Deleted");
            }
        }
        public DynamicNamespaceDescriptorColumns Created
        {
            get
            {
                return new DynamicNamespaceDescriptorColumns("Created");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(DynamicNamespaceDescriptor);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}