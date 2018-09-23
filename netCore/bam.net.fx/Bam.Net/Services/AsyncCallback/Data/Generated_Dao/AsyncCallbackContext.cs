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

namespace Bam.Net.Services.AsyncCallback.Data.Dao
{
	// schema = AsyncCallback 
    public static class AsyncCallbackContext
    {
		public static string ConnectionName
		{
			get
			{
				return "AsyncCallback";
			}
		}

		public static Database Db
		{
			get
			{
				return Bam.Net.Data.Db.For(ConnectionName);
			}
		}


	public class AsyncExecutionDataQueryContext
	{
			public AsyncExecutionDataCollection Where(WhereDelegate<AsyncExecutionDataColumns> where, Database db = null)
			{
				return Bam.Net.Services.AsyncCallback.Data.Dao.AsyncExecutionData.Where(where, db);
			}
		   
			public AsyncExecutionDataCollection Where(WhereDelegate<AsyncExecutionDataColumns> where, OrderBy<AsyncExecutionDataColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Services.AsyncCallback.Data.Dao.AsyncExecutionData.Where(where, orderBy, db);
			}

			public AsyncExecutionData OneWhere(WhereDelegate<AsyncExecutionDataColumns> where, Database db = null)
			{
				return Bam.Net.Services.AsyncCallback.Data.Dao.AsyncExecutionData.OneWhere(where, db);
			}

			public static AsyncExecutionData GetOneWhere(WhereDelegate<AsyncExecutionDataColumns> where, Database db = null)
			{
				return Bam.Net.Services.AsyncCallback.Data.Dao.AsyncExecutionData.GetOneWhere(where, db);
			}
		
			public AsyncExecutionData FirstOneWhere(WhereDelegate<AsyncExecutionDataColumns> where, Database db = null)
			{
				return Bam.Net.Services.AsyncCallback.Data.Dao.AsyncExecutionData.FirstOneWhere(where, db);
			}

			public AsyncExecutionDataCollection Top(int count, WhereDelegate<AsyncExecutionDataColumns> where, Database db = null)
			{
				return Bam.Net.Services.AsyncCallback.Data.Dao.AsyncExecutionData.Top(count, where, db);
			}

			public AsyncExecutionDataCollection Top(int count, WhereDelegate<AsyncExecutionDataColumns> where, OrderBy<AsyncExecutionDataColumns> orderBy, Database db = null)
			{
				return Bam.Net.Services.AsyncCallback.Data.Dao.AsyncExecutionData.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<AsyncExecutionDataColumns> where, Database db = null)
			{
				return Bam.Net.Services.AsyncCallback.Data.Dao.AsyncExecutionData.Count(where, db);
			}
	}

	static AsyncExecutionDataQueryContext _asyncExecutionDatas;
	static object _asyncExecutionDatasLock = new object();
	public static AsyncExecutionDataQueryContext AsyncExecutionDatas
	{
		get
		{
			return _asyncExecutionDatasLock.DoubleCheckLock<AsyncExecutionDataQueryContext>(ref _asyncExecutionDatas, () => new AsyncExecutionDataQueryContext());
		}
	}
	public class AsyncExecutionRequestDataQueryContext
	{
			public AsyncExecutionRequestDataCollection Where(WhereDelegate<AsyncExecutionRequestDataColumns> where, Database db = null)
			{
				return Bam.Net.Services.AsyncCallback.Data.Dao.AsyncExecutionRequestData.Where(where, db);
			}
		   
			public AsyncExecutionRequestDataCollection Where(WhereDelegate<AsyncExecutionRequestDataColumns> where, OrderBy<AsyncExecutionRequestDataColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Services.AsyncCallback.Data.Dao.AsyncExecutionRequestData.Where(where, orderBy, db);
			}

			public AsyncExecutionRequestData OneWhere(WhereDelegate<AsyncExecutionRequestDataColumns> where, Database db = null)
			{
				return Bam.Net.Services.AsyncCallback.Data.Dao.AsyncExecutionRequestData.OneWhere(where, db);
			}

			public static AsyncExecutionRequestData GetOneWhere(WhereDelegate<AsyncExecutionRequestDataColumns> where, Database db = null)
			{
				return Bam.Net.Services.AsyncCallback.Data.Dao.AsyncExecutionRequestData.GetOneWhere(where, db);
			}
		
			public AsyncExecutionRequestData FirstOneWhere(WhereDelegate<AsyncExecutionRequestDataColumns> where, Database db = null)
			{
				return Bam.Net.Services.AsyncCallback.Data.Dao.AsyncExecutionRequestData.FirstOneWhere(where, db);
			}

			public AsyncExecutionRequestDataCollection Top(int count, WhereDelegate<AsyncExecutionRequestDataColumns> where, Database db = null)
			{
				return Bam.Net.Services.AsyncCallback.Data.Dao.AsyncExecutionRequestData.Top(count, where, db);
			}

			public AsyncExecutionRequestDataCollection Top(int count, WhereDelegate<AsyncExecutionRequestDataColumns> where, OrderBy<AsyncExecutionRequestDataColumns> orderBy, Database db = null)
			{
				return Bam.Net.Services.AsyncCallback.Data.Dao.AsyncExecutionRequestData.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<AsyncExecutionRequestDataColumns> where, Database db = null)
			{
				return Bam.Net.Services.AsyncCallback.Data.Dao.AsyncExecutionRequestData.Count(where, db);
			}
	}

	static AsyncExecutionRequestDataQueryContext _asyncExecutionRequestDatas;
	static object _asyncExecutionRequestDatasLock = new object();
	public static AsyncExecutionRequestDataQueryContext AsyncExecutionRequestDatas
	{
		get
		{
			return _asyncExecutionRequestDatasLock.DoubleCheckLock<AsyncExecutionRequestDataQueryContext>(ref _asyncExecutionRequestDatas, () => new AsyncExecutionRequestDataQueryContext());
		}
	}
	public class AsyncExecutionResponseDataQueryContext
	{
			public AsyncExecutionResponseDataCollection Where(WhereDelegate<AsyncExecutionResponseDataColumns> where, Database db = null)
			{
				return Bam.Net.Services.AsyncCallback.Data.Dao.AsyncExecutionResponseData.Where(where, db);
			}
		   
			public AsyncExecutionResponseDataCollection Where(WhereDelegate<AsyncExecutionResponseDataColumns> where, OrderBy<AsyncExecutionResponseDataColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Services.AsyncCallback.Data.Dao.AsyncExecutionResponseData.Where(where, orderBy, db);
			}

			public AsyncExecutionResponseData OneWhere(WhereDelegate<AsyncExecutionResponseDataColumns> where, Database db = null)
			{
				return Bam.Net.Services.AsyncCallback.Data.Dao.AsyncExecutionResponseData.OneWhere(where, db);
			}

			public static AsyncExecutionResponseData GetOneWhere(WhereDelegate<AsyncExecutionResponseDataColumns> where, Database db = null)
			{
				return Bam.Net.Services.AsyncCallback.Data.Dao.AsyncExecutionResponseData.GetOneWhere(where, db);
			}
		
			public AsyncExecutionResponseData FirstOneWhere(WhereDelegate<AsyncExecutionResponseDataColumns> where, Database db = null)
			{
				return Bam.Net.Services.AsyncCallback.Data.Dao.AsyncExecutionResponseData.FirstOneWhere(where, db);
			}

			public AsyncExecutionResponseDataCollection Top(int count, WhereDelegate<AsyncExecutionResponseDataColumns> where, Database db = null)
			{
				return Bam.Net.Services.AsyncCallback.Data.Dao.AsyncExecutionResponseData.Top(count, where, db);
			}

			public AsyncExecutionResponseDataCollection Top(int count, WhereDelegate<AsyncExecutionResponseDataColumns> where, OrderBy<AsyncExecutionResponseDataColumns> orderBy, Database db = null)
			{
				return Bam.Net.Services.AsyncCallback.Data.Dao.AsyncExecutionResponseData.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<AsyncExecutionResponseDataColumns> where, Database db = null)
			{
				return Bam.Net.Services.AsyncCallback.Data.Dao.AsyncExecutionResponseData.Count(where, db);
			}
	}

	static AsyncExecutionResponseDataQueryContext _asyncExecutionResponseDatas;
	static object _asyncExecutionResponseDatasLock = new object();
	public static AsyncExecutionResponseDataQueryContext AsyncExecutionResponseDatas
	{
		get
		{
			return _asyncExecutionResponseDatasLock.DoubleCheckLock<AsyncExecutionResponseDataQueryContext>(ref _asyncExecutionResponseDatas, () => new AsyncExecutionResponseDataQueryContext());
		}
	}    }
}																								
