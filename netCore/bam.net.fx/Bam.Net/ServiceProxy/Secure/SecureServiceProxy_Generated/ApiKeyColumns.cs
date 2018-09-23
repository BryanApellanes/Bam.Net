using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.ServiceProxy.Secure
{
    public class ApiKeyColumns: QueryFilter<ApiKeyColumns>, IFilterToken
    {
        public ApiKeyColumns() { }
        public ApiKeyColumns(string columnName)
            : base(columnName)
        { }
		
		public ApiKeyColumns KeyColumn
		{
			get
			{
				return new ApiKeyColumns("Id");
			}
		}	

				
        public ApiKeyColumns Id
        {
            get
            {
                return new ApiKeyColumns("Id");
            }
        }
        public ApiKeyColumns Uuid
        {
            get
            {
                return new ApiKeyColumns("Uuid");
            }
        }
        public ApiKeyColumns Cuid
        {
            get
            {
                return new ApiKeyColumns("Cuid");
            }
        }
        public ApiKeyColumns ClientId
        {
            get
            {
                return new ApiKeyColumns("ClientId");
            }
        }
        public ApiKeyColumns SharedSecret
        {
            get
            {
                return new ApiKeyColumns("SharedSecret");
            }
        }
        public ApiKeyColumns CreatedBy
        {
            get
            {
                return new ApiKeyColumns("CreatedBy");
            }
        }
        public ApiKeyColumns CreatedAt
        {
            get
            {
                return new ApiKeyColumns("CreatedAt");
            }
        }
        public ApiKeyColumns Confirmed
        {
            get
            {
                return new ApiKeyColumns("Confirmed");
            }
        }
        public ApiKeyColumns Disabled
        {
            get
            {
                return new ApiKeyColumns("Disabled");
            }
        }
        public ApiKeyColumns DisabledBy
        {
            get
            {
                return new ApiKeyColumns("DisabledBy");
            }
        }

        public ApiKeyColumns ApplicationId
        {
            get
            {
                return new ApiKeyColumns("ApplicationId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(ApiKey);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}