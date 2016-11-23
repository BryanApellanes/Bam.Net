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

namespace Bam.Net.CoreServices.Data.Daos
{
	// schema = CoreRegistry
	// connection Name = CoreRegistry
	[Serializable]
	[Bam.Net.Data.Table("Configuration", "CoreRegistry")]
	public partial class Configuration: Dao
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

            this.ChildCollections.Add("ConfigurationMachine_ConfigurationId", new ConfigurationMachineCollection(Database.GetQuery<ConfigurationMachineColumns, ConfigurationMachine>((c) => c.ConfigurationId == GetLongValue("Id")), this, "ConfigurationId"));	
            this.ChildCollections.Add("ConfigurationApplication_ConfigurationId", new ConfigurationApplicationCollection(Database.GetQuery<ConfigurationApplicationColumns, ConfigurationApplication>((c) => c.ConfigurationId == GetLongValue("Id")), this, "ConfigurationId"));				
            this.ChildCollections.Add("Configuration_ConfigurationMachine_Machine",  new XrefDaoCollection<ConfigurationMachine, Machine>(this, false));
				
            this.ChildCollections.Add("Configuration_ConfigurationApplication_Application",  new XrefDaoCollection<ConfigurationApplication, Application>(this, false));
							
		}

	// property:Id, columnName:Id	
	[Bam.Net.Exclude]
	[Bam.Net.Data.KeyColumn(Name="Id", DbDataType="BigInt", MaxLength="19")]
	public long? Id
	{
		get
		{
			return GetLongValue("Id");
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

	// property:Key, columnName:Key	
	[Bam.Net.Data.Column(Name="Key", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string Key
	{
		get
		{
			return GetStringValue("Key");
		}
		set
		{
			SetValue("Key", value);
		}
	}

	// property:Value, columnName:Value	
	[Bam.Net.Data.Column(Name="Value", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string Value
	{
		get
		{
			return GetStringValue("Value");
		}
		set
		{
			SetValue("Value", value);
		}
	}

	// property:CreatedBy, columnName:CreatedBy	
	[Bam.Net.Data.Column(Name="CreatedBy", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string CreatedBy
	{
		get
		{
			return GetStringValue("CreatedBy");
		}
		set
		{
			SetValue("CreatedBy", value);
		}
	}

	// property:Created, columnName:Created	
	[Bam.Net.Data.Column(Name="Created", DbDataType="DateTime", MaxLength="8", AllowNull=true)]
	public DateTime? Created
	{
		get
		{
			return GetDateTimeValue("Created");
		}
		set
		{
			SetValue("Created", value);
		}
	}

	// property:ModifiedBy, columnName:ModifiedBy	
	[Bam.Net.Data.Column(Name="ModifiedBy", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string ModifiedBy
	{
		get
		{
			return GetStringValue("ModifiedBy");
		}
		set
		{
			SetValue("ModifiedBy", value);
		}
	}

	// property:Modified, columnName:Modified	
	[Bam.Net.Data.Column(Name="Modified", DbDataType="DateTime", MaxLength="8", AllowNull=true)]
	public DateTime? Modified
	{
		get
		{
			return GetDateTimeValue("Modified");
		}
		set
		{
			SetValue("Modified", value);
		}
	}

	// property:Deleted, columnName:Deleted	
	[Bam.Net.Data.Column(Name="Deleted", DbDataType="DateTime", MaxLength="8", AllowNull=true)]
	public DateTime? Deleted
	{
		get
		{
			return GetDateTimeValue("Deleted");
		}
		set
		{
			SetValue("Deleted", value);
		}
	}



				

	[Bam.Net.Exclude]	
	public ConfigurationMachineCollection ConfigurationMachinesByConfigurationId
	{
		get
		{
			if (this.IsNew)
			{
				throw new InvalidOperationException("The current instance of type({0}) hasn't been saved and will have no child collections, call Save() or Save(Database) first."._Format(this.GetType().Name));
			}

			if(!this.ChildCollections.ContainsKey("ConfigurationMachine_ConfigurationId"))
			{
				SetChildren();
			}

			var c = (ConfigurationMachineCollection)this.ChildCollections["ConfigurationMachine_ConfigurationId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
	
	[Bam.Net.Exclude]	
	public ConfigurationApplicationCollection ConfigurationApplicationsByConfigurationId
	{
		get
		{
			if (this.IsNew)
			{
				throw new InvalidOperationException("The current instance of type({0}) hasn't been saved and will have no child collections, call Save() or Save(Database) first."._Format(this.GetType().Name));
			}

			if(!this.ChildCollections.ContainsKey("ConfigurationApplication_ConfigurationId"))
			{
				SetChildren();
			}

			var c = (ConfigurationApplicationCollection)this.ChildCollections["ConfigurationApplication_ConfigurationId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
			

		// Xref       
        public XrefDaoCollection<ConfigurationMachine, Machine> Machines
        {
            get
            {			
				if (this.IsNew)
				{
					throw new InvalidOperationException("The current instance of type({0}) hasn't been saved and will have no child collections, call Save() or Save(Database) first."._Format(this.GetType().Name));
				}

				if(!this.ChildCollections.ContainsKey("Configuration_ConfigurationMachine_Machine"))
				{
					SetChildren();
				}

				var xref = (XrefDaoCollection<ConfigurationMachine, Machine>)this.ChildCollections["Configuration_ConfigurationMachine_Machine"];
				if(!xref.Loaded)
				{
					xref.Load(Database);
				}

				return xref;
            }
        }
		// Xref       
        public XrefDaoCollection<ConfigurationApplication, Application> Applications
        {
            get
            {			
				if (this.IsNew)
				{
					throw new InvalidOperationException("The current instance of type({0}) hasn't been saved and will have no child collections, call Save() or Save(Database) first."._Format(this.GetType().Name));
				}

				if(!this.ChildCollections.ContainsKey("Configuration_ConfigurationApplication_Application"))
				{
					SetChildren();
				}

				var xref = (XrefDaoCollection<ConfigurationApplication, Application>)this.ChildCollections["Configuration_ConfigurationApplication_Application"];
				if(!xref.Loaded)
				{
					xref.Load(Database);
				}

				return xref;
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
				return UniqueFilterProvider();
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
			SqlStringBuilder sql = new SqlStringBuilder();
			sql.Select<Configuration>();
			Database db = database ?? Db.For<Configuration>();
			var results = new ConfigurationCollection(sql.GetDataTable(db));
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static async Task BatchAll(int batchSize, Action<IEnumerable<Configuration>> batchProcessor, Database database = null)
		{
			await Task.Run(async ()=>
			{
				ConfigurationColumns columns = new ConfigurationColumns();
				var orderBy = Bam.Net.Data.Order.By<ConfigurationColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
				var results = Top(batchSize, (c) => c.KeyColumn > 0, orderBy, database);
				while(results.Count > 0)
				{
					await Task.Run(()=>
					{
						batchProcessor(results);
					});
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (c) => c.KeyColumn > topId, orderBy, database);
				}
			});			
		}
			 
		[Bam.Net.Exclude]
		public static async Task BatchQuery(int batchSize, QueryFilter filter, Action<IEnumerable<Configuration>> batchProcessor, Database database = null)
		{
			await BatchQuery(batchSize, (c) => filter, batchProcessor, database);			
		}

		[Bam.Net.Exclude]
		public static async Task BatchQuery(int batchSize, WhereDelegate<ConfigurationColumns> where, Action<IEnumerable<Configuration>> batchProcessor, Database database = null)
		{
			await Task.Run(async ()=>
			{
				ConfigurationColumns columns = new ConfigurationColumns();
				var orderBy = Bam.Net.Data.Order.By<ConfigurationColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (ConfigurationColumns)where(columns) && columns.KeyColumn > topId, orderBy, database);
				}
			});			
		}

		public static Configuration GetById(int id, Database database = null)
		{
			return GetById((long)id, database);
		}

		public static Configuration GetById(long id, Database database = null)
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
		/// <param name="database"></param>
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
		/// <param name="db"></param>
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
		/// <param name="db"></param>
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
		/// <param name="db"></param>
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
