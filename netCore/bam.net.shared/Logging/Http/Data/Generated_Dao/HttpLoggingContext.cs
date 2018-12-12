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

namespace Bam.Net.Logging.Http.Data.Dao
{
	// schema = HttpLogging 
    public static class HttpLoggingContext
    {
		public static string ConnectionName
		{
			get
			{
				return "HttpLogging";
			}
		}

		public static Database Db
		{
			get
			{
				return Bam.Net.Data.Db.For(ConnectionName);
			}
		}


	public class CookieDataQueryContext
	{
			public CookieDataCollection Where(WhereDelegate<CookieDataColumns> where, Database db = null)
			{
				return Bam.Net.Logging.Http.Data.Dao.CookieData.Where(where, db);
			}
		   
			public CookieDataCollection Where(WhereDelegate<CookieDataColumns> where, OrderBy<CookieDataColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Logging.Http.Data.Dao.CookieData.Where(where, orderBy, db);
			}

			public CookieData OneWhere(WhereDelegate<CookieDataColumns> where, Database db = null)
			{
				return Bam.Net.Logging.Http.Data.Dao.CookieData.OneWhere(where, db);
			}

			public static CookieData GetOneWhere(WhereDelegate<CookieDataColumns> where, Database db = null)
			{
				return Bam.Net.Logging.Http.Data.Dao.CookieData.GetOneWhere(where, db);
			}
		
			public CookieData FirstOneWhere(WhereDelegate<CookieDataColumns> where, Database db = null)
			{
				return Bam.Net.Logging.Http.Data.Dao.CookieData.FirstOneWhere(where, db);
			}

			public CookieDataCollection Top(int count, WhereDelegate<CookieDataColumns> where, Database db = null)
			{
				return Bam.Net.Logging.Http.Data.Dao.CookieData.Top(count, where, db);
			}

			public CookieDataCollection Top(int count, WhereDelegate<CookieDataColumns> where, OrderBy<CookieDataColumns> orderBy, Database db = null)
			{
				return Bam.Net.Logging.Http.Data.Dao.CookieData.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<CookieDataColumns> where, Database db = null)
			{
				return Bam.Net.Logging.Http.Data.Dao.CookieData.Count(where, db);
			}
	}

	static CookieDataQueryContext _cookieDatas;
	static object _cookieDatasLock = new object();
	public static CookieDataQueryContext CookieDatas
	{
		get
		{
			return _cookieDatasLock.DoubleCheckLock<CookieDataQueryContext>(ref _cookieDatas, () => new CookieDataQueryContext());
		}
	}
	public class HeaderDataQueryContext
	{
			public HeaderDataCollection Where(WhereDelegate<HeaderDataColumns> where, Database db = null)
			{
				return Bam.Net.Logging.Http.Data.Dao.HeaderData.Where(where, db);
			}
		   
			public HeaderDataCollection Where(WhereDelegate<HeaderDataColumns> where, OrderBy<HeaderDataColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Logging.Http.Data.Dao.HeaderData.Where(where, orderBy, db);
			}

			public HeaderData OneWhere(WhereDelegate<HeaderDataColumns> where, Database db = null)
			{
				return Bam.Net.Logging.Http.Data.Dao.HeaderData.OneWhere(where, db);
			}

			public static HeaderData GetOneWhere(WhereDelegate<HeaderDataColumns> where, Database db = null)
			{
				return Bam.Net.Logging.Http.Data.Dao.HeaderData.GetOneWhere(where, db);
			}
		
			public HeaderData FirstOneWhere(WhereDelegate<HeaderDataColumns> where, Database db = null)
			{
				return Bam.Net.Logging.Http.Data.Dao.HeaderData.FirstOneWhere(where, db);
			}

			public HeaderDataCollection Top(int count, WhereDelegate<HeaderDataColumns> where, Database db = null)
			{
				return Bam.Net.Logging.Http.Data.Dao.HeaderData.Top(count, where, db);
			}

			public HeaderDataCollection Top(int count, WhereDelegate<HeaderDataColumns> where, OrderBy<HeaderDataColumns> orderBy, Database db = null)
			{
				return Bam.Net.Logging.Http.Data.Dao.HeaderData.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<HeaderDataColumns> where, Database db = null)
			{
				return Bam.Net.Logging.Http.Data.Dao.HeaderData.Count(where, db);
			}
	}

	static HeaderDataQueryContext _headerDatas;
	static object _headerDatasLock = new object();
	public static HeaderDataQueryContext HeaderDatas
	{
		get
		{
			return _headerDatasLock.DoubleCheckLock<HeaderDataQueryContext>(ref _headerDatas, () => new HeaderDataQueryContext());
		}
	}
	public class QueryStringDataQueryContext
	{
			public QueryStringDataCollection Where(WhereDelegate<QueryStringDataColumns> where, Database db = null)
			{
				return Bam.Net.Logging.Http.Data.Dao.QueryStringData.Where(where, db);
			}
		   
			public QueryStringDataCollection Where(WhereDelegate<QueryStringDataColumns> where, OrderBy<QueryStringDataColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Logging.Http.Data.Dao.QueryStringData.Where(where, orderBy, db);
			}

			public QueryStringData OneWhere(WhereDelegate<QueryStringDataColumns> where, Database db = null)
			{
				return Bam.Net.Logging.Http.Data.Dao.QueryStringData.OneWhere(where, db);
			}

			public static QueryStringData GetOneWhere(WhereDelegate<QueryStringDataColumns> where, Database db = null)
			{
				return Bam.Net.Logging.Http.Data.Dao.QueryStringData.GetOneWhere(where, db);
			}
		
			public QueryStringData FirstOneWhere(WhereDelegate<QueryStringDataColumns> where, Database db = null)
			{
				return Bam.Net.Logging.Http.Data.Dao.QueryStringData.FirstOneWhere(where, db);
			}

			public QueryStringDataCollection Top(int count, WhereDelegate<QueryStringDataColumns> where, Database db = null)
			{
				return Bam.Net.Logging.Http.Data.Dao.QueryStringData.Top(count, where, db);
			}

			public QueryStringDataCollection Top(int count, WhereDelegate<QueryStringDataColumns> where, OrderBy<QueryStringDataColumns> orderBy, Database db = null)
			{
				return Bam.Net.Logging.Http.Data.Dao.QueryStringData.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<QueryStringDataColumns> where, Database db = null)
			{
				return Bam.Net.Logging.Http.Data.Dao.QueryStringData.Count(where, db);
			}
	}

	static QueryStringDataQueryContext _queryStringDatas;
	static object _queryStringDatasLock = new object();
	public static QueryStringDataQueryContext QueryStringDatas
	{
		get
		{
			return _queryStringDatasLock.DoubleCheckLock<QueryStringDataQueryContext>(ref _queryStringDatas, () => new QueryStringDataQueryContext());
		}
	}
	public class RequestDataQueryContext
	{
			public RequestDataCollection Where(WhereDelegate<RequestDataColumns> where, Database db = null)
			{
				return Bam.Net.Logging.Http.Data.Dao.RequestData.Where(where, db);
			}
		   
			public RequestDataCollection Where(WhereDelegate<RequestDataColumns> where, OrderBy<RequestDataColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Logging.Http.Data.Dao.RequestData.Where(where, orderBy, db);
			}

			public RequestData OneWhere(WhereDelegate<RequestDataColumns> where, Database db = null)
			{
				return Bam.Net.Logging.Http.Data.Dao.RequestData.OneWhere(where, db);
			}

			public static RequestData GetOneWhere(WhereDelegate<RequestDataColumns> where, Database db = null)
			{
				return Bam.Net.Logging.Http.Data.Dao.RequestData.GetOneWhere(where, db);
			}
		
			public RequestData FirstOneWhere(WhereDelegate<RequestDataColumns> where, Database db = null)
			{
				return Bam.Net.Logging.Http.Data.Dao.RequestData.FirstOneWhere(where, db);
			}

			public RequestDataCollection Top(int count, WhereDelegate<RequestDataColumns> where, Database db = null)
			{
				return Bam.Net.Logging.Http.Data.Dao.RequestData.Top(count, where, db);
			}

			public RequestDataCollection Top(int count, WhereDelegate<RequestDataColumns> where, OrderBy<RequestDataColumns> orderBy, Database db = null)
			{
				return Bam.Net.Logging.Http.Data.Dao.RequestData.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<RequestDataColumns> where, Database db = null)
			{
				return Bam.Net.Logging.Http.Data.Dao.RequestData.Count(where, db);
			}
	}

	static RequestDataQueryContext _requestDatas;
	static object _requestDatasLock = new object();
	public static RequestDataQueryContext RequestDatas
	{
		get
		{
			return _requestDatasLock.DoubleCheckLock<RequestDataQueryContext>(ref _requestDatas, () => new RequestDataQueryContext());
		}
	}
	public class UriDataQueryContext
	{
			public UriDataCollection Where(WhereDelegate<UriDataColumns> where, Database db = null)
			{
				return Bam.Net.Logging.Http.Data.Dao.UriData.Where(where, db);
			}
		   
			public UriDataCollection Where(WhereDelegate<UriDataColumns> where, OrderBy<UriDataColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Logging.Http.Data.Dao.UriData.Where(where, orderBy, db);
			}

			public UriData OneWhere(WhereDelegate<UriDataColumns> where, Database db = null)
			{
				return Bam.Net.Logging.Http.Data.Dao.UriData.OneWhere(where, db);
			}

			public static UriData GetOneWhere(WhereDelegate<UriDataColumns> where, Database db = null)
			{
				return Bam.Net.Logging.Http.Data.Dao.UriData.GetOneWhere(where, db);
			}
		
			public UriData FirstOneWhere(WhereDelegate<UriDataColumns> where, Database db = null)
			{
				return Bam.Net.Logging.Http.Data.Dao.UriData.FirstOneWhere(where, db);
			}

			public UriDataCollection Top(int count, WhereDelegate<UriDataColumns> where, Database db = null)
			{
				return Bam.Net.Logging.Http.Data.Dao.UriData.Top(count, where, db);
			}

			public UriDataCollection Top(int count, WhereDelegate<UriDataColumns> where, OrderBy<UriDataColumns> orderBy, Database db = null)
			{
				return Bam.Net.Logging.Http.Data.Dao.UriData.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<UriDataColumns> where, Database db = null)
			{
				return Bam.Net.Logging.Http.Data.Dao.UriData.Count(where, db);
			}
	}

	static UriDataQueryContext _uriDatas;
	static object _uriDatasLock = new object();
	public static UriDataQueryContext UriDatas
	{
		get
		{
			return _uriDatasLock.DoubleCheckLock<UriDataQueryContext>(ref _uriDatas, () => new UriDataQueryContext());
		}
	}    }
}																								
