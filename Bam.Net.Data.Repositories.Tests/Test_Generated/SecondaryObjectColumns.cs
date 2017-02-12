using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Data.Repositories.Tests
{
    public class SecondaryObjectColumns: QueryFilter<SecondaryObjectColumns>, IFilterToken
    {
        public SecondaryObjectColumns() { }
        public SecondaryObjectColumns(string columnName)
            : base(columnName)
        { }
		
		public SecondaryObjectColumns KeyColumn
		{
			get
			{
				return new SecondaryObjectColumns("Id");
			}
		}	

				
        public SecondaryObjectColumns Id
        {
            get
            {
                return new SecondaryObjectColumns("Id");
            }
        }
        public SecondaryObjectColumns Uuid
        {
            get
            {
                return new SecondaryObjectColumns("Uuid");
            }
        }
        public SecondaryObjectColumns Cuid
        {
            get
            {
                return new SecondaryObjectColumns("Cuid");
            }
        }
        public SecondaryObjectColumns Created
        {
            get
            {
                return new SecondaryObjectColumns("Created");
            }
        }
        public SecondaryObjectColumns Name
        {
            get
            {
                return new SecondaryObjectColumns("Name");
            }
        }

        public SecondaryObjectColumns MainId
        {
            get
            {
                return new SecondaryObjectColumns("MainId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(SecondaryObject);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}