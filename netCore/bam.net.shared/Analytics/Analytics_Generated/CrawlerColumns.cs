using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Analytics
{
    public class CrawlerColumns: QueryFilter<CrawlerColumns>, IFilterToken
    {
        public CrawlerColumns() { }
        public CrawlerColumns(string columnName)
            : base(columnName)
        { }
		
		public CrawlerColumns KeyColumn
		{
			get
			{
				return new CrawlerColumns("Id");
			}
		}	

				
        public CrawlerColumns Id
        {
            get
            {
                return new CrawlerColumns("Id");
            }
        }
        public CrawlerColumns Uuid
        {
            get
            {
                return new CrawlerColumns("Uuid");
            }
        }
        public CrawlerColumns Cuid
        {
            get
            {
                return new CrawlerColumns("Cuid");
            }
        }
        public CrawlerColumns Name
        {
            get
            {
                return new CrawlerColumns("Name");
            }
        }
        public CrawlerColumns RootUrl
        {
            get
            {
                return new CrawlerColumns("RootUrl");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(Crawler);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}