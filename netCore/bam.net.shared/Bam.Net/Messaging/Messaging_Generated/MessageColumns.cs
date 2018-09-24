using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Messaging.Data
{
    public class MessageColumns: QueryFilter<MessageColumns>, IFilterToken
    {
        public MessageColumns() { }
        public MessageColumns(string columnName)
            : base(columnName)
        { }
		
		public MessageColumns KeyColumn
		{
			get
			{
				return new MessageColumns("Id");
			}
		}	

				
        public MessageColumns Id
        {
            get
            {
                return new MessageColumns("Id");
            }
        }
        public MessageColumns Uuid
        {
            get
            {
                return new MessageColumns("Uuid");
            }
        }
        public MessageColumns Cuid
        {
            get
            {
                return new MessageColumns("Cuid");
            }
        }
        public MessageColumns CreatedDate
        {
            get
            {
                return new MessageColumns("CreatedDate");
            }
        }
        public MessageColumns From
        {
            get
            {
                return new MessageColumns("From");
            }
        }
        public MessageColumns FromEmail
        {
            get
            {
                return new MessageColumns("FromEmail");
            }
        }
        public MessageColumns Subject
        {
            get
            {
                return new MessageColumns("Subject");
            }
        }
        public MessageColumns Body
        {
            get
            {
                return new MessageColumns("Body");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(Message);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}