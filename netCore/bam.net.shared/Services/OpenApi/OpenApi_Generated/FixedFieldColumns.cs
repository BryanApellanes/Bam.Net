using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Services.OpenApi
{
    public class FixedFieldColumns: QueryFilter<FixedFieldColumns>, IFilterToken
    {
        public FixedFieldColumns() { }
        public FixedFieldColumns(string columnName)
            : base(columnName)
        { }
		
		public FixedFieldColumns KeyColumn
		{
			get
			{
				return new FixedFieldColumns("Id");
			}
		}	

				
        public FixedFieldColumns Id
        {
            get
            {
                return new FixedFieldColumns("Id");
            }
        }
        public FixedFieldColumns Uuid
        {
            get
            {
                return new FixedFieldColumns("Uuid");
            }
        }
        public FixedFieldColumns Cuid
        {
            get
            {
                return new FixedFieldColumns("Cuid");
            }
        }
        public FixedFieldColumns FieldName
        {
            get
            {
                return new FixedFieldColumns("FieldName");
            }
        }
        public FixedFieldColumns Type
        {
            get
            {
                return new FixedFieldColumns("Type");
            }
        }
        public FixedFieldColumns AppliesTo
        {
            get
            {
                return new FixedFieldColumns("AppliesTo");
            }
        }
        public FixedFieldColumns Description
        {
            get
            {
                return new FixedFieldColumns("Description");
            }
        }

        public FixedFieldColumns ObjectDescriptorId
        {
            get
            {
                return new FixedFieldColumns("ObjectDescriptorId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(FixedField);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}