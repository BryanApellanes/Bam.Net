using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Analytics
{
    public class ImageColumns: QueryFilter<ImageColumns>, IFilterToken
    {
        public ImageColumns() { }
        public ImageColumns(string columnName)
            : base(columnName)
        { }
		
		public ImageColumns KeyColumn
		{
			get
			{
				return new ImageColumns("Id");
			}
		}	

				
        public ImageColumns Id
        {
            get
            {
                return new ImageColumns("Id");
            }
        }
        public ImageColumns Uuid
        {
            get
            {
                return new ImageColumns("Uuid");
            }
        }
        public ImageColumns Cuid
        {
            get
            {
                return new ImageColumns("Cuid");
            }
        }
        public ImageColumns Date
        {
            get
            {
                return new ImageColumns("Date");
            }
        }

        public ImageColumns UrlId
        {
            get
            {
                return new ImageColumns("UrlId");
            }
        }
        public ImageColumns CrawlerId
        {
            get
            {
                return new ImageColumns("CrawlerId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(Image);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}