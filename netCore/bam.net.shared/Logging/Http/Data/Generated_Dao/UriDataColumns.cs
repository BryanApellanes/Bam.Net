using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Logging.Http.Data.Dao
{
    public class UriDataColumns: QueryFilter<UriDataColumns>, IFilterToken
    {
        public UriDataColumns() { }
        public UriDataColumns(string columnName)
            : base(columnName)
        { }
		
		public UriDataColumns KeyColumn
		{
			get
			{
				return new UriDataColumns("Id");
			}
		}	

				
        public UriDataColumns Id
        {
            get
            {
                return new UriDataColumns("Id");
            }
        }
        public UriDataColumns Uuid
        {
            get
            {
                return new UriDataColumns("Uuid");
            }
        }
        public UriDataColumns Cuid
        {
            get
            {
                return new UriDataColumns("Cuid");
            }
        }
        public UriDataColumns Created
        {
            get
            {
                return new UriDataColumns("Created");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(UriData);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}