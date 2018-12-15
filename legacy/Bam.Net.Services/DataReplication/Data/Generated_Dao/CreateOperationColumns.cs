using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Services.DataReplication.Data.Dao
{
    public class CreateOperationColumns: QueryFilter<CreateOperationColumns>, IFilterToken
    {
        public CreateOperationColumns() { }
        public CreateOperationColumns(string columnName)
            : base(columnName)
        { }
		
		public CreateOperationColumns KeyColumn
		{
			get
			{
				return new CreateOperationColumns("Id");
			}
		}	

				
        public CreateOperationColumns Id
        {
            get
            {
                return new CreateOperationColumns("Id");
            }
        }
        public CreateOperationColumns Uuid
        {
            get
            {
                return new CreateOperationColumns("Uuid");
            }
        }
        public CreateOperationColumns Cuid
        {
            get
            {
                return new CreateOperationColumns("Cuid");
            }
        }
        public CreateOperationColumns TypeNamespace
        {
            get
            {
                return new CreateOperationColumns("TypeNamespace");
            }
        }
        public CreateOperationColumns TypeName
        {
            get
            {
                return new CreateOperationColumns("TypeName");
            }
        }
        public CreateOperationColumns CreatedBy
        {
            get
            {
                return new CreateOperationColumns("CreatedBy");
            }
        }
        public CreateOperationColumns ModifiedBy
        {
            get
            {
                return new CreateOperationColumns("ModifiedBy");
            }
        }
        public CreateOperationColumns Modified
        {
            get
            {
                return new CreateOperationColumns("Modified");
            }
        }
        public CreateOperationColumns Deleted
        {
            get
            {
                return new CreateOperationColumns("Deleted");
            }
        }
        public CreateOperationColumns Created
        {
            get
            {
                return new CreateOperationColumns("Created");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(CreateOperation);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}