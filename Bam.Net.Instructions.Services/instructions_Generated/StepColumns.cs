using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Instructions
{
    public class StepColumns: QueryFilter<StepColumns>, IFilterToken
    {
        public StepColumns() { }
        public StepColumns(string columnName)
            : base(columnName)
        { }
		
		public StepColumns KeyColumn
		{
			get
			{
				return new StepColumns("Id");
			}
		}	

				
        public StepColumns Id
        {
            get
            {
                return new StepColumns("Id");
            }
        }
        public StepColumns Uuid
        {
            get
            {
                return new StepColumns("Uuid");
            }
        }
        public StepColumns Number
        {
            get
            {
                return new StepColumns("Number");
            }
        }
        public StepColumns Description
        {
            get
            {
                return new StepColumns("Description");
            }
        }
        public StepColumns Detail
        {
            get
            {
                return new StepColumns("Detail");
            }
        }

        public StepColumns SectionId
        {
            get
            {
                return new StepColumns("SectionId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(Step);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}