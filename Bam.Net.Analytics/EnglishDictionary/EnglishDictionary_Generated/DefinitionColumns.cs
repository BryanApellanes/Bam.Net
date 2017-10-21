using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Analytics.EnglishDictionary
{
    public class DefinitionColumns: QueryFilter<DefinitionColumns>, IFilterToken
    {
        public DefinitionColumns() { }
        public DefinitionColumns(string columnName)
            : base(columnName)
        { }
		
		public DefinitionColumns KeyColumn
		{
			get
			{
				return new DefinitionColumns("Id");
			}
		}	

				
        public DefinitionColumns Id
        {
            get
            {
                return new DefinitionColumns("Id");
            }
        }
        public DefinitionColumns Uuid
        {
            get
            {
                return new DefinitionColumns("Uuid");
            }
        }
        public DefinitionColumns Cuid
        {
            get
            {
                return new DefinitionColumns("Cuid");
            }
        }
        public DefinitionColumns WordType
        {
            get
            {
                return new DefinitionColumns("WordType");
            }
        }
        public DefinitionColumns Value
        {
            get
            {
                return new DefinitionColumns("Value");
            }
        }

        public DefinitionColumns WordId
        {
            get
            {
                return new DefinitionColumns("WordId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(Definition);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}