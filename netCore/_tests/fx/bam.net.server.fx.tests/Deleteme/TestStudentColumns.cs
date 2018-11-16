using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Server.Tests.Dao
{
    public class TestStudentColumns: QueryFilter<TestStudentColumns>, IFilterToken
    {
        public TestStudentColumns() { }
        public TestStudentColumns(string columnName)
            : base(columnName)
        { }
		
		public TestStudentColumns KeyColumn
		{
			get
			{
				return new TestStudentColumns("Id");
			}
		}	

				
        public TestStudentColumns Id
        {
            get
            {
                return new TestStudentColumns("Id");
            }
        }
        public TestStudentColumns Uuid
        {
            get
            {
                return new TestStudentColumns("Uuid");
            }
        }
        public TestStudentColumns Cuid
        {
            get
            {
                return new TestStudentColumns("Cuid");
            }
        }
        public TestStudentColumns Name
        {
            get
            {
                return new TestStudentColumns("Name");
            }
        }

        public TestStudentColumns TestClassId
        {
            get
            {
                return new TestStudentColumns("TestClassId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(TestStudent);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}