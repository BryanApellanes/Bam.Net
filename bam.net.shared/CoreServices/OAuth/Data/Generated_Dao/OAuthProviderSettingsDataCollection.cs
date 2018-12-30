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
    public class OAuthProviderSettingsDataCollection: DaoCollection<OAuthProviderSettingsDataColumns, OAuthProviderSettingsData>
    { 
		public OAuthProviderSettingsDataCollection(){}
		public OAuthProviderSettingsDataCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public OAuthProviderSettingsDataCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public OAuthProviderSettingsDataCollection(Query<OAuthProviderSettingsDataColumns, OAuthProviderSettingsData> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public OAuthProviderSettingsDataCollection(Database db, Query<OAuthProviderSettingsDataColumns, OAuthProviderSettingsData> q, bool load) : base(db, q, load) { }
		public OAuthProviderSettingsDataCollection(Query<OAuthProviderSettingsDataColumns, OAuthProviderSettingsData> q, bool load) : base(q, load) { }
    }
}