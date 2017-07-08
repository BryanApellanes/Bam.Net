/*
This file was generated and should not be modified directly
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Bam.Net;
using Bam.Net.Data;
using Bam.Net.Data.Repositories;
using Bam.Net.CoreServices.OAuth.Data;

namespace Bam.Net.CoreServices.OAuth.Data.Dao.Repository
{
	[Serializable]
	public class OAuthSettingsRepository: DaoRepository
	{
		public OAuthSettingsRepository()
		{
			SchemaName = "OAuthSettings";
			BaseNamespace = "Bam.Net.CoreServices.OAuth.Data";			
﻿			
			AddType<Bam.Net.CoreServices.OAuth.Data.OAuthSettingsData>();
			DaoAssembly = typeof(OAuthSettingsRepository).Assembly;
		}

		object _addLock = new object();
        public override void AddType(Type type)
        {
            lock (_addLock)
            {
                base.AddType(type);
                DaoAssembly = typeof(OAuthSettingsRepository).Assembly;
            }
        }

﻿		
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		/// <param name="where"></param>
		public Bam.Net.CoreServices.OAuth.Data.OAuthSettingsData GetOneOAuthSettingsDataWhere(WhereDelegate<OAuthSettingsDataColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.CoreServices.OAuth.Data.OAuthSettingsData>();
			return (Bam.Net.CoreServices.OAuth.Data.OAuthSettingsData)Bam.Net.CoreServices.OAuth.Data.Dao.OAuthSettingsData.GetOneWhere(where, Database).CopyAs(wrapperType, this);
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single OAuthSettingsData instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a OAuthSettingsDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between OAuthSettingsDataColumns and other values
		/// </param>
		public Bam.Net.CoreServices.OAuth.Data.OAuthSettingsData OneOAuthSettingsDataWhere(WhereDelegate<OAuthSettingsDataColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.CoreServices.OAuth.Data.OAuthSettingsData>();
            return (Bam.Net.CoreServices.OAuth.Data.OAuthSettingsData)Bam.Net.CoreServices.OAuth.Data.Dao.OAuthSettingsData.OneWhere(where, Database).CopyAs(wrapperType, this);
        }

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a Bam.Net.CoreServices.OAuth.Data.OAuthSettingsDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between Bam.Net.CoreServices.OAuth.Data.OAuthSettingsDataColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.CoreServices.OAuth.Data.OAuthSettingsData> OAuthSettingsDatasWhere(WhereDelegate<OAuthSettingsDataColumns> where, OrderBy<OAuthSettingsDataColumns> orderBy = null)
        {
            return Wrap<Bam.Net.CoreServices.OAuth.Data.OAuthSettingsData>(Bam.Net.CoreServices.OAuth.Data.Dao.OAuthSettingsData.Where(where, orderBy, Database));
        }
		
		/// <summary>
		/// Execute a query and return the specified number
		/// of values. This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="count">The number of values to return.
		/// This value is used in the sql query so no more than this 
		/// number of values will be returned by the database.
		/// </param>
		/// <param name="where">A WhereDelegate that recieves a OAuthSettingsDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between OAuthSettingsDataColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.CoreServices.OAuth.Data.OAuthSettingsData> TopOAuthSettingsDatasWhere(int count, WhereDelegate<OAuthSettingsDataColumns> where)
        {
            return Wrap<Bam.Net.CoreServices.OAuth.Data.OAuthSettingsData>(Bam.Net.CoreServices.OAuth.Data.Dao.OAuthSettingsData.Top(count, where, Database));
        }

		/// <summary>
		/// Return the count of OAuthSettingsDatas
		/// </summary>
		public long CountOAuthSettingsDatas()
        {
            return Bam.Net.CoreServices.OAuth.Data.Dao.OAuthSettingsData.Count(Database);
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a OAuthSettingsDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between OAuthSettingsDataColumns and other values
		/// </param>
        public long CountOAuthSettingsDatasWhere(WhereDelegate<OAuthSettingsDataColumns> where)
        {
            return Bam.Net.CoreServices.OAuth.Data.Dao.OAuthSettingsData.Count(where, Database);
        }
        
        public async Task BatchQueryOAuthSettingsDatas(int batchSize, WhereDelegate<OAuthSettingsDataColumns> where, Action<IEnumerable<Bam.Net.CoreServices.OAuth.Data.OAuthSettingsData>> batchProcessor)
        {
            await Bam.Net.CoreServices.OAuth.Data.Dao.OAuthSettingsData.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.OAuth.Data.OAuthSettingsData>(batch));
            }, Database);
        }
		
        public async Task BatchAllOAuthSettingsDatas(int batchSize, Action<IEnumerable<Bam.Net.CoreServices.OAuth.Data.OAuthSettingsData>> batchProcessor)
        {
            await Bam.Net.CoreServices.OAuth.Data.Dao.OAuthSettingsData.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.OAuth.Data.OAuthSettingsData>(batch));
            }, Database);
        }
	}
}																								
