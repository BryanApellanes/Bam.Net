/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.CoreServices.Data.Daos
{
    public class IpAddressCollection: DaoCollection<IpAddressColumns, IpAddress>
    { 
		public IpAddressCollection(){}
		public IpAddressCollection(Database db, DataTable table, Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public IpAddressCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public IpAddressCollection(Query<IpAddressColumns, IpAddress> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public IpAddressCollection(Database db, Query<IpAddressColumns, IpAddress> q, bool load) : base(db, q, load) { }
		public IpAddressCollection(Query<IpAddressColumns, IpAddress> q, bool load) : base(q, load) { }
    }
}