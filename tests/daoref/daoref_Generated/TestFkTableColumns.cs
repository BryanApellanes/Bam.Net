using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.DaoRef
{
    public class TestFkTableColumns: QueryFilter<TestFkTableColumns>, IFilterToken
    {
        public TestFkTableColumns() { }
        public TestFkTableColumns(string columnName)
            : base(columnName)
        { }
		
		public TestFkTableColumns KeyColumn
		{
			get
			{
				return new TestFkTableColumns("Id");
			}
		}	

        public TestFkTableColumns Id
        {
            get
            {
                return new TestFkTableColumns("Id");
            }
        }
        public TestFkTableColumns Uuid
        {
            get
            {
                return new TestFkTableColumns("Uuid");
            }
        }
        public TestFkTableColumns Cuid
        {
            get
            {
                return new TestFkTableColumns("Cuid");
            }
        }
        public TestFkTableColumns Name
        {
            get
            {
                return new TestFkTableColumns("Name");
            }
        }


        public TestFkTableColumns TestTableId
        {
            get
            {
                return new TestFkTableColumns("TestTableId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(TestFkTable);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}