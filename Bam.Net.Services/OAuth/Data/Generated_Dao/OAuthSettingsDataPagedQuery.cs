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
    public class OAuthSettingsDataPagedQuery: PagedQuery<OAuthSettingsDataColumns, OAuthSettingsData>
    { 
		public OAuthSettingsDataPagedQuery(OAuthSettingsDataColumns orderByColumn, OAuthSettingsDataQuery query, Database db = null) : base(orderByColumn, query, db) { }
    }
}