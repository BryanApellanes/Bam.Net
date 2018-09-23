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

namespace Bam.Net.Analytics
{
	// schema = Analytics 
    public static class AnalyticsContext
    {
		public static string ConnectionName
		{
			get
			{
				return "Analytics";
			}
		}

		public static Database Db
		{
			get
			{
				return Bam.Net.Data.Db.For(ConnectionName);
			}
		}


	public class CategoryQueryContext
	{
			public CategoryCollection Where(WhereDelegate<CategoryColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.Category.Where(where, db);
			}
		   
			public CategoryCollection Where(WhereDelegate<CategoryColumns> where, OrderBy<CategoryColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Analytics.Category.Where(where, orderBy, db);
			}

			public Category OneWhere(WhereDelegate<CategoryColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.Category.OneWhere(where, db);
			}

			public static Category GetOneWhere(WhereDelegate<CategoryColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.Category.GetOneWhere(where, db);
			}
		
			public Category FirstOneWhere(WhereDelegate<CategoryColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.Category.FirstOneWhere(where, db);
			}

			public CategoryCollection Top(int count, WhereDelegate<CategoryColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.Category.Top(count, where, db);
			}

			public CategoryCollection Top(int count, WhereDelegate<CategoryColumns> where, OrderBy<CategoryColumns> orderBy, Database db = null)
			{
				return Bam.Net.Analytics.Category.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<CategoryColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.Category.Count(where, db);
			}
	}

	static CategoryQueryContext _categories;
	static object _categoriesLock = new object();
	public static CategoryQueryContext Categories
	{
		get
		{
			return _categoriesLock.DoubleCheckLock<CategoryQueryContext>(ref _categories, () => new CategoryQueryContext());
		}
	}
	public class FeatureQueryContext
	{
			public FeatureCollection Where(WhereDelegate<FeatureColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.Feature.Where(where, db);
			}
		   
			public FeatureCollection Where(WhereDelegate<FeatureColumns> where, OrderBy<FeatureColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Analytics.Feature.Where(where, orderBy, db);
			}

			public Feature OneWhere(WhereDelegate<FeatureColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.Feature.OneWhere(where, db);
			}

			public static Feature GetOneWhere(WhereDelegate<FeatureColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.Feature.GetOneWhere(where, db);
			}
		
			public Feature FirstOneWhere(WhereDelegate<FeatureColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.Feature.FirstOneWhere(where, db);
			}

			public FeatureCollection Top(int count, WhereDelegate<FeatureColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.Feature.Top(count, where, db);
			}

			public FeatureCollection Top(int count, WhereDelegate<FeatureColumns> where, OrderBy<FeatureColumns> orderBy, Database db = null)
			{
				return Bam.Net.Analytics.Feature.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<FeatureColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.Feature.Count(where, db);
			}
	}

	static FeatureQueryContext _features;
	static object _featuresLock = new object();
	public static FeatureQueryContext Features
	{
		get
		{
			return _featuresLock.DoubleCheckLock<FeatureQueryContext>(ref _features, () => new FeatureQueryContext());
		}
	}
	public class CrawlerQueryContext
	{
			public CrawlerCollection Where(WhereDelegate<CrawlerColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.Crawler.Where(where, db);
			}
		   
			public CrawlerCollection Where(WhereDelegate<CrawlerColumns> where, OrderBy<CrawlerColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Analytics.Crawler.Where(where, orderBy, db);
			}

			public Crawler OneWhere(WhereDelegate<CrawlerColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.Crawler.OneWhere(where, db);
			}

			public static Crawler GetOneWhere(WhereDelegate<CrawlerColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.Crawler.GetOneWhere(where, db);
			}
		
			public Crawler FirstOneWhere(WhereDelegate<CrawlerColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.Crawler.FirstOneWhere(where, db);
			}

			public CrawlerCollection Top(int count, WhereDelegate<CrawlerColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.Crawler.Top(count, where, db);
			}

			public CrawlerCollection Top(int count, WhereDelegate<CrawlerColumns> where, OrderBy<CrawlerColumns> orderBy, Database db = null)
			{
				return Bam.Net.Analytics.Crawler.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<CrawlerColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.Crawler.Count(where, db);
			}
	}

	static CrawlerQueryContext _crawlers;
	static object _crawlersLock = new object();
	public static CrawlerQueryContext Crawlers
	{
		get
		{
			return _crawlersLock.DoubleCheckLock<CrawlerQueryContext>(ref _crawlers, () => new CrawlerQueryContext());
		}
	}
	public class ProtocolQueryContext
	{
			public ProtocolCollection Where(WhereDelegate<ProtocolColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.Protocol.Where(where, db);
			}
		   
			public ProtocolCollection Where(WhereDelegate<ProtocolColumns> where, OrderBy<ProtocolColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Analytics.Protocol.Where(where, orderBy, db);
			}

			public Protocol OneWhere(WhereDelegate<ProtocolColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.Protocol.OneWhere(where, db);
			}

			public static Protocol GetOneWhere(WhereDelegate<ProtocolColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.Protocol.GetOneWhere(where, db);
			}
		
			public Protocol FirstOneWhere(WhereDelegate<ProtocolColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.Protocol.FirstOneWhere(where, db);
			}

			public ProtocolCollection Top(int count, WhereDelegate<ProtocolColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.Protocol.Top(count, where, db);
			}

			public ProtocolCollection Top(int count, WhereDelegate<ProtocolColumns> where, OrderBy<ProtocolColumns> orderBy, Database db = null)
			{
				return Bam.Net.Analytics.Protocol.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<ProtocolColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.Protocol.Count(where, db);
			}
	}

	static ProtocolQueryContext _protocols;
	static object _protocolsLock = new object();
	public static ProtocolQueryContext Protocols
	{
		get
		{
			return _protocolsLock.DoubleCheckLock<ProtocolQueryContext>(ref _protocols, () => new ProtocolQueryContext());
		}
	}
	public class DomainQueryContext
	{
			public DomainCollection Where(WhereDelegate<DomainColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.Domain.Where(where, db);
			}
		   
			public DomainCollection Where(WhereDelegate<DomainColumns> where, OrderBy<DomainColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Analytics.Domain.Where(where, orderBy, db);
			}

			public Domain OneWhere(WhereDelegate<DomainColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.Domain.OneWhere(where, db);
			}

			public static Domain GetOneWhere(WhereDelegate<DomainColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.Domain.GetOneWhere(where, db);
			}
		
			public Domain FirstOneWhere(WhereDelegate<DomainColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.Domain.FirstOneWhere(where, db);
			}

			public DomainCollection Top(int count, WhereDelegate<DomainColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.Domain.Top(count, where, db);
			}

			public DomainCollection Top(int count, WhereDelegate<DomainColumns> where, OrderBy<DomainColumns> orderBy, Database db = null)
			{
				return Bam.Net.Analytics.Domain.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<DomainColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.Domain.Count(where, db);
			}
	}

	static DomainQueryContext _domains;
	static object _domainsLock = new object();
	public static DomainQueryContext Domains
	{
		get
		{
			return _domainsLock.DoubleCheckLock<DomainQueryContext>(ref _domains, () => new DomainQueryContext());
		}
	}
	public class PortQueryContext
	{
			public PortCollection Where(WhereDelegate<PortColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.Port.Where(where, db);
			}
		   
			public PortCollection Where(WhereDelegate<PortColumns> where, OrderBy<PortColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Analytics.Port.Where(where, orderBy, db);
			}

			public Port OneWhere(WhereDelegate<PortColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.Port.OneWhere(where, db);
			}

			public static Port GetOneWhere(WhereDelegate<PortColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.Port.GetOneWhere(where, db);
			}
		
			public Port FirstOneWhere(WhereDelegate<PortColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.Port.FirstOneWhere(where, db);
			}

			public PortCollection Top(int count, WhereDelegate<PortColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.Port.Top(count, where, db);
			}

			public PortCollection Top(int count, WhereDelegate<PortColumns> where, OrderBy<PortColumns> orderBy, Database db = null)
			{
				return Bam.Net.Analytics.Port.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<PortColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.Port.Count(where, db);
			}
	}

	static PortQueryContext _ports;
	static object _portsLock = new object();
	public static PortQueryContext Ports
	{
		get
		{
			return _portsLock.DoubleCheckLock<PortQueryContext>(ref _ports, () => new PortQueryContext());
		}
	}
	public class PathQueryContext
	{
			public PathCollection Where(WhereDelegate<PathColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.Path.Where(where, db);
			}
		   
			public PathCollection Where(WhereDelegate<PathColumns> where, OrderBy<PathColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Analytics.Path.Where(where, orderBy, db);
			}

			public Path OneWhere(WhereDelegate<PathColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.Path.OneWhere(where, db);
			}

			public static Path GetOneWhere(WhereDelegate<PathColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.Path.GetOneWhere(where, db);
			}
		
			public Path FirstOneWhere(WhereDelegate<PathColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.Path.FirstOneWhere(where, db);
			}

			public PathCollection Top(int count, WhereDelegate<PathColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.Path.Top(count, where, db);
			}

			public PathCollection Top(int count, WhereDelegate<PathColumns> where, OrderBy<PathColumns> orderBy, Database db = null)
			{
				return Bam.Net.Analytics.Path.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<PathColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.Path.Count(where, db);
			}
	}

	static PathQueryContext _paths;
	static object _pathsLock = new object();
	public static PathQueryContext Paths
	{
		get
		{
			return _pathsLock.DoubleCheckLock<PathQueryContext>(ref _paths, () => new PathQueryContext());
		}
	}
	public class QueryStringQueryContext
	{
			public QueryStringCollection Where(WhereDelegate<QueryStringColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.QueryString.Where(where, db);
			}
		   
			public QueryStringCollection Where(WhereDelegate<QueryStringColumns> where, OrderBy<QueryStringColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Analytics.QueryString.Where(where, orderBy, db);
			}

			public QueryString OneWhere(WhereDelegate<QueryStringColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.QueryString.OneWhere(where, db);
			}

			public static QueryString GetOneWhere(WhereDelegate<QueryStringColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.QueryString.GetOneWhere(where, db);
			}
		
			public QueryString FirstOneWhere(WhereDelegate<QueryStringColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.QueryString.FirstOneWhere(where, db);
			}

			public QueryStringCollection Top(int count, WhereDelegate<QueryStringColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.QueryString.Top(count, where, db);
			}

			public QueryStringCollection Top(int count, WhereDelegate<QueryStringColumns> where, OrderBy<QueryStringColumns> orderBy, Database db = null)
			{
				return Bam.Net.Analytics.QueryString.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<QueryStringColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.QueryString.Count(where, db);
			}
	}

	static QueryStringQueryContext _queryStrings;
	static object _queryStringsLock = new object();
	public static QueryStringQueryContext QueryStrings
	{
		get
		{
			return _queryStringsLock.DoubleCheckLock<QueryStringQueryContext>(ref _queryStrings, () => new QueryStringQueryContext());
		}
	}
	public class FragmentQueryContext
	{
			public FragmentCollection Where(WhereDelegate<FragmentColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.Fragment.Where(where, db);
			}
		   
			public FragmentCollection Where(WhereDelegate<FragmentColumns> where, OrderBy<FragmentColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Analytics.Fragment.Where(where, orderBy, db);
			}

			public Fragment OneWhere(WhereDelegate<FragmentColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.Fragment.OneWhere(where, db);
			}

			public static Fragment GetOneWhere(WhereDelegate<FragmentColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.Fragment.GetOneWhere(where, db);
			}
		
			public Fragment FirstOneWhere(WhereDelegate<FragmentColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.Fragment.FirstOneWhere(where, db);
			}

			public FragmentCollection Top(int count, WhereDelegate<FragmentColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.Fragment.Top(count, where, db);
			}

			public FragmentCollection Top(int count, WhereDelegate<FragmentColumns> where, OrderBy<FragmentColumns> orderBy, Database db = null)
			{
				return Bam.Net.Analytics.Fragment.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<FragmentColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.Fragment.Count(where, db);
			}
	}

	static FragmentQueryContext _fragments;
	static object _fragmentsLock = new object();
	public static FragmentQueryContext Fragments
	{
		get
		{
			return _fragmentsLock.DoubleCheckLock<FragmentQueryContext>(ref _fragments, () => new FragmentQueryContext());
		}
	}
	public class UrlQueryContext
	{
			public UrlCollection Where(WhereDelegate<UrlColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.Url.Where(where, db);
			}
		   
			public UrlCollection Where(WhereDelegate<UrlColumns> where, OrderBy<UrlColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Analytics.Url.Where(where, orderBy, db);
			}

			public Url OneWhere(WhereDelegate<UrlColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.Url.OneWhere(where, db);
			}

			public static Url GetOneWhere(WhereDelegate<UrlColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.Url.GetOneWhere(where, db);
			}
		
			public Url FirstOneWhere(WhereDelegate<UrlColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.Url.FirstOneWhere(where, db);
			}

			public UrlCollection Top(int count, WhereDelegate<UrlColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.Url.Top(count, where, db);
			}

			public UrlCollection Top(int count, WhereDelegate<UrlColumns> where, OrderBy<UrlColumns> orderBy, Database db = null)
			{
				return Bam.Net.Analytics.Url.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<UrlColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.Url.Count(where, db);
			}
	}

	static UrlQueryContext _urls;
	static object _urlsLock = new object();
	public static UrlQueryContext Urls
	{
		get
		{
			return _urlsLock.DoubleCheckLock<UrlQueryContext>(ref _urls, () => new UrlQueryContext());
		}
	}
	public class TagQueryContext
	{
			public TagCollection Where(WhereDelegate<TagColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.Tag.Where(where, db);
			}
		   
			public TagCollection Where(WhereDelegate<TagColumns> where, OrderBy<TagColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Analytics.Tag.Where(where, orderBy, db);
			}

			public Tag OneWhere(WhereDelegate<TagColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.Tag.OneWhere(where, db);
			}

			public static Tag GetOneWhere(WhereDelegate<TagColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.Tag.GetOneWhere(where, db);
			}
		
			public Tag FirstOneWhere(WhereDelegate<TagColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.Tag.FirstOneWhere(where, db);
			}

			public TagCollection Top(int count, WhereDelegate<TagColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.Tag.Top(count, where, db);
			}

			public TagCollection Top(int count, WhereDelegate<TagColumns> where, OrderBy<TagColumns> orderBy, Database db = null)
			{
				return Bam.Net.Analytics.Tag.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<TagColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.Tag.Count(where, db);
			}
	}

	static TagQueryContext _tags;
	static object _tagsLock = new object();
	public static TagQueryContext Tags
	{
		get
		{
			return _tagsLock.DoubleCheckLock<TagQueryContext>(ref _tags, () => new TagQueryContext());
		}
	}
	public class ImageQueryContext
	{
			public ImageCollection Where(WhereDelegate<ImageColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.Image.Where(where, db);
			}
		   
			public ImageCollection Where(WhereDelegate<ImageColumns> where, OrderBy<ImageColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Analytics.Image.Where(where, orderBy, db);
			}

			public Image OneWhere(WhereDelegate<ImageColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.Image.OneWhere(where, db);
			}

			public static Image GetOneWhere(WhereDelegate<ImageColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.Image.GetOneWhere(where, db);
			}
		
			public Image FirstOneWhere(WhereDelegate<ImageColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.Image.FirstOneWhere(where, db);
			}

			public ImageCollection Top(int count, WhereDelegate<ImageColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.Image.Top(count, where, db);
			}

			public ImageCollection Top(int count, WhereDelegate<ImageColumns> where, OrderBy<ImageColumns> orderBy, Database db = null)
			{
				return Bam.Net.Analytics.Image.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<ImageColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.Image.Count(where, db);
			}
	}

	static ImageQueryContext _images;
	static object _imagesLock = new object();
	public static ImageQueryContext Images
	{
		get
		{
			return _imagesLock.DoubleCheckLock<ImageQueryContext>(ref _images, () => new ImageQueryContext());
		}
	}
	public class TimerQueryContext
	{
			public TimerCollection Where(WhereDelegate<TimerColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.Timer.Where(where, db);
			}
		   
			public TimerCollection Where(WhereDelegate<TimerColumns> where, OrderBy<TimerColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Analytics.Timer.Where(where, orderBy, db);
			}

			public Timer OneWhere(WhereDelegate<TimerColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.Timer.OneWhere(where, db);
			}

			public static Timer GetOneWhere(WhereDelegate<TimerColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.Timer.GetOneWhere(where, db);
			}
		
			public Timer FirstOneWhere(WhereDelegate<TimerColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.Timer.FirstOneWhere(where, db);
			}

			public TimerCollection Top(int count, WhereDelegate<TimerColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.Timer.Top(count, where, db);
			}

			public TimerCollection Top(int count, WhereDelegate<TimerColumns> where, OrderBy<TimerColumns> orderBy, Database db = null)
			{
				return Bam.Net.Analytics.Timer.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<TimerColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.Timer.Count(where, db);
			}
	}

	static TimerQueryContext _timers;
	static object _timersLock = new object();
	public static TimerQueryContext Timers
	{
		get
		{
			return _timersLock.DoubleCheckLock<TimerQueryContext>(ref _timers, () => new TimerQueryContext());
		}
	}
	public class MethodTimerQueryContext
	{
			public MethodTimerCollection Where(WhereDelegate<MethodTimerColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.MethodTimer.Where(where, db);
			}
		   
			public MethodTimerCollection Where(WhereDelegate<MethodTimerColumns> where, OrderBy<MethodTimerColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Analytics.MethodTimer.Where(where, orderBy, db);
			}

			public MethodTimer OneWhere(WhereDelegate<MethodTimerColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.MethodTimer.OneWhere(where, db);
			}

			public static MethodTimer GetOneWhere(WhereDelegate<MethodTimerColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.MethodTimer.GetOneWhere(where, db);
			}
		
			public MethodTimer FirstOneWhere(WhereDelegate<MethodTimerColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.MethodTimer.FirstOneWhere(where, db);
			}

			public MethodTimerCollection Top(int count, WhereDelegate<MethodTimerColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.MethodTimer.Top(count, where, db);
			}

			public MethodTimerCollection Top(int count, WhereDelegate<MethodTimerColumns> where, OrderBy<MethodTimerColumns> orderBy, Database db = null)
			{
				return Bam.Net.Analytics.MethodTimer.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<MethodTimerColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.MethodTimer.Count(where, db);
			}
	}

	static MethodTimerQueryContext _methodTimers;
	static object _methodTimersLock = new object();
	public static MethodTimerQueryContext MethodTimers
	{
		get
		{
			return _methodTimersLock.DoubleCheckLock<MethodTimerQueryContext>(ref _methodTimers, () => new MethodTimerQueryContext());
		}
	}
	public class LoadTimerQueryContext
	{
			public LoadTimerCollection Where(WhereDelegate<LoadTimerColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.LoadTimer.Where(where, db);
			}
		   
			public LoadTimerCollection Where(WhereDelegate<LoadTimerColumns> where, OrderBy<LoadTimerColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Analytics.LoadTimer.Where(where, orderBy, db);
			}

			public LoadTimer OneWhere(WhereDelegate<LoadTimerColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.LoadTimer.OneWhere(where, db);
			}

			public static LoadTimer GetOneWhere(WhereDelegate<LoadTimerColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.LoadTimer.GetOneWhere(where, db);
			}
		
			public LoadTimer FirstOneWhere(WhereDelegate<LoadTimerColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.LoadTimer.FirstOneWhere(where, db);
			}

			public LoadTimerCollection Top(int count, WhereDelegate<LoadTimerColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.LoadTimer.Top(count, where, db);
			}

			public LoadTimerCollection Top(int count, WhereDelegate<LoadTimerColumns> where, OrderBy<LoadTimerColumns> orderBy, Database db = null)
			{
				return Bam.Net.Analytics.LoadTimer.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<LoadTimerColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.LoadTimer.Count(where, db);
			}
	}

	static LoadTimerQueryContext _loadTimers;
	static object _loadTimersLock = new object();
	public static LoadTimerQueryContext LoadTimers
	{
		get
		{
			return _loadTimersLock.DoubleCheckLock<LoadTimerQueryContext>(ref _loadTimers, () => new LoadTimerQueryContext());
		}
	}
	public class CustomTimerQueryContext
	{
			public CustomTimerCollection Where(WhereDelegate<CustomTimerColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.CustomTimer.Where(where, db);
			}
		   
			public CustomTimerCollection Where(WhereDelegate<CustomTimerColumns> where, OrderBy<CustomTimerColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Analytics.CustomTimer.Where(where, orderBy, db);
			}

			public CustomTimer OneWhere(WhereDelegate<CustomTimerColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.CustomTimer.OneWhere(where, db);
			}

			public static CustomTimer GetOneWhere(WhereDelegate<CustomTimerColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.CustomTimer.GetOneWhere(where, db);
			}
		
			public CustomTimer FirstOneWhere(WhereDelegate<CustomTimerColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.CustomTimer.FirstOneWhere(where, db);
			}

			public CustomTimerCollection Top(int count, WhereDelegate<CustomTimerColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.CustomTimer.Top(count, where, db);
			}

			public CustomTimerCollection Top(int count, WhereDelegate<CustomTimerColumns> where, OrderBy<CustomTimerColumns> orderBy, Database db = null)
			{
				return Bam.Net.Analytics.CustomTimer.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<CustomTimerColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.CustomTimer.Count(where, db);
			}
	}

	static CustomTimerQueryContext _customTimers;
	static object _customTimersLock = new object();
	public static CustomTimerQueryContext CustomTimers
	{
		get
		{
			return _customTimersLock.DoubleCheckLock<CustomTimerQueryContext>(ref _customTimers, () => new CustomTimerQueryContext());
		}
	}
	public class CounterQueryContext
	{
			public CounterCollection Where(WhereDelegate<CounterColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.Counter.Where(where, db);
			}
		   
			public CounterCollection Where(WhereDelegate<CounterColumns> where, OrderBy<CounterColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Analytics.Counter.Where(where, orderBy, db);
			}

			public Counter OneWhere(WhereDelegate<CounterColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.Counter.OneWhere(where, db);
			}

			public static Counter GetOneWhere(WhereDelegate<CounterColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.Counter.GetOneWhere(where, db);
			}
		
			public Counter FirstOneWhere(WhereDelegate<CounterColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.Counter.FirstOneWhere(where, db);
			}

			public CounterCollection Top(int count, WhereDelegate<CounterColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.Counter.Top(count, where, db);
			}

			public CounterCollection Top(int count, WhereDelegate<CounterColumns> where, OrderBy<CounterColumns> orderBy, Database db = null)
			{
				return Bam.Net.Analytics.Counter.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<CounterColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.Counter.Count(where, db);
			}
	}

	static CounterQueryContext _counters;
	static object _countersLock = new object();
	public static CounterQueryContext Counters
	{
		get
		{
			return _countersLock.DoubleCheckLock<CounterQueryContext>(ref _counters, () => new CounterQueryContext());
		}
	}
	public class MethodCounterQueryContext
	{
			public MethodCounterCollection Where(WhereDelegate<MethodCounterColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.MethodCounter.Where(where, db);
			}
		   
			public MethodCounterCollection Where(WhereDelegate<MethodCounterColumns> where, OrderBy<MethodCounterColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Analytics.MethodCounter.Where(where, orderBy, db);
			}

			public MethodCounter OneWhere(WhereDelegate<MethodCounterColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.MethodCounter.OneWhere(where, db);
			}

			public static MethodCounter GetOneWhere(WhereDelegate<MethodCounterColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.MethodCounter.GetOneWhere(where, db);
			}
		
			public MethodCounter FirstOneWhere(WhereDelegate<MethodCounterColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.MethodCounter.FirstOneWhere(where, db);
			}

			public MethodCounterCollection Top(int count, WhereDelegate<MethodCounterColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.MethodCounter.Top(count, where, db);
			}

			public MethodCounterCollection Top(int count, WhereDelegate<MethodCounterColumns> where, OrderBy<MethodCounterColumns> orderBy, Database db = null)
			{
				return Bam.Net.Analytics.MethodCounter.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<MethodCounterColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.MethodCounter.Count(where, db);
			}
	}

	static MethodCounterQueryContext _methodCounters;
	static object _methodCountersLock = new object();
	public static MethodCounterQueryContext MethodCounters
	{
		get
		{
			return _methodCountersLock.DoubleCheckLock<MethodCounterQueryContext>(ref _methodCounters, () => new MethodCounterQueryContext());
		}
	}
	public class LoadCounterQueryContext
	{
			public LoadCounterCollection Where(WhereDelegate<LoadCounterColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.LoadCounter.Where(where, db);
			}
		   
			public LoadCounterCollection Where(WhereDelegate<LoadCounterColumns> where, OrderBy<LoadCounterColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Analytics.LoadCounter.Where(where, orderBy, db);
			}

			public LoadCounter OneWhere(WhereDelegate<LoadCounterColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.LoadCounter.OneWhere(where, db);
			}

			public static LoadCounter GetOneWhere(WhereDelegate<LoadCounterColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.LoadCounter.GetOneWhere(where, db);
			}
		
			public LoadCounter FirstOneWhere(WhereDelegate<LoadCounterColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.LoadCounter.FirstOneWhere(where, db);
			}

			public LoadCounterCollection Top(int count, WhereDelegate<LoadCounterColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.LoadCounter.Top(count, where, db);
			}

			public LoadCounterCollection Top(int count, WhereDelegate<LoadCounterColumns> where, OrderBy<LoadCounterColumns> orderBy, Database db = null)
			{
				return Bam.Net.Analytics.LoadCounter.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<LoadCounterColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.LoadCounter.Count(where, db);
			}
	}

	static LoadCounterQueryContext _loadCounters;
	static object _loadCountersLock = new object();
	public static LoadCounterQueryContext LoadCounters
	{
		get
		{
			return _loadCountersLock.DoubleCheckLock<LoadCounterQueryContext>(ref _loadCounters, () => new LoadCounterQueryContext());
		}
	}
	public class UserIdentifierQueryContext
	{
			public UserIdentifierCollection Where(WhereDelegate<UserIdentifierColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.UserIdentifier.Where(where, db);
			}
		   
			public UserIdentifierCollection Where(WhereDelegate<UserIdentifierColumns> where, OrderBy<UserIdentifierColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Analytics.UserIdentifier.Where(where, orderBy, db);
			}

			public UserIdentifier OneWhere(WhereDelegate<UserIdentifierColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.UserIdentifier.OneWhere(where, db);
			}

			public static UserIdentifier GetOneWhere(WhereDelegate<UserIdentifierColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.UserIdentifier.GetOneWhere(where, db);
			}
		
			public UserIdentifier FirstOneWhere(WhereDelegate<UserIdentifierColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.UserIdentifier.FirstOneWhere(where, db);
			}

			public UserIdentifierCollection Top(int count, WhereDelegate<UserIdentifierColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.UserIdentifier.Top(count, where, db);
			}

			public UserIdentifierCollection Top(int count, WhereDelegate<UserIdentifierColumns> where, OrderBy<UserIdentifierColumns> orderBy, Database db = null)
			{
				return Bam.Net.Analytics.UserIdentifier.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<UserIdentifierColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.UserIdentifier.Count(where, db);
			}
	}

	static UserIdentifierQueryContext _userIdentifiers;
	static object _userIdentifiersLock = new object();
	public static UserIdentifierQueryContext UserIdentifiers
	{
		get
		{
			return _userIdentifiersLock.DoubleCheckLock<UserIdentifierQueryContext>(ref _userIdentifiers, () => new UserIdentifierQueryContext());
		}
	}
	public class ClickCounterQueryContext
	{
			public ClickCounterCollection Where(WhereDelegate<ClickCounterColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.ClickCounter.Where(where, db);
			}
		   
			public ClickCounterCollection Where(WhereDelegate<ClickCounterColumns> where, OrderBy<ClickCounterColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Analytics.ClickCounter.Where(where, orderBy, db);
			}

			public ClickCounter OneWhere(WhereDelegate<ClickCounterColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.ClickCounter.OneWhere(where, db);
			}

			public static ClickCounter GetOneWhere(WhereDelegate<ClickCounterColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.ClickCounter.GetOneWhere(where, db);
			}
		
			public ClickCounter FirstOneWhere(WhereDelegate<ClickCounterColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.ClickCounter.FirstOneWhere(where, db);
			}

			public ClickCounterCollection Top(int count, WhereDelegate<ClickCounterColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.ClickCounter.Top(count, where, db);
			}

			public ClickCounterCollection Top(int count, WhereDelegate<ClickCounterColumns> where, OrderBy<ClickCounterColumns> orderBy, Database db = null)
			{
				return Bam.Net.Analytics.ClickCounter.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<ClickCounterColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.ClickCounter.Count(where, db);
			}
	}

	static ClickCounterQueryContext _clickCounters;
	static object _clickCountersLock = new object();
	public static ClickCounterQueryContext ClickCounters
	{
		get
		{
			return _clickCountersLock.DoubleCheckLock<ClickCounterQueryContext>(ref _clickCounters, () => new ClickCounterQueryContext());
		}
	}
	public class LoginCounterQueryContext
	{
			public LoginCounterCollection Where(WhereDelegate<LoginCounterColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.LoginCounter.Where(where, db);
			}
		   
			public LoginCounterCollection Where(WhereDelegate<LoginCounterColumns> where, OrderBy<LoginCounterColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Analytics.LoginCounter.Where(where, orderBy, db);
			}

			public LoginCounter OneWhere(WhereDelegate<LoginCounterColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.LoginCounter.OneWhere(where, db);
			}

			public static LoginCounter GetOneWhere(WhereDelegate<LoginCounterColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.LoginCounter.GetOneWhere(where, db);
			}
		
			public LoginCounter FirstOneWhere(WhereDelegate<LoginCounterColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.LoginCounter.FirstOneWhere(where, db);
			}

			public LoginCounterCollection Top(int count, WhereDelegate<LoginCounterColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.LoginCounter.Top(count, where, db);
			}

			public LoginCounterCollection Top(int count, WhereDelegate<LoginCounterColumns> where, OrderBy<LoginCounterColumns> orderBy, Database db = null)
			{
				return Bam.Net.Analytics.LoginCounter.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<LoginCounterColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.LoginCounter.Count(where, db);
			}
	}

	static LoginCounterQueryContext _loginCounters;
	static object _loginCountersLock = new object();
	public static LoginCounterQueryContext LoginCounters
	{
		get
		{
			return _loginCountersLock.DoubleCheckLock<LoginCounterQueryContext>(ref _loginCounters, () => new LoginCounterQueryContext());
		}
	}
	public class UrlTagQueryContext
	{
			public UrlTagCollection Where(WhereDelegate<UrlTagColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.UrlTag.Where(where, db);
			}
		   
			public UrlTagCollection Where(WhereDelegate<UrlTagColumns> where, OrderBy<UrlTagColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Analytics.UrlTag.Where(where, orderBy, db);
			}

			public UrlTag OneWhere(WhereDelegate<UrlTagColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.UrlTag.OneWhere(where, db);
			}

			public static UrlTag GetOneWhere(WhereDelegate<UrlTagColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.UrlTag.GetOneWhere(where, db);
			}
		
			public UrlTag FirstOneWhere(WhereDelegate<UrlTagColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.UrlTag.FirstOneWhere(where, db);
			}

			public UrlTagCollection Top(int count, WhereDelegate<UrlTagColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.UrlTag.Top(count, where, db);
			}

			public UrlTagCollection Top(int count, WhereDelegate<UrlTagColumns> where, OrderBy<UrlTagColumns> orderBy, Database db = null)
			{
				return Bam.Net.Analytics.UrlTag.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<UrlTagColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.UrlTag.Count(where, db);
			}
	}

	static UrlTagQueryContext _urlTags;
	static object _urlTagsLock = new object();
	public static UrlTagQueryContext UrlTags
	{
		get
		{
			return _urlTagsLock.DoubleCheckLock<UrlTagQueryContext>(ref _urlTags, () => new UrlTagQueryContext());
		}
	}
	public class ImageTagQueryContext
	{
			public ImageTagCollection Where(WhereDelegate<ImageTagColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.ImageTag.Where(where, db);
			}
		   
			public ImageTagCollection Where(WhereDelegate<ImageTagColumns> where, OrderBy<ImageTagColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Analytics.ImageTag.Where(where, orderBy, db);
			}

			public ImageTag OneWhere(WhereDelegate<ImageTagColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.ImageTag.OneWhere(where, db);
			}

			public static ImageTag GetOneWhere(WhereDelegate<ImageTagColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.ImageTag.GetOneWhere(where, db);
			}
		
			public ImageTag FirstOneWhere(WhereDelegate<ImageTagColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.ImageTag.FirstOneWhere(where, db);
			}

			public ImageTagCollection Top(int count, WhereDelegate<ImageTagColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.ImageTag.Top(count, where, db);
			}

			public ImageTagCollection Top(int count, WhereDelegate<ImageTagColumns> where, OrderBy<ImageTagColumns> orderBy, Database db = null)
			{
				return Bam.Net.Analytics.ImageTag.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<ImageTagColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.ImageTag.Count(where, db);
			}
	}

	static ImageTagQueryContext _imageTags;
	static object _imageTagsLock = new object();
	public static ImageTagQueryContext ImageTags
	{
		get
		{
			return _imageTagsLock.DoubleCheckLock<ImageTagQueryContext>(ref _imageTags, () => new ImageTagQueryContext());
		}
	}    }
}																								
