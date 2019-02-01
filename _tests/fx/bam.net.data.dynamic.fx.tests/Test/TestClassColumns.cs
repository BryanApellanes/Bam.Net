using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Test.DataBanana.Dao
{
    public class TestClassColumns: QueryFilter<TestClassColumns>, IFilterToken
    {
        public TestClassColumns() { }
        public TestClassColumns(string columnName)
            : base(columnName)
        { }
		
		public TestClassColumns KeyColumn
		{
			get
			{
				return new TestClassColumns("Id");
			}
		}	

				
        public TestClassColumns Id
        {
            get
            {
                return new TestClassColumns("Id");
            }
        }
        public TestClassColumns Uuid
        {
            get
            {
                return new TestClassColumns("Uuid");
            }
        }
        public TestClassColumns Cuid
        {
            get
            {
                return new TestClassColumns("Cuid");
            }
        }
        public TestClassColumns Value
        {
            get
            {
                return new TestClassColumns("Value");
            }
        }
        public TestClassColumns Created
        {
            get
            {
                return new TestClassColumns("Created");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(TestClass);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}