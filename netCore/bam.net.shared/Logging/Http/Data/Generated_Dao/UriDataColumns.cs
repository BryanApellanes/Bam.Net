using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Logging.Http.Data.Dao
{
    public class UriDataColumns: QueryFilter<UriDataColumns>, IFilterToken
    {
        public UriDataColumns() { }
        public UriDataColumns(string columnName)
            : base(columnName)
        { }
		
		public UriDataColumns KeyColumn
		{
			get
			{
				return new UriDataColumns("Id");
			}
		}	

				
        public UriDataColumns Id
        {
            get
            {
                return new UriDataColumns("Id");
            }
        }
        public UriDataColumns Uuid
        {
            get
            {
                return new UriDataColumns("Uuid");
            }
        }
        public UriDataColumns Cuid
        {
            get
            {
                return new UriDataColumns("Cuid");
            }
        }
        public UriDataColumns IsDefaultPort
        {
            get
            {
                return new UriDataColumns("IsDefaultPort");
            }
        }
        public UriDataColumns Authority
        {
            get
            {
                return new UriDataColumns("Authority");
            }
        }
        public UriDataColumns DnsSafeHost
        {
            get
            {
                return new UriDataColumns("DnsSafeHost");
            }
        }
        public UriDataColumns Fragment
        {
            get
            {
                return new UriDataColumns("Fragment");
            }
        }
        public UriDataColumns Host
        {
            get
            {
                return new UriDataColumns("Host");
            }
        }
        public UriDataColumns IdnHost
        {
            get
            {
                return new UriDataColumns("IdnHost");
            }
        }
        public UriDataColumns IsAbsoluteUri
        {
            get
            {
                return new UriDataColumns("IsAbsoluteUri");
            }
        }
        public UriDataColumns IsFile
        {
            get
            {
                return new UriDataColumns("IsFile");
            }
        }
        public UriDataColumns IsUnc
        {
            get
            {
                return new UriDataColumns("IsUnc");
            }
        }
        public UriDataColumns LocalPath
        {
            get
            {
                return new UriDataColumns("LocalPath");
            }
        }
        public UriDataColumns OriginalString
        {
            get
            {
                return new UriDataColumns("OriginalString");
            }
        }
        public UriDataColumns PathAndQuery
        {
            get
            {
                return new UriDataColumns("PathAndQuery");
            }
        }
        public UriDataColumns Port
        {
            get
            {
                return new UriDataColumns("Port");
            }
        }
        public UriDataColumns QueryString
        {
            get
            {
                return new UriDataColumns("QueryString");
            }
        }
        public UriDataColumns Scheme
        {
            get
            {
                return new UriDataColumns("Scheme");
            }
        }
        public UriDataColumns AbsoluteUri
        {
            get
            {
                return new UriDataColumns("AbsoluteUri");
            }
        }
        public UriDataColumns IsLoopback
        {
            get
            {
                return new UriDataColumns("IsLoopback");
            }
        }
        public UriDataColumns AbsolutePath
        {
            get
            {
                return new UriDataColumns("AbsolutePath");
            }
        }
        public UriDataColumns UserInfo
        {
            get
            {
                return new UriDataColumns("UserInfo");
            }
        }
        public UriDataColumns UserEscaped
        {
            get
            {
                return new UriDataColumns("UserEscaped");
            }
        }
        public UriDataColumns Created
        {
            get
            {
                return new UriDataColumns("Created");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(UriData);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}