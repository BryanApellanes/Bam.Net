using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.CoreServices.ApplicationRegistration.Data.Dao
{
    public class ClientServerConnectionColumns: QueryFilter<ClientServerConnectionColumns>, IFilterToken
    {
        public ClientServerConnectionColumns() { }
        public ClientServerConnectionColumns(string columnName)
            : base(columnName)
        { }
		
		public ClientServerConnectionColumns KeyColumn
		{
			get
			{
				return new ClientServerConnectionColumns("Id");
			}
		}	

				
        public ClientServerConnectionColumns Id
        {
            get
            {
                return new ClientServerConnectionColumns("Id");
            }
        }
        public ClientServerConnectionColumns Uuid
        {
            get
            {
                return new ClientServerConnectionColumns("Uuid");
            }
        }
        public ClientServerConnectionColumns Cuid
        {
            get
            {
                return new ClientServerConnectionColumns("Cuid");
            }
        }
        public ClientServerConnectionColumns ClientId
        {
            get
            {
                return new ClientServerConnectionColumns("ClientId");
            }
        }
        public ClientServerConnectionColumns ServerId
        {
            get
            {
                return new ClientServerConnectionColumns("ServerId");
            }
        }
        public ClientServerConnectionColumns CreatedBy
        {
            get
            {
                return new ClientServerConnectionColumns("CreatedBy");
            }
        }
        public ClientServerConnectionColumns ModifiedBy
        {
            get
            {
                return new ClientServerConnectionColumns("ModifiedBy");
            }
        }
        public ClientServerConnectionColumns Modified
        {
            get
            {
                return new ClientServerConnectionColumns("Modified");
            }
        }
        public ClientServerConnectionColumns Deleted
        {
            get
            {
                return new ClientServerConnectionColumns("Deleted");
            }
        }
        public ClientServerConnectionColumns Created
        {
            get
            {
                return new ClientServerConnectionColumns("Created");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(ClientServerConnection);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}