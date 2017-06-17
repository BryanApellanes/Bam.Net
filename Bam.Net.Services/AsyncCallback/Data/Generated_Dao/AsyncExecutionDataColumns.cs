using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Services.AsyncCallback.Data.Dao
{
    public class AsyncExecutionDataColumns: QueryFilter<AsyncExecutionDataColumns>, IFilterToken
    {
        public AsyncExecutionDataColumns() { }
        public AsyncExecutionDataColumns(string columnName)
            : base(columnName)
        { }
		
		public AsyncExecutionDataColumns KeyColumn
		{
			get
			{
				return new AsyncExecutionDataColumns("Id");
			}
		}	

				
        public AsyncExecutionDataColumns Id
        {
            get
            {
                return new AsyncExecutionDataColumns("Id");
            }
        }
        public AsyncExecutionDataColumns Uuid
        {
            get
            {
                return new AsyncExecutionDataColumns("Uuid");
            }
        }
        public AsyncExecutionDataColumns Cuid
        {
            get
            {
                return new AsyncExecutionDataColumns("Cuid");
            }
        }
        public AsyncExecutionDataColumns RequestCuid
        {
            get
            {
                return new AsyncExecutionDataColumns("RequestCuid");
            }
        }
        public AsyncExecutionDataColumns RequestHash
        {
            get
            {
                return new AsyncExecutionDataColumns("RequestHash");
            }
        }
        public AsyncExecutionDataColumns ResponseCuid
        {
            get
            {
                return new AsyncExecutionDataColumns("ResponseCuid");
            }
        }
        public AsyncExecutionDataColumns ResponseHash
        {
            get
            {
                return new AsyncExecutionDataColumns("ResponseHash");
            }
        }
        public AsyncExecutionDataColumns Success
        {
            get
            {
                return new AsyncExecutionDataColumns("Success");
            }
        }
        public AsyncExecutionDataColumns Requested
        {
            get
            {
                return new AsyncExecutionDataColumns("Requested");
            }
        }
        public AsyncExecutionDataColumns Responded
        {
            get
            {
                return new AsyncExecutionDataColumns("Responded");
            }
        }
        public AsyncExecutionDataColumns Created
        {
            get
            {
                return new AsyncExecutionDataColumns("Created");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(AsyncExecutionData);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}