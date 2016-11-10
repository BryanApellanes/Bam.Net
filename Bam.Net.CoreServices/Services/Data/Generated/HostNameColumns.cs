using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.CoreServices.Data.Daos
{
    public class HostNameColumns: QueryFilter<HostNameColumns>, IFilterToken
    {
        public HostNameColumns() { }
        public HostNameColumns(string columnName)
            : base(columnName)
        { }
		
		public HostNameColumns KeyColumn
		{
			get
			{
				return new HostNameColumns("Id");
			}
		}	

				
        public HostNameColumns Id
        {
            get
            {
                return new HostNameColumns("Id");
            }
        }
        public HostNameColumns Uuid
        {
            get
            {
                return new HostNameColumns("Uuid");
            }
        }
        public HostNameColumns Cuid
        {
            get
            {
                return new HostNameColumns("Cuid");
            }
        }
        public HostNameColumns Value
        {
            get
            {
                return new HostNameColumns("Value");
            }
        }
        public HostNameColumns CreatedBy
        {
            get
            {
                return new HostNameColumns("CreatedBy");
            }
        }
        public HostNameColumns Created
        {
            get
            {
                return new HostNameColumns("Created");
            }
        }

        public HostNameColumns ApplicationId
        {
            get
            {
                return new HostNameColumns("ApplicationId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(HostName);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}