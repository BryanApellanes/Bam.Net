/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.CoreServices.Data.Daos
{
    public class UserOrganizationPagedQuery: PagedQuery<UserOrganizationColumns, UserOrganization>
    { 
		public UserOrganizationPagedQuery(UserOrganizationColumns orderByColumn, UserOrganizationQuery query, Database db = null) : base(orderByColumn, query, db) { }
    }
}