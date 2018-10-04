using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Analytics
{
    public class TagColumns: QueryFilter<TagColumns>, IFilterToken
    {
        public TagColumns() { }
        public TagColumns(string columnName)
            : base(columnName)
        { }
		
		public TagColumns KeyColumn
		{
			get
			{
				return new TagColumns("Id");
			}
		}	

				
        public TagColumns Id
        {
            get
            {
                return new TagColumns("Id");
            }
        }
        public TagColumns Uuid
        {
            get
            {
                return new TagColumns("Uuid");
            }
        }
        public TagColumns Cuid
        {
            get
            {
                return new TagColumns("Cuid");
            }
        }
        public TagColumns Value
        {
            get
            {
                return new TagColumns("Value");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(Tag);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}