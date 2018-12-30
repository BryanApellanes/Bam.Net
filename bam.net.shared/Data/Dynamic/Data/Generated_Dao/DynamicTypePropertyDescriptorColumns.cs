using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Data.Dynamic.Data.Dao
{
    public class DynamicTypePropertyDescriptorColumns: QueryFilter<DynamicTypePropertyDescriptorColumns>, IFilterToken
    {
        public DynamicTypePropertyDescriptorColumns() { }
        public DynamicTypePropertyDescriptorColumns(string columnName)
            : base(columnName)
        { }
		
		public DynamicTypePropertyDescriptorColumns KeyColumn
		{
			get
			{
				return new DynamicTypePropertyDescriptorColumns("Id");
			}
		}	

				
        public DynamicTypePropertyDescriptorColumns Id
        {
            get
            {
                return new DynamicTypePropertyDescriptorColumns("Id");
            }
        }
        public DynamicTypePropertyDescriptorColumns Uuid
        {
            get
            {
                return new DynamicTypePropertyDescriptorColumns("Uuid");
            }
        }
        public DynamicTypePropertyDescriptorColumns Cuid
        {
            get
            {
                return new DynamicTypePropertyDescriptorColumns("Cuid");
            }
        }
        public DynamicTypePropertyDescriptorColumns ParentTypeName
        {
            get
            {
                return new DynamicTypePropertyDescriptorColumns("ParentTypeName");
            }
        }
        public DynamicTypePropertyDescriptorColumns PropertyType
        {
            get
            {
                return new DynamicTypePropertyDescriptorColumns("PropertyType");
            }
        }
        public DynamicTypePropertyDescriptorColumns PropertyName
        {
            get
            {
                return new DynamicTypePropertyDescriptorColumns("PropertyName");
            }
        }
        public DynamicTypePropertyDescriptorColumns Created
        {
            get
            {
                return new DynamicTypePropertyDescriptorColumns("Created");
            }
        }

        public DynamicTypePropertyDescriptorColumns DynamicTypeDescriptorId
        {
            get
            {
                return new DynamicTypePropertyDescriptorColumns("DynamicTypeDescriptorId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(DynamicTypePropertyDescriptor);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}