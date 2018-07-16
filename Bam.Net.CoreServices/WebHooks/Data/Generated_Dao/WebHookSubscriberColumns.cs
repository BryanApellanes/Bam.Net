using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.CoreServices.WebHooks.Data.Dao
{
    public class WebHookSubscriberColumns: QueryFilter<WebHookSubscriberColumns>, IFilterToken
    {
        public WebHookSubscriberColumns() { }
        public WebHookSubscriberColumns(string columnName)
            : base(columnName)
        { }
		
		public WebHookSubscriberColumns KeyColumn
		{
			get
			{
				return new WebHookSubscriberColumns("Id");
			}
		}	

				
        public WebHookSubscriberColumns Id
        {
            get
            {
                return new WebHookSubscriberColumns("Id");
            }
        }
        public WebHookSubscriberColumns Uuid
        {
            get
            {
                return new WebHookSubscriberColumns("Uuid");
            }
        }
        public WebHookSubscriberColumns Cuid
        {
            get
            {
                return new WebHookSubscriberColumns("Cuid");
            }
        }
        public WebHookSubscriberColumns Url
        {
            get
            {
                return new WebHookSubscriberColumns("Url");
            }
        }
        public WebHookSubscriberColumns SharedSecret
        {
            get
            {
                return new WebHookSubscriberColumns("SharedSecret");
            }
        }
        public WebHookSubscriberColumns CreatedBy
        {
            get
            {
                return new WebHookSubscriberColumns("CreatedBy");
            }
        }
        public WebHookSubscriberColumns ModifiedBy
        {
            get
            {
                return new WebHookSubscriberColumns("ModifiedBy");
            }
        }
        public WebHookSubscriberColumns Modified
        {
            get
            {
                return new WebHookSubscriberColumns("Modified");
            }
        }
        public WebHookSubscriberColumns Deleted
        {
            get
            {
                return new WebHookSubscriberColumns("Deleted");
            }
        }
        public WebHookSubscriberColumns Created
        {
            get
            {
                return new WebHookSubscriberColumns("Created");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(WebHookSubscriber);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}