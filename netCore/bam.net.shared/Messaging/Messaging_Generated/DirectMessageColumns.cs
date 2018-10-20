using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Messaging.Data
{
    public class DirectMessageColumns: QueryFilter<DirectMessageColumns>, IFilterToken
    {
        public DirectMessageColumns() { }
        public DirectMessageColumns(string columnName)
            : base(columnName)
        { }
		
		public DirectMessageColumns KeyColumn
		{
			get
			{
				return new DirectMessageColumns("Id");
			}
		}	

				
        public DirectMessageColumns Id
        {
            get
            {
                return new DirectMessageColumns("Id");
            }
        }
        public DirectMessageColumns Uuid
        {
            get
            {
                return new DirectMessageColumns("Uuid");
            }
        }
        public DirectMessageColumns Cuid
        {
            get
            {
                return new DirectMessageColumns("Cuid");
            }
        }
        public DirectMessageColumns To
        {
            get
            {
                return new DirectMessageColumns("To");
            }
        }
        public DirectMessageColumns ToEmail
        {
            get
            {
                return new DirectMessageColumns("ToEmail");
            }
        }

        public DirectMessageColumns MessageId
        {
            get
            {
                return new DirectMessageColumns("MessageId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(DirectMessage);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}