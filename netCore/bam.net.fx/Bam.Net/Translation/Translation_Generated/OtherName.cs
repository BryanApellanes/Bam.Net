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

namespace Bam.Net.Translation
{
	// schema = Translation
	// connection Name = Translation
	[Serializable]
	[Bam.Net.Data.Table("OtherName", "Translation")]
	public partial class OtherName: Bam.Net.Data.Dao
	{
		public OtherName():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public OtherName(DataRow data)
			: base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public OtherName(Database db)
			: base(db)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public OtherName(Database db, DataRow data)
			: base(db, data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		[Bam.Net.Exclude]
		public static implicit operator OtherName(DataRow data)
		{
			return new OtherName(data);
		}

		private void SetChildren()
		{
						
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

	// property:LanguageName, columnName:LanguageName	
	[Bam.Net.Data.Column(Name="LanguageName", DbDataType="VarChar", MaxLength="4000", AllowNull=false)]
	public string LanguageName
	{
		get
		{
			return GetStringValue("LanguageName");
		}
		set
		{
			SetValue("LanguageName", value);
		}
	}

	// property:Value, columnName:Value	
	[Bam.Net.Data.Column(Name="Value", DbDataType="VarChar", MaxLength="4000", AllowNull=false)]
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



	// start LanguageId -> LanguageId
	[Bam.Net.Data.ForeignKey(
        Table="OtherName",
		Name="LanguageId", 
		DbDataType="BigInt", 
		MaxLength="",
		AllowNull=true, 
		ReferencedKey="Id",
		ReferencedTable="Language",
		Suffix="1")]
	public ulong? LanguageId
	{
		get
		{
			return GetULongValue("LanguageId");
		}
		set
		{
			SetValue("LanguageId", value);
		}
	}

	Language _languageOfLanguageId;
	public Language LanguageOfLanguageId
	{
		get
		{
			if(_languageOfLanguageId == null)
			{
				_languageOfLanguageId = Bam.Net.Translation.Language.OneWhere(c => c.KeyColumn == this.LanguageId, this.Database);
			}
			return _languageOfLanguageId;
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
				var colFilter = new OtherNameColumns();
				return (colFilter.KeyColumn == IdValue);
			}			
		}

		/// <summary>
		/// Return every record in the OtherName table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static OtherNameCollection LoadAll(Database database = null)
		{
			Database db = database ?? Db.For<OtherName>();
			SqlStringBuilder sql = db.GetSqlStringBuilder();
			sql.Select<OtherName>();
			var results = new OtherNameCollection(db, sql.GetDataTable(db))
			{
				Database = db
			};
			return results;
		}

		/// <summary>
		/// Process all records in batches of the specified size
		/// </summary>
		[Bam.Net.Exclude]
		public static async Task BatchAll(int batchSize, Action<IEnumerable<OtherName>> batchProcessor, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				OtherNameColumns columns = new OtherNameColumns();
				var orderBy = Bam.Net.Data.Order.By<OtherNameColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
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
		public static async Task BatchQuery(int batchSize, QueryFilter filter, Action<IEnumerable<OtherName>> batchProcessor, Database database = null)
		{
			await BatchQuery(batchSize, (c) => filter, batchProcessor, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery(int batchSize, WhereDelegate<OtherNameColumns> where, Action<IEnumerable<OtherName>> batchProcessor, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				OtherNameColumns columns = new OtherNameColumns();
				var orderBy = Bam.Net.Data.Order.By<OtherNameColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (OtherNameColumns)where(columns) && columns.KeyColumn > topId, orderBy, database);
				}
			});			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>			 
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, QueryFilter filter, Action<IEnumerable<OtherName>> batchProcessor, Bam.Net.Data.OrderBy<OtherNameColumns> orderBy, Database database = null)
		{
			await BatchQuery<ColType>(batchSize, (c) => filter, batchProcessor, orderBy, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, WhereDelegate<OtherNameColumns> where, Action<IEnumerable<OtherName>> batchProcessor, Bam.Net.Data.OrderBy<OtherNameColumns> orderBy, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				OtherNameColumns columns = new OtherNameColumns();
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					ColType top = results.Select(d => d.Property<ColType>(orderBy.Column.ToString())).ToArray().Largest();
					results = Top(batchSize, (OtherNameColumns)where(columns) && orderBy.Column > top, orderBy, database);
				}
			});			
		}

		public static OtherName GetById(uint id, Database database = null)
		{
			return GetById((ulong)id, database);
		}

		public static OtherName GetById(int id, Database database = null)
		{
			return GetById((long)id, database);
		}

		public static OtherName GetById(long id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static OtherName GetById(ulong id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static OtherName GetByUuid(string uuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Uuid") == uuid, database);
		}

		public static OtherName GetByCuid(string cuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Cuid") == cuid, database);
		}

		[Bam.Net.Exclude]
		public static OtherNameCollection Query(QueryFilter filter, Database database = null)
		{
			return Where(filter, database);
		}

		[Bam.Net.Exclude]		
		public static OtherNameCollection Where(QueryFilter filter, Database database = null)
		{
			WhereDelegate<OtherNameColumns> whereDelegate = (c) => filter;
			return Where(whereDelegate, database);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a OtherNameColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between OtherNameColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static OtherNameCollection Where(Func<OtherNameColumns, QueryFilter<OtherNameColumns>> where, OrderBy<OtherNameColumns> orderBy = null, Database database = null)
		{
			database = database ?? Db.For<OtherName>();
			return new OtherNameCollection(database.GetQuery<OtherNameColumns, OtherName>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a OtherNameColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between OtherNameColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static OtherNameCollection Where(WhereDelegate<OtherNameColumns> where, Database database = null)
		{		
			database = database ?? Db.For<OtherName>();
			var results = new OtherNameCollection(database, database.GetQuery<OtherNameColumns, OtherName>(where), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a OtherNameColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between OtherNameColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static OtherNameCollection Where(WhereDelegate<OtherNameColumns> where, OrderBy<OtherNameColumns> orderBy = null, Database database = null)
		{		
			database = database ?? Db.For<OtherName>();
			var results = new OtherNameCollection(database, database.GetQuery<OtherNameColumns, OtherName>(where, orderBy), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate&lt;OtherNameColumns&gt;.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static OtherNameCollection Where(QiQuery where, Database database = null)
		{
			var results = new OtherNameCollection(database, Select<OtherNameColumns>.From<OtherName>().Where(where, database));
			return results;
		}
				
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		[Bam.Net.Exclude]
		public static OtherName GetOneWhere(QueryFilter where, Database database = null)
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
		public static OtherName OneWhere(QueryFilter where, Database database = null)
		{
			WhereDelegate<OtherNameColumns> whereDelegate = (c) => where;
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
		public static OtherName GetOneWhere(WhereDelegate<OtherNameColumns> where, Database database = null)
		{
			var result = OneWhere(where, database);
			if(result == null)
			{
				OtherNameColumns c = new OtherNameColumns();
				IQueryFilter filter = where(c); 
				result = CreateFromFilter(filter, database);
			}

			return result;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single OtherName instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a OtherNameColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between OtherNameColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static OtherName OneWhere(WhereDelegate<OtherNameColumns> where, Database database = null)
		{
			var result = Top(1, where, database);
			return OneOrThrow(result);
		}
					 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<OtherNameColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static OtherName OneWhere(QiQuery where, Database database = null)
		{
			var results = Top(1, where, database);
			return OneOrThrow(results);
		}

		/// <summary>
		/// Execute a query and return the first result.  This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a OtherNameColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between OtherNameColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static OtherName FirstOneWhere(WhereDelegate<OtherNameColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a OtherNameColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between OtherNameColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static OtherName FirstOneWhere(WhereDelegate<OtherNameColumns> where, OrderBy<OtherNameColumns> orderBy, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a OtherNameColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between OtherNameColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static OtherName FirstOneWhere(QueryFilter where, OrderBy<OtherNameColumns> orderBy = null, Database database = null)
		{
			WhereDelegate<OtherNameColumns> whereDelegate = (c) => where;
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
		/// <param name="where">A WhereDelegate that recieves a OtherNameColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between OtherNameColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static OtherNameCollection Top(int count, WhereDelegate<OtherNameColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a OtherNameColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between OtherNameColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static OtherNameCollection Top(int count, WhereDelegate<OtherNameColumns> where, OrderBy<OtherNameColumns> orderBy, Database database = null)
		{
			OtherNameColumns c = new OtherNameColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database ?? Db.For<OtherName>();
			QuerySet query = GetQuerySet(db); 
			query.Top<OtherName>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<OtherNameColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<OtherNameCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static OtherNameCollection Top(int count, QueryFilter where, Database database)
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
		public static OtherNameCollection Top(int count, QueryFilter where, OrderBy<OtherNameColumns> orderBy = null, Database database = null)
		{
			Database db = database ?? Db.For<OtherName>();
			QuerySet query = GetQuerySet(db);
			query.Top<OtherName>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy<OtherNameColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<OtherNameCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static OtherNameCollection Top(int count, QueryFilter where, string orderBy = null, SortOrder sortOrder = SortOrder.Ascending, Database database = null)
		{
			Database db = database ?? Db.For<OtherName>();
			QuerySet query = GetQuerySet(db);
			query.Top<OtherName>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy(orderBy, sortOrder);
			}

			query.Execute(db);
			var results = query.Results.As<OtherNameCollection>(0);
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
		public static OtherNameCollection Top(int count, QiQuery where, Database database = null)
		{
			Database db = database ?? Db.For<OtherName>();
			QuerySet query = GetQuerySet(db);
			query.Top<OtherName>(count);
			query.Where(where);
			query.Execute(db);
			var results = query.Results.As<OtherNameCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Return the count of OtherNames
		/// </summary>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		public static long Count(Database database = null)
        {
			Database db = database ?? Db.For<OtherName>();
            QuerySet query = GetQuerySet(db);
            query.Count<OtherName>();
            query.Execute(db);
            return (long)query.Results[0].DataRow[0];
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a OtherNameColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between OtherNameColumns and other values
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static long Count(WhereDelegate<OtherNameColumns> where, Database database = null)
		{
			OtherNameColumns c = new OtherNameColumns();
			IQueryFilter filter = where(c) ;

			Database db = database ?? Db.For<OtherName>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<OtherName>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
		 
		public static long Count(QiQuery where, Database database = null)
		{
		    Database db = database ?? Db.For<OtherName>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<OtherName>();
			query.Where(where);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		} 		

		private static OtherName CreateFromFilter(IQueryFilter filter, Database database = null)
		{
			Database db = database ?? Db.For<OtherName>();			
			var dao = new OtherName();
			filter.Parameters.Each(p=>
			{
				dao.Property(p.ColumnName, p.Value);
			});
			dao.Save(db);
			return dao;
		}
		
		private static OtherName OneOrThrow(OtherNameCollection c)
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
