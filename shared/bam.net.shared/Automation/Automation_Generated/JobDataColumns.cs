using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Automation.Data
{
    public class JobDataColumns: QueryFilter<JobDataColumns>, IFilterToken
    {
        public JobDataColumns() { }
        public JobDataColumns(string columnName)
            : base(columnName)
        { }
		
		public JobDataColumns KeyColumn
		{
			get
			{
				return new JobDataColumns("Id");
			}
		}	

				
        public JobDataColumns Id
        {
            get
            {
                return new JobDataColumns("Id");
            }
        }
        public JobDataColumns Uuid
        {
            get
            {
                return new JobDataColumns("Uuid");
            }
        }
        public JobDataColumns Cuid
        {
            get
            {
                return new JobDataColumns("Cuid");
            }
        }
        public JobDataColumns Name
        {
            get
            {
                return new JobDataColumns("Name");
            }
        }
        public JobDataColumns Description
        {
            get
            {
                return new JobDataColumns("Description");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(JobData);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}