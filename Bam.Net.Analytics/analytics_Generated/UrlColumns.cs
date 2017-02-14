using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Analytics
{
    public class UrlColumns: QueryFilter<UrlColumns>, IFilterToken
    {
        public UrlColumns() { }
        public UrlColumns(string columnName)
            : base(columnName)
        { }
		
		public UrlColumns KeyColumn
		{
			get
			{
				return new UrlColumns("Id");
			}
		}	

				
        public UrlColumns Id
        {
            get
            {
                return new UrlColumns("Id");
            }
        }
        public UrlColumns Uuid
        {
            get
            {
                return new UrlColumns("Uuid");
            }
        }
        public UrlColumns Cuid
        {
            get
            {
                return new UrlColumns("Cuid");
            }
        }

        public UrlColumns ProtocolId
        {
            get
            {
                return new UrlColumns("ProtocolId");
            }
        }
        public UrlColumns DomainId
        {
            get
            {
                return new UrlColumns("DomainId");
            }
        }
        public UrlColumns PortId
        {
            get
            {
                return new UrlColumns("PortId");
            }
        }
        public UrlColumns PathId
        {
            get
            {
                return new UrlColumns("PathId");
            }
        }
        public UrlColumns QueryStringId
        {
            get
            {
                return new UrlColumns("QueryStringId");
            }
        }
        public UrlColumns FragmentId
        {
            get
            {
                return new UrlColumns("FragmentId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(Url);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}