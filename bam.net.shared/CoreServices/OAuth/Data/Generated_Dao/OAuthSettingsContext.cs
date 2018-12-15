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

namespace Bam.Net.CoreServices.OAuth.Data.Dao
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


	public class OAuthProviderSettingsDataQueryContext
	{
			public OAuthProviderSettingsDataCollection Where(WhereDelegate<OAuthProviderSettingsDataColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.OAuth.Data.Dao.OAuthProviderSettingsData.Where(where, db);
			}
		   
			public OAuthProviderSettingsDataCollection Where(WhereDelegate<OAuthProviderSettingsDataColumns> where, OrderBy<OAuthProviderSettingsDataColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.CoreServices.OAuth.Data.Dao.OAuthProviderSettingsData.Where(where, orderBy, db);
			}

			public OAuthProviderSettingsData OneWhere(WhereDelegate<OAuthProviderSettingsDataColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.OAuth.Data.Dao.OAuthProviderSettingsData.OneWhere(where, db);
			}

			public static OAuthProviderSettingsData GetOneWhere(WhereDelegate<OAuthProviderSettingsDataColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.OAuth.Data.Dao.OAuthProviderSettingsData.GetOneWhere(where, db);
			}
		
			public OAuthProviderSettingsData FirstOneWhere(WhereDelegate<OAuthProviderSettingsDataColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.OAuth.Data.Dao.OAuthProviderSettingsData.FirstOneWhere(where, db);
			}

			public OAuthProviderSettingsDataCollection Top(int count, WhereDelegate<OAuthProviderSettingsDataColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.OAuth.Data.Dao.OAuthProviderSettingsData.Top(count, where, db);
			}

			public OAuthProviderSettingsDataCollection Top(int count, WhereDelegate<OAuthProviderSettingsDataColumns> where, OrderBy<OAuthProviderSettingsDataColumns> orderBy, Database db = null)
			{
				return Bam.Net.CoreServices.OAuth.Data.Dao.OAuthProviderSettingsData.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<OAuthProviderSettingsDataColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.OAuth.Data.Dao.OAuthProviderSettingsData.Count(where, db);
			}
	}

	static OAuthProviderSettingsDataQueryContext _oAuthProviderSettingsDatas;
	static object _oAuthProviderSettingsDatasLock = new object();
	public static OAuthProviderSettingsDataQueryContext OAuthProviderSettingsDatas
	{
		get
		{
			return _oAuthProviderSettingsDatasLock.DoubleCheckLock<OAuthProviderSettingsDataQueryContext>(ref _oAuthProviderSettingsDatas, () => new OAuthProviderSettingsDataQueryContext());
		}
	}    }
}																								
