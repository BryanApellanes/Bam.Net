using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Data.Dynamic.Data.Dao
{
    public class DataInstanceColumns: QueryFilter<DataInstanceColumns>, IFilterToken
    {
        public DataInstanceColumns() { }
        public DataInstanceColumns(string columnName)
            : base(columnName)
        { }
		
		public DataInstanceColumns KeyColumn
		{
			get
			{
				return new DataInstanceColumns("Id");
			}
		}	

				
        public DataInstanceColumns Id
        {
            get
            {
                return new DataInstanceColumns("Id");
            }
        }
        public DataInstanceColumns Uuid
        {
            get
            {
                return new DataInstanceColumns("Uuid");
            }
        }
        public DataInstanceColumns Cuid
        {
            get
            {
                return new DataInstanceColumns("Cuid");
            }
        }
        public DataInstanceColumns TypeName
        {
            get
            {
                return new DataInstanceColumns("TypeName");
            }
        }
        public DataInstanceColumns RootHash
        {
            get
            {
                return new DataInstanceColumns("RootHash");
            }
        }
        public DataInstanceColumns ParentHash
        {
            get
            {
                return new DataInstanceColumns("ParentHash");
            }
        }
        public DataInstanceColumns Instancehash
        {
            get
            {
                return new DataInstanceColumns("Instancehash");
            }
        }
        public DataInstanceColumns Key
        {
            get
            {
                return new DataInstanceColumns("Key");
            }
        }
        public DataInstanceColumns CreatedBy
        {
            get
            {
                return new DataInstanceColumns("CreatedBy");
            }
        }
        public DataInstanceColumns ModifiedBy
        {
            get
            {
                return new DataInstanceColumns("ModifiedBy");
            }
        }
        public DataInstanceColumns Modified
        {
            get
            {
                return new DataInstanceColumns("Modified");
            }
        }
        public DataInstanceColumns Deleted
        {
            get
            {
                return new DataInstanceColumns("Deleted");
            }
        }
        public DataInstanceColumns Created
        {
            get
            {
                return new DataInstanceColumns("Created");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(DataInstance);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}