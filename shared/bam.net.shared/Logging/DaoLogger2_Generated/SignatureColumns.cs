using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Logging.Data
{
    public class SignatureColumns: QueryFilter<SignatureColumns>, IFilterToken
    {
        public SignatureColumns() { }
        public SignatureColumns(string columnName)
            : base(columnName)
        { }
		
		public SignatureColumns KeyColumn
		{
			get
			{
				return new SignatureColumns("Id");
			}
		}	

				
        public SignatureColumns Id
        {
            get
            {
                return new SignatureColumns("Id");
            }
        }
        public SignatureColumns Uuid
        {
            get
            {
                return new SignatureColumns("Uuid");
            }
        }
        public SignatureColumns Cuid
        {
            get
            {
                return new SignatureColumns("Cuid");
            }
        }
        public SignatureColumns Value
        {
            get
            {
                return new SignatureColumns("Value");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(Signature);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}