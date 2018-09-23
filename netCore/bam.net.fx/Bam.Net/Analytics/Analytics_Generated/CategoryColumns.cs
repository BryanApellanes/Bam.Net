using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Analytics
{
    public class CategoryColumns: QueryFilter<CategoryColumns>, IFilterToken
    {
        public CategoryColumns() { }
        public CategoryColumns(string columnName)
            : base(columnName)
        { }
		
		public CategoryColumns KeyColumn
		{
			get
			{
				return new CategoryColumns("Id");
			}
		}	

				
        public CategoryColumns Id
        {
            get
            {
                return new CategoryColumns("Id");
            }
        }
        public CategoryColumns Uuid
        {
            get
            {
                return new CategoryColumns("Uuid");
            }
        }
        public CategoryColumns Cuid
        {
            get
            {
                return new CategoryColumns("Cuid");
            }
        }
        public CategoryColumns Value
        {
            get
            {
                return new CategoryColumns("Value");
            }
        }
        public CategoryColumns DocumentCount
        {
            get
            {
                return new CategoryColumns("DocumentCount");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(Category);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}