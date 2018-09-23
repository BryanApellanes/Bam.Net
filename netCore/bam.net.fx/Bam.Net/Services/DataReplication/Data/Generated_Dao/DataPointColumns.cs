using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Services.DataReplication.Data.Dao
{
    public class DataPointColumns: QueryFilter<DataPointColumns>, IFilterToken
    {
        public DataPointColumns() { }
        public DataPointColumns(string columnName)
            : base(columnName)
        { }
		
		public DataPointColumns KeyColumn
		{
			get
			{
				return new DataPointColumns("Id");
			}
		}	

				
        public DataPointColumns Id
        {
            get
            {
                return new DataPointColumns("Id");
            }
        }
        public DataPointColumns Uuid
        {
            get
            {
                return new DataPointColumns("Uuid");
            }
        }
        public DataPointColumns Cuid
        {
            get
            {
                return new DataPointColumns("Cuid");
            }
        }
        public DataPointColumns TypeNamespace
        {
            get
            {
                return new DataPointColumns("TypeNamespace");
            }
        }
        public DataPointColumns TypeName
        {
            get
            {
                return new DataPointColumns("TypeName");
            }
        }
        public DataPointColumns Description
        {
            get
            {
                return new DataPointColumns("Description");
            }
        }
        public DataPointColumns DataProperties
        {
            get
            {
                return new DataPointColumns("DataProperties");
            }
        }
        public DataPointColumns CreatedBy
        {
            get
            {
                return new DataPointColumns("CreatedBy");
            }
        }
        public DataPointColumns ModifiedBy
        {
            get
            {
                return new DataPointColumns("ModifiedBy");
            }
        }
        public DataPointColumns Modified
        {
            get
            {
                return new DataPointColumns("Modified");
            }
        }
        public DataPointColumns Deleted
        {
            get
            {
                return new DataPointColumns("Deleted");
            }
        }
        public DataPointColumns Created
        {
            get
            {
                return new DataPointColumns("Created");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(DataPoint);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}