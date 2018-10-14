/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.CoreServices.ApplicationRegistration.Data.Dao
{
    public class OrganizationUserPagedQuery: PagedQuery<OrganizationUserColumns, OrganizationUser>
    { 
		public OrganizationUserPagedQuery(OrganizationUserColumns orderByColumn, OrganizationUserQuery query, Database db = null) : base(orderByColumn, query, db) { }
    }
}