/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.CoreServices.ApplicationRegistration.Dao
{
    public class UserPagedQuery: PagedQuery<UserColumns, User>
    { 
		public UserPagedQuery(UserColumns orderByColumn, UserQuery query, Database db = null) : base(orderByColumn, query, db) { }
    }
}