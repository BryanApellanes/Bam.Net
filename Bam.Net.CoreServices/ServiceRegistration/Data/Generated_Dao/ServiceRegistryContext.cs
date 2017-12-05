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

namespace Bam.Net.CoreServices.ServiceRegistration.Data.Dao
{
	// schema = ServiceRegistry 
    public static class ServiceRegistryContext
    {
		public static string ConnectionName
		{
			get
			{
				return "ServiceRegistry";
			}
		}

		public static Database Db
		{
			get
			{
				return Bam.Net.Data.Db.For(ConnectionName);
			}
		}


	public class MachineRegistriesQueryContext
	{
			public MachineRegistriesCollection Where(WhereDelegate<MachineRegistriesColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ServiceRegistration.Data.Dao.MachineRegistries.Where(where, db);
			}
		   
			public MachineRegistriesCollection Where(WhereDelegate<MachineRegistriesColumns> where, OrderBy<MachineRegistriesColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.CoreServices.ServiceRegistration.Data.Dao.MachineRegistries.Where(where, orderBy, db);
			}

			public MachineRegistries OneWhere(WhereDelegate<MachineRegistriesColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ServiceRegistration.Data.Dao.MachineRegistries.OneWhere(where, db);
			}

			public static MachineRegistries GetOneWhere(WhereDelegate<MachineRegistriesColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ServiceRegistration.Data.Dao.MachineRegistries.GetOneWhere(where, db);
			}
		
			public MachineRegistries FirstOneWhere(WhereDelegate<MachineRegistriesColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ServiceRegistration.Data.Dao.MachineRegistries.FirstOneWhere(where, db);
			}

			public MachineRegistriesCollection Top(int count, WhereDelegate<MachineRegistriesColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ServiceRegistration.Data.Dao.MachineRegistries.Top(count, where, db);
			}

			public MachineRegistriesCollection Top(int count, WhereDelegate<MachineRegistriesColumns> where, OrderBy<MachineRegistriesColumns> orderBy, Database db = null)
			{
				return Bam.Net.CoreServices.ServiceRegistration.Data.Dao.MachineRegistries.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<MachineRegistriesColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ServiceRegistration.Data.Dao.MachineRegistries.Count(where, db);
			}
	}

	static MachineRegistriesQueryContext _machineRegistries;
	static object _machineRegistriesLock = new object();
	public static MachineRegistriesQueryContext MachineRegistries
	{
		get
		{
			return _machineRegistriesLock.DoubleCheckLock<MachineRegistriesQueryContext>(ref _machineRegistries, () => new MachineRegistriesQueryContext());
		}
	}
	public class ServiceTypeIdentifierQueryContext
	{
			public ServiceTypeIdentifierCollection Where(WhereDelegate<ServiceTypeIdentifierColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ServiceRegistration.Data.Dao.ServiceTypeIdentifier.Where(where, db);
			}
		   
			public ServiceTypeIdentifierCollection Where(WhereDelegate<ServiceTypeIdentifierColumns> where, OrderBy<ServiceTypeIdentifierColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.CoreServices.ServiceRegistration.Data.Dao.ServiceTypeIdentifier.Where(where, orderBy, db);
			}

			public ServiceTypeIdentifier OneWhere(WhereDelegate<ServiceTypeIdentifierColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ServiceRegistration.Data.Dao.ServiceTypeIdentifier.OneWhere(where, db);
			}

			public static ServiceTypeIdentifier GetOneWhere(WhereDelegate<ServiceTypeIdentifierColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ServiceRegistration.Data.Dao.ServiceTypeIdentifier.GetOneWhere(where, db);
			}
		
			public ServiceTypeIdentifier FirstOneWhere(WhereDelegate<ServiceTypeIdentifierColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ServiceRegistration.Data.Dao.ServiceTypeIdentifier.FirstOneWhere(where, db);
			}

			public ServiceTypeIdentifierCollection Top(int count, WhereDelegate<ServiceTypeIdentifierColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ServiceRegistration.Data.Dao.ServiceTypeIdentifier.Top(count, where, db);
			}

			public ServiceTypeIdentifierCollection Top(int count, WhereDelegate<ServiceTypeIdentifierColumns> where, OrderBy<ServiceTypeIdentifierColumns> orderBy, Database db = null)
			{
				return Bam.Net.CoreServices.ServiceRegistration.Data.Dao.ServiceTypeIdentifier.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<ServiceTypeIdentifierColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ServiceRegistration.Data.Dao.ServiceTypeIdentifier.Count(where, db);
			}
	}

	static ServiceTypeIdentifierQueryContext _serviceTypeIdentifiers;
	static object _serviceTypeIdentifiersLock = new object();
	public static ServiceTypeIdentifierQueryContext ServiceTypeIdentifiers
	{
		get
		{
			return _serviceTypeIdentifiersLock.DoubleCheckLock<ServiceTypeIdentifierQueryContext>(ref _serviceTypeIdentifiers, () => new ServiceTypeIdentifierQueryContext());
		}
	}
	public class ServiceDescriptorQueryContext
	{
			public ServiceDescriptorCollection Where(WhereDelegate<ServiceDescriptorColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ServiceRegistration.Data.Dao.ServiceDescriptor.Where(where, db);
			}
		   
			public ServiceDescriptorCollection Where(WhereDelegate<ServiceDescriptorColumns> where, OrderBy<ServiceDescriptorColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.CoreServices.ServiceRegistration.Data.Dao.ServiceDescriptor.Where(where, orderBy, db);
			}

			public ServiceDescriptor OneWhere(WhereDelegate<ServiceDescriptorColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ServiceRegistration.Data.Dao.ServiceDescriptor.OneWhere(where, db);
			}

			public static ServiceDescriptor GetOneWhere(WhereDelegate<ServiceDescriptorColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ServiceRegistration.Data.Dao.ServiceDescriptor.GetOneWhere(where, db);
			}
		
			public ServiceDescriptor FirstOneWhere(WhereDelegate<ServiceDescriptorColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ServiceRegistration.Data.Dao.ServiceDescriptor.FirstOneWhere(where, db);
			}

			public ServiceDescriptorCollection Top(int count, WhereDelegate<ServiceDescriptorColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ServiceRegistration.Data.Dao.ServiceDescriptor.Top(count, where, db);
			}

			public ServiceDescriptorCollection Top(int count, WhereDelegate<ServiceDescriptorColumns> where, OrderBy<ServiceDescriptorColumns> orderBy, Database db = null)
			{
				return Bam.Net.CoreServices.ServiceRegistration.Data.Dao.ServiceDescriptor.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<ServiceDescriptorColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ServiceRegistration.Data.Dao.ServiceDescriptor.Count(where, db);
			}
	}

	static ServiceDescriptorQueryContext _serviceDescriptors;
	static object _serviceDescriptorsLock = new object();
	public static ServiceDescriptorQueryContext ServiceDescriptors
	{
		get
		{
			return _serviceDescriptorsLock.DoubleCheckLock<ServiceDescriptorQueryContext>(ref _serviceDescriptors, () => new ServiceDescriptorQueryContext());
		}
	}
	public class ServiceRegistryDescriptorQueryContext
	{
			public ServiceRegistryDescriptorCollection Where(WhereDelegate<ServiceRegistryDescriptorColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ServiceRegistration.Data.Dao.ServiceRegistryDescriptor.Where(where, db);
			}
		   
			public ServiceRegistryDescriptorCollection Where(WhereDelegate<ServiceRegistryDescriptorColumns> where, OrderBy<ServiceRegistryDescriptorColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.CoreServices.ServiceRegistration.Data.Dao.ServiceRegistryDescriptor.Where(where, orderBy, db);
			}

			public ServiceRegistryDescriptor OneWhere(WhereDelegate<ServiceRegistryDescriptorColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ServiceRegistration.Data.Dao.ServiceRegistryDescriptor.OneWhere(where, db);
			}

			public static ServiceRegistryDescriptor GetOneWhere(WhereDelegate<ServiceRegistryDescriptorColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ServiceRegistration.Data.Dao.ServiceRegistryDescriptor.GetOneWhere(where, db);
			}
		
			public ServiceRegistryDescriptor FirstOneWhere(WhereDelegate<ServiceRegistryDescriptorColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ServiceRegistration.Data.Dao.ServiceRegistryDescriptor.FirstOneWhere(where, db);
			}

			public ServiceRegistryDescriptorCollection Top(int count, WhereDelegate<ServiceRegistryDescriptorColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ServiceRegistration.Data.Dao.ServiceRegistryDescriptor.Top(count, where, db);
			}

			public ServiceRegistryDescriptorCollection Top(int count, WhereDelegate<ServiceRegistryDescriptorColumns> where, OrderBy<ServiceRegistryDescriptorColumns> orderBy, Database db = null)
			{
				return Bam.Net.CoreServices.ServiceRegistration.Data.Dao.ServiceRegistryDescriptor.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<ServiceRegistryDescriptorColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ServiceRegistration.Data.Dao.ServiceRegistryDescriptor.Count(where, db);
			}
	}

	static ServiceRegistryDescriptorQueryContext _serviceRegistryDescriptors;
	static object _serviceRegistryDescriptorsLock = new object();
	public static ServiceRegistryDescriptorQueryContext ServiceRegistryDescriptors
	{
		get
		{
			return _serviceRegistryDescriptorsLock.DoubleCheckLock<ServiceRegistryDescriptorQueryContext>(ref _serviceRegistryDescriptors, () => new ServiceRegistryDescriptorQueryContext());
		}
	}
	public class ServiceRegistryLoaderDescriptorQueryContext
	{
			public ServiceRegistryLoaderDescriptorCollection Where(WhereDelegate<ServiceRegistryLoaderDescriptorColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ServiceRegistration.Data.Dao.ServiceRegistryLoaderDescriptor.Where(where, db);
			}
		   
			public ServiceRegistryLoaderDescriptorCollection Where(WhereDelegate<ServiceRegistryLoaderDescriptorColumns> where, OrderBy<ServiceRegistryLoaderDescriptorColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.CoreServices.ServiceRegistration.Data.Dao.ServiceRegistryLoaderDescriptor.Where(where, orderBy, db);
			}

			public ServiceRegistryLoaderDescriptor OneWhere(WhereDelegate<ServiceRegistryLoaderDescriptorColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ServiceRegistration.Data.Dao.ServiceRegistryLoaderDescriptor.OneWhere(where, db);
			}

			public static ServiceRegistryLoaderDescriptor GetOneWhere(WhereDelegate<ServiceRegistryLoaderDescriptorColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ServiceRegistration.Data.Dao.ServiceRegistryLoaderDescriptor.GetOneWhere(where, db);
			}
		
			public ServiceRegistryLoaderDescriptor FirstOneWhere(WhereDelegate<ServiceRegistryLoaderDescriptorColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ServiceRegistration.Data.Dao.ServiceRegistryLoaderDescriptor.FirstOneWhere(where, db);
			}

			public ServiceRegistryLoaderDescriptorCollection Top(int count, WhereDelegate<ServiceRegistryLoaderDescriptorColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ServiceRegistration.Data.Dao.ServiceRegistryLoaderDescriptor.Top(count, where, db);
			}

			public ServiceRegistryLoaderDescriptorCollection Top(int count, WhereDelegate<ServiceRegistryLoaderDescriptorColumns> where, OrderBy<ServiceRegistryLoaderDescriptorColumns> orderBy, Database db = null)
			{
				return Bam.Net.CoreServices.ServiceRegistration.Data.Dao.ServiceRegistryLoaderDescriptor.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<ServiceRegistryLoaderDescriptorColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ServiceRegistration.Data.Dao.ServiceRegistryLoaderDescriptor.Count(where, db);
			}
	}

	static ServiceRegistryLoaderDescriptorQueryContext _serviceRegistryLoaderDescriptors;
	static object _serviceRegistryLoaderDescriptorsLock = new object();
	public static ServiceRegistryLoaderDescriptorQueryContext ServiceRegistryLoaderDescriptors
	{
		get
		{
			return _serviceRegistryLoaderDescriptorsLock.DoubleCheckLock<ServiceRegistryLoaderDescriptorQueryContext>(ref _serviceRegistryLoaderDescriptors, () => new ServiceRegistryLoaderDescriptorQueryContext());
		}
	}
	public class ServiceRegistryLockQueryContext
	{
			public ServiceRegistryLockCollection Where(WhereDelegate<ServiceRegistryLockColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ServiceRegistration.Data.Dao.ServiceRegistryLock.Where(where, db);
			}
		   
			public ServiceRegistryLockCollection Where(WhereDelegate<ServiceRegistryLockColumns> where, OrderBy<ServiceRegistryLockColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.CoreServices.ServiceRegistration.Data.Dao.ServiceRegistryLock.Where(where, orderBy, db);
			}

			public ServiceRegistryLock OneWhere(WhereDelegate<ServiceRegistryLockColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ServiceRegistration.Data.Dao.ServiceRegistryLock.OneWhere(where, db);
			}

			public static ServiceRegistryLock GetOneWhere(WhereDelegate<ServiceRegistryLockColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ServiceRegistration.Data.Dao.ServiceRegistryLock.GetOneWhere(where, db);
			}
		
			public ServiceRegistryLock FirstOneWhere(WhereDelegate<ServiceRegistryLockColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ServiceRegistration.Data.Dao.ServiceRegistryLock.FirstOneWhere(where, db);
			}

			public ServiceRegistryLockCollection Top(int count, WhereDelegate<ServiceRegistryLockColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ServiceRegistration.Data.Dao.ServiceRegistryLock.Top(count, where, db);
			}

			public ServiceRegistryLockCollection Top(int count, WhereDelegate<ServiceRegistryLockColumns> where, OrderBy<ServiceRegistryLockColumns> orderBy, Database db = null)
			{
				return Bam.Net.CoreServices.ServiceRegistration.Data.Dao.ServiceRegistryLock.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<ServiceRegistryLockColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ServiceRegistration.Data.Dao.ServiceRegistryLock.Count(where, db);
			}
	}

	static ServiceRegistryLockQueryContext _serviceRegistryLocks;
	static object _serviceRegistryLocksLock = new object();
	public static ServiceRegistryLockQueryContext ServiceRegistryLocks
	{
		get
		{
			return _serviceRegistryLocksLock.DoubleCheckLock<ServiceRegistryLockQueryContext>(ref _serviceRegistryLocks, () => new ServiceRegistryLockQueryContext());
		}
	}
	public class ServiceDescriptorServiceRegistryDescriptorQueryContext
	{
			public ServiceDescriptorServiceRegistryDescriptorCollection Where(WhereDelegate<ServiceDescriptorServiceRegistryDescriptorColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ServiceRegistration.Data.Dao.ServiceDescriptorServiceRegistryDescriptor.Where(where, db);
			}
		   
			public ServiceDescriptorServiceRegistryDescriptorCollection Where(WhereDelegate<ServiceDescriptorServiceRegistryDescriptorColumns> where, OrderBy<ServiceDescriptorServiceRegistryDescriptorColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.CoreServices.ServiceRegistration.Data.Dao.ServiceDescriptorServiceRegistryDescriptor.Where(where, orderBy, db);
			}

			public ServiceDescriptorServiceRegistryDescriptor OneWhere(WhereDelegate<ServiceDescriptorServiceRegistryDescriptorColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ServiceRegistration.Data.Dao.ServiceDescriptorServiceRegistryDescriptor.OneWhere(where, db);
			}

			public static ServiceDescriptorServiceRegistryDescriptor GetOneWhere(WhereDelegate<ServiceDescriptorServiceRegistryDescriptorColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ServiceRegistration.Data.Dao.ServiceDescriptorServiceRegistryDescriptor.GetOneWhere(where, db);
			}
		
			public ServiceDescriptorServiceRegistryDescriptor FirstOneWhere(WhereDelegate<ServiceDescriptorServiceRegistryDescriptorColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ServiceRegistration.Data.Dao.ServiceDescriptorServiceRegistryDescriptor.FirstOneWhere(where, db);
			}

			public ServiceDescriptorServiceRegistryDescriptorCollection Top(int count, WhereDelegate<ServiceDescriptorServiceRegistryDescriptorColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ServiceRegistration.Data.Dao.ServiceDescriptorServiceRegistryDescriptor.Top(count, where, db);
			}

			public ServiceDescriptorServiceRegistryDescriptorCollection Top(int count, WhereDelegate<ServiceDescriptorServiceRegistryDescriptorColumns> where, OrderBy<ServiceDescriptorServiceRegistryDescriptorColumns> orderBy, Database db = null)
			{
				return Bam.Net.CoreServices.ServiceRegistration.Data.Dao.ServiceDescriptorServiceRegistryDescriptor.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<ServiceDescriptorServiceRegistryDescriptorColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.ServiceRegistration.Data.Dao.ServiceDescriptorServiceRegistryDescriptor.Count(where, db);
			}
	}

	static ServiceDescriptorServiceRegistryDescriptorQueryContext _serviceDescriptorServiceRegistryDescriptors;
	static object _serviceDescriptorServiceRegistryDescriptorsLock = new object();
	public static ServiceDescriptorServiceRegistryDescriptorQueryContext ServiceDescriptorServiceRegistryDescriptors
	{
		get
		{
			return _serviceDescriptorServiceRegistryDescriptorsLock.DoubleCheckLock<ServiceDescriptorServiceRegistryDescriptorQueryContext>(ref _serviceDescriptorServiceRegistryDescriptors, () => new ServiceDescriptorServiceRegistryDescriptorQueryContext());
		}
	}    }
}																								
