using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.CoreServices.Data.Daos
{
    public class ExternalEventSubscriptionColumns: QueryFilter<ExternalEventSubscriptionColumns>, IFilterToken
    {
        public ExternalEventSubscriptionColumns() { }
        public ExternalEventSubscriptionColumns(string columnName)
            : base(columnName)
        { }
		
		public ExternalEventSubscriptionColumns KeyColumn
		{
			get
			{
				return new ExternalEventSubscriptionColumns("Id");
			}
		}	

				
        public ExternalEventSubscriptionColumns Id
        {
            get
            {
                return new ExternalEventSubscriptionColumns("Id");
            }
        }
        public ExternalEventSubscriptionColumns Uuid
        {
            get
            {
                return new ExternalEventSubscriptionColumns("Uuid");
            }
        }
        public ExternalEventSubscriptionColumns Cuid
        {
            get
            {
                return new ExternalEventSubscriptionColumns("Cuid");
            }
        }
        public ExternalEventSubscriptionColumns ClientName
        {
            get
            {
                return new ExternalEventSubscriptionColumns("ClientName");
            }
        }
        public ExternalEventSubscriptionColumns EventName
        {
            get
            {
                return new ExternalEventSubscriptionColumns("EventName");
            }
        }
        public ExternalEventSubscriptionColumns WebHookEndpoint
        {
            get
            {
                return new ExternalEventSubscriptionColumns("WebHookEndpoint");
            }
        }
        public ExternalEventSubscriptionColumns CreatedBy
        {
            get
            {
                return new ExternalEventSubscriptionColumns("CreatedBy");
            }
        }
        public ExternalEventSubscriptionColumns Created
        {
            get
            {
                return new ExternalEventSubscriptionColumns("Created");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(ExternalEventSubscription);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}