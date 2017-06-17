using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Services.AsyncCallback.Data.Dao
{
    public class AsyncExecutionResponseDataColumns: QueryFilter<AsyncExecutionResponseDataColumns>, IFilterToken
    {
        public AsyncExecutionResponseDataColumns() { }
        public AsyncExecutionResponseDataColumns(string columnName)
            : base(columnName)
        { }
		
		public AsyncExecutionResponseDataColumns KeyColumn
		{
			get
			{
				return new AsyncExecutionResponseDataColumns("Id");
			}
		}	

				
        public AsyncExecutionResponseDataColumns Id
        {
            get
            {
                return new AsyncExecutionResponseDataColumns("Id");
            }
        }
        public AsyncExecutionResponseDataColumns Uuid
        {
            get
            {
                return new AsyncExecutionResponseDataColumns("Uuid");
            }
        }
        public AsyncExecutionResponseDataColumns Cuid
        {
            get
            {
                return new AsyncExecutionResponseDataColumns("Cuid");
            }
        }
        public AsyncExecutionResponseDataColumns RequestId
        {
            get
            {
                return new AsyncExecutionResponseDataColumns("RequestId");
            }
        }
        public AsyncExecutionResponseDataColumns ResponseHash
        {
            get
            {
                return new AsyncExecutionResponseDataColumns("ResponseHash");
            }
        }
        public AsyncExecutionResponseDataColumns RequestHash
        {
            get
            {
                return new AsyncExecutionResponseDataColumns("RequestHash");
            }
        }
        public AsyncExecutionResponseDataColumns ResultJson
        {
            get
            {
                return new AsyncExecutionResponseDataColumns("ResultJson");
            }
        }
        public AsyncExecutionResponseDataColumns Created
        {
            get
            {
                return new AsyncExecutionResponseDataColumns("Created");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(AsyncExecutionResponseData);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}