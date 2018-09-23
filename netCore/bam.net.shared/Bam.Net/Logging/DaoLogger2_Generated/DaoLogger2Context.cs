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

namespace Bam.Net.Logging.Data
{
	// schema = DaoLogger2 
    public static class DaoLogger2Context
    {
		public static string ConnectionName
		{
			get
			{
				return "DaoLogger2";
			}
		}

		public static Database Db
		{
			get
			{
				return Bam.Net.Data.Db.For(ConnectionName);
			}
		}


	public class SourceNameQueryContext
	{
			public SourceNameCollection Where(WhereDelegate<SourceNameColumns> where, Database db = null)
			{
				return Bam.Net.Logging.Data.SourceName.Where(where, db);
			}
		   
			public SourceNameCollection Where(WhereDelegate<SourceNameColumns> where, OrderBy<SourceNameColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Logging.Data.SourceName.Where(where, orderBy, db);
			}

			public SourceName OneWhere(WhereDelegate<SourceNameColumns> where, Database db = null)
			{
				return Bam.Net.Logging.Data.SourceName.OneWhere(where, db);
			}

			public static SourceName GetOneWhere(WhereDelegate<SourceNameColumns> where, Database db = null)
			{
				return Bam.Net.Logging.Data.SourceName.GetOneWhere(where, db);
			}
		
			public SourceName FirstOneWhere(WhereDelegate<SourceNameColumns> where, Database db = null)
			{
				return Bam.Net.Logging.Data.SourceName.FirstOneWhere(where, db);
			}

			public SourceNameCollection Top(int count, WhereDelegate<SourceNameColumns> where, Database db = null)
			{
				return Bam.Net.Logging.Data.SourceName.Top(count, where, db);
			}

			public SourceNameCollection Top(int count, WhereDelegate<SourceNameColumns> where, OrderBy<SourceNameColumns> orderBy, Database db = null)
			{
				return Bam.Net.Logging.Data.SourceName.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<SourceNameColumns> where, Database db = null)
			{
				return Bam.Net.Logging.Data.SourceName.Count(where, db);
			}
	}

	static SourceNameQueryContext _sourceNames;
	static object _sourceNamesLock = new object();
	public static SourceNameQueryContext SourceNames
	{
		get
		{
			return _sourceNamesLock.DoubleCheckLock<SourceNameQueryContext>(ref _sourceNames, () => new SourceNameQueryContext());
		}
	}
	public class UserNameQueryContext
	{
			public UserNameCollection Where(WhereDelegate<UserNameColumns> where, Database db = null)
			{
				return Bam.Net.Logging.Data.UserName.Where(where, db);
			}
		   
			public UserNameCollection Where(WhereDelegate<UserNameColumns> where, OrderBy<UserNameColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Logging.Data.UserName.Where(where, orderBy, db);
			}

			public UserName OneWhere(WhereDelegate<UserNameColumns> where, Database db = null)
			{
				return Bam.Net.Logging.Data.UserName.OneWhere(where, db);
			}

			public static UserName GetOneWhere(WhereDelegate<UserNameColumns> where, Database db = null)
			{
				return Bam.Net.Logging.Data.UserName.GetOneWhere(where, db);
			}
		
			public UserName FirstOneWhere(WhereDelegate<UserNameColumns> where, Database db = null)
			{
				return Bam.Net.Logging.Data.UserName.FirstOneWhere(where, db);
			}

			public UserNameCollection Top(int count, WhereDelegate<UserNameColumns> where, Database db = null)
			{
				return Bam.Net.Logging.Data.UserName.Top(count, where, db);
			}

			public UserNameCollection Top(int count, WhereDelegate<UserNameColumns> where, OrderBy<UserNameColumns> orderBy, Database db = null)
			{
				return Bam.Net.Logging.Data.UserName.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<UserNameColumns> where, Database db = null)
			{
				return Bam.Net.Logging.Data.UserName.Count(where, db);
			}
	}

	static UserNameQueryContext _userNames;
	static object _userNamesLock = new object();
	public static UserNameQueryContext UserNames
	{
		get
		{
			return _userNamesLock.DoubleCheckLock<UserNameQueryContext>(ref _userNames, () => new UserNameQueryContext());
		}
	}
	public class CategoryNameQueryContext
	{
			public CategoryNameCollection Where(WhereDelegate<CategoryNameColumns> where, Database db = null)
			{
				return Bam.Net.Logging.Data.CategoryName.Where(where, db);
			}
		   
			public CategoryNameCollection Where(WhereDelegate<CategoryNameColumns> where, OrderBy<CategoryNameColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Logging.Data.CategoryName.Where(where, orderBy, db);
			}

			public CategoryName OneWhere(WhereDelegate<CategoryNameColumns> where, Database db = null)
			{
				return Bam.Net.Logging.Data.CategoryName.OneWhere(where, db);
			}

			public static CategoryName GetOneWhere(WhereDelegate<CategoryNameColumns> where, Database db = null)
			{
				return Bam.Net.Logging.Data.CategoryName.GetOneWhere(where, db);
			}
		
			public CategoryName FirstOneWhere(WhereDelegate<CategoryNameColumns> where, Database db = null)
			{
				return Bam.Net.Logging.Data.CategoryName.FirstOneWhere(where, db);
			}

			public CategoryNameCollection Top(int count, WhereDelegate<CategoryNameColumns> where, Database db = null)
			{
				return Bam.Net.Logging.Data.CategoryName.Top(count, where, db);
			}

			public CategoryNameCollection Top(int count, WhereDelegate<CategoryNameColumns> where, OrderBy<CategoryNameColumns> orderBy, Database db = null)
			{
				return Bam.Net.Logging.Data.CategoryName.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<CategoryNameColumns> where, Database db = null)
			{
				return Bam.Net.Logging.Data.CategoryName.Count(where, db);
			}
	}

	static CategoryNameQueryContext _categoryNames;
	static object _categoryNamesLock = new object();
	public static CategoryNameQueryContext CategoryNames
	{
		get
		{
			return _categoryNamesLock.DoubleCheckLock<CategoryNameQueryContext>(ref _categoryNames, () => new CategoryNameQueryContext());
		}
	}
	public class ComputerNameQueryContext
	{
			public ComputerNameCollection Where(WhereDelegate<ComputerNameColumns> where, Database db = null)
			{
				return Bam.Net.Logging.Data.ComputerName.Where(where, db);
			}
		   
			public ComputerNameCollection Where(WhereDelegate<ComputerNameColumns> where, OrderBy<ComputerNameColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Logging.Data.ComputerName.Where(where, orderBy, db);
			}

			public ComputerName OneWhere(WhereDelegate<ComputerNameColumns> where, Database db = null)
			{
				return Bam.Net.Logging.Data.ComputerName.OneWhere(where, db);
			}

			public static ComputerName GetOneWhere(WhereDelegate<ComputerNameColumns> where, Database db = null)
			{
				return Bam.Net.Logging.Data.ComputerName.GetOneWhere(where, db);
			}
		
			public ComputerName FirstOneWhere(WhereDelegate<ComputerNameColumns> where, Database db = null)
			{
				return Bam.Net.Logging.Data.ComputerName.FirstOneWhere(where, db);
			}

			public ComputerNameCollection Top(int count, WhereDelegate<ComputerNameColumns> where, Database db = null)
			{
				return Bam.Net.Logging.Data.ComputerName.Top(count, where, db);
			}

			public ComputerNameCollection Top(int count, WhereDelegate<ComputerNameColumns> where, OrderBy<ComputerNameColumns> orderBy, Database db = null)
			{
				return Bam.Net.Logging.Data.ComputerName.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<ComputerNameColumns> where, Database db = null)
			{
				return Bam.Net.Logging.Data.ComputerName.Count(where, db);
			}
	}

	static ComputerNameQueryContext _computerNames;
	static object _computerNamesLock = new object();
	public static ComputerNameQueryContext ComputerNames
	{
		get
		{
			return _computerNamesLock.DoubleCheckLock<ComputerNameQueryContext>(ref _computerNames, () => new ComputerNameQueryContext());
		}
	}
	public class SignatureQueryContext
	{
			public SignatureCollection Where(WhereDelegate<SignatureColumns> where, Database db = null)
			{
				return Bam.Net.Logging.Data.Signature.Where(where, db);
			}
		   
			public SignatureCollection Where(WhereDelegate<SignatureColumns> where, OrderBy<SignatureColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Logging.Data.Signature.Where(where, orderBy, db);
			}

			public Signature OneWhere(WhereDelegate<SignatureColumns> where, Database db = null)
			{
				return Bam.Net.Logging.Data.Signature.OneWhere(where, db);
			}

			public static Signature GetOneWhere(WhereDelegate<SignatureColumns> where, Database db = null)
			{
				return Bam.Net.Logging.Data.Signature.GetOneWhere(where, db);
			}
		
			public Signature FirstOneWhere(WhereDelegate<SignatureColumns> where, Database db = null)
			{
				return Bam.Net.Logging.Data.Signature.FirstOneWhere(where, db);
			}

			public SignatureCollection Top(int count, WhereDelegate<SignatureColumns> where, Database db = null)
			{
				return Bam.Net.Logging.Data.Signature.Top(count, where, db);
			}

			public SignatureCollection Top(int count, WhereDelegate<SignatureColumns> where, OrderBy<SignatureColumns> orderBy, Database db = null)
			{
				return Bam.Net.Logging.Data.Signature.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<SignatureColumns> where, Database db = null)
			{
				return Bam.Net.Logging.Data.Signature.Count(where, db);
			}
	}

	static SignatureQueryContext _signatures;
	static object _signaturesLock = new object();
	public static SignatureQueryContext Signatures
	{
		get
		{
			return _signaturesLock.DoubleCheckLock<SignatureQueryContext>(ref _signatures, () => new SignatureQueryContext());
		}
	}
	public class ParamQueryContext
	{
			public ParamCollection Where(WhereDelegate<ParamColumns> where, Database db = null)
			{
				return Bam.Net.Logging.Data.Param.Where(where, db);
			}
		   
			public ParamCollection Where(WhereDelegate<ParamColumns> where, OrderBy<ParamColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Logging.Data.Param.Where(where, orderBy, db);
			}

			public Param OneWhere(WhereDelegate<ParamColumns> where, Database db = null)
			{
				return Bam.Net.Logging.Data.Param.OneWhere(where, db);
			}

			public static Param GetOneWhere(WhereDelegate<ParamColumns> where, Database db = null)
			{
				return Bam.Net.Logging.Data.Param.GetOneWhere(where, db);
			}
		
			public Param FirstOneWhere(WhereDelegate<ParamColumns> where, Database db = null)
			{
				return Bam.Net.Logging.Data.Param.FirstOneWhere(where, db);
			}

			public ParamCollection Top(int count, WhereDelegate<ParamColumns> where, Database db = null)
			{
				return Bam.Net.Logging.Data.Param.Top(count, where, db);
			}

			public ParamCollection Top(int count, WhereDelegate<ParamColumns> where, OrderBy<ParamColumns> orderBy, Database db = null)
			{
				return Bam.Net.Logging.Data.Param.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<ParamColumns> where, Database db = null)
			{
				return Bam.Net.Logging.Data.Param.Count(where, db);
			}
	}

	static ParamQueryContext _params;
	static object _paramsLock = new object();
	public static ParamQueryContext Params
	{
		get
		{
			return _paramsLock.DoubleCheckLock<ParamQueryContext>(ref _params, () => new ParamQueryContext());
		}
	}
	public class EventQueryContext
	{
			public EventCollection Where(WhereDelegate<EventColumns> where, Database db = null)
			{
				return Bam.Net.Logging.Data.Event.Where(where, db);
			}
		   
			public EventCollection Where(WhereDelegate<EventColumns> where, OrderBy<EventColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Logging.Data.Event.Where(where, orderBy, db);
			}

			public Event OneWhere(WhereDelegate<EventColumns> where, Database db = null)
			{
				return Bam.Net.Logging.Data.Event.OneWhere(where, db);
			}

			public static Event GetOneWhere(WhereDelegate<EventColumns> where, Database db = null)
			{
				return Bam.Net.Logging.Data.Event.GetOneWhere(where, db);
			}
		
			public Event FirstOneWhere(WhereDelegate<EventColumns> where, Database db = null)
			{
				return Bam.Net.Logging.Data.Event.FirstOneWhere(where, db);
			}

			public EventCollection Top(int count, WhereDelegate<EventColumns> where, Database db = null)
			{
				return Bam.Net.Logging.Data.Event.Top(count, where, db);
			}

			public EventCollection Top(int count, WhereDelegate<EventColumns> where, OrderBy<EventColumns> orderBy, Database db = null)
			{
				return Bam.Net.Logging.Data.Event.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<EventColumns> where, Database db = null)
			{
				return Bam.Net.Logging.Data.Event.Count(where, db);
			}
	}

	static EventQueryContext _events;
	static object _eventsLock = new object();
	public static EventQueryContext Events
	{
		get
		{
			return _eventsLock.DoubleCheckLock<EventQueryContext>(ref _events, () => new EventQueryContext());
		}
	}
	public class EventParamQueryContext
	{
			public EventParamCollection Where(WhereDelegate<EventParamColumns> where, Database db = null)
			{
				return Bam.Net.Logging.Data.EventParam.Where(where, db);
			}
		   
			public EventParamCollection Where(WhereDelegate<EventParamColumns> where, OrderBy<EventParamColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Logging.Data.EventParam.Where(where, orderBy, db);
			}

			public EventParam OneWhere(WhereDelegate<EventParamColumns> where, Database db = null)
			{
				return Bam.Net.Logging.Data.EventParam.OneWhere(where, db);
			}

			public static EventParam GetOneWhere(WhereDelegate<EventParamColumns> where, Database db = null)
			{
				return Bam.Net.Logging.Data.EventParam.GetOneWhere(where, db);
			}
		
			public EventParam FirstOneWhere(WhereDelegate<EventParamColumns> where, Database db = null)
			{
				return Bam.Net.Logging.Data.EventParam.FirstOneWhere(where, db);
			}

			public EventParamCollection Top(int count, WhereDelegate<EventParamColumns> where, Database db = null)
			{
				return Bam.Net.Logging.Data.EventParam.Top(count, where, db);
			}

			public EventParamCollection Top(int count, WhereDelegate<EventParamColumns> where, OrderBy<EventParamColumns> orderBy, Database db = null)
			{
				return Bam.Net.Logging.Data.EventParam.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<EventParamColumns> where, Database db = null)
			{
				return Bam.Net.Logging.Data.EventParam.Count(where, db);
			}
	}

	static EventParamQueryContext _eventParams;
	static object _eventParamsLock = new object();
	public static EventParamQueryContext EventParams
	{
		get
		{
			return _eventParamsLock.DoubleCheckLock<EventParamQueryContext>(ref _eventParams, () => new EventParamQueryContext());
		}
	}    }
}																								
