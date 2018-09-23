using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Translation
{
    public class TranslationColumns: QueryFilter<TranslationColumns>, IFilterToken
    {
        public TranslationColumns() { }
        public TranslationColumns(string columnName)
            : base(columnName)
        { }
		
		public TranslationColumns KeyColumn
		{
			get
			{
				return new TranslationColumns("Id");
			}
		}	

				
        public TranslationColumns Id
        {
            get
            {
                return new TranslationColumns("Id");
            }
        }
        public TranslationColumns Uuid
        {
            get
            {
                return new TranslationColumns("Uuid");
            }
        }
        public TranslationColumns Cuid
        {
            get
            {
                return new TranslationColumns("Cuid");
            }
        }
        public TranslationColumns Translator
        {
            get
            {
                return new TranslationColumns("Translator");
            }
        }
        public TranslationColumns Value
        {
            get
            {
                return new TranslationColumns("Value");
            }
        }

        public TranslationColumns TextId
        {
            get
            {
                return new TranslationColumns("TextId");
            }
        }
        public TranslationColumns LanguageId
        {
            get
            {
                return new TranslationColumns("LanguageId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(Translation);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}