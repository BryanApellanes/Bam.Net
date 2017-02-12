using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Data.Repositories.Tests
{
    public class TernaryObjectColumns: QueryFilter<TernaryObjectColumns>, IFilterToken
    {
        public TernaryObjectColumns() { }
        public TernaryObjectColumns(string columnName)
            : base(columnName)
        { }
		
		public TernaryObjectColumns KeyColumn
		{
			get
			{
				return new TernaryObjectColumns("Id");
			}
		}	

				
        public TernaryObjectColumns Id
        {
            get
            {
                return new TernaryObjectColumns("Id");
            }
        }
        public TernaryObjectColumns Uuid
        {
            get
            {
                return new TernaryObjectColumns("Uuid");
            }
        }
        public TernaryObjectColumns Cuid
        {
            get
            {
                return new TernaryObjectColumns("Cuid");
            }
        }
        public TernaryObjectColumns Created
        {
            get
            {
                return new TernaryObjectColumns("Created");
            }
        }
        public TernaryObjectColumns Name
        {
            get
            {
                return new TernaryObjectColumns("Name");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(TernaryObject);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}