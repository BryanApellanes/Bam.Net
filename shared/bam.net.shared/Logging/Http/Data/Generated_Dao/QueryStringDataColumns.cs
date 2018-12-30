using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Logging.Http.Data.Dao
{
    public class QueryStringDataColumns: QueryFilter<QueryStringDataColumns>, IFilterToken
    {
        public QueryStringDataColumns() { }
        public QueryStringDataColumns(string columnName)
            : base(columnName)
        { }
		
		public QueryStringDataColumns KeyColumn
		{
			get
			{
				return new QueryStringDataColumns("Id");
			}
		}	

				
        public QueryStringDataColumns Id
        {
            get
            {
                return new QueryStringDataColumns("Id");
            }
        }
        public QueryStringDataColumns Uuid
        {
            get
            {
                return new QueryStringDataColumns("Uuid");
            }
        }
        public QueryStringDataColumns Cuid
        {
            get
            {
                return new QueryStringDataColumns("Cuid");
            }
        }
        public QueryStringDataColumns Created
        {
            get
            {
                return new QueryStringDataColumns("Created");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(QueryStringData);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}