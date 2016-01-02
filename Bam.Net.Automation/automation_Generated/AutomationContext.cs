/*
	Copyright © Bryan Apellanes 2015  
*/
// model is SchemaDefinition
using System;
using System.Data;
using System.Data.Common;
using Bam.Net;
using Bam.Net.Data;
using Bam.Net.Data.Qi;

namespace Bam.Net.Automation.Data
{
	// schema = Automation 
    public static class AutomationContext
    {
		public static string ConnectionName
		{
			get
			{
				return "Automation";
			}
		}

		public static Database Db
		{
			get
			{
				return Bam.Net.Data.Db.For(ConnectionName);
			}
		}

﻿
	public class DeferredJobQueryContext
	{
			public DeferredJobCollection Where(WhereDelegate<DeferredJobColumns> where, Database db = null)
			{
				return Bam.Net.Automation.Data.DeferredJob.Where(where, db);
			}
		   
			public DeferredJobCollection Where(WhereDelegate<DeferredJobColumns> where, OrderBy<DeferredJobColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Automation.Data.DeferredJob.Where(where, orderBy, db);
			}

			public DeferredJob OneWhere(WhereDelegate<DeferredJobColumns> where, Database db = null)
			{
				return Bam.Net.Automation.Data.DeferredJob.OneWhere(where, db);
			}
		
			public DeferredJob FirstOneWhere(WhereDelegate<DeferredJobColumns> where, Database db = null)
			{
				return Bam.Net.Automation.Data.DeferredJob.FirstOneWhere(where, db);
			}

			public DeferredJobCollection Top(int count, WhereDelegate<DeferredJobColumns> where, Database db = null)
			{
				return Bam.Net.Automation.Data.DeferredJob.Top(count, where, db);
			}

			public DeferredJobCollection Top(int count, WhereDelegate<DeferredJobColumns> where, OrderBy<DeferredJobColumns> orderBy, Database db = null)
			{
				return Bam.Net.Automation.Data.DeferredJob.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<DeferredJobColumns> where, Database db = null)
			{
				return Bam.Net.Automation.Data.DeferredJob.Count(where, db);
			}
	}

	static DeferredJobQueryContext _deferredJobs;
	static object _deferredJobsLock = new object();
	public static DeferredJobQueryContext DeferredJobs
	{
		get
		{
			return _deferredJobsLock.DoubleCheckLock<DeferredJobQueryContext>(ref _deferredJobs, () => new DeferredJobQueryContext());
		}
	}﻿
	public class RunningJobQueryContext
	{
			public RunningJobCollection Where(WhereDelegate<RunningJobColumns> where, Database db = null)
			{
				return Bam.Net.Automation.Data.RunningJob.Where(where, db);
			}
		   
			public RunningJobCollection Where(WhereDelegate<RunningJobColumns> where, OrderBy<RunningJobColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Automation.Data.RunningJob.Where(where, orderBy, db);
			}

			public RunningJob OneWhere(WhereDelegate<RunningJobColumns> where, Database db = null)
			{
				return Bam.Net.Automation.Data.RunningJob.OneWhere(where, db);
			}
		
			public RunningJob FirstOneWhere(WhereDelegate<RunningJobColumns> where, Database db = null)
			{
				return Bam.Net.Automation.Data.RunningJob.FirstOneWhere(where, db);
			}

			public RunningJobCollection Top(int count, WhereDelegate<RunningJobColumns> where, Database db = null)
			{
				return Bam.Net.Automation.Data.RunningJob.Top(count, where, db);
			}

			public RunningJobCollection Top(int count, WhereDelegate<RunningJobColumns> where, OrderBy<RunningJobColumns> orderBy, Database db = null)
			{
				return Bam.Net.Automation.Data.RunningJob.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<RunningJobColumns> where, Database db = null)
			{
				return Bam.Net.Automation.Data.RunningJob.Count(where, db);
			}
	}

	static RunningJobQueryContext _runningJobs;
	static object _runningJobsLock = new object();
	public static RunningJobQueryContext RunningJobs
	{
		get
		{
			return _runningJobsLock.DoubleCheckLock<RunningJobQueryContext>(ref _runningJobs, () => new RunningJobQueryContext());
		}
	}    }
}																								
