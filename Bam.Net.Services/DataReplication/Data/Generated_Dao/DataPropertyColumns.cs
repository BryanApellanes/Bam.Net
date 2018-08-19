using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Services.DataReplication.Data.Dao
{
    public class DataPropertyColumns: QueryFilter<DataPropertyColumns>, IFilterToken
    {
        public DataPropertyColumns() { }
        public DataPropertyColumns(string columnName)
            : base(columnName)
        { }
		
		public DataPropertyColumns KeyColumn
		{
			get
			{
				return new DataPropertyColumns("Id");
			}
		}	

				
        public DataPropertyColumns Id
        {
            get
            {
                return new DataPropertyColumns("Id");
            }
        }
        public DataPropertyColumns Uuid
        {
            get
            {
                return new DataPropertyColumns("Uuid");
            }
        }
        public DataPropertyColumns Cuid
        {
            get
            {
                return new DataPropertyColumns("Cuid");
            }
        }
        public DataPropertyColumns InstanceCuid
        {
            get
            {
                return new DataPropertyColumns("InstanceCuid");
            }
        }
        public DataPropertyColumns Name
        {
            get
            {
                return new DataPropertyColumns("Name");
            }
        }
        public DataPropertyColumns CreatedBy
        {
            get
            {
                return new DataPropertyColumns("CreatedBy");
            }
        }
        public DataPropertyColumns ModifiedBy
        {
            get
            {
                return new DataPropertyColumns("ModifiedBy");
            }
        }
        public DataPropertyColumns Modified
        {
            get
            {
                return new DataPropertyColumns("Modified");
            }
        }
        public DataPropertyColumns Deleted
        {
            get
            {
                return new DataPropertyColumns("Deleted");
            }
        }
        public DataPropertyColumns Created
        {
            get
            {
                return new DataPropertyColumns("Created");
            }
        }

        public DataPropertyColumns CreateOperationId
        {
            get
            {
                return new DataPropertyColumns("CreateOperationId");
            }
        }
        public DataPropertyColumns DeleteEventId
        {
            get
            {
                return new DataPropertyColumns("DeleteEventId");
            }
        }
        public DataPropertyColumns DeleteOperationId
        {
            get
            {
                return new DataPropertyColumns("DeleteOperationId");
            }
        }
        public DataPropertyColumns QueryOperationId
        {
            get
            {
                return new DataPropertyColumns("QueryOperationId");
            }
        }
        public DataPropertyColumns SaveOperationId
        {
            get
            {
                return new DataPropertyColumns("SaveOperationId");
            }
        }
        public DataPropertyColumns UpdateOperationId
        {
            get
            {
                return new DataPropertyColumns("UpdateOperationId");
            }
        }
        public DataPropertyColumns WriteEventId
        {
            get
            {
                return new DataPropertyColumns("WriteEventId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(DataProperty);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}