using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.CoreServices.WebHooks.Data.Dao
{
    public class WebHookDescriptorColumns: QueryFilter<WebHookDescriptorColumns>, IFilterToken
    {
        public WebHookDescriptorColumns() { }
        public WebHookDescriptorColumns(string columnName)
            : base(columnName)
        { }
		
		public WebHookDescriptorColumns KeyColumn
		{
			get
			{
				return new WebHookDescriptorColumns("Id");
			}
		}	

				
        public WebHookDescriptorColumns Id
        {
            get
            {
                return new WebHookDescriptorColumns("Id");
            }
        }
        public WebHookDescriptorColumns Uuid
        {
            get
            {
                return new WebHookDescriptorColumns("Uuid");
            }
        }
        public WebHookDescriptorColumns Cuid
        {
            get
            {
                return new WebHookDescriptorColumns("Cuid");
            }
        }
        public WebHookDescriptorColumns WebHookName
        {
            get
            {
                return new WebHookDescriptorColumns("WebHookName");
            }
        }
        public WebHookDescriptorColumns Description
        {
            get
            {
                return new WebHookDescriptorColumns("Description");
            }
        }
        public WebHookDescriptorColumns SharedSecret
        {
            get
            {
                return new WebHookDescriptorColumns("SharedSecret");
            }
        }
        public WebHookDescriptorColumns CreatedBy
        {
            get
            {
                return new WebHookDescriptorColumns("CreatedBy");
            }
        }
        public WebHookDescriptorColumns ModifiedBy
        {
            get
            {
                return new WebHookDescriptorColumns("ModifiedBy");
            }
        }
        public WebHookDescriptorColumns Modified
        {
            get
            {
                return new WebHookDescriptorColumns("Modified");
            }
        }
        public WebHookDescriptorColumns Deleted
        {
            get
            {
                return new WebHookDescriptorColumns("Deleted");
            }
        }
        public WebHookDescriptorColumns Created
        {
            get
            {
                return new WebHookDescriptorColumns("Created");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(WebHookDescriptor);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}