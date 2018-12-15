using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Services.DataReplication.Data.Dao
{
    public class RetrieveOperationColumns: QueryFilter<RetrieveOperationColumns>, IFilterToken
    {
        public RetrieveOperationColumns() { }
        public RetrieveOperationColumns(string columnName)
            : base(columnName)
        { }
		
		public RetrieveOperationColumns KeyColumn
		{
			get
			{
				return new RetrieveOperationColumns("Id");
			}
		}	

				
        public RetrieveOperationColumns Id
        {
            get
            {
                return new RetrieveOperationColumns("Id");
            }
        }
        public RetrieveOperationColumns Uuid
        {
            get
            {
                return new RetrieveOperationColumns("Uuid");
            }
        }
        public RetrieveOperationColumns Cuid
        {
            get
            {
                return new RetrieveOperationColumns("Cuid");
            }
        }
        public RetrieveOperationColumns Identifier
        {
            get
            {
                return new RetrieveOperationColumns("Identifier");
            }
        }
        public RetrieveOperationColumns TypeNamespace
        {
            get
            {
                return new RetrieveOperationColumns("TypeNamespace");
            }
        }
        public RetrieveOperationColumns TypeName
        {
            get
            {
                return new RetrieveOperationColumns("TypeName");
            }
        }
        public RetrieveOperationColumns CreatedBy
        {
            get
            {
                return new RetrieveOperationColumns("CreatedBy");
            }
        }
        public RetrieveOperationColumns ModifiedBy
        {
            get
            {
                return new RetrieveOperationColumns("ModifiedBy");
            }
        }
        public RetrieveOperationColumns Modified
        {
            get
            {
                return new RetrieveOperationColumns("Modified");
            }
        }
        public RetrieveOperationColumns Deleted
        {
            get
            {
                return new RetrieveOperationColumns("Deleted");
            }
        }
        public RetrieveOperationColumns Created
        {
            get
            {
                return new RetrieveOperationColumns("Created");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(RetrieveOperation);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}