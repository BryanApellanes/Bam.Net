using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Logging.Data
{
    public class CategoryNameColumns: QueryFilter<CategoryNameColumns>, IFilterToken
    {
        public CategoryNameColumns() { }
        public CategoryNameColumns(string columnName)
            : base(columnName)
        { }
		
		public CategoryNameColumns KeyColumn
		{
			get
			{
				return new CategoryNameColumns("Id");
			}
		}	

				
        public CategoryNameColumns Id
        {
            get
            {
                return new CategoryNameColumns("Id");
            }
        }
        public CategoryNameColumns Uuid
        {
            get
            {
                return new CategoryNameColumns("Uuid");
            }
        }
        public CategoryNameColumns Cuid
        {
            get
            {
                return new CategoryNameColumns("Cuid");
            }
        }
        public CategoryNameColumns Value
        {
            get
            {
                return new CategoryNameColumns("Value");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(CategoryName);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}