using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.CoreServices.ApplicationRegistration.Data.Dao
{
    public class SubscriptionColumns: QueryFilter<SubscriptionColumns>, IFilterToken
    {
        public SubscriptionColumns() { }
        public SubscriptionColumns(string columnName)
            : base(columnName)
        { }
		
		public SubscriptionColumns KeyColumn
		{
			get
			{
				return new SubscriptionColumns("Id");
			}
		}	

				
        public SubscriptionColumns Id
        {
            get
            {
                return new SubscriptionColumns("Id");
            }
        }
        public SubscriptionColumns Uuid
        {
            get
            {
                return new SubscriptionColumns("Uuid");
            }
        }
        public SubscriptionColumns Cuid
        {
            get
            {
                return new SubscriptionColumns("Cuid");
            }
        }
        public SubscriptionColumns SubscriptionLevel
        {
            get
            {
                return new SubscriptionColumns("SubscriptionLevel");
            }
        }
        public SubscriptionColumns MaxOrganizations
        {
            get
            {
                return new SubscriptionColumns("MaxOrganizations");
            }
        }
        public SubscriptionColumns MaxApplications
        {
            get
            {
                return new SubscriptionColumns("MaxApplications");
            }
        }
        public SubscriptionColumns ExpirationDate
        {
            get
            {
                return new SubscriptionColumns("ExpirationDate");
            }
        }
        public SubscriptionColumns CreatedBy
        {
            get
            {
                return new SubscriptionColumns("CreatedBy");
            }
        }
        public SubscriptionColumns ModifiedBy
        {
            get
            {
                return new SubscriptionColumns("ModifiedBy");
            }
        }
        public SubscriptionColumns Modified
        {
            get
            {
                return new SubscriptionColumns("Modified");
            }
        }
        public SubscriptionColumns Deleted
        {
            get
            {
                return new SubscriptionColumns("Deleted");
            }
        }
        public SubscriptionColumns Created
        {
            get
            {
                return new SubscriptionColumns("Created");
            }
        }

        public SubscriptionColumns UserId
        {
            get
            {
                return new SubscriptionColumns("UserId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(Subscription);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}