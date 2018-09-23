using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Services.DataReplication.Data.Dao
{
    public class UpdateOperationColumns: QueryFilter<UpdateOperationColumns>, IFilterToken
    {
        public UpdateOperationColumns() { }
        public UpdateOperationColumns(string columnName)
            : base(columnName)
        { }
		
		public UpdateOperationColumns KeyColumn
		{
			get
			{
				return new UpdateOperationColumns("Id");
			}
		}	

				
        public UpdateOperationColumns Id
        {
            get
            {
                return new UpdateOperationColumns("Id");
            }
        }
        public UpdateOperationColumns Uuid
        {
            get
            {
                return new UpdateOperationColumns("Uuid");
            }
        }
        public UpdateOperationColumns Cuid
        {
            get
            {
                return new UpdateOperationColumns("Cuid");
            }
        }
        public UpdateOperationColumns TypeNamespace
        {
            get
            {
                return new UpdateOperationColumns("TypeNamespace");
            }
        }
        public UpdateOperationColumns TypeName
        {
            get
            {
                return new UpdateOperationColumns("TypeName");
            }
        }
        public UpdateOperationColumns CreatedBy
        {
            get
            {
                return new UpdateOperationColumns("CreatedBy");
            }
        }
        public UpdateOperationColumns ModifiedBy
        {
            get
            {
                return new UpdateOperationColumns("ModifiedBy");
            }
        }
        public UpdateOperationColumns Modified
        {
            get
            {
                return new UpdateOperationColumns("Modified");
            }
        }
        public UpdateOperationColumns Deleted
        {
            get
            {
                return new UpdateOperationColumns("Deleted");
            }
        }
        public UpdateOperationColumns Created
        {
            get
            {
                return new UpdateOperationColumns("Created");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(UpdateOperation);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}