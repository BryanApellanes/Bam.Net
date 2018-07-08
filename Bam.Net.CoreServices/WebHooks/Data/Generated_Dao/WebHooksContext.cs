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

namespace Bam.Net.CoreServices.WebHooks.Data.Dao
{
	// schema = WebHooks 
    public static class WebHooksContext
    {
		public static string ConnectionName
		{
			get
			{
				return "WebHooks";
			}
		}

		public static Database Db
		{
			get
			{
				return Bam.Net.Data.Db.For(ConnectionName);
			}
		}


	public class WebHookCallQueryContext
	{
			public WebHookCallCollection Where(WhereDelegate<WebHookCallColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.WebHooks.Data.Dao.WebHookCall.Where(where, db);
			}
		   
			public WebHookCallCollection Where(WhereDelegate<WebHookCallColumns> where, OrderBy<WebHookCallColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.CoreServices.WebHooks.Data.Dao.WebHookCall.Where(where, orderBy, db);
			}

			public WebHookCall OneWhere(WhereDelegate<WebHookCallColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.WebHooks.Data.Dao.WebHookCall.OneWhere(where, db);
			}

			public static WebHookCall GetOneWhere(WhereDelegate<WebHookCallColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.WebHooks.Data.Dao.WebHookCall.GetOneWhere(where, db);
			}
		
			public WebHookCall FirstOneWhere(WhereDelegate<WebHookCallColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.WebHooks.Data.Dao.WebHookCall.FirstOneWhere(where, db);
			}

			public WebHookCallCollection Top(int count, WhereDelegate<WebHookCallColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.WebHooks.Data.Dao.WebHookCall.Top(count, where, db);
			}

			public WebHookCallCollection Top(int count, WhereDelegate<WebHookCallColumns> where, OrderBy<WebHookCallColumns> orderBy, Database db = null)
			{
				return Bam.Net.CoreServices.WebHooks.Data.Dao.WebHookCall.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<WebHookCallColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.WebHooks.Data.Dao.WebHookCall.Count(where, db);
			}
	}

	static WebHookCallQueryContext _webHookCalls;
	static object _webHookCallsLock = new object();
	public static WebHookCallQueryContext WebHookCalls
	{
		get
		{
			return _webHookCallsLock.DoubleCheckLock<WebHookCallQueryContext>(ref _webHookCalls, () => new WebHookCallQueryContext());
		}
	}
	public class WebHookDescriptorQueryContext
	{
			public WebHookDescriptorCollection Where(WhereDelegate<WebHookDescriptorColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.WebHooks.Data.Dao.WebHookDescriptor.Where(where, db);
			}
		   
			public WebHookDescriptorCollection Where(WhereDelegate<WebHookDescriptorColumns> where, OrderBy<WebHookDescriptorColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.CoreServices.WebHooks.Data.Dao.WebHookDescriptor.Where(where, orderBy, db);
			}

			public WebHookDescriptor OneWhere(WhereDelegate<WebHookDescriptorColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.WebHooks.Data.Dao.WebHookDescriptor.OneWhere(where, db);
			}

			public static WebHookDescriptor GetOneWhere(WhereDelegate<WebHookDescriptorColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.WebHooks.Data.Dao.WebHookDescriptor.GetOneWhere(where, db);
			}
		
			public WebHookDescriptor FirstOneWhere(WhereDelegate<WebHookDescriptorColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.WebHooks.Data.Dao.WebHookDescriptor.FirstOneWhere(where, db);
			}

			public WebHookDescriptorCollection Top(int count, WhereDelegate<WebHookDescriptorColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.WebHooks.Data.Dao.WebHookDescriptor.Top(count, where, db);
			}

			public WebHookDescriptorCollection Top(int count, WhereDelegate<WebHookDescriptorColumns> where, OrderBy<WebHookDescriptorColumns> orderBy, Database db = null)
			{
				return Bam.Net.CoreServices.WebHooks.Data.Dao.WebHookDescriptor.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<WebHookDescriptorColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.WebHooks.Data.Dao.WebHookDescriptor.Count(where, db);
			}
	}

	static WebHookDescriptorQueryContext _webHookDescriptors;
	static object _webHookDescriptorsLock = new object();
	public static WebHookDescriptorQueryContext WebHookDescriptors
	{
		get
		{
			return _webHookDescriptorsLock.DoubleCheckLock<WebHookDescriptorQueryContext>(ref _webHookDescriptors, () => new WebHookDescriptorQueryContext());
		}
	}
	public class WebHookSubscriberQueryContext
	{
			public WebHookSubscriberCollection Where(WhereDelegate<WebHookSubscriberColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.WebHooks.Data.Dao.WebHookSubscriber.Where(where, db);
			}
		   
			public WebHookSubscriberCollection Where(WhereDelegate<WebHookSubscriberColumns> where, OrderBy<WebHookSubscriberColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.CoreServices.WebHooks.Data.Dao.WebHookSubscriber.Where(where, orderBy, db);
			}

			public WebHookSubscriber OneWhere(WhereDelegate<WebHookSubscriberColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.WebHooks.Data.Dao.WebHookSubscriber.OneWhere(where, db);
			}

			public static WebHookSubscriber GetOneWhere(WhereDelegate<WebHookSubscriberColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.WebHooks.Data.Dao.WebHookSubscriber.GetOneWhere(where, db);
			}
		
			public WebHookSubscriber FirstOneWhere(WhereDelegate<WebHookSubscriberColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.WebHooks.Data.Dao.WebHookSubscriber.FirstOneWhere(where, db);
			}

			public WebHookSubscriberCollection Top(int count, WhereDelegate<WebHookSubscriberColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.WebHooks.Data.Dao.WebHookSubscriber.Top(count, where, db);
			}

			public WebHookSubscriberCollection Top(int count, WhereDelegate<WebHookSubscriberColumns> where, OrderBy<WebHookSubscriberColumns> orderBy, Database db = null)
			{
				return Bam.Net.CoreServices.WebHooks.Data.Dao.WebHookSubscriber.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<WebHookSubscriberColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.WebHooks.Data.Dao.WebHookSubscriber.Count(where, db);
			}
	}

	static WebHookSubscriberQueryContext _webHookSubscribers;
	static object _webHookSubscribersLock = new object();
	public static WebHookSubscriberQueryContext WebHookSubscribers
	{
		get
		{
			return _webHookSubscribersLock.DoubleCheckLock<WebHookSubscriberQueryContext>(ref _webHookSubscribers, () => new WebHookSubscriberQueryContext());
		}
	}
	public class WebHookDescriptorWebHookSubscriberQueryContext
	{
			public WebHookDescriptorWebHookSubscriberCollection Where(WhereDelegate<WebHookDescriptorWebHookSubscriberColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.WebHooks.Data.Dao.WebHookDescriptorWebHookSubscriber.Where(where, db);
			}
		   
			public WebHookDescriptorWebHookSubscriberCollection Where(WhereDelegate<WebHookDescriptorWebHookSubscriberColumns> where, OrderBy<WebHookDescriptorWebHookSubscriberColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.CoreServices.WebHooks.Data.Dao.WebHookDescriptorWebHookSubscriber.Where(where, orderBy, db);
			}

			public WebHookDescriptorWebHookSubscriber OneWhere(WhereDelegate<WebHookDescriptorWebHookSubscriberColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.WebHooks.Data.Dao.WebHookDescriptorWebHookSubscriber.OneWhere(where, db);
			}

			public static WebHookDescriptorWebHookSubscriber GetOneWhere(WhereDelegate<WebHookDescriptorWebHookSubscriberColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.WebHooks.Data.Dao.WebHookDescriptorWebHookSubscriber.GetOneWhere(where, db);
			}
		
			public WebHookDescriptorWebHookSubscriber FirstOneWhere(WhereDelegate<WebHookDescriptorWebHookSubscriberColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.WebHooks.Data.Dao.WebHookDescriptorWebHookSubscriber.FirstOneWhere(where, db);
			}

			public WebHookDescriptorWebHookSubscriberCollection Top(int count, WhereDelegate<WebHookDescriptorWebHookSubscriberColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.WebHooks.Data.Dao.WebHookDescriptorWebHookSubscriber.Top(count, where, db);
			}

			public WebHookDescriptorWebHookSubscriberCollection Top(int count, WhereDelegate<WebHookDescriptorWebHookSubscriberColumns> where, OrderBy<WebHookDescriptorWebHookSubscriberColumns> orderBy, Database db = null)
			{
				return Bam.Net.CoreServices.WebHooks.Data.Dao.WebHookDescriptorWebHookSubscriber.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<WebHookDescriptorWebHookSubscriberColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.WebHooks.Data.Dao.WebHookDescriptorWebHookSubscriber.Count(where, db);
			}
	}

	static WebHookDescriptorWebHookSubscriberQueryContext _webHookDescriptorWebHookSubscribers;
	static object _webHookDescriptorWebHookSubscribersLock = new object();
	public static WebHookDescriptorWebHookSubscriberQueryContext WebHookDescriptorWebHookSubscribers
	{
		get
		{
			return _webHookDescriptorWebHookSubscribersLock.DoubleCheckLock<WebHookDescriptorWebHookSubscriberQueryContext>(ref _webHookDescriptorWebHookSubscribers, () => new WebHookDescriptorWebHookSubscriberQueryContext());
		}
	}    }
}																								
