using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Logging.Http.Data.Dao
{
    public class HeaderDataColumns: QueryFilter<HeaderDataColumns>, IFilterToken
    {
        public HeaderDataColumns() { }
        public HeaderDataColumns(string columnName)
            : base(columnName)
        { }
		
		public HeaderDataColumns KeyColumn
		{
			get
			{
				return new HeaderDataColumns("Id");
			}
		}	

				
        public HeaderDataColumns Id
        {
            get
            {
                return new HeaderDataColumns("Id");
            }
        }
        public HeaderDataColumns Uuid
        {
            get
            {
                return new HeaderDataColumns("Uuid");
            }
        }
        public HeaderDataColumns Cuid
        {
            get
            {
                return new HeaderDataColumns("Cuid");
            }
        }
        public HeaderDataColumns Name
        {
            get
            {
                return new HeaderDataColumns("Name");
            }
        }
        public HeaderDataColumns Value
        {
            get
            {
                return new HeaderDataColumns("Value");
            }
        }
        public HeaderDataColumns Created
        {
            get
            {
                return new HeaderDataColumns("Created");
            }
        }

        public HeaderDataColumns RequestDataId
        {
            get
            {
                return new HeaderDataColumns("RequestDataId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(HeaderData);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}