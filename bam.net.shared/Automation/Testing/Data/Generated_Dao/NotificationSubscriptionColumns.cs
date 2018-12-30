using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Automation.Testing.Data.Dao
{
    public class NotificationSubscriptionColumns: QueryFilter<NotificationSubscriptionColumns>, IFilterToken
    {
        public NotificationSubscriptionColumns() { }
        public NotificationSubscriptionColumns(string columnName)
            : base(columnName)
        { }
		
		public NotificationSubscriptionColumns KeyColumn
		{
			get
			{
				return new NotificationSubscriptionColumns("Id");
			}
		}	

				
        public NotificationSubscriptionColumns Id
        {
            get
            {
                return new NotificationSubscriptionColumns("Id");
            }
        }
        public NotificationSubscriptionColumns Uuid
        {
            get
            {
                return new NotificationSubscriptionColumns("Uuid");
            }
        }
        public NotificationSubscriptionColumns Cuid
        {
            get
            {
                return new NotificationSubscriptionColumns("Cuid");
            }
        }
        public NotificationSubscriptionColumns EmailAddress
        {
            get
            {
                return new NotificationSubscriptionColumns("EmailAddress");
            }
        }
        public NotificationSubscriptionColumns IsActive
        {
            get
            {
                return new NotificationSubscriptionColumns("IsActive");
            }
        }
        public NotificationSubscriptionColumns Created
        {
            get
            {
                return new NotificationSubscriptionColumns("Created");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(NotificationSubscription);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}