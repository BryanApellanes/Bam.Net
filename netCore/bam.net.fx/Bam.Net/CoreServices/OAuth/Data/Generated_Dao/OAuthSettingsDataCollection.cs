/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.CoreServices.OAuth.Data.Dao
{
    public class OAuthSettingsDataCollection: DaoCollection<OAuthSettingsDataColumns, OAuthSettingsData>
    { 
		public OAuthSettingsDataCollection(){}
		public OAuthSettingsDataCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public OAuthSettingsDataCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public OAuthSettingsDataCollection(Query<OAuthSettingsDataColumns, OAuthSettingsData> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public OAuthSettingsDataCollection(Database db, Query<OAuthSettingsDataColumns, OAuthSettingsData> q, bool load) : base(db, q, load) { }
		public OAuthSettingsDataCollection(Query<OAuthSettingsDataColumns, OAuthSettingsData> q, bool load) : base(q, load) { }
    }
}