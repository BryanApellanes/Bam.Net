/*
	This file was generated and should not be modified directly
*/
// Model is Table
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Bam.Net;
using Bam.Net.Data;
using Bam.Net.Data.Qi;

namespace Bam.Net.ServiceProxy.Secure
{
	// schema = SecureServiceProxy
	// connection Name = SecureServiceProxy
	[Serializable]
	[Bam.Net.Data.Table("Configuration", "SecureServiceProxy")]
	public partial class Configuration: Bam.Net.Data.Dao
	{
		public Configuration():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public Configuration(DataRow data)
			: base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public Configuration(Database db)
			: base(db)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public Configuration(Database db, DataRow data)
			: base(db, data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		[Bam.Net.Exclude]
		public static implicit operator Configuration(DataRow data)
		{
			return new Configuration(data);
		}

		private void SetChildren()
		{

			if(_database != null)
			{
				this.ChildCollections.Add("ConfigSetting_ConfigurationId", new ConfigSettingCollection(Database.GetQuery<ConfigSettingColumns, ConfigSetting>((c) => c.ConfigurationId == GetULongValue("Id")), this, "ConfigurationId"));				
			}						
		}

	// property:Id, columnName:Id	
	[Bam.Net.Exclude]
	[Bam.Net.Data.KeyColumn(Name="Id", DbDataType="BigInt", MaxLength="19")]
	public ulong? Id
	{
		get
		{
			return GetULongValue("Id");
		}
		set
		{
			SetValue("Id", value);
		}
	}

	// property:Uuid, columnName:Uuid	
	[Bam.Net.Data.Column(Name="Uuid", DbDataType="VarChar", MaxLength="4000", AllowNull=false)]
	public string Uuid
	{
		get
		{
			return GetStringValue("Uuid");
		}
		set
		{
			SetValue("Uuid", value);
		}
	}

	// property:Cuid, columnName:Cuid	
	[Bam.Net.Data.Column(Name="Cuid", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string Cuid
	{
		get
		{
			return GetStringValue("Cuid");
		}
		set
		{
			SetValue("Cuid", value);
		}
	}

	// property:Name, columnName:Name	
	[Bam.Net.Data.Column(Name="Name", DbDataType="VarChar", MaxLength="4000", AllowNull=false)]
	public string Name
	{
		get
		{
			return GetStringValue("Name");
		}
		set
		{
			SetValue("Name", value);
		}
	}



	// start ApplicationId -> ApplicationId
	[Bam.Net.Data.ForeignKey(
        Table="Configuration",
		Name="ApplicationId", 
		DbDataType="BigInt", 
		MaxLength="",
		AllowNull=true, 
		ReferencedKey="Id",
		ReferencedTable="Application",
		Suffix="1")]
	public ulong? ApplicationId
	{
		get
		{
			return GetULongValue("ApplicationId");
		}
		set
		{
			SetValue("ApplicationId", value);
		}
	}

	Application _applicationOfApplicationId;
	public Application ApplicationOfApplicationId
	{
		get
		{
			if(_applicationOfApplicationId == null)
			{
				_applicationOfApplicationId = Bam.Net.ServiceProxy.Secure.Application.OneWhere(c => c.KeyColumn == this.ApplicationId, this.Database);
			}
			return _applicationOfApplicationId;
		}
	}
	
				

	[Bam.Net.Exclude]	
	public ConfigSettingCollection ConfigSettingsByConfigurationId
	{
		get
		{
			if (this.IsNew)
			{
				throw new InvalidOperationException("The current instance of type({0}) hasn't been saved and will have no child collections, call Save() or Save(Database) first."._Format(this.GetType().Name));
			}

			if(!this.ChildCollections.ContainsKey("ConfigSetting_ConfigurationId"))
			{
				SetChildren();
			}

			var c = (ConfigSettingCollection)this.ChildCollections["ConfigSetting_ConfigurationId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
			

		/// <summary>
		/// Gets a query filter that should uniquely identify
		/// the current instance.  The default implementation
		/// compares the Id/key field to the current instance's.
		/// </summary>
		[Bam.Net.Exclude] 
		public override IQueryFilter GetUniqueFilter()
		{
			if(UniqueFilterProvider != null)
			{
				return UniqueFilterProvider(this);
			}
			else
			{
				var colFilter = new ConfigurationColumns();
				return (colFilter.KeyColumn == IdValue);
			}			
		}

		/// <summary>
		/// Return every record in the Configuration table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static ConfigurationCollection LoadAll(Database database = null)
		{
			Database db = database ?? Db.For<Configuration>();
			SqlStringBuilder sql = db.GetSqlStringBuilder();
			sql.Select<Configuration>();
			var results = new ConfigurationCollection(db, sql.GetDataTable(db))
			{
				Database = db
			};
			return results;
		}

		/// <summary>
		/// Process all records in batches of the specified size
		/// </summary>
		[Bam.Net.Exclude]
		public static async Task BatchAll(int batchSize, Action<IEnumerable<Configuration>> batchProcessor, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				ConfigurationColumns columns = new ConfigurationColumns();
				var orderBy = Bam.Net.Data.Order.By<ConfigurationColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
				var results = Top(batchSize, (c) => c.KeyColumn > 0, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{
						batchProcessor(results);
					});
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (c) => c.KeyColumn > topId, orderBy, database);
				}
			});			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>			 
		[Bam.Net.Exclude]
		public static async Task BatchQuery(int batchSize, QueryFilter filter, Action<IEnumerable<Configuration>> batchProcessor, Database database = null)
		{
			await BatchQuery(batchSize, (c) => filter, batchProcessor, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery(int batchSize, WhereDelegate<ConfigurationColumns> where, Action<IEnumerable<Configuration>> batchProcessor, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				ConfigurationColumns columns = new ConfigurationColumns();
				var orderBy = Bam.Net.Data.Order.By<ConfigurationColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (ConfigurationColumns)where(columns) && columns.KeyColumn > topId, orderBy, database);
				}
			});			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>			 
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, QueryFilter filter, Action<IEnumerable<Configuration>> batchProcessor, Bam.Net.Data.OrderBy<ConfigurationColumns> orderBy, Database database = null)
		{
			await BatchQuery<ColType>(batchSize, (c) => filter, batchProcessor, orderBy, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, WhereDelegate<ConfigurationColumns> where, Action<IEnumerable<Configuration>> batchProcessor, Bam.Net.Data.OrderBy<ConfigurationColumns> orderBy, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				ConfigurationColumns columns = new ConfigurationColumns();
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					ColType top = results.Select(d => d.Property<ColType>(orderBy.Column.ToString())).ToArray().Largest();
					results = Top(batchSize, (ConfigurationColumns)where(columns) && orderBy.Column > top, orderBy, database);
				}
			});			
		}

		public static Configuration GetById(uint id, Database database = null)
		{
			return GetById((ulong)id, database);
		}

		public static Configuration GetById(int id, Database database = null)
		{
			return GetById((long)id, database);
		}

		public static Configuration GetById(long id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static Configuration GetById(ulong id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static Configuration GetByUuid(string uuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Uuid") == uuid, database);
		}

		public static Configuration GetByCuid(string cuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Cuid") == cuid, database);
		}

		[Bam.Net.Exclude]
		public static ConfigurationCollection Query(QueryFilter filter, Database database = null)
		{
			return Where(filter, database);
		}

		[Bam.Net.Exclude]		
		public static ConfigurationCollection Where(QueryFilter filter, Database database = null)
		{
			WhereDelegate<ConfigurationColumns> whereDelegate = (c) => filter;
			return Where(whereDelegate, database);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a ConfigurationColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between ConfigurationColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static ConfigurationCollection Where(Func<ConfigurationColumns, QueryFilter<ConfigurationColumns>> where, OrderBy<ConfigurationColumns> orderBy = null, Database database = null)
		{
			database = database ?? Db.For<Configuration>();
			return new ConfigurationCollection(database.GetQuery<ConfigurationColumns, Configuration>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ConfigurationColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ConfigurationColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static ConfigurationCollection Where(WhereDelegate<ConfigurationColumns> where, Database database = null)
		{		
			database = database ?? Db.For<Configuration>();
			var results = new ConfigurationCollection(database, database.GetQuery<ConfigurationColumns, Configuration>(where), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ConfigurationColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ConfigurationColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static ConfigurationCollection Where(WhereDelegate<ConfigurationColumns> where, OrderBy<ConfigurationColumns> orderBy = null, Database database = null)
		{		
			database = database ?? Db.For<Configuration>();
			var results = new ConfigurationCollection(database, database.GetQuery<ConfigurationColumns, Configuration>(where, orderBy), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate&lt;ConfigurationColumns&gt;.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static ConfigurationCollection Where(QiQuery where, Database database = null)
		{
			var results = new ConfigurationCollection(database, Select<ConfigurationColumns>.From<Configuration>().Where(where, database));
			return results;
		}
				
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		[Bam.Net.Exclude]
		public static Configuration GetOneWhere(QueryFilter where, Database database = null)
		{
			var result = OneWhere(where, database);
			if(result == null)
			{
				result = CreateFromFilter(where, database);
			}

			return result;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static Configuration OneWhere(QueryFilter where, Database database = null)
		{
			WhereDelegate<ConfigurationColumns> whereDelegate = (c) => where;
			var result = Top(1, whereDelegate, database);
			return OneOrThrow(result);
		}

		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static Configuration GetOneWhere(WhereDelegate<ConfigurationColumns> where, Database database = null)
		{
			var result = OneWhere(where, database);
			if(result == null)
			{
				ConfigurationColumns c = new ConfigurationColumns();
				IQueryFilter filter = where(c); 
				result = CreateFromFilter(filter, database);
			}

			return result;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single Configuration instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ConfigurationColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ConfigurationColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static Configuration OneWhere(WhereDelegate<ConfigurationColumns> where, Database database = null)
		{
			var result = Top(1, where, database);
			return OneOrThrow(result);
		}
					 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<ConfigurationColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static Configuration OneWhere(QiQuery where, Database database = null)
		{
			var results = Top(1, where, database);
			return OneOrThrow(results);
		}

		/// <summary>
		/// Execute a query and return the first result.  This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ConfigurationColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ConfigurationColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static Configuration FirstOneWhere(WhereDelegate<ConfigurationColumns> where, Database database = null)
		{
			var results = Top(1, where, database);
			if(results.Count > 0)
			{
				return results[0];
			}
			else
			{
				return null;
			}
		}
		
		/// <summary>
		/// Execute a query and return the first result.  This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ConfigurationColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ConfigurationColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static Configuration FirstOneWhere(WhereDelegate<ConfigurationColumns> where, OrderBy<ConfigurationColumns> orderBy, Database database = null)
		{
			var results = Top(1, where, orderBy, database);
			if(results.Count > 0)
			{
				return results[0];
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// Shortcut for Top(1, where, orderBy, database)
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ConfigurationColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ConfigurationColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static Configuration FirstOneWhere(QueryFilter where, OrderBy<ConfigurationColumns> orderBy = null, Database database = null)
		{
			WhereDelegate<ConfigurationColumns> whereDelegate = (c) => where;
			var results = Top(1, whereDelegate, orderBy, database);
			if(results.Count > 0)
			{
				return results[0];
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// Execute a query and return the specified number
		/// of values. This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="count">The number of values to return.
		/// This value is used in the sql query so no more than this 
		/// number of values will be returned by the database.
		/// </param>
		/// <param name="where">A WhereDelegate that recieves a ConfigurationColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ConfigurationColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static ConfigurationCollection Top(int count, WhereDelegate<ConfigurationColumns> where, Database database = null)
		{
			return Top(count, where, null, database);
		}

		/// <summary>
		/// Execute a query and return the specified number of values.  This method
		/// will issue a sql TOP clause so only the specified number of values
		/// will be returned.
		/// </summary>
		/// <param name="count">The number of values to return.
		/// This value is used in the sql query so no more than this 
		/// number of values will be returned by the database.
		/// </param>
		/// <param name="where">A WhereDelegate that recieves a ConfigurationColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ConfigurationColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static ConfigurationCollection Top(int count, WhereDelegate<ConfigurationColumns> where, OrderBy<ConfigurationColumns> orderBy, Database database = null)
		{
			ConfigurationColumns c = new ConfigurationColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database ?? Db.For<Configuration>();
			QuerySet query = GetQuerySet(db); 
			query.Top<Configuration>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<ConfigurationColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<ConfigurationCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static ConfigurationCollection Top(int count, QueryFilter where, Database database)
		{
			return Top(count, where, null, database);
		}
		/// <summary>
		/// Execute a query and return the specified number of values.  This method
		/// will issue a sql TOP clause so only the specified number of values
		/// will be returned.
		/// of values
		/// </summary>
		/// <param name="count">The number of values to return.
		/// This value is used in the sql query so no more than this 
		/// number of values will be returned by the database.
		/// </param>
		/// <param name="where">A QueryFilter used to filter the 
		/// results
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static ConfigurationCollection Top(int count, QueryFilter where, OrderBy<ConfigurationColumns> orderBy = null, Database database = null)
		{
			Database db = database ?? Db.For<Configuration>();
			QuerySet query = GetQuerySet(db);
			query.Top<Configuration>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy<ConfigurationColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<ConfigurationCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static ConfigurationCollection Top(int count, QueryFilter where, string orderBy = null, SortOrder sortOrder = SortOrder.Ascending, Database database = null)
		{
			Database db = database ?? Db.For<Configuration>();
			QuerySet query = GetQuerySet(db);
			query.Top<Configuration>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy(orderBy, sortOrder);
			}

			query.Execute(db);
			var results = query.Results.As<ConfigurationCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the specified number of values.  This method
		/// will issue a sql TOP clause so only the specified number of values
		/// will be returned.
		/// of values
		/// </summary>
		/// <param name="count">The number of values to return.
		/// This value is used in the sql query so no more than this 
		/// number of values will be returned by the database.
		/// </param>
		/// <param name="where">A QueryFilter used to filter the 
		/// results
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		public static ConfigurationCollection Top(int count, QiQuery where, Database database = null)
		{
			Database db = database ?? Db.For<Configuration>();
			QuerySet query = GetQuerySet(db);
			query.Top<Configuration>(count);
			query.Where(where);
			query.Execute(db);
			var results = query.Results.As<ConfigurationCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Return the count of Configurations
		/// </summary>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		public static long Count(Database database = null)
        {
			Database db = database ?? Db.For<Configuration>();
            QuerySet query = GetQuerySet(db);
            query.Count<Configuration>();
            query.Execute(db);
            return (long)query.Results[0].DataRow[0];
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ConfigurationColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ConfigurationColumns and other values
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static long Count(WhereDelegate<ConfigurationColumns> where, Database database = null)
		{
			ConfigurationColumns c = new ConfigurationColumns();
			IQueryFilter filter = where(c) ;

			Database db = database ?? Db.For<Configuration>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<Configuration>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
		 
		public static long Count(QiQuery where, Database database = null)
		{
		    Database db = database ?? Db.For<Configuration>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<Configuration>();
			query.Where(where);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		} 		

		private static Configuration CreateFromFilter(IQueryFilter filter, Database database = null)
		{
			Database db = database ?? Db.For<Configuration>();			
			var dao = new Configuration();
			filter.Parameters.Each(p=>
			{
				dao.Property(p.ColumnName, p.Value);
			});
			dao.Save(db);
			return dao;
		}
		
		private static Configuration OneOrThrow(ConfigurationCollection c)
		{
			if(c.Count == 1)
			{
				return c[0];
			}
			else if(c.Count > 1)
			{
				throw new MultipleEntriesFoundException();
			}

			return null;
		}

	}
}																								
