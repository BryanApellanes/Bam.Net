using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Logging.Data
{
    public class EventColumns: QueryFilter<EventColumns>, IFilterToken
    {
        public EventColumns() { }
        public EventColumns(string columnName)
            : base(columnName)
        { }
		
		public EventColumns KeyColumn
		{
			get
			{
				return new EventColumns("Id");
			}
		}	

				
        public EventColumns Id
        {
            get
            {
                return new EventColumns("Id");
            }
        }
        public EventColumns Uuid
        {
            get
            {
                return new EventColumns("Uuid");
            }
        }
        public EventColumns Cuid
        {
            get
            {
                return new EventColumns("Cuid");
            }
        }
        public EventColumns Time
        {
            get
            {
                return new EventColumns("Time");
            }
        }
        public EventColumns Severity
        {
            get
            {
                return new EventColumns("Severity");
            }
        }
        public EventColumns EventId
        {
            get
            {
                return new EventColumns("EventId");
            }
        }

        public EventColumns SignatureId
        {
            get
            {
                return new EventColumns("SignatureId");
            }
        }
        public EventColumns ComputerNameId
        {
            get
            {
                return new EventColumns("ComputerNameId");
            }
        }
        public EventColumns CategoryNameId
        {
            get
            {
                return new EventColumns("CategoryNameId");
            }
        }
        public EventColumns SourceNameId
        {
            get
            {
                return new EventColumns("SourceNameId");
            }
        }
        public EventColumns UserNameId
        {
            get
            {
                return new EventColumns("UserNameId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(Event);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}