using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Services.OpenApi
{
    public class PatternedFieldColumns: QueryFilter<PatternedFieldColumns>, IFilterToken
    {
        public PatternedFieldColumns() { }
        public PatternedFieldColumns(string columnName)
            : base(columnName)
        { }
		
		public PatternedFieldColumns KeyColumn
		{
			get
			{
				return new PatternedFieldColumns("Id");
			}
		}	

				
        public PatternedFieldColumns Id
        {
            get
            {
                return new PatternedFieldColumns("Id");
            }
        }
        public PatternedFieldColumns Uuid
        {
            get
            {
                return new PatternedFieldColumns("Uuid");
            }
        }
        public PatternedFieldColumns Cuid
        {
            get
            {
                return new PatternedFieldColumns("Cuid");
            }
        }
        public PatternedFieldColumns FieldPattern
        {
            get
            {
                return new PatternedFieldColumns("FieldPattern");
            }
        }
        public PatternedFieldColumns Type
        {
            get
            {
                return new PatternedFieldColumns("Type");
            }
        }
        public PatternedFieldColumns AppliesTo
        {
            get
            {
                return new PatternedFieldColumns("AppliesTo");
            }
        }
        public PatternedFieldColumns Description
        {
            get
            {
                return new PatternedFieldColumns("Description");
            }
        }

        public PatternedFieldColumns ObjectDescriptorId
        {
            get
            {
                return new PatternedFieldColumns("ObjectDescriptorId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(PatternedField);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}