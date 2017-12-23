using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.CoreServices.ApplicationRegistration.Data.Dao
{
    public class OrganizationColumns: QueryFilter<OrganizationColumns>, IFilterToken
    {
        public OrganizationColumns() { }
        public OrganizationColumns(string columnName)
            : base(columnName)
        { }
		
		public OrganizationColumns KeyColumn
		{
			get
			{
				return new OrganizationColumns("Id");
			}
		}	

				
        public OrganizationColumns Id
        {
            get
            {
                return new OrganizationColumns("Id");
            }
        }
        public OrganizationColumns Uuid
        {
            get
            {
                return new OrganizationColumns("Uuid");
            }
        }
        public OrganizationColumns Cuid
        {
            get
            {
                return new OrganizationColumns("Cuid");
            }
        }
        public OrganizationColumns Name
        {
            get
            {
                return new OrganizationColumns("Name");
            }
        }
        public OrganizationColumns CreatedBy
        {
            get
            {
                return new OrganizationColumns("CreatedBy");
            }
        }
        public OrganizationColumns ModifiedBy
        {
            get
            {
                return new OrganizationColumns("ModifiedBy");
            }
        }
        public OrganizationColumns Modified
        {
            get
            {
                return new OrganizationColumns("Modified");
            }
        }
        public OrganizationColumns Deleted
        {
            get
            {
                return new OrganizationColumns("Deleted");
            }
        }
        public OrganizationColumns Created
        {
            get
            {
                return new OrganizationColumns("Created");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(Organization);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}