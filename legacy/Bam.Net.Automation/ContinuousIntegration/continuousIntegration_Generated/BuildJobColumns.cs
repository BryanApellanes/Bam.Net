using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Automation.ContinuousIntegration.Data
{
    public class BuildJobColumns: QueryFilter<BuildJobColumns>, IFilterToken
    {
        public BuildJobColumns() { }
        public BuildJobColumns(string columnName)
            : base(columnName)
        { }
		
		public BuildJobColumns KeyColumn
		{
			get
			{
				return new BuildJobColumns("Id");
			}
		}	

				
        public BuildJobColumns Id
        {
            get
            {
                return new BuildJobColumns("Id");
            }
        }
        public BuildJobColumns Uuid
        {
            get
            {
                return new BuildJobColumns("Uuid");
            }
        }
        public BuildJobColumns Cuid
        {
            get
            {
                return new BuildJobColumns("Cuid");
            }
        }
        public BuildJobColumns Name
        {
            get
            {
                return new BuildJobColumns("Name");
            }
        }
        public BuildJobColumns UserName
        {
            get
            {
                return new BuildJobColumns("UserName");
            }
        }
        public BuildJobColumns HostName
        {
            get
            {
                return new BuildJobColumns("HostName");
            }
        }
        public BuildJobColumns OutputPath
        {
            get
            {
                return new BuildJobColumns("OutputPath");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(BuildJob);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}