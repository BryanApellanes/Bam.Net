using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Data.Dynamic.Data.Dao
{
    public class DataInstancePropertyValueColumns: QueryFilter<DataInstancePropertyValueColumns>, IFilterToken
    {
        public DataInstancePropertyValueColumns() { }
        public DataInstancePropertyValueColumns(string columnName)
            : base(columnName)
        { }
		
		public DataInstancePropertyValueColumns KeyColumn
		{
			get
			{
				return new DataInstancePropertyValueColumns("Id");
			}
		}	

				
        public DataInstancePropertyValueColumns Id
        {
            get
            {
                return new DataInstancePropertyValueColumns("Id");
            }
        }
        public DataInstancePropertyValueColumns Uuid
        {
            get
            {
                return new DataInstancePropertyValueColumns("Uuid");
            }
        }
        public DataInstancePropertyValueColumns Cuid
        {
            get
            {
                return new DataInstancePropertyValueColumns("Cuid");
            }
        }
        public DataInstancePropertyValueColumns RootHash
        {
            get
            {
                return new DataInstancePropertyValueColumns("RootHash");
            }
        }
        public DataInstancePropertyValueColumns InstanceHash
        {
            get
            {
                return new DataInstancePropertyValueColumns("InstanceHash");
            }
        }
        public DataInstancePropertyValueColumns ParentTypeNamespace
        {
            get
            {
                return new DataInstancePropertyValueColumns("ParentTypeNamespace");
            }
        }
        public DataInstancePropertyValueColumns ParentTypeName
        {
            get
            {
                return new DataInstancePropertyValueColumns("ParentTypeName");
            }
        }
        public DataInstancePropertyValueColumns PropertyName
        {
            get
            {
                return new DataInstancePropertyValueColumns("PropertyName");
            }
        }
        public DataInstancePropertyValueColumns Value
        {
            get
            {
                return new DataInstancePropertyValueColumns("Value");
            }
        }
        public DataInstancePropertyValueColumns Key
        {
            get
            {
                return new DataInstancePropertyValueColumns("Key");
            }
        }
        public DataInstancePropertyValueColumns CreatedBy
        {
            get
            {
                return new DataInstancePropertyValueColumns("CreatedBy");
            }
        }
        public DataInstancePropertyValueColumns ModifiedBy
        {
            get
            {
                return new DataInstancePropertyValueColumns("ModifiedBy");
            }
        }
        public DataInstancePropertyValueColumns Modified
        {
            get
            {
                return new DataInstancePropertyValueColumns("Modified");
            }
        }
        public DataInstancePropertyValueColumns Deleted
        {
            get
            {
                return new DataInstancePropertyValueColumns("Deleted");
            }
        }
        public DataInstancePropertyValueColumns Created
        {
            get
            {
                return new DataInstancePropertyValueColumns("Created");
            }
        }

        public DataInstancePropertyValueColumns DataInstanceId
        {
            get
            {
                return new DataInstancePropertyValueColumns("DataInstanceId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(DataInstancePropertyValue);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}