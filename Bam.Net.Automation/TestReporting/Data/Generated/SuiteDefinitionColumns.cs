using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Automation.TestReporting.Data.Dao
{
    public class SuiteDefinitionColumns: QueryFilter<SuiteDefinitionColumns>, IFilterToken
    {
        public SuiteDefinitionColumns() { }
        public SuiteDefinitionColumns(string columnName)
            : base(columnName)
        { }
		
		public SuiteDefinitionColumns KeyColumn
		{
			get
			{
				return new SuiteDefinitionColumns("Id");
			}
		}	

				
        public SuiteDefinitionColumns Id
        {
            get
            {
                return new SuiteDefinitionColumns("Id");
            }
        }
        public SuiteDefinitionColumns Uuid
        {
            get
            {
                return new SuiteDefinitionColumns("Uuid");
            }
        }
        public SuiteDefinitionColumns Cuid
        {
            get
            {
                return new SuiteDefinitionColumns("Cuid");
            }
        }
        public SuiteDefinitionColumns Title
        {
            get
            {
                return new SuiteDefinitionColumns("Title");
            }
        }
        public SuiteDefinitionColumns Created
        {
            get
            {
                return new SuiteDefinitionColumns("Created");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(SuiteDefinition);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}