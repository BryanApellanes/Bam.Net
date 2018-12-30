using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Logging.Http.Data.Dao
{
    public class RequestDataColumns: QueryFilter<RequestDataColumns>, IFilterToken
    {
        public RequestDataColumns() { }
        public RequestDataColumns(string columnName)
            : base(columnName)
        { }
		
		public RequestDataColumns KeyColumn
		{
			get
			{
				return new RequestDataColumns("Id");
			}
		}	

				
        public RequestDataColumns Id
        {
            get
            {
                return new RequestDataColumns("Id");
            }
        }
        public RequestDataColumns Uuid
        {
            get
            {
                return new RequestDataColumns("Uuid");
            }
        }
        public RequestDataColumns Cuid
        {
            get
            {
                return new RequestDataColumns("Cuid");
            }
        }
        public RequestDataColumns AcceptTypes
        {
            get
            {
                return new RequestDataColumns("AcceptTypes");
            }
        }
        public RequestDataColumns ContentEncoding
        {
            get
            {
                return new RequestDataColumns("ContentEncoding");
            }
        }
        public RequestDataColumns ContentLength
        {
            get
            {
                return new RequestDataColumns("ContentLength");
            }
        }
        public RequestDataColumns ContentType
        {
            get
            {
                return new RequestDataColumns("ContentType");
            }
        }
        public RequestDataColumns HttpMethod
        {
            get
            {
                return new RequestDataColumns("HttpMethod");
            }
        }
        public RequestDataColumns UrlId
        {
            get
            {
                return new RequestDataColumns("UrlId");
            }
        }
        public RequestDataColumns UrlReferrerId
        {
            get
            {
                return new RequestDataColumns("UrlReferrerId");
            }
        }
        public RequestDataColumns UserAgent
        {
            get
            {
                return new RequestDataColumns("UserAgent");
            }
        }
        public RequestDataColumns UserHostAddress
        {
            get
            {
                return new RequestDataColumns("UserHostAddress");
            }
        }
        public RequestDataColumns UserHostName
        {
            get
            {
                return new RequestDataColumns("UserHostName");
            }
        }
        public RequestDataColumns UserLanguages
        {
            get
            {
                return new RequestDataColumns("UserLanguages");
            }
        }
        public RequestDataColumns RawUrl
        {
            get
            {
                return new RequestDataColumns("RawUrl");
            }
        }
        public RequestDataColumns Created
        {
            get
            {
                return new RequestDataColumns("Created");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(RequestData);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}