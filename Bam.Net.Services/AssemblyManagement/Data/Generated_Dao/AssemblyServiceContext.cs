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

namespace Bam.Net.Services.AssemblyManagement.Data.Dao
{
	// schema = AssemblyService 
    public static class AssemblyServiceContext
    {
		public static string ConnectionName
		{
			get
			{
				return "AssemblyService";
			}
		}

		public static Database Db
		{
			get
			{
				return Bam.Net.Data.Db.For(ConnectionName);
			}
		}


	public class AssemblyReferenceDescriptorQueryContext
	{
			public AssemblyReferenceDescriptorCollection Where(WhereDelegate<AssemblyReferenceDescriptorColumns> where, Database db = null)
			{
				return Bam.Net.Services.AssemblyManagement.Data.Dao.AssemblyReferenceDescriptor.Where(where, db);
			}
		   
			public AssemblyReferenceDescriptorCollection Where(WhereDelegate<AssemblyReferenceDescriptorColumns> where, OrderBy<AssemblyReferenceDescriptorColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Services.AssemblyManagement.Data.Dao.AssemblyReferenceDescriptor.Where(where, orderBy, db);
			}

			public AssemblyReferenceDescriptor OneWhere(WhereDelegate<AssemblyReferenceDescriptorColumns> where, Database db = null)
			{
				return Bam.Net.Services.AssemblyManagement.Data.Dao.AssemblyReferenceDescriptor.OneWhere(where, db);
			}

			public static AssemblyReferenceDescriptor GetOneWhere(WhereDelegate<AssemblyReferenceDescriptorColumns> where, Database db = null)
			{
				return Bam.Net.Services.AssemblyManagement.Data.Dao.AssemblyReferenceDescriptor.GetOneWhere(where, db);
			}
		
			public AssemblyReferenceDescriptor FirstOneWhere(WhereDelegate<AssemblyReferenceDescriptorColumns> where, Database db = null)
			{
				return Bam.Net.Services.AssemblyManagement.Data.Dao.AssemblyReferenceDescriptor.FirstOneWhere(where, db);
			}

			public AssemblyReferenceDescriptorCollection Top(int count, WhereDelegate<AssemblyReferenceDescriptorColumns> where, Database db = null)
			{
				return Bam.Net.Services.AssemblyManagement.Data.Dao.AssemblyReferenceDescriptor.Top(count, where, db);
			}

			public AssemblyReferenceDescriptorCollection Top(int count, WhereDelegate<AssemblyReferenceDescriptorColumns> where, OrderBy<AssemblyReferenceDescriptorColumns> orderBy, Database db = null)
			{
				return Bam.Net.Services.AssemblyManagement.Data.Dao.AssemblyReferenceDescriptor.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<AssemblyReferenceDescriptorColumns> where, Database db = null)
			{
				return Bam.Net.Services.AssemblyManagement.Data.Dao.AssemblyReferenceDescriptor.Count(where, db);
			}
	}

	static AssemblyReferenceDescriptorQueryContext _assemblyReferenceDescriptors;
	static object _assemblyReferenceDescriptorsLock = new object();
	public static AssemblyReferenceDescriptorQueryContext AssemblyReferenceDescriptors
	{
		get
		{
			return _assemblyReferenceDescriptorsLock.DoubleCheckLock<AssemblyReferenceDescriptorQueryContext>(ref _assemblyReferenceDescriptors, () => new AssemblyReferenceDescriptorQueryContext());
		}
	}
	public class AssemblyDescriptorQueryContext
	{
			public AssemblyDescriptorCollection Where(WhereDelegate<AssemblyDescriptorColumns> where, Database db = null)
			{
				return Bam.Net.Services.AssemblyManagement.Data.Dao.AssemblyDescriptor.Where(where, db);
			}
		   
			public AssemblyDescriptorCollection Where(WhereDelegate<AssemblyDescriptorColumns> where, OrderBy<AssemblyDescriptorColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Services.AssemblyManagement.Data.Dao.AssemblyDescriptor.Where(where, orderBy, db);
			}

			public AssemblyDescriptor OneWhere(WhereDelegate<AssemblyDescriptorColumns> where, Database db = null)
			{
				return Bam.Net.Services.AssemblyManagement.Data.Dao.AssemblyDescriptor.OneWhere(where, db);
			}

			public static AssemblyDescriptor GetOneWhere(WhereDelegate<AssemblyDescriptorColumns> where, Database db = null)
			{
				return Bam.Net.Services.AssemblyManagement.Data.Dao.AssemblyDescriptor.GetOneWhere(where, db);
			}
		
			public AssemblyDescriptor FirstOneWhere(WhereDelegate<AssemblyDescriptorColumns> where, Database db = null)
			{
				return Bam.Net.Services.AssemblyManagement.Data.Dao.AssemblyDescriptor.FirstOneWhere(where, db);
			}

			public AssemblyDescriptorCollection Top(int count, WhereDelegate<AssemblyDescriptorColumns> where, Database db = null)
			{
				return Bam.Net.Services.AssemblyManagement.Data.Dao.AssemblyDescriptor.Top(count, where, db);
			}

			public AssemblyDescriptorCollection Top(int count, WhereDelegate<AssemblyDescriptorColumns> where, OrderBy<AssemblyDescriptorColumns> orderBy, Database db = null)
			{
				return Bam.Net.Services.AssemblyManagement.Data.Dao.AssemblyDescriptor.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<AssemblyDescriptorColumns> where, Database db = null)
			{
				return Bam.Net.Services.AssemblyManagement.Data.Dao.AssemblyDescriptor.Count(where, db);
			}
	}

	static AssemblyDescriptorQueryContext _assemblyDescriptors;
	static object _assemblyDescriptorsLock = new object();
	public static AssemblyDescriptorQueryContext AssemblyDescriptors
	{
		get
		{
			return _assemblyDescriptorsLock.DoubleCheckLock<AssemblyDescriptorQueryContext>(ref _assemblyDescriptors, () => new AssemblyDescriptorQueryContext());
		}
	}
	public class ProcessRuntimeDescriptorQueryContext
	{
			public ProcessRuntimeDescriptorCollection Where(WhereDelegate<ProcessRuntimeDescriptorColumns> where, Database db = null)
			{
				return Bam.Net.Services.AssemblyManagement.Data.Dao.ProcessRuntimeDescriptor.Where(where, db);
			}
		   
			public ProcessRuntimeDescriptorCollection Where(WhereDelegate<ProcessRuntimeDescriptorColumns> where, OrderBy<ProcessRuntimeDescriptorColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Services.AssemblyManagement.Data.Dao.ProcessRuntimeDescriptor.Where(where, orderBy, db);
			}

			public ProcessRuntimeDescriptor OneWhere(WhereDelegate<ProcessRuntimeDescriptorColumns> where, Database db = null)
			{
				return Bam.Net.Services.AssemblyManagement.Data.Dao.ProcessRuntimeDescriptor.OneWhere(where, db);
			}

			public static ProcessRuntimeDescriptor GetOneWhere(WhereDelegate<ProcessRuntimeDescriptorColumns> where, Database db = null)
			{
				return Bam.Net.Services.AssemblyManagement.Data.Dao.ProcessRuntimeDescriptor.GetOneWhere(where, db);
			}
		
			public ProcessRuntimeDescriptor FirstOneWhere(WhereDelegate<ProcessRuntimeDescriptorColumns> where, Database db = null)
			{
				return Bam.Net.Services.AssemblyManagement.Data.Dao.ProcessRuntimeDescriptor.FirstOneWhere(where, db);
			}

			public ProcessRuntimeDescriptorCollection Top(int count, WhereDelegate<ProcessRuntimeDescriptorColumns> where, Database db = null)
			{
				return Bam.Net.Services.AssemblyManagement.Data.Dao.ProcessRuntimeDescriptor.Top(count, where, db);
			}

			public ProcessRuntimeDescriptorCollection Top(int count, WhereDelegate<ProcessRuntimeDescriptorColumns> where, OrderBy<ProcessRuntimeDescriptorColumns> orderBy, Database db = null)
			{
				return Bam.Net.Services.AssemblyManagement.Data.Dao.ProcessRuntimeDescriptor.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<ProcessRuntimeDescriptorColumns> where, Database db = null)
			{
				return Bam.Net.Services.AssemblyManagement.Data.Dao.ProcessRuntimeDescriptor.Count(where, db);
			}
	}

	static ProcessRuntimeDescriptorQueryContext _processRuntimeDescriptors;
	static object _processRuntimeDescriptorsLock = new object();
	public static ProcessRuntimeDescriptorQueryContext ProcessRuntimeDescriptors
	{
		get
		{
			return _processRuntimeDescriptorsLock.DoubleCheckLock<ProcessRuntimeDescriptorQueryContext>(ref _processRuntimeDescriptors, () => new ProcessRuntimeDescriptorQueryContext());
		}
	}
	public class AssemblyReferenceDescriptorAssemblyDescriptorQueryContext
	{
			public AssemblyReferenceDescriptorAssemblyDescriptorCollection Where(WhereDelegate<AssemblyReferenceDescriptorAssemblyDescriptorColumns> where, Database db = null)
			{
				return Bam.Net.Services.AssemblyManagement.Data.Dao.AssemblyReferenceDescriptorAssemblyDescriptor.Where(where, db);
			}
		   
			public AssemblyReferenceDescriptorAssemblyDescriptorCollection Where(WhereDelegate<AssemblyReferenceDescriptorAssemblyDescriptorColumns> where, OrderBy<AssemblyReferenceDescriptorAssemblyDescriptorColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Services.AssemblyManagement.Data.Dao.AssemblyReferenceDescriptorAssemblyDescriptor.Where(where, orderBy, db);
			}

			public AssemblyReferenceDescriptorAssemblyDescriptor OneWhere(WhereDelegate<AssemblyReferenceDescriptorAssemblyDescriptorColumns> where, Database db = null)
			{
				return Bam.Net.Services.AssemblyManagement.Data.Dao.AssemblyReferenceDescriptorAssemblyDescriptor.OneWhere(where, db);
			}

			public static AssemblyReferenceDescriptorAssemblyDescriptor GetOneWhere(WhereDelegate<AssemblyReferenceDescriptorAssemblyDescriptorColumns> where, Database db = null)
			{
				return Bam.Net.Services.AssemblyManagement.Data.Dao.AssemblyReferenceDescriptorAssemblyDescriptor.GetOneWhere(where, db);
			}
		
			public AssemblyReferenceDescriptorAssemblyDescriptor FirstOneWhere(WhereDelegate<AssemblyReferenceDescriptorAssemblyDescriptorColumns> where, Database db = null)
			{
				return Bam.Net.Services.AssemblyManagement.Data.Dao.AssemblyReferenceDescriptorAssemblyDescriptor.FirstOneWhere(where, db);
			}

			public AssemblyReferenceDescriptorAssemblyDescriptorCollection Top(int count, WhereDelegate<AssemblyReferenceDescriptorAssemblyDescriptorColumns> where, Database db = null)
			{
				return Bam.Net.Services.AssemblyManagement.Data.Dao.AssemblyReferenceDescriptorAssemblyDescriptor.Top(count, where, db);
			}

			public AssemblyReferenceDescriptorAssemblyDescriptorCollection Top(int count, WhereDelegate<AssemblyReferenceDescriptorAssemblyDescriptorColumns> where, OrderBy<AssemblyReferenceDescriptorAssemblyDescriptorColumns> orderBy, Database db = null)
			{
				return Bam.Net.Services.AssemblyManagement.Data.Dao.AssemblyReferenceDescriptorAssemblyDescriptor.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<AssemblyReferenceDescriptorAssemblyDescriptorColumns> where, Database db = null)
			{
				return Bam.Net.Services.AssemblyManagement.Data.Dao.AssemblyReferenceDescriptorAssemblyDescriptor.Count(where, db);
			}
	}

	static AssemblyReferenceDescriptorAssemblyDescriptorQueryContext _assemblyReferenceDescriptorAssemblyDescriptors;
	static object _assemblyReferenceDescriptorAssemblyDescriptorsLock = new object();
	public static AssemblyReferenceDescriptorAssemblyDescriptorQueryContext AssemblyReferenceDescriptorAssemblyDescriptors
	{
		get
		{
			return _assemblyReferenceDescriptorAssemblyDescriptorsLock.DoubleCheckLock<AssemblyReferenceDescriptorAssemblyDescriptorQueryContext>(ref _assemblyReferenceDescriptorAssemblyDescriptors, () => new AssemblyReferenceDescriptorAssemblyDescriptorQueryContext());
		}
	}
	public class AssemblyDescriptorProcessRuntimeDescriptorQueryContext
	{
			public AssemblyDescriptorProcessRuntimeDescriptorCollection Where(WhereDelegate<AssemblyDescriptorProcessRuntimeDescriptorColumns> where, Database db = null)
			{
				return Bam.Net.Services.AssemblyManagement.Data.Dao.AssemblyDescriptorProcessRuntimeDescriptor.Where(where, db);
			}
		   
			public AssemblyDescriptorProcessRuntimeDescriptorCollection Where(WhereDelegate<AssemblyDescriptorProcessRuntimeDescriptorColumns> where, OrderBy<AssemblyDescriptorProcessRuntimeDescriptorColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Services.AssemblyManagement.Data.Dao.AssemblyDescriptorProcessRuntimeDescriptor.Where(where, orderBy, db);
			}

			public AssemblyDescriptorProcessRuntimeDescriptor OneWhere(WhereDelegate<AssemblyDescriptorProcessRuntimeDescriptorColumns> where, Database db = null)
			{
				return Bam.Net.Services.AssemblyManagement.Data.Dao.AssemblyDescriptorProcessRuntimeDescriptor.OneWhere(where, db);
			}

			public static AssemblyDescriptorProcessRuntimeDescriptor GetOneWhere(WhereDelegate<AssemblyDescriptorProcessRuntimeDescriptorColumns> where, Database db = null)
			{
				return Bam.Net.Services.AssemblyManagement.Data.Dao.AssemblyDescriptorProcessRuntimeDescriptor.GetOneWhere(where, db);
			}
		
			public AssemblyDescriptorProcessRuntimeDescriptor FirstOneWhere(WhereDelegate<AssemblyDescriptorProcessRuntimeDescriptorColumns> where, Database db = null)
			{
				return Bam.Net.Services.AssemblyManagement.Data.Dao.AssemblyDescriptorProcessRuntimeDescriptor.FirstOneWhere(where, db);
			}

			public AssemblyDescriptorProcessRuntimeDescriptorCollection Top(int count, WhereDelegate<AssemblyDescriptorProcessRuntimeDescriptorColumns> where, Database db = null)
			{
				return Bam.Net.Services.AssemblyManagement.Data.Dao.AssemblyDescriptorProcessRuntimeDescriptor.Top(count, where, db);
			}

			public AssemblyDescriptorProcessRuntimeDescriptorCollection Top(int count, WhereDelegate<AssemblyDescriptorProcessRuntimeDescriptorColumns> where, OrderBy<AssemblyDescriptorProcessRuntimeDescriptorColumns> orderBy, Database db = null)
			{
				return Bam.Net.Services.AssemblyManagement.Data.Dao.AssemblyDescriptorProcessRuntimeDescriptor.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<AssemblyDescriptorProcessRuntimeDescriptorColumns> where, Database db = null)
			{
				return Bam.Net.Services.AssemblyManagement.Data.Dao.AssemblyDescriptorProcessRuntimeDescriptor.Count(where, db);
			}
	}

	static AssemblyDescriptorProcessRuntimeDescriptorQueryContext _assemblyDescriptorProcessRuntimeDescriptors;
	static object _assemblyDescriptorProcessRuntimeDescriptorsLock = new object();
	public static AssemblyDescriptorProcessRuntimeDescriptorQueryContext AssemblyDescriptorProcessRuntimeDescriptors
	{
		get
		{
			return _assemblyDescriptorProcessRuntimeDescriptorsLock.DoubleCheckLock<AssemblyDescriptorProcessRuntimeDescriptorQueryContext>(ref _assemblyDescriptorProcessRuntimeDescriptors, () => new AssemblyDescriptorProcessRuntimeDescriptorQueryContext());
		}
	}    }
}																								
