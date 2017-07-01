/*
	This file was generated and should not be modified directly
*/
// model is SchemaDefinition
using System;
using System.Data;
using System.Data.Common;
using Bam.Net;
using Bam.Net.Data;
using Bam.Net.Data.Qi;

namespace Bam.Net.Services.OAuth.Data.Dao
{
	// schema = OAuthSettings 
    public static class OAuthSettingsContext
    {
		public static string ConnectionName
		{
			get
			{
				return "OAuthSettings";
			}
		}

		public static Database Db
		{
			get
			{
				return Bam.Net.Data.Db.For(ConnectionName);
			}
		}


	public class OAuthSettingsDataQueryContext
	{
			public OAuthSettingsDataCollection Where(WhereDelegate<OAuthSettingsDataColumns> where, Database db = null)
			{
				return Bam.Net.Services.OAuth.Data.Dao.OAuthSettingsData.Where(where, db);
			}
		   
			public OAuthSettingsDataCollection Where(WhereDelegate<OAuthSettingsDataColumns> where, OrderBy<OAuthSettingsDataColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Services.OAuth.Data.Dao.OAuthSettingsData.Where(where, orderBy, db);
			}

			public OAuthSettingsData OneWhere(WhereDelegate<OAuthSettingsDataColumns> where, Database db = null)
			{
				return Bam.Net.Services.OAuth.Data.Dao.OAuthSettingsData.OneWhere(where, db);
			}

			public static OAuthSettingsData GetOneWhere(WhereDelegate<OAuthSettingsDataColumns> where, Database db = null)
			{
				return Bam.Net.Services.OAuth.Data.Dao.OAuthSettingsData.GetOneWhere(where, db);
			}
		
			public OAuthSettingsData FirstOneWhere(WhereDelegate<OAuthSettingsDataColumns> where, Database db = null)
			{
				return Bam.Net.Services.OAuth.Data.Dao.OAuthSettingsData.FirstOneWhere(where, db);
			}

			public OAuthSettingsDataCollection Top(int count, WhereDelegate<OAuthSettingsDataColumns> where, Database db = null)
			{
				return Bam.Net.Services.OAuth.Data.Dao.OAuthSettingsData.Top(count, where, db);
			}

			public OAuthSettingsDataCollection Top(int count, WhereDelegate<OAuthSettingsDataColumns> where, OrderBy<OAuthSettingsDataColumns> orderBy, Database db = null)
			{
				return Bam.Net.Services.OAuth.Data.Dao.OAuthSettingsData.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<OAuthSettingsDataColumns> where, Database db = null)
			{
				return Bam.Net.Services.OAuth.Data.Dao.OAuthSettingsData.Count(where, db);
			}
	}

	static OAuthSettingsDataQueryContext _oAuthSettingsDatas;
	static object _oAuthSettingsDatasLock = new object();
	public static OAuthSettingsDataQueryContext OAuthSettingsDatas
	{
		get
		{
			return _oAuthSettingsDatasLock.DoubleCheckLock<OAuthSettingsDataQueryContext>(ref _oAuthSettingsDatas, () => new OAuthSettingsDataQueryContext());
		}
	}    }
}																								
