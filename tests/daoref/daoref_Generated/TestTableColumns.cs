using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.DaoRef
{
    public class TestTableColumns: QueryFilter<TestTableColumns>, IFilterToken
    {
        public TestTableColumns() { }
        public TestTableColumns(string columnName)
            : base(columnName)
        { }
		
		public TestTableColumns KeyColumn
		{
			get
			{
				return new TestTableColumns("Id");
			}
		}	

        public TestTableColumns Id
        {
            get
            {
                return new TestTableColumns("Id");
            }
        }
        public TestTableColumns Uuid
        {
            get
            {
                return new TestTableColumns("Uuid");
            }
        }
        public TestTableColumns Cuid
        {
            get
            {
                return new TestTableColumns("Cuid");
            }
        }
        public TestTableColumns Name
        {
            get
            {
                return new TestTableColumns("Name");
            }
        }
        public TestTableColumns Description
        {
            get
            {
                return new TestTableColumns("Description");
            }
        }



		protected internal Type TableType
		{
			get
			{
				return typeof(TestTable);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}