using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.CoreServices.ApplicationRegistration.Data.Dao
{
    public class OrganizationUserColumns: QueryFilter<OrganizationUserColumns>, IFilterToken
    {
        public OrganizationUserColumns() { }
        public OrganizationUserColumns(string columnName)
            : base(columnName)
        { }
		
		public OrganizationUserColumns KeyColumn
		{
			get
			{
				return new OrganizationUserColumns("Id");
			}
		}	

				
        public OrganizationUserColumns Id
        {
            get
            {
                return new OrganizationUserColumns("Id");
            }
        }
        public OrganizationUserColumns Uuid
        {
            get
            {
                return new OrganizationUserColumns("Uuid");
            }
        }

        public OrganizationUserColumns OrganizationId
        {
            get
            {
                return new OrganizationUserColumns("OrganizationId");
            }
        }
        public OrganizationUserColumns UserId
        {
            get
            {
                return new OrganizationUserColumns("UserId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(OrganizationUser);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}