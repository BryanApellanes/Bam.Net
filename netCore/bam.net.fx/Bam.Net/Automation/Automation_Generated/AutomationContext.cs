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


	public class JobDataQueryContext
	{
			public JobDataCollection Where(WhereDelegate<JobDataColumns> where, Database db = null)
			{
				return Bam.Net.Automation.Data.JobData.Where(where, db);
			}
		   
			public JobDataCollection Where(WhereDelegate<JobDataColumns> where, OrderBy<JobDataColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Automation.Data.JobData.Where(where, orderBy, db);
			}

			public JobData OneWhere(WhereDelegate<JobDataColumns> where, Database db = null)
			{
				return Bam.Net.Automation.Data.JobData.OneWhere(where, db);
			}

			public static JobData GetOneWhere(WhereDelegate<JobDataColumns> where, Database db = null)
			{
				return Bam.Net.Automation.Data.JobData.GetOneWhere(where, db);
			}
		
			public JobData FirstOneWhere(WhereDelegate<JobDataColumns> where, Database db = null)
			{
				return Bam.Net.Automation.Data.JobData.FirstOneWhere(where, db);
			}

			public JobDataCollection Top(int count, WhereDelegate<JobDataColumns> where, Database db = null)
			{
				return Bam.Net.Automation.Data.JobData.Top(count, where, db);
			}

			public JobDataCollection Top(int count, WhereDelegate<JobDataColumns> where, OrderBy<JobDataColumns> orderBy, Database db = null)
			{
				return Bam.Net.Automation.Data.JobData.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<JobDataColumns> where, Database db = null)
			{
				return Bam.Net.Automation.Data.JobData.Count(where, db);
			}
	}

	static JobDataQueryContext _jobDatas;
	static object _jobDatasLock = new object();
	public static JobDataQueryContext JobDatas
	{
		get
		{
			return _jobDatasLock.DoubleCheckLock<JobDataQueryContext>(ref _jobDatas, () => new JobDataQueryContext());
		}
	}
	public class DeferredJobDataQueryContext
	{
			public DeferredJobDataCollection Where(WhereDelegate<DeferredJobDataColumns> where, Database db = null)
			{
				return Bam.Net.Automation.Data.DeferredJobData.Where(where, db);
			}
		   
			public DeferredJobDataCollection Where(WhereDelegate<DeferredJobDataColumns> where, OrderBy<DeferredJobDataColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Automation.Data.DeferredJobData.Where(where, orderBy, db);
			}

			public DeferredJobData OneWhere(WhereDelegate<DeferredJobDataColumns> where, Database db = null)
			{
				return Bam.Net.Automation.Data.DeferredJobData.OneWhere(where, db);
			}

			public static DeferredJobData GetOneWhere(WhereDelegate<DeferredJobDataColumns> where, Database db = null)
			{
				return Bam.Net.Automation.Data.DeferredJobData.GetOneWhere(where, db);
			}
		
			public DeferredJobData FirstOneWhere(WhereDelegate<DeferredJobDataColumns> where, Database db = null)
			{
				return Bam.Net.Automation.Data.DeferredJobData.FirstOneWhere(where, db);
			}

			public DeferredJobDataCollection Top(int count, WhereDelegate<DeferredJobDataColumns> where, Database db = null)
			{
				return Bam.Net.Automation.Data.DeferredJobData.Top(count, where, db);
			}

			public DeferredJobDataCollection Top(int count, WhereDelegate<DeferredJobDataColumns> where, OrderBy<DeferredJobDataColumns> orderBy, Database db = null)
			{
				return Bam.Net.Automation.Data.DeferredJobData.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<DeferredJobDataColumns> where, Database db = null)
			{
				return Bam.Net.Automation.Data.DeferredJobData.Count(where, db);
			}
	}

	static DeferredJobDataQueryContext _deferredJobDatas;
	static object _deferredJobDatasLock = new object();
	public static DeferredJobDataQueryContext DeferredJobDatas
	{
		get
		{
			return _deferredJobDatasLock.DoubleCheckLock<DeferredJobDataQueryContext>(ref _deferredJobDatas, () => new DeferredJobDataQueryContext());
		}
	}
	public class JobRunDataQueryContext
	{
			public JobRunDataCollection Where(WhereDelegate<JobRunDataColumns> where, Database db = null)
			{
				return Bam.Net.Automation.Data.JobRunData.Where(where, db);
			}
		   
			public JobRunDataCollection Where(WhereDelegate<JobRunDataColumns> where, OrderBy<JobRunDataColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Automation.Data.JobRunData.Where(where, orderBy, db);
			}

			public JobRunData OneWhere(WhereDelegate<JobRunDataColumns> where, Database db = null)
			{
				return Bam.Net.Automation.Data.JobRunData.OneWhere(where, db);
			}

			public static JobRunData GetOneWhere(WhereDelegate<JobRunDataColumns> where, Database db = null)
			{
				return Bam.Net.Automation.Data.JobRunData.GetOneWhere(where, db);
			}
		
			public JobRunData FirstOneWhere(WhereDelegate<JobRunDataColumns> where, Database db = null)
			{
				return Bam.Net.Automation.Data.JobRunData.FirstOneWhere(where, db);
			}

			public JobRunDataCollection Top(int count, WhereDelegate<JobRunDataColumns> where, Database db = null)
			{
				return Bam.Net.Automation.Data.JobRunData.Top(count, where, db);
			}

			public JobRunDataCollection Top(int count, WhereDelegate<JobRunDataColumns> where, OrderBy<JobRunDataColumns> orderBy, Database db = null)
			{
				return Bam.Net.Automation.Data.JobRunData.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<JobRunDataColumns> where, Database db = null)
			{
				return Bam.Net.Automation.Data.JobRunData.Count(where, db);
			}
	}

	static JobRunDataQueryContext _jobRunDatas;
	static object _jobRunDatasLock = new object();
	public static JobRunDataQueryContext JobRunDatas
	{
		get
		{
			return _jobRunDatasLock.DoubleCheckLock<JobRunDataQueryContext>(ref _jobRunDatas, () => new JobRunDataQueryContext());
		}
	}    }
}																								
