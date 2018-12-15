/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.CoreServices.OAuth.Data.Dao
{
    public class OAuthProviderSettingsDataQuery: Query<OAuthProviderSettingsDataColumns, OAuthProviderSettingsData>
    { 
		public OAuthProviderSettingsDataQuery(){}
		public OAuthProviderSettingsDataQuery(WhereDelegate<OAuthProviderSettingsDataColumns> where, OrderBy<OAuthProviderSettingsDataColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public OAuthProviderSettingsDataQuery(Func<OAuthProviderSettingsDataColumns, QueryFilter<OAuthProviderSettingsDataColumns>> where, OrderBy<OAuthProviderSettingsDataColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public OAuthProviderSettingsDataQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static OAuthProviderSettingsDataQuery Where(WhereDelegate<OAuthProviderSettingsDataColumns> where)
        {
            return Where(where, null, null);
        }

        public static OAuthProviderSettingsDataQuery Where(WhereDelegate<OAuthProviderSettingsDataColumns> where, OrderBy<OAuthProviderSettingsDataColumns> orderBy = null, Database db = null)
        {
            return new OAuthProviderSettingsDataQuery(where, orderBy, db);
        }

		public OAuthProviderSettingsDataCollection Execute()
		{
			return new OAuthProviderSettingsDataCollection(this, true);
		}
    }
}