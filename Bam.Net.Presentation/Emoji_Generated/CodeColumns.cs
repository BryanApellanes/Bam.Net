using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Presentation.Unicode
{
    public class CodeColumns: QueryFilter<CodeColumns>, IFilterToken
    {
        public CodeColumns() { }
        public CodeColumns(string columnName)
            : base(columnName)
        { }
		
		public CodeColumns KeyColumn
		{
			get
			{
				return new CodeColumns("Id");
			}
		}	

				
        public CodeColumns Id
        {
            get
            {
                return new CodeColumns("Id");
            }
        }
        public CodeColumns Uuid
        {
            get
            {
                return new CodeColumns("Uuid");
            }
        }
        public CodeColumns Cuid
        {
            get
            {
                return new CodeColumns("Cuid");
            }
        }
        public CodeColumns Value
        {
            get
            {
                return new CodeColumns("Value");
            }
        }

        public CodeColumns EmojiId
        {
            get
            {
                return new CodeColumns("EmojiId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(Code);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}