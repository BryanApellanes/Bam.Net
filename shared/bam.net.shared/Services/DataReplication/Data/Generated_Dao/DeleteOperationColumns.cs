using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Services.DataReplication.Data.Dao
{
    public class DeleteOperationColumns: QueryFilter<DeleteOperationColumns>, IFilterToken
    {
        public DeleteOperationColumns() { }
        public DeleteOperationColumns(string columnName)
            : base(columnName)
        { }
		
		public DeleteOperationColumns KeyColumn
		{
			get
			{
				return new DeleteOperationColumns("Id");
			}
		}	

				
        public DeleteOperationColumns Id
        {
            get
            {
                return new DeleteOperationColumns("Id");
            }
        }
        public DeleteOperationColumns Uuid
        {
            get
            {
                return new DeleteOperationColumns("Uuid");
            }
        }
        public DeleteOperationColumns Cuid
        {
            get
            {
                return new DeleteOperationColumns("Cuid");
            }
        }
        public DeleteOperationColumns Identifier
        {
            get
            {
                return new DeleteOperationColumns("Identifier");
            }
        }
        public DeleteOperationColumns TypeNamespace
        {
            get
            {
                return new DeleteOperationColumns("TypeNamespace");
            }
        }
        public DeleteOperationColumns TypeName
        {
            get
            {
                return new DeleteOperationColumns("TypeName");
            }
        }
        public DeleteOperationColumns CreatedBy
        {
            get
            {
                return new DeleteOperationColumns("CreatedBy");
            }
        }
        public DeleteOperationColumns ModifiedBy
        {
            get
            {
                return new DeleteOperationColumns("ModifiedBy");
            }
        }
        public DeleteOperationColumns Modified
        {
            get
            {
                return new DeleteOperationColumns("Modified");
            }
        }
        public DeleteOperationColumns Deleted
        {
            get
            {
                return new DeleteOperationColumns("Deleted");
            }
        }
        public DeleteOperationColumns Created
        {
            get
            {
                return new DeleteOperationColumns("Created");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(DeleteOperation);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}