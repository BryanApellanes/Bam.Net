using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.CoreServices.Data.Daos
{
    public class ExternalEventSubscriptionDescriptorColumns: QueryFilter<ExternalEventSubscriptionDescriptorColumns>, IFilterToken
    {
        public ExternalEventSubscriptionDescriptorColumns() { }
        public ExternalEventSubscriptionDescriptorColumns(string columnName)
            : base(columnName)
        { }
		
		public ExternalEventSubscriptionDescriptorColumns KeyColumn
		{
			get
			{
				return new ExternalEventSubscriptionDescriptorColumns("Id");
			}
		}	

				
        public ExternalEventSubscriptionDescriptorColumns Id
        {
            get
            {
                return new ExternalEventSubscriptionDescriptorColumns("Id");
            }
        }
        public ExternalEventSubscriptionDescriptorColumns Uuid
        {
            get
            {
                return new ExternalEventSubscriptionDescriptorColumns("Uuid");
            }
        }
        public ExternalEventSubscriptionDescriptorColumns Cuid
        {
            get
            {
                return new ExternalEventSubscriptionDescriptorColumns("Cuid");
            }
        }
        public ExternalEventSubscriptionDescriptorColumns ClientName
        {
            get
            {
                return new ExternalEventSubscriptionDescriptorColumns("ClientName");
            }
        }
        public ExternalEventSubscriptionDescriptorColumns EventName
        {
            get
            {
                return new ExternalEventSubscriptionDescriptorColumns("EventName");
            }
        }
        public ExternalEventSubscriptionDescriptorColumns WebHookEndpoint
        {
            get
            {
                return new ExternalEventSubscriptionDescriptorColumns("WebHookEndpoint");
            }
        }
        public ExternalEventSubscriptionDescriptorColumns CreatedBy
        {
            get
            {
                return new ExternalEventSubscriptionDescriptorColumns("CreatedBy");
            }
        }
        public ExternalEventSubscriptionDescriptorColumns Created
        {
            get
            {
                return new ExternalEventSubscriptionDescriptorColumns("Created");
            }
        }
        public ExternalEventSubscriptionDescriptorColumns ModifiedBy
        {
            get
            {
                return new ExternalEventSubscriptionDescriptorColumns("ModifiedBy");
            }
        }
        public ExternalEventSubscriptionDescriptorColumns Modified
        {
            get
            {
                return new ExternalEventSubscriptionDescriptorColumns("Modified");
            }
        }
        public ExternalEventSubscriptionDescriptorColumns Deleted
        {
            get
            {
                return new ExternalEventSubscriptionDescriptorColumns("Deleted");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(ExternalEventSubscriptionDescriptor);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}