/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Services.OAuth.Data.Dao
{
    public class OAuthSettingsDataQuery: Query<OAuthSettingsDataColumns, OAuthSettingsData>
    { 
		public OAuthSettingsDataQuery(){}
		public OAuthSettingsDataQuery(WhereDelegate<OAuthSettingsDataColumns> where, OrderBy<OAuthSettingsDataColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public OAuthSettingsDataQuery(Func<OAuthSettingsDataColumns, QueryFilter<OAuthSettingsDataColumns>> where, OrderBy<OAuthSettingsDataColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public OAuthSettingsDataQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static OAuthSettingsDataQuery Where(WhereDelegate<OAuthSettingsDataColumns> where)
        {
            return Where(where, null, null);
        }

        public static OAuthSettingsDataQuery Where(WhereDelegate<OAuthSettingsDataColumns> where, OrderBy<OAuthSettingsDataColumns> orderBy = null, Database db = null)
        {
            return new OAuthSettingsDataQuery(where, orderBy, db);
        }

		public OAuthSettingsDataCollection Execute()
		{
			return new OAuthSettingsDataCollection(this, true);
		}
    }
}