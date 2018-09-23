using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Services.AsyncCallback.Data.Dao
{
    public class AsyncExecutionRequestDataColumns: QueryFilter<AsyncExecutionRequestDataColumns>, IFilterToken
    {
        public AsyncExecutionRequestDataColumns() { }
        public AsyncExecutionRequestDataColumns(string columnName)
            : base(columnName)
        { }
		
		public AsyncExecutionRequestDataColumns KeyColumn
		{
			get
			{
				return new AsyncExecutionRequestDataColumns("Id");
			}
		}	

				
        public AsyncExecutionRequestDataColumns Id
        {
            get
            {
                return new AsyncExecutionRequestDataColumns("Id");
            }
        }
        public AsyncExecutionRequestDataColumns Uuid
        {
            get
            {
                return new AsyncExecutionRequestDataColumns("Uuid");
            }
        }
        public AsyncExecutionRequestDataColumns Cuid
        {
            get
            {
                return new AsyncExecutionRequestDataColumns("Cuid");
            }
        }
        public AsyncExecutionRequestDataColumns RequestHash
        {
            get
            {
                return new AsyncExecutionRequestDataColumns("RequestHash");
            }
        }
        public AsyncExecutionRequestDataColumns ClassName
        {
            get
            {
                return new AsyncExecutionRequestDataColumns("ClassName");
            }
        }
        public AsyncExecutionRequestDataColumns MethodName
        {
            get
            {
                return new AsyncExecutionRequestDataColumns("MethodName");
            }
        }
        public AsyncExecutionRequestDataColumns JsonParams
        {
            get
            {
                return new AsyncExecutionRequestDataColumns("JsonParams");
            }
        }
        public AsyncExecutionRequestDataColumns Created
        {
            get
            {
                return new AsyncExecutionRequestDataColumns("Created");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(AsyncExecutionRequestData);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}