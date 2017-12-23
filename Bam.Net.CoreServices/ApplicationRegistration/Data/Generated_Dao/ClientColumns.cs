using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.CoreServices.ApplicationRegistration.Data.Dao
{
    public class ClientColumns: QueryFilter<ClientColumns>, IFilterToken
    {
        public ClientColumns() { }
        public ClientColumns(string columnName)
            : base(columnName)
        { }
		
		public ClientColumns KeyColumn
		{
			get
			{
				return new ClientColumns("Id");
			}
		}	

				
        public ClientColumns Id
        {
            get
            {
                return new ClientColumns("Id");
            }
        }
        public ClientColumns Uuid
        {
            get
            {
                return new ClientColumns("Uuid");
            }
        }
        public ClientColumns Cuid
        {
            get
            {
                return new ClientColumns("Cuid");
            }
        }
        public ClientColumns ApplicationName
        {
            get
            {
                return new ClientColumns("ApplicationName");
            }
        }
        public ClientColumns MachineName
        {
            get
            {
                return new ClientColumns("MachineName");
            }
        }
        public ClientColumns ServerHost
        {
            get
            {
                return new ClientColumns("ServerHost");
            }
        }
        public ClientColumns Port
        {
            get
            {
                return new ClientColumns("Port");
            }
        }
        public ClientColumns Secret
        {
            get
            {
                return new ClientColumns("Secret");
            }
        }
        public ClientColumns CreatedBy
        {
            get
            {
                return new ClientColumns("CreatedBy");
            }
        }
        public ClientColumns ModifiedBy
        {
            get
            {
                return new ClientColumns("ModifiedBy");
            }
        }
        public ClientColumns Modified
        {
            get
            {
                return new ClientColumns("Modified");
            }
        }
        public ClientColumns Deleted
        {
            get
            {
                return new ClientColumns("Deleted");
            }
        }
        public ClientColumns Created
        {
            get
            {
                return new ClientColumns("Created");
            }
        }

        public ClientColumns ApplicationId
        {
            get
            {
                return new ClientColumns("ApplicationId");
            }
        }
        public ClientColumns MachineId
        {
            get
            {
                return new ClientColumns("MachineId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(Client);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}