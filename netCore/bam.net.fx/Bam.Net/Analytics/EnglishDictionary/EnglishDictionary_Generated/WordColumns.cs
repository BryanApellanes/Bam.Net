using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Analytics.EnglishDictionary
{
    public class WordColumns: QueryFilter<WordColumns>, IFilterToken
    {
        public WordColumns() { }
        public WordColumns(string columnName)
            : base(columnName)
        { }
		
		public WordColumns KeyColumn
		{
			get
			{
				return new WordColumns("Id");
			}
		}	

				
        public WordColumns Id
        {
            get
            {
                return new WordColumns("Id");
            }
        }
        public WordColumns Uuid
        {
            get
            {
                return new WordColumns("Uuid");
            }
        }
        public WordColumns Cuid
        {
            get
            {
                return new WordColumns("Cuid");
            }
        }
        public WordColumns Value
        {
            get
            {
                return new WordColumns("Value");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(Word);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}