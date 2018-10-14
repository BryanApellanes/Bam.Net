using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Services.Data
{
    public class KeyValuePairColumns: QueryFilter<KeyValuePairColumns>, IFilterToken
    {
        public KeyValuePairColumns() { }
        public KeyValuePairColumns(string columnName)
            : base(columnName)
        { }
		
		public KeyValuePairColumns KeyColumn
		{
			get
			{
				return new KeyValuePairColumns("Id");
			}
		}	

				
        public KeyValuePairColumns Id
        {
            get
            {
                return new KeyValuePairColumns("Id");
            }
        }
        public KeyValuePairColumns Uuid
        {
            get
            {
                return new KeyValuePairColumns("Uuid");
            }
        }
        public KeyValuePairColumns Cuid
        {
            get
            {
                return new KeyValuePairColumns("Cuid");
            }
        }
        public KeyValuePairColumns Key
        {
            get
            {
                return new KeyValuePairColumns("Key");
            }
        }
        public KeyValuePairColumns Value
        {
            get
            {
                return new KeyValuePairColumns("Value");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(KeyValuePair);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}