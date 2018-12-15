using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Data.Repositories.Tests
{
    public class SecondaryObjectTernaryObjectColumns: QueryFilter<SecondaryObjectTernaryObjectColumns>, IFilterToken
    {
        public SecondaryObjectTernaryObjectColumns() { }
        public SecondaryObjectTernaryObjectColumns(string columnName)
            : base(columnName)
        { }
		
		public SecondaryObjectTernaryObjectColumns KeyColumn
		{
			get
			{
				return new SecondaryObjectTernaryObjectColumns("Id");
			}
		}	

				
        public SecondaryObjectTernaryObjectColumns Id
        {
            get
            {
                return new SecondaryObjectTernaryObjectColumns("Id");
            }
        }
        public SecondaryObjectTernaryObjectColumns Uuid
        {
            get
            {
                return new SecondaryObjectTernaryObjectColumns("Uuid");
            }
        }

        public SecondaryObjectTernaryObjectColumns SecondaryObjectId
        {
            get
            {
                return new SecondaryObjectTernaryObjectColumns("SecondaryObjectId");
            }
        }
        public SecondaryObjectTernaryObjectColumns TernaryObjectId
        {
            get
            {
                return new SecondaryObjectTernaryObjectColumns("TernaryObjectId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(SecondaryObjectTernaryObject);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}