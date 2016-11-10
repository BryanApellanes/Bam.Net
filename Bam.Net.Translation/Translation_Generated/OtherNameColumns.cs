using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Translation
{
    public class OtherNameColumns: QueryFilter<OtherNameColumns>, IFilterToken
    {
        public OtherNameColumns() { }
        public OtherNameColumns(string columnName)
            : base(columnName)
        { }
		
		public OtherNameColumns KeyColumn
		{
			get
			{
				return new OtherNameColumns("Id");
			}
		}	

				
        public OtherNameColumns Id
        {
            get
            {
                return new OtherNameColumns("Id");
            }
        }
        public OtherNameColumns Uuid
        {
            get
            {
                return new OtherNameColumns("Uuid");
            }
        }
        public OtherNameColumns Cuid
        {
            get
            {
                return new OtherNameColumns("Cuid");
            }
        }
        public OtherNameColumns LanguageName
        {
            get
            {
                return new OtherNameColumns("LanguageName");
            }
        }
        public OtherNameColumns Value
        {
            get
            {
                return new OtherNameColumns("Value");
            }
        }

        public OtherNameColumns LanguageId
        {
            get
            {
                return new OtherNameColumns("LanguageId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(OtherName);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}