using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.CoreServices.WebHooks.Data.Dao
{
    public class WebHookCallColumns: QueryFilter<WebHookCallColumns>, IFilterToken
    {
        public WebHookCallColumns() { }
        public WebHookCallColumns(string columnName)
            : base(columnName)
        { }
		
		public WebHookCallColumns KeyColumn
		{
			get
			{
				return new WebHookCallColumns("Id");
			}
		}	

				
        public WebHookCallColumns Id
        {
            get
            {
                return new WebHookCallColumns("Id");
            }
        }
        public WebHookCallColumns Uuid
        {
            get
            {
                return new WebHookCallColumns("Uuid");
            }
        }
        public WebHookCallColumns Cuid
        {
            get
            {
                return new WebHookCallColumns("Cuid");
            }
        }
        public WebHookCallColumns Succeeded
        {
            get
            {
                return new WebHookCallColumns("Succeeded");
            }
        }
        public WebHookCallColumns Response
        {
            get
            {
                return new WebHookCallColumns("Response");
            }
        }
        public WebHookCallColumns CreatedBy
        {
            get
            {
                return new WebHookCallColumns("CreatedBy");
            }
        }
        public WebHookCallColumns ModifiedBy
        {
            get
            {
                return new WebHookCallColumns("ModifiedBy");
            }
        }
        public WebHookCallColumns Modified
        {
            get
            {
                return new WebHookCallColumns("Modified");
            }
        }
        public WebHookCallColumns Deleted
        {
            get
            {
                return new WebHookCallColumns("Deleted");
            }
        }
        public WebHookCallColumns Created
        {
            get
            {
                return new WebHookCallColumns("Created");
            }
        }

        public WebHookCallColumns WebHookDescriptorId
        {
            get
            {
                return new WebHookCallColumns("WebHookDescriptorId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(WebHookCall);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}