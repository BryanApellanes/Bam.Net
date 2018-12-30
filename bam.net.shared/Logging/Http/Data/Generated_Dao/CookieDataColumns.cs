using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Logging.Http.Data.Dao
{
    public class CookieDataColumns: QueryFilter<CookieDataColumns>, IFilterToken
    {
        public CookieDataColumns() { }
        public CookieDataColumns(string columnName)
            : base(columnName)
        { }
		
		public CookieDataColumns KeyColumn
		{
			get
			{
				return new CookieDataColumns("Id");
			}
		}	

				
        public CookieDataColumns Id
        {
            get
            {
                return new CookieDataColumns("Id");
            }
        }
        public CookieDataColumns Uuid
        {
            get
            {
                return new CookieDataColumns("Uuid");
            }
        }
        public CookieDataColumns Cuid
        {
            get
            {
                return new CookieDataColumns("Cuid");
            }
        }
        public CookieDataColumns TimeStamp
        {
            get
            {
                return new CookieDataColumns("TimeStamp");
            }
        }
        public CookieDataColumns Secure
        {
            get
            {
                return new CookieDataColumns("Secure");
            }
        }
        public CookieDataColumns Port
        {
            get
            {
                return new CookieDataColumns("Port");
            }
        }
        public CookieDataColumns Path
        {
            get
            {
                return new CookieDataColumns("Path");
            }
        }
        public CookieDataColumns Name
        {
            get
            {
                return new CookieDataColumns("Name");
            }
        }
        public CookieDataColumns HttpOnly
        {
            get
            {
                return new CookieDataColumns("HttpOnly");
            }
        }
        public CookieDataColumns Expires
        {
            get
            {
                return new CookieDataColumns("Expires");
            }
        }
        public CookieDataColumns Domain
        {
            get
            {
                return new CookieDataColumns("Domain");
            }
        }
        public CookieDataColumns Value
        {
            get
            {
                return new CookieDataColumns("Value");
            }
        }
        public CookieDataColumns Discard
        {
            get
            {
                return new CookieDataColumns("Discard");
            }
        }
        public CookieDataColumns UriId
        {
            get
            {
                return new CookieDataColumns("UriId");
            }
        }
        public CookieDataColumns Comment
        {
            get
            {
                return new CookieDataColumns("Comment");
            }
        }
        public CookieDataColumns Expired
        {
            get
            {
                return new CookieDataColumns("Expired");
            }
        }
        public CookieDataColumns Version
        {
            get
            {
                return new CookieDataColumns("Version");
            }
        }
        public CookieDataColumns Created
        {
            get
            {
                return new CookieDataColumns("Created");
            }
        }

        public CookieDataColumns RequestDataId
        {
            get
            {
                return new CookieDataColumns("RequestDataId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(CookieData);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}