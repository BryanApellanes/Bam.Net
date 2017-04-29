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

namespace Bam.Net.CoreServices.Data.Dao
{
	// schema = CoreRegistry
	// connection Name = CoreRegistry
	[Serializable]
	[Bam.Net.Data.Table("ConfigurationSetting", "CoreRegistry")]
	public partial class ConfigurationSetting: Bam.Net.Data.Dao
	{
		public ConfigurationSetting():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public ConfigurationSetting(DataRow data)
			: base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public ConfigurationSetting(Database db)
			: base(db)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public ConfigurationSetting(Database db, DataRow data)
			: base(db, data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		[Bam.Net.Exclude]
		public static implicit operator ConfigurationSetting(DataRow data)
		{
			return new ConfigurationSetting(data);
		}

		private void SetChildren()
		{
						
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



	// start ConfigurationId -> ConfigurationId
	[Bam.Net.Data.ForeignKey(
        Table="ConfigurationSetting",
		Name="ConfigurationId", 
		DbDataType="BigInt", 
		MaxLength="",
		AllowNull=true, 
		ReferencedKey="Id",
		ReferencedTable="Configuration",
		Suffix="1")]
	public long? ConfigurationId
	{
		get
		{
			return GetLongValue("ConfigurationId");
		}
		set
		{
			SetValue("ConfigurationId", value);
		}
	}

	Configuration _configurationOfConfigurationId;
	public Configuration ConfigurationOfConfigurationId
	{
		get
		{
			if(_configurationOfConfigurationId == null)
			{
				_configurationOfConfigurationId = Bam.Net.CoreServices.Data.Dao.Configuration.OneWhere(c => c.KeyColumn == this.ConfigurationId, this.Database);
			}
			return _configurationOfConfigurationId;
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
				var colFilter = new ConfigurationSettingColumns();
				return (colFilter.KeyColumn == IdValue);
			}			
		}

		/// <summary>
		/// Return every record in the ConfigurationSetting table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static ConfigurationSettingCollection LoadAll(Database database = null)
		{
			SqlStringBuilder sql = new SqlStringBuilder();
			sql.Select<ConfigurationSetting>();
			Database db = database ?? Db.For<ConfigurationSetting>();
			var results = new ConfigurationSettingCollection(sql.GetDataTable(db));
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Process all records in batches of the specified size
		/// </summary>
		[Bam.Net.Exclude]
		public static async Task BatchAll(int batchSize, Action<IEnumerable<ConfigurationSetting>> batchProcessor, Database database = null)
		{
			await Task.Run(async ()=>
			{
				ConfigurationSettingColumns columns = new ConfigurationSettingColumns();
				var orderBy = Bam.Net.Data.Order.By<ConfigurationSettingColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
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

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>			 
		[Bam.Net.Exclude]
		public static async Task BatchQuery(int batchSize, QueryFilter filter, Action<IEnumerable<ConfigurationSetting>> batchProcessor, Database database = null)
		{
			await BatchQuery(batchSize, (c) => filter, batchProcessor, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery(int batchSize, WhereDelegate<ConfigurationSettingColumns> where, Action<IEnumerable<ConfigurationSetting>> batchProcessor, Database database = null)
		{
			await Task.Run(async ()=>
			{
				ConfigurationSettingColumns columns = new ConfigurationSettingColumns();
				var orderBy = Bam.Net.Data.Order.By<ConfigurationSettingColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (ConfigurationSettingColumns)where(columns) && columns.KeyColumn > topId, orderBy, database);
				}
			});			
		}

		public static ConfigurationSetting GetById(int id, Database database = null)
		{
			return GetById((long)id, database);
		}

		public static ConfigurationSetting GetById(long id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static ConfigurationSetting GetByUuid(string uuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Uuid") == uuid, database);
		}

		public static ConfigurationSetting GetByCuid(string cuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Cuid") == cuid, database);
		}

		[Bam.Net.Exclude]
		public static ConfigurationSettingCollection Query(QueryFilter filter, Database database = null)
		{
			return Where(filter, database);
		}

		[Bam.Net.Exclude]		
		public static ConfigurationSettingCollection Where(QueryFilter filter, Database database = null)
		{
			WhereDelegate<ConfigurationSettingColumns> whereDelegate = (c) => filter;
			return Where(whereDelegate, database);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a ConfigurationSettingColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between ConfigurationSettingColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static ConfigurationSettingCollection Where(Func<ConfigurationSettingColumns, QueryFilter<ConfigurationSettingColumns>> where, OrderBy<ConfigurationSettingColumns> orderBy = null, Database database = null)
		{
			database = database ?? Db.For<ConfigurationSetting>();
			return new ConfigurationSettingCollection(database.GetQuery<ConfigurationSettingColumns, ConfigurationSetting>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ConfigurationSettingColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ConfigurationSettingColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static ConfigurationSettingCollection Where(WhereDelegate<ConfigurationSettingColumns> where, Database database = null)
		{		
			database = database ?? Db.For<ConfigurationSetting>();
			var results = new ConfigurationSettingCollection(database, database.GetQuery<ConfigurationSettingColumns, ConfigurationSetting>(where), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ConfigurationSettingColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ConfigurationSettingColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static ConfigurationSettingCollection Where(WhereDelegate<ConfigurationSettingColumns> where, OrderBy<ConfigurationSettingColumns> orderBy = null, Database database = null)
		{		
			database = database ?? Db.For<ConfigurationSetting>();
			var results = new ConfigurationSettingCollection(database, database.GetQuery<ConfigurationSettingColumns, ConfigurationSetting>(where, orderBy), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate&lt;ConfigurationSettingColumns&gt;.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static ConfigurationSettingCollection Where(QiQuery where, Database database = null)
		{
			var results = new ConfigurationSettingCollection(database, Select<ConfigurationSettingColumns>.From<ConfigurationSetting>().Where(where, database));
			return results;
		}
				
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		[Bam.Net.Exclude]
		public static ConfigurationSetting GetOneWhere(QueryFilter where, Database database = null)
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
		public static ConfigurationSetting OneWhere(QueryFilter where, Database database = null)
		{
			WhereDelegate<ConfigurationSettingColumns> whereDelegate = (c) => where;
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
		public static ConfigurationSetting GetOneWhere(WhereDelegate<ConfigurationSettingColumns> where, Database database = null)
		{
			var result = OneWhere(where, database);
			if(result == null)
			{
				ConfigurationSettingColumns c = new ConfigurationSettingColumns();
				IQueryFilter filter = where(c); 
				result = CreateFromFilter(filter, database);
			}

			return result;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single ConfigurationSetting instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ConfigurationSettingColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ConfigurationSettingColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static ConfigurationSetting OneWhere(WhereDelegate<ConfigurationSettingColumns> where, Database database = null)
		{
			var result = Top(1, where, database);
			return OneOrThrow(result);
		}
					 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<ConfigurationSettingColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static ConfigurationSetting OneWhere(QiQuery where, Database database = null)
		{
			var results = Top(1, where, database);
			return OneOrThrow(results);
		}

		/// <summary>
		/// Execute a query and return the first result.  This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ConfigurationSettingColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ConfigurationSettingColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static ConfigurationSetting FirstOneWhere(WhereDelegate<ConfigurationSettingColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a ConfigurationSettingColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ConfigurationSettingColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static ConfigurationSetting FirstOneWhere(WhereDelegate<ConfigurationSettingColumns> where, OrderBy<ConfigurationSettingColumns> orderBy, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a ConfigurationSettingColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ConfigurationSettingColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static ConfigurationSetting FirstOneWhere(QueryFilter where, OrderBy<ConfigurationSettingColumns> orderBy = null, Database database = null)
		{
			WhereDelegate<ConfigurationSettingColumns> whereDelegate = (c) => where;
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
		/// <param name="where">A WhereDelegate that recieves a ConfigurationSettingColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ConfigurationSettingColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static ConfigurationSettingCollection Top(int count, WhereDelegate<ConfigurationSettingColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a ConfigurationSettingColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ConfigurationSettingColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static ConfigurationSettingCollection Top(int count, WhereDelegate<ConfigurationSettingColumns> where, OrderBy<ConfigurationSettingColumns> orderBy, Database database = null)
		{
			ConfigurationSettingColumns c = new ConfigurationSettingColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database ?? Db.For<ConfigurationSetting>();
			QuerySet query = GetQuerySet(db); 
			query.Top<ConfigurationSetting>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<ConfigurationSettingColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<ConfigurationSettingCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static ConfigurationSettingCollection Top(int count, QueryFilter where, Database database)
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
		public static ConfigurationSettingCollection Top(int count, QueryFilter where, OrderBy<ConfigurationSettingColumns> orderBy = null, Database database = null)
		{
			Database db = database ?? Db.For<ConfigurationSetting>();
			QuerySet query = GetQuerySet(db);
			query.Top<ConfigurationSetting>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy<ConfigurationSettingColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<ConfigurationSettingCollection>(0);
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
		public static ConfigurationSettingCollection Top(int count, QiQuery where, Database database = null)
		{
			Database db = database ?? Db.For<ConfigurationSetting>();
			QuerySet query = GetQuerySet(db);
			query.Top<ConfigurationSetting>(count);
			query.Where(where);
			query.Execute(db);
			var results = query.Results.As<ConfigurationSettingCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Return the count of ConfigurationSettings
		/// </summary>
		public static long Count(Database database = null)
        {
			Database db = database ?? Db.For<ConfigurationSetting>();
            QuerySet query = GetQuerySet(db);
            query.Count<ConfigurationSetting>();
            query.Execute(db);
            return (long)query.Results[0].DataRow[0];
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ConfigurationSettingColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ConfigurationSettingColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static long Count(WhereDelegate<ConfigurationSettingColumns> where, Database database = null)
		{
			ConfigurationSettingColumns c = new ConfigurationSettingColumns();
			IQueryFilter filter = where(c) ;

			Database db = database ?? Db.For<ConfigurationSetting>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<ConfigurationSetting>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
		 
		public static long Count(QiQuery where, Database database = null)
		{
		    Database db = database ?? Db.For<ConfigurationSetting>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<ConfigurationSetting>();
			query.Where(where);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		} 		

		private static ConfigurationSetting CreateFromFilter(IQueryFilter filter, Database database = null)
		{
			Database db = database ?? Db.For<ConfigurationSetting>();			
			var dao = new ConfigurationSetting();
			filter.Parameters.Each(p=>
			{
				dao.Property(p.ColumnName, p.Value);
			});
			dao.Save(db);
			return dao;
		}
		
		private static ConfigurationSetting OneOrThrow(ConfigurationSettingCollection c)
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
