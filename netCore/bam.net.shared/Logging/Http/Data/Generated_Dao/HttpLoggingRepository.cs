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
using Bam.Net.Logging.Http.Data;

namespace Bam.Net.Logging.Http.Data.Dao.Repository
{
	[Serializable]
	public class HttpLoggingRepository: DaoRepository
	{
		public HttpLoggingRepository()
		{
			SchemaName = "HttpLogging";
			BaseNamespace = "Bam.Net.Logging.Http.Data";			
﻿			
			AddType<Bam.Net.Logging.Http.Data.CookieData>();﻿			
			AddType<Bam.Net.Logging.Http.Data.HeaderData>();﻿			
			AddType<Bam.Net.Logging.Http.Data.QueryStringData>();﻿			
			AddType<Bam.Net.Logging.Http.Data.RequestData>();﻿			
			AddType<Bam.Net.Logging.Http.Data.UriData>();
			DaoAssembly = typeof(HttpLoggingRepository).Assembly;
		}

		object _addLock = new object();
        public override void AddType(Type type)
        {
            lock (_addLock)
            {
                base.AddType(type);
                DaoAssembly = typeof(HttpLoggingRepository).Assembly;
            }
        }

﻿		
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		/// <param name="where"></param>
		public Bam.Net.Logging.Http.Data.CookieData GetOneCookieDataWhere(WhereDelegate<CookieDataColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.Logging.Http.Data.CookieData>();
			return (Bam.Net.Logging.Http.Data.CookieData)Bam.Net.Logging.Http.Data.Dao.CookieData.GetOneWhere(where, Database)?.CopyAs(wrapperType, this);
		}

		/// <summary>
		/// Execute a query that should return only one result.  If no result is found null is returned.  If more
		/// than one result is returned a MultipleEntriesFoundException is thrown.  This method is most commonly used to retrieve a
		/// single CookieData instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a CookieDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CookieDataColumns and other values
		/// </param>
		public Bam.Net.Logging.Http.Data.CookieData OneCookieDataWhere(WhereDelegate<CookieDataColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.Logging.Http.Data.CookieData>();
            return (Bam.Net.Logging.Http.Data.CookieData)Bam.Net.Logging.Http.Data.Dao.CookieData.OneWhere(where, Database)?.CopyAs(wrapperType, this);
        }

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a Bam.Net.Logging.Http.Data.CookieDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between Bam.Net.Logging.Http.Data.CookieDataColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.Logging.Http.Data.CookieData> CookieDatasWhere(WhereDelegate<CookieDataColumns> where, OrderBy<CookieDataColumns> orderBy = null)
        {
            return Wrap<Bam.Net.Logging.Http.Data.CookieData>(Bam.Net.Logging.Http.Data.Dao.CookieData.Where(where, orderBy, Database));
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
		/// <param name="where">A WhereDelegate that recieves a CookieDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CookieDataColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.Logging.Http.Data.CookieData> TopCookieDatasWhere(int count, WhereDelegate<CookieDataColumns> where)
        {
            return Wrap<Bam.Net.Logging.Http.Data.CookieData>(Bam.Net.Logging.Http.Data.Dao.CookieData.Top(count, where, Database));
        }

		/// <summary>
		/// Return the count of CookieDatas
		/// </summary>
		public long CountCookieDatas()
        {
            return Bam.Net.Logging.Http.Data.Dao.CookieData.Count(Database);
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a CookieDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CookieDataColumns and other values
		/// </param>
        public long CountCookieDatasWhere(WhereDelegate<CookieDataColumns> where)
        {
            return Bam.Net.Logging.Http.Data.Dao.CookieData.Count(where, Database);
        }
        
        public async Task BatchQueryCookieDatas(int batchSize, WhereDelegate<CookieDataColumns> where, Action<IEnumerable<Bam.Net.Logging.Http.Data.CookieData>> batchProcessor)
        {
            await Bam.Net.Logging.Http.Data.Dao.CookieData.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.Logging.Http.Data.CookieData>(batch));
            }, Database);
        }
		
        public async Task BatchAllCookieDatas(int batchSize, Action<IEnumerable<Bam.Net.Logging.Http.Data.CookieData>> batchProcessor)
        {
            await Bam.Net.Logging.Http.Data.Dao.CookieData.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.Logging.Http.Data.CookieData>(batch));
            }, Database);
        }﻿		
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		/// <param name="where"></param>
		public Bam.Net.Logging.Http.Data.HeaderData GetOneHeaderDataWhere(WhereDelegate<HeaderDataColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.Logging.Http.Data.HeaderData>();
			return (Bam.Net.Logging.Http.Data.HeaderData)Bam.Net.Logging.Http.Data.Dao.HeaderData.GetOneWhere(where, Database)?.CopyAs(wrapperType, this);
		}

		/// <summary>
		/// Execute a query that should return only one result.  If no result is found null is returned.  If more
		/// than one result is returned a MultipleEntriesFoundException is thrown.  This method is most commonly used to retrieve a
		/// single HeaderData instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a HeaderDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between HeaderDataColumns and other values
		/// </param>
		public Bam.Net.Logging.Http.Data.HeaderData OneHeaderDataWhere(WhereDelegate<HeaderDataColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.Logging.Http.Data.HeaderData>();
            return (Bam.Net.Logging.Http.Data.HeaderData)Bam.Net.Logging.Http.Data.Dao.HeaderData.OneWhere(where, Database)?.CopyAs(wrapperType, this);
        }

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a Bam.Net.Logging.Http.Data.HeaderDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between Bam.Net.Logging.Http.Data.HeaderDataColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.Logging.Http.Data.HeaderData> HeaderDatasWhere(WhereDelegate<HeaderDataColumns> where, OrderBy<HeaderDataColumns> orderBy = null)
        {
            return Wrap<Bam.Net.Logging.Http.Data.HeaderData>(Bam.Net.Logging.Http.Data.Dao.HeaderData.Where(where, orderBy, Database));
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
		/// <param name="where">A WhereDelegate that recieves a HeaderDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between HeaderDataColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.Logging.Http.Data.HeaderData> TopHeaderDatasWhere(int count, WhereDelegate<HeaderDataColumns> where)
        {
            return Wrap<Bam.Net.Logging.Http.Data.HeaderData>(Bam.Net.Logging.Http.Data.Dao.HeaderData.Top(count, where, Database));
        }

		/// <summary>
		/// Return the count of HeaderDatas
		/// </summary>
		public long CountHeaderDatas()
        {
            return Bam.Net.Logging.Http.Data.Dao.HeaderData.Count(Database);
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a HeaderDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between HeaderDataColumns and other values
		/// </param>
        public long CountHeaderDatasWhere(WhereDelegate<HeaderDataColumns> where)
        {
            return Bam.Net.Logging.Http.Data.Dao.HeaderData.Count(where, Database);
        }
        
        public async Task BatchQueryHeaderDatas(int batchSize, WhereDelegate<HeaderDataColumns> where, Action<IEnumerable<Bam.Net.Logging.Http.Data.HeaderData>> batchProcessor)
        {
            await Bam.Net.Logging.Http.Data.Dao.HeaderData.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.Logging.Http.Data.HeaderData>(batch));
            }, Database);
        }
		
        public async Task BatchAllHeaderDatas(int batchSize, Action<IEnumerable<Bam.Net.Logging.Http.Data.HeaderData>> batchProcessor)
        {
            await Bam.Net.Logging.Http.Data.Dao.HeaderData.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.Logging.Http.Data.HeaderData>(batch));
            }, Database);
        }﻿		
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		/// <param name="where"></param>
		public Bam.Net.Logging.Http.Data.QueryStringData GetOneQueryStringDataWhere(WhereDelegate<QueryStringDataColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.Logging.Http.Data.QueryStringData>();
			return (Bam.Net.Logging.Http.Data.QueryStringData)Bam.Net.Logging.Http.Data.Dao.QueryStringData.GetOneWhere(where, Database)?.CopyAs(wrapperType, this);
		}

		/// <summary>
		/// Execute a query that should return only one result.  If no result is found null is returned.  If more
		/// than one result is returned a MultipleEntriesFoundException is thrown.  This method is most commonly used to retrieve a
		/// single QueryStringData instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a QueryStringDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between QueryStringDataColumns and other values
		/// </param>
		public Bam.Net.Logging.Http.Data.QueryStringData OneQueryStringDataWhere(WhereDelegate<QueryStringDataColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.Logging.Http.Data.QueryStringData>();
            return (Bam.Net.Logging.Http.Data.QueryStringData)Bam.Net.Logging.Http.Data.Dao.QueryStringData.OneWhere(where, Database)?.CopyAs(wrapperType, this);
        }

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a Bam.Net.Logging.Http.Data.QueryStringDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between Bam.Net.Logging.Http.Data.QueryStringDataColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.Logging.Http.Data.QueryStringData> QueryStringDatasWhere(WhereDelegate<QueryStringDataColumns> where, OrderBy<QueryStringDataColumns> orderBy = null)
        {
            return Wrap<Bam.Net.Logging.Http.Data.QueryStringData>(Bam.Net.Logging.Http.Data.Dao.QueryStringData.Where(where, orderBy, Database));
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
		/// <param name="where">A WhereDelegate that recieves a QueryStringDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between QueryStringDataColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.Logging.Http.Data.QueryStringData> TopQueryStringDatasWhere(int count, WhereDelegate<QueryStringDataColumns> where)
        {
            return Wrap<Bam.Net.Logging.Http.Data.QueryStringData>(Bam.Net.Logging.Http.Data.Dao.QueryStringData.Top(count, where, Database));
        }

		/// <summary>
		/// Return the count of QueryStringDatas
		/// </summary>
		public long CountQueryStringDatas()
        {
            return Bam.Net.Logging.Http.Data.Dao.QueryStringData.Count(Database);
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a QueryStringDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between QueryStringDataColumns and other values
		/// </param>
        public long CountQueryStringDatasWhere(WhereDelegate<QueryStringDataColumns> where)
        {
            return Bam.Net.Logging.Http.Data.Dao.QueryStringData.Count(where, Database);
        }
        
        public async Task BatchQueryQueryStringDatas(int batchSize, WhereDelegate<QueryStringDataColumns> where, Action<IEnumerable<Bam.Net.Logging.Http.Data.QueryStringData>> batchProcessor)
        {
            await Bam.Net.Logging.Http.Data.Dao.QueryStringData.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.Logging.Http.Data.QueryStringData>(batch));
            }, Database);
        }
		
        public async Task BatchAllQueryStringDatas(int batchSize, Action<IEnumerable<Bam.Net.Logging.Http.Data.QueryStringData>> batchProcessor)
        {
            await Bam.Net.Logging.Http.Data.Dao.QueryStringData.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.Logging.Http.Data.QueryStringData>(batch));
            }, Database);
        }﻿		
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		/// <param name="where"></param>
		public Bam.Net.Logging.Http.Data.RequestData GetOneRequestDataWhere(WhereDelegate<RequestDataColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.Logging.Http.Data.RequestData>();
			return (Bam.Net.Logging.Http.Data.RequestData)Bam.Net.Logging.Http.Data.Dao.RequestData.GetOneWhere(where, Database)?.CopyAs(wrapperType, this);
		}

		/// <summary>
		/// Execute a query that should return only one result.  If no result is found null is returned.  If more
		/// than one result is returned a MultipleEntriesFoundException is thrown.  This method is most commonly used to retrieve a
		/// single RequestData instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a RequestDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between RequestDataColumns and other values
		/// </param>
		public Bam.Net.Logging.Http.Data.RequestData OneRequestDataWhere(WhereDelegate<RequestDataColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.Logging.Http.Data.RequestData>();
            return (Bam.Net.Logging.Http.Data.RequestData)Bam.Net.Logging.Http.Data.Dao.RequestData.OneWhere(where, Database)?.CopyAs(wrapperType, this);
        }

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a Bam.Net.Logging.Http.Data.RequestDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between Bam.Net.Logging.Http.Data.RequestDataColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.Logging.Http.Data.RequestData> RequestDatasWhere(WhereDelegate<RequestDataColumns> where, OrderBy<RequestDataColumns> orderBy = null)
        {
            return Wrap<Bam.Net.Logging.Http.Data.RequestData>(Bam.Net.Logging.Http.Data.Dao.RequestData.Where(where, orderBy, Database));
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
		/// <param name="where">A WhereDelegate that recieves a RequestDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between RequestDataColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.Logging.Http.Data.RequestData> TopRequestDatasWhere(int count, WhereDelegate<RequestDataColumns> where)
        {
            return Wrap<Bam.Net.Logging.Http.Data.RequestData>(Bam.Net.Logging.Http.Data.Dao.RequestData.Top(count, where, Database));
        }

		/// <summary>
		/// Return the count of RequestDatas
		/// </summary>
		public long CountRequestDatas()
        {
            return Bam.Net.Logging.Http.Data.Dao.RequestData.Count(Database);
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a RequestDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between RequestDataColumns and other values
		/// </param>
        public long CountRequestDatasWhere(WhereDelegate<RequestDataColumns> where)
        {
            return Bam.Net.Logging.Http.Data.Dao.RequestData.Count(where, Database);
        }
        
        public async Task BatchQueryRequestDatas(int batchSize, WhereDelegate<RequestDataColumns> where, Action<IEnumerable<Bam.Net.Logging.Http.Data.RequestData>> batchProcessor)
        {
            await Bam.Net.Logging.Http.Data.Dao.RequestData.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.Logging.Http.Data.RequestData>(batch));
            }, Database);
        }
		
        public async Task BatchAllRequestDatas(int batchSize, Action<IEnumerable<Bam.Net.Logging.Http.Data.RequestData>> batchProcessor)
        {
            await Bam.Net.Logging.Http.Data.Dao.RequestData.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.Logging.Http.Data.RequestData>(batch));
            }, Database);
        }﻿		
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		/// <param name="where"></param>
		public Bam.Net.Logging.Http.Data.UriData GetOneUriDataWhere(WhereDelegate<UriDataColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.Logging.Http.Data.UriData>();
			return (Bam.Net.Logging.Http.Data.UriData)Bam.Net.Logging.Http.Data.Dao.UriData.GetOneWhere(where, Database)?.CopyAs(wrapperType, this);
		}

		/// <summary>
		/// Execute a query that should return only one result.  If no result is found null is returned.  If more
		/// than one result is returned a MultipleEntriesFoundException is thrown.  This method is most commonly used to retrieve a
		/// single UriData instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a UriDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between UriDataColumns and other values
		/// </param>
		public Bam.Net.Logging.Http.Data.UriData OneUriDataWhere(WhereDelegate<UriDataColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.Logging.Http.Data.UriData>();
            return (Bam.Net.Logging.Http.Data.UriData)Bam.Net.Logging.Http.Data.Dao.UriData.OneWhere(where, Database)?.CopyAs(wrapperType, this);
        }

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a Bam.Net.Logging.Http.Data.UriDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between Bam.Net.Logging.Http.Data.UriDataColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.Logging.Http.Data.UriData> UriDatasWhere(WhereDelegate<UriDataColumns> where, OrderBy<UriDataColumns> orderBy = null)
        {
            return Wrap<Bam.Net.Logging.Http.Data.UriData>(Bam.Net.Logging.Http.Data.Dao.UriData.Where(where, orderBy, Database));
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
		/// <param name="where">A WhereDelegate that recieves a UriDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between UriDataColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.Logging.Http.Data.UriData> TopUriDatasWhere(int count, WhereDelegate<UriDataColumns> where)
        {
            return Wrap<Bam.Net.Logging.Http.Data.UriData>(Bam.Net.Logging.Http.Data.Dao.UriData.Top(count, where, Database));
        }

		/// <summary>
		/// Return the count of UriDatas
		/// </summary>
		public long CountUriDatas()
        {
            return Bam.Net.Logging.Http.Data.Dao.UriData.Count(Database);
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a UriDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between UriDataColumns and other values
		/// </param>
        public long CountUriDatasWhere(WhereDelegate<UriDataColumns> where)
        {
            return Bam.Net.Logging.Http.Data.Dao.UriData.Count(where, Database);
        }
        
        public async Task BatchQueryUriDatas(int batchSize, WhereDelegate<UriDataColumns> where, Action<IEnumerable<Bam.Net.Logging.Http.Data.UriData>> batchProcessor)
        {
            await Bam.Net.Logging.Http.Data.Dao.UriData.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.Logging.Http.Data.UriData>(batch));
            }, Database);
        }
		
        public async Task BatchAllUriDatas(int batchSize, Action<IEnumerable<Bam.Net.Logging.Http.Data.UriData>> batchProcessor)
        {
            await Bam.Net.Logging.Http.Data.Dao.UriData.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.Logging.Http.Data.UriData>(batch));
            }, Database);
        }
	}
}																								
