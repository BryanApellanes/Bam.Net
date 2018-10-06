using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Translation
{
    public class TextColumns: QueryFilter<TextColumns>, IFilterToken
    {
        public TextColumns() { }
        public TextColumns(string columnName)
            : base(columnName)
        { }
		
		public TextColumns KeyColumn
		{
			get
			{
				return new TextColumns("Id");
			}
		}	

				
        public TextColumns Id
        {
            get
            {
                return new TextColumns("Id");
            }
        }
        public TextColumns Uuid
        {
            get
            {
                return new TextColumns("Uuid");
            }
        }
        public TextColumns Cuid
        {
            get
            {
                return new TextColumns("Cuid");
            }
        }
        public TextColumns Value
        {
            get
            {
                return new TextColumns("Value");
            }
        }

        public TextColumns LanguageId
        {
            get
            {
                return new TextColumns("LanguageId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(Text);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}