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
    public class OAuthProviderSettingsDataPagedQuery: PagedQuery<OAuthProviderSettingsDataColumns, OAuthProviderSettingsData>
    { 
		public OAuthProviderSettingsDataPagedQuery(OAuthProviderSettingsDataColumns orderByColumn, OAuthProviderSettingsDataQuery query, Database db = null) : base(orderByColumn, query, db) { }
    }
}