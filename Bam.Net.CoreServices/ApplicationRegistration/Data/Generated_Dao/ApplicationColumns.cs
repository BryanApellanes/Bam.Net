using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.CoreServices.ApplicationRegistration.Data.Dao
{
    public class ApplicationColumns: QueryFilter<ApplicationColumns>, IFilterToken
    {
        public ApplicationColumns() { }
        public ApplicationColumns(string columnName)
            : base(columnName)
        { }
		
		public ApplicationColumns KeyColumn
		{
			get
			{
				return new ApplicationColumns("Id");
			}
		}	

				
        public ApplicationColumns Id
        {
            get
            {
                return new ApplicationColumns("Id");
            }
        }
        public ApplicationColumns Uuid
        {
            get
            {
                return new ApplicationColumns("Uuid");
            }
        }
        public ApplicationColumns Cuid
        {
            get
            {
                return new ApplicationColumns("Cuid");
            }
        }
        public ApplicationColumns Name
        {
            get
            {
                return new ApplicationColumns("Name");
            }
        }
        public ApplicationColumns Description
        {
            get
            {
                return new ApplicationColumns("Description");
            }
        }
        public ApplicationColumns CreatedBy
        {
            get
            {
                return new ApplicationColumns("CreatedBy");
            }
        }
        public ApplicationColumns ModifiedBy
        {
            get
            {
                return new ApplicationColumns("ModifiedBy");
            }
        }
        public ApplicationColumns Modified
        {
            get
            {
                return new ApplicationColumns("Modified");
            }
        }
        public ApplicationColumns Deleted
        {
            get
            {
                return new ApplicationColumns("Deleted");
            }
        }
        public ApplicationColumns Created
        {
            get
            {
                return new ApplicationColumns("Created");
            }
        }

        public ApplicationColumns OrganizationId
        {
            get
            {
                return new ApplicationColumns("OrganizationId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(Application);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}