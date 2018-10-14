using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.CoreServices.WebHooks.Data.Dao
{
    public class WebHookDescriptorWebHookSubscriberColumns: QueryFilter<WebHookDescriptorWebHookSubscriberColumns>, IFilterToken
    {
        public WebHookDescriptorWebHookSubscriberColumns() { }
        public WebHookDescriptorWebHookSubscriberColumns(string columnName)
            : base(columnName)
        { }
		
		public WebHookDescriptorWebHookSubscriberColumns KeyColumn
		{
			get
			{
				return new WebHookDescriptorWebHookSubscriberColumns("Id");
			}
		}	

				
        public WebHookDescriptorWebHookSubscriberColumns Id
        {
            get
            {
                return new WebHookDescriptorWebHookSubscriberColumns("Id");
            }
        }
        public WebHookDescriptorWebHookSubscriberColumns Uuid
        {
            get
            {
                return new WebHookDescriptorWebHookSubscriberColumns("Uuid");
            }
        }

        public WebHookDescriptorWebHookSubscriberColumns WebHookDescriptorId
        {
            get
            {
                return new WebHookDescriptorWebHookSubscriberColumns("WebHookDescriptorId");
            }
        }
        public WebHookDescriptorWebHookSubscriberColumns WebHookSubscriberId
        {
            get
            {
                return new WebHookDescriptorWebHookSubscriberColumns("WebHookSubscriberId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(WebHookDescriptorWebHookSubscriber);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}