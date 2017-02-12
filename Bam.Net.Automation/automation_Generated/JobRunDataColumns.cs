using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Automation.Data
{
    public class JobRunDataColumns: QueryFilter<JobRunDataColumns>, IFilterToken
    {
        public JobRunDataColumns() { }
        public JobRunDataColumns(string columnName)
            : base(columnName)
        { }
		
		public JobRunDataColumns KeyColumn
		{
			get
			{
				return new JobRunDataColumns("Id");
			}
		}	

				
        public JobRunDataColumns Id
        {
            get
            {
                return new JobRunDataColumns("Id");
            }
        }
        public JobRunDataColumns Uuid
        {
            get
            {
                return new JobRunDataColumns("Uuid");
            }
        }
        public JobRunDataColumns Cuid
        {
            get
            {
                return new JobRunDataColumns("Cuid");
            }
        }
        public JobRunDataColumns Success
        {
            get
            {
                return new JobRunDataColumns("Success");
            }
        }
        public JobRunDataColumns Message
        {
            get
            {
                return new JobRunDataColumns("Message");
            }
        }

        public JobRunDataColumns JobDataId
        {
            get
            {
                return new JobRunDataColumns("JobDataId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(JobRunData);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}