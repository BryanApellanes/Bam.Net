using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.CoreServices.Data.Daos
{
    public class ApplicationInstanceColumns: QueryFilter<ApplicationInstanceColumns>, IFilterToken
    {
        public ApplicationInstanceColumns() { }
        public ApplicationInstanceColumns(string columnName)
            : base(columnName)
        { }
		
		public ApplicationInstanceColumns KeyColumn
		{
			get
			{
				return new ApplicationInstanceColumns("Id");
			}
		}	

				
        public ApplicationInstanceColumns Id
        {
            get
            {
                return new ApplicationInstanceColumns("Id");
            }
        }
        public ApplicationInstanceColumns Uuid
        {
            get
            {
                return new ApplicationInstanceColumns("Uuid");
            }
        }
        public ApplicationInstanceColumns Cuid
        {
            get
            {
                return new ApplicationInstanceColumns("Cuid");
            }
        }
        public ApplicationInstanceColumns InstanceIdentifier
        {
            get
            {
                return new ApplicationInstanceColumns("InstanceIdentifier");
            }
        }
        public ApplicationInstanceColumns CreatedBy
        {
            get
            {
                return new ApplicationInstanceColumns("CreatedBy");
            }
        }
        public ApplicationInstanceColumns Created
        {
            get
            {
                return new ApplicationInstanceColumns("Created");
            }
        }

        public ApplicationInstanceColumns ApplicationId
        {
            get
            {
                return new ApplicationInstanceColumns("ApplicationId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(ApplicationInstance);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}