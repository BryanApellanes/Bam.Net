using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Services.DataReplication.Data.Dao
{
    public class QueryOperationColumns: QueryFilter<QueryOperationColumns>, IFilterToken
    {
        public QueryOperationColumns() { }
        public QueryOperationColumns(string columnName)
            : base(columnName)
        { }
		
		public QueryOperationColumns KeyColumn
		{
			get
			{
				return new QueryOperationColumns("Id");
			}
		}	

				
        public QueryOperationColumns Id
        {
            get
            {
                return new QueryOperationColumns("Id");
            }
        }
        public QueryOperationColumns Uuid
        {
            get
            {
                return new QueryOperationColumns("Uuid");
            }
        }
        public QueryOperationColumns Cuid
        {
            get
            {
                return new QueryOperationColumns("Cuid");
            }
        }
        public QueryOperationColumns TypeNamespace
        {
            get
            {
                return new QueryOperationColumns("TypeNamespace");
            }
        }
        public QueryOperationColumns TypeName
        {
            get
            {
                return new QueryOperationColumns("TypeName");
            }
        }
        public QueryOperationColumns CreatedBy
        {
            get
            {
                return new QueryOperationColumns("CreatedBy");
            }
        }
        public QueryOperationColumns ModifiedBy
        {
            get
            {
                return new QueryOperationColumns("ModifiedBy");
            }
        }
        public QueryOperationColumns Modified
        {
            get
            {
                return new QueryOperationColumns("Modified");
            }
        }
        public QueryOperationColumns Deleted
        {
            get
            {
                return new QueryOperationColumns("Deleted");
            }
        }
        public QueryOperationColumns Created
        {
            get
            {
                return new QueryOperationColumns("Created");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(QueryOperation);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}