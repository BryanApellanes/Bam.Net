using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Automation.Data
{
    public class DeferredJobDataColumns: QueryFilter<DeferredJobDataColumns>, IFilterToken
    {
        public DeferredJobDataColumns() { }
        public DeferredJobDataColumns(string columnName)
            : base(columnName)
        { }
		
		public DeferredJobDataColumns KeyColumn
		{
			get
			{
				return new DeferredJobDataColumns("Id");
			}
		}	

				
        public DeferredJobDataColumns Id
        {
            get
            {
                return new DeferredJobDataColumns("Id");
            }
        }
        public DeferredJobDataColumns Uuid
        {
            get
            {
                return new DeferredJobDataColumns("Uuid");
            }
        }
        public DeferredJobDataColumns Cuid
        {
            get
            {
                return new DeferredJobDataColumns("Cuid");
            }
        }
        public DeferredJobDataColumns Name
        {
            get
            {
                return new DeferredJobDataColumns("Name");
            }
        }
        public DeferredJobDataColumns JobDirectory
        {
            get
            {
                return new DeferredJobDataColumns("JobDirectory");
            }
        }
        public DeferredJobDataColumns HostName
        {
            get
            {
                return new DeferredJobDataColumns("HostName");
            }
        }
        public DeferredJobDataColumns LastStepNumber
        {
            get
            {
                return new DeferredJobDataColumns("LastStepNumber");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(DeferredJobData);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}