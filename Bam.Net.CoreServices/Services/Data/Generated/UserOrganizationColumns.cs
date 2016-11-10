using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.CoreServices.Data.Daos
{
    public class UserOrganizationColumns: QueryFilter<UserOrganizationColumns>, IFilterToken
    {
        public UserOrganizationColumns() { }
        public UserOrganizationColumns(string columnName)
            : base(columnName)
        { }
		
		public UserOrganizationColumns KeyColumn
		{
			get
			{
				return new UserOrganizationColumns("Id");
			}
		}	

				
        public UserOrganizationColumns Id
        {
            get
            {
                return new UserOrganizationColumns("Id");
            }
        }
        public UserOrganizationColumns Uuid
        {
            get
            {
                return new UserOrganizationColumns("Uuid");
            }
        }

        public UserOrganizationColumns UserId
        {
            get
            {
                return new UserOrganizationColumns("UserId");
            }
        }
        public UserOrganizationColumns OrganizationId
        {
            get
            {
                return new UserOrganizationColumns("OrganizationId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(UserOrganization);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}