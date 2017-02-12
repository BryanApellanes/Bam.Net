using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Messaging.Data
{
    public class EmailMessageColumns: QueryFilter<EmailMessageColumns>, IFilterToken
    {
        public EmailMessageColumns() { }
        public EmailMessageColumns(string columnName)
            : base(columnName)
        { }
		
		public EmailMessageColumns KeyColumn
		{
			get
			{
				return new EmailMessageColumns("Id");
			}
		}	

				
        public EmailMessageColumns Id
        {
            get
            {
                return new EmailMessageColumns("Id");
            }
        }
        public EmailMessageColumns Uuid
        {
            get
            {
                return new EmailMessageColumns("Uuid");
            }
        }
        public EmailMessageColumns Cuid
        {
            get
            {
                return new EmailMessageColumns("Cuid");
            }
        }
        public EmailMessageColumns Sent
        {
            get
            {
                return new EmailMessageColumns("Sent");
            }
        }
        public EmailMessageColumns TemplateName
        {
            get
            {
                return new EmailMessageColumns("TemplateName");
            }
        }
        public EmailMessageColumns TemplateJsonData
        {
            get
            {
                return new EmailMessageColumns("TemplateJsonData");
            }
        }

        public EmailMessageColumns DirectMessageId
        {
            get
            {
                return new EmailMessageColumns("DirectMessageId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(EmailMessage);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}