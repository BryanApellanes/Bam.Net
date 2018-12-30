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

namespace Bam.Net.CoreServices.AccessControl.Data.Dao
{
	// schema = AccessControl 
    public static class AccessControlContext
    {
		public static string ConnectionName
		{
			get
			{
				return "AccessControl";
			}
		}

		public static Database Db
		{
			get
			{
				return Bam.Net.Data.Db.For(ConnectionName);
			}
		}


	public class ResourceHostQueryContext
	{
			public ResourceHostCollection Where(WhereDelegate<ResourceHostColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.AccessControl.Data.Dao.ResourceHost.Where(where, db);
			}
		   
			public ResourceHostCollection Where(WhereDelegate<ResourceHostColumns> where, OrderBy<ResourceHostColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.CoreServices.AccessControl.Data.Dao.ResourceHost.Where(where, orderBy, db);
			}

			public ResourceHost OneWhere(WhereDelegate<ResourceHostColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.AccessControl.Data.Dao.ResourceHost.OneWhere(where, db);
			}

			public static ResourceHost GetOneWhere(WhereDelegate<ResourceHostColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.AccessControl.Data.Dao.ResourceHost.GetOneWhere(where, db);
			}
		
			public ResourceHost FirstOneWhere(WhereDelegate<ResourceHostColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.AccessControl.Data.Dao.ResourceHost.FirstOneWhere(where, db);
			}

			public ResourceHostCollection Top(int count, WhereDelegate<ResourceHostColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.AccessControl.Data.Dao.ResourceHost.Top(count, where, db);
			}

			public ResourceHostCollection Top(int count, WhereDelegate<ResourceHostColumns> where, OrderBy<ResourceHostColumns> orderBy, Database db = null)
			{
				return Bam.Net.CoreServices.AccessControl.Data.Dao.ResourceHost.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<ResourceHostColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.AccessControl.Data.Dao.ResourceHost.Count(where, db);
			}
	}

	static ResourceHostQueryContext _resourceHosts;
	static object _resourceHostsLock = new object();
	public static ResourceHostQueryContext ResourceHosts
	{
		get
		{
			return _resourceHostsLock.DoubleCheckLock<ResourceHostQueryContext>(ref _resourceHosts, () => new ResourceHostQueryContext());
		}
	}
	public class ResourceQueryContext
	{
			public ResourceCollection Where(WhereDelegate<ResourceColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.AccessControl.Data.Dao.Resource.Where(where, db);
			}
		   
			public ResourceCollection Where(WhereDelegate<ResourceColumns> where, OrderBy<ResourceColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.CoreServices.AccessControl.Data.Dao.Resource.Where(where, orderBy, db);
			}

			public Resource OneWhere(WhereDelegate<ResourceColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.AccessControl.Data.Dao.Resource.OneWhere(where, db);
			}

			public static Resource GetOneWhere(WhereDelegate<ResourceColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.AccessControl.Data.Dao.Resource.GetOneWhere(where, db);
			}
		
			public Resource FirstOneWhere(WhereDelegate<ResourceColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.AccessControl.Data.Dao.Resource.FirstOneWhere(where, db);
			}

			public ResourceCollection Top(int count, WhereDelegate<ResourceColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.AccessControl.Data.Dao.Resource.Top(count, where, db);
			}

			public ResourceCollection Top(int count, WhereDelegate<ResourceColumns> where, OrderBy<ResourceColumns> orderBy, Database db = null)
			{
				return Bam.Net.CoreServices.AccessControl.Data.Dao.Resource.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<ResourceColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.AccessControl.Data.Dao.Resource.Count(where, db);
			}
	}

	static ResourceQueryContext _resources;
	static object _resourcesLock = new object();
	public static ResourceQueryContext Resources
	{
		get
		{
			return _resourcesLock.DoubleCheckLock<ResourceQueryContext>(ref _resources, () => new ResourceQueryContext());
		}
	}
	public class PermissionSpecificationQueryContext
	{
			public PermissionSpecificationCollection Where(WhereDelegate<PermissionSpecificationColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.AccessControl.Data.Dao.PermissionSpecification.Where(where, db);
			}
		   
			public PermissionSpecificationCollection Where(WhereDelegate<PermissionSpecificationColumns> where, OrderBy<PermissionSpecificationColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.CoreServices.AccessControl.Data.Dao.PermissionSpecification.Where(where, orderBy, db);
			}

			public PermissionSpecification OneWhere(WhereDelegate<PermissionSpecificationColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.AccessControl.Data.Dao.PermissionSpecification.OneWhere(where, db);
			}

			public static PermissionSpecification GetOneWhere(WhereDelegate<PermissionSpecificationColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.AccessControl.Data.Dao.PermissionSpecification.GetOneWhere(where, db);
			}
		
			public PermissionSpecification FirstOneWhere(WhereDelegate<PermissionSpecificationColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.AccessControl.Data.Dao.PermissionSpecification.FirstOneWhere(where, db);
			}

			public PermissionSpecificationCollection Top(int count, WhereDelegate<PermissionSpecificationColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.AccessControl.Data.Dao.PermissionSpecification.Top(count, where, db);
			}

			public PermissionSpecificationCollection Top(int count, WhereDelegate<PermissionSpecificationColumns> where, OrderBy<PermissionSpecificationColumns> orderBy, Database db = null)
			{
				return Bam.Net.CoreServices.AccessControl.Data.Dao.PermissionSpecification.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<PermissionSpecificationColumns> where, Database db = null)
			{
				return Bam.Net.CoreServices.AccessControl.Data.Dao.PermissionSpecification.Count(where, db);
			}
	}

	static PermissionSpecificationQueryContext _permissionSpecifications;
	static object _permissionSpecificationsLock = new object();
	public static PermissionSpecificationQueryContext PermissionSpecifications
	{
		get
		{
			return _permissionSpecificationsLock.DoubleCheckLock<PermissionSpecificationQueryContext>(ref _permissionSpecifications, () => new PermissionSpecificationQueryContext());
		}
	}    }
}																								
