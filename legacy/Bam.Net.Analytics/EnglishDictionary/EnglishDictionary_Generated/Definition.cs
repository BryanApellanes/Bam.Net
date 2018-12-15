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

namespace Bam.Net.Analytics.EnglishDictionary
{
	// schema = EnglishDictionary
	// connection Name = EnglishDictionary
	[Serializable]
	[Bam.Net.Data.Table("Definition", "EnglishDictionary")]
	public partial class Definition: Bam.Net.Data.Dao
	{
		public Definition():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public Definition(DataRow data)
			: base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public Definition(Database db)
			: base(db)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public Definition(Database db, DataRow data)
			: base(db, data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		[Bam.Net.Exclude]
		public static implicit operator Definition(DataRow data)
		{
			return new Definition(data);
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

	// property:WordType, columnName:WordType	
	[Bam.Net.Data.Column(Name="WordType", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string WordType
	{
		get
		{
			return GetStringValue("WordType");
		}
		set
		{
			SetValue("WordType", value);
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



	// start WordId -> WordId
	[Bam.Net.Data.ForeignKey(
        Table="Definition",
		Name="WordId", 
		DbDataType="BigInt", 
		MaxLength="",
		AllowNull=true, 
		ReferencedKey="Id",
		ReferencedTable="Word",
		Suffix="1")]
	public ulong? WordId
	{
		get
		{
			return GetULongValue("WordId");
		}
		set
		{
			SetValue("WordId", value);
		}
	}

	Word _wordOfWordId;
	public Word WordOfWordId
	{
		get
		{
			if(_wordOfWordId == null)
			{
				_wordOfWordId = Bam.Net.Analytics.EnglishDictionary.Word.OneWhere(c => c.KeyColumn == this.WordId, this.Database);
			}
			return _wordOfWordId;
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
				var colFilter = new DefinitionColumns();
				return (colFilter.KeyColumn == IdValue);
			}			
		}

		/// <summary>
		/// Return every record in the Definition table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static DefinitionCollection LoadAll(Database database = null)
		{
			Database db = database ?? Db.For<Definition>();
			SqlStringBuilder sql = db.GetSqlStringBuilder();
			sql.Select<Definition>();
			var results = new DefinitionCollection(db, sql.GetDataTable(db))
			{
				Database = db
			};
			return results;
		}

		/// <summary>
		/// Process all records in batches of the specified size
		/// </summary>
		[Bam.Net.Exclude]
		public static async Task BatchAll(int batchSize, Action<IEnumerable<Definition>> batchProcessor, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				DefinitionColumns columns = new DefinitionColumns();
				var orderBy = Bam.Net.Data.Order.By<DefinitionColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
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
		public static async Task BatchQuery(int batchSize, QueryFilter filter, Action<IEnumerable<Definition>> batchProcessor, Database database = null)
		{
			await BatchQuery(batchSize, (c) => filter, batchProcessor, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery(int batchSize, WhereDelegate<DefinitionColumns> where, Action<IEnumerable<Definition>> batchProcessor, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				DefinitionColumns columns = new DefinitionColumns();
				var orderBy = Bam.Net.Data.Order.By<DefinitionColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (DefinitionColumns)where(columns) && columns.KeyColumn > topId, orderBy, database);
				}
			});			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>			 
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, QueryFilter filter, Action<IEnumerable<Definition>> batchProcessor, Bam.Net.Data.OrderBy<DefinitionColumns> orderBy, Database database = null)
		{
			await BatchQuery<ColType>(batchSize, (c) => filter, batchProcessor, orderBy, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, WhereDelegate<DefinitionColumns> where, Action<IEnumerable<Definition>> batchProcessor, Bam.Net.Data.OrderBy<DefinitionColumns> orderBy, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				DefinitionColumns columns = new DefinitionColumns();
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					ColType top = results.Select(d => d.Property<ColType>(orderBy.Column.ToString())).ToArray().Largest();
					results = Top(batchSize, (DefinitionColumns)where(columns) && orderBy.Column > top, orderBy, database);
				}
			});			
		}

		public static Definition GetById(uint id, Database database = null)
		{
			return GetById((ulong)id, database);
		}

		public static Definition GetById(int id, Database database = null)
		{
			return GetById((long)id, database);
		}

		public static Definition GetById(long id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static Definition GetById(ulong id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static Definition GetByUuid(string uuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Uuid") == uuid, database);
		}

		public static Definition GetByCuid(string cuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Cuid") == cuid, database);
		}

		[Bam.Net.Exclude]
		public static DefinitionCollection Query(QueryFilter filter, Database database = null)
		{
			return Where(filter, database);
		}

		[Bam.Net.Exclude]		
		public static DefinitionCollection Where(QueryFilter filter, Database database = null)
		{
			WhereDelegate<DefinitionColumns> whereDelegate = (c) => filter;
			return Where(whereDelegate, database);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a DefinitionColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between DefinitionColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static DefinitionCollection Where(Func<DefinitionColumns, QueryFilter<DefinitionColumns>> where, OrderBy<DefinitionColumns> orderBy = null, Database database = null)
		{
			database = database ?? Db.For<Definition>();
			return new DefinitionCollection(database.GetQuery<DefinitionColumns, Definition>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a DefinitionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DefinitionColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static DefinitionCollection Where(WhereDelegate<DefinitionColumns> where, Database database = null)
		{		
			database = database ?? Db.For<Definition>();
			var results = new DefinitionCollection(database, database.GetQuery<DefinitionColumns, Definition>(where), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a DefinitionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DefinitionColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static DefinitionCollection Where(WhereDelegate<DefinitionColumns> where, OrderBy<DefinitionColumns> orderBy = null, Database database = null)
		{		
			database = database ?? Db.For<Definition>();
			var results = new DefinitionCollection(database, database.GetQuery<DefinitionColumns, Definition>(where, orderBy), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate&lt;DefinitionColumns&gt;.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static DefinitionCollection Where(QiQuery where, Database database = null)
		{
			var results = new DefinitionCollection(database, Select<DefinitionColumns>.From<Definition>().Where(where, database));
			return results;
		}
				
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		[Bam.Net.Exclude]
		public static Definition GetOneWhere(QueryFilter where, Database database = null)
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
		public static Definition OneWhere(QueryFilter where, Database database = null)
		{
			WhereDelegate<DefinitionColumns> whereDelegate = (c) => where;
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
		public static Definition GetOneWhere(WhereDelegate<DefinitionColumns> where, Database database = null)
		{
			var result = OneWhere(where, database);
			if(result == null)
			{
				DefinitionColumns c = new DefinitionColumns();
				IQueryFilter filter = where(c); 
				result = CreateFromFilter(filter, database);
			}

			return result;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single Definition instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a DefinitionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DefinitionColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static Definition OneWhere(WhereDelegate<DefinitionColumns> where, Database database = null)
		{
			var result = Top(1, where, database);
			return OneOrThrow(result);
		}
					 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<DefinitionColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static Definition OneWhere(QiQuery where, Database database = null)
		{
			var results = Top(1, where, database);
			return OneOrThrow(results);
		}

		/// <summary>
		/// Execute a query and return the first result.  This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a DefinitionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DefinitionColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static Definition FirstOneWhere(WhereDelegate<DefinitionColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a DefinitionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DefinitionColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static Definition FirstOneWhere(WhereDelegate<DefinitionColumns> where, OrderBy<DefinitionColumns> orderBy, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a DefinitionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DefinitionColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static Definition FirstOneWhere(QueryFilter where, OrderBy<DefinitionColumns> orderBy = null, Database database = null)
		{
			WhereDelegate<DefinitionColumns> whereDelegate = (c) => where;
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
		/// <param name="where">A WhereDelegate that recieves a DefinitionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DefinitionColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static DefinitionCollection Top(int count, WhereDelegate<DefinitionColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a DefinitionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DefinitionColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static DefinitionCollection Top(int count, WhereDelegate<DefinitionColumns> where, OrderBy<DefinitionColumns> orderBy, Database database = null)
		{
			DefinitionColumns c = new DefinitionColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database ?? Db.For<Definition>();
			QuerySet query = GetQuerySet(db); 
			query.Top<Definition>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<DefinitionColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<DefinitionCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static DefinitionCollection Top(int count, QueryFilter where, Database database)
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
		public static DefinitionCollection Top(int count, QueryFilter where, OrderBy<DefinitionColumns> orderBy = null, Database database = null)
		{
			Database db = database ?? Db.For<Definition>();
			QuerySet query = GetQuerySet(db);
			query.Top<Definition>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy<DefinitionColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<DefinitionCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static DefinitionCollection Top(int count, QueryFilter where, string orderBy = null, SortOrder sortOrder = SortOrder.Ascending, Database database = null)
		{
			Database db = database ?? Db.For<Definition>();
			QuerySet query = GetQuerySet(db);
			query.Top<Definition>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy(orderBy, sortOrder);
			}

			query.Execute(db);
			var results = query.Results.As<DefinitionCollection>(0);
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
		public static DefinitionCollection Top(int count, QiQuery where, Database database = null)
		{
			Database db = database ?? Db.For<Definition>();
			QuerySet query = GetQuerySet(db);
			query.Top<Definition>(count);
			query.Where(where);
			query.Execute(db);
			var results = query.Results.As<DefinitionCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Return the count of Definitions
		/// </summary>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		public static long Count(Database database = null)
        {
			Database db = database ?? Db.For<Definition>();
            QuerySet query = GetQuerySet(db);
            query.Count<Definition>();
            query.Execute(db);
            return (long)query.Results[0].DataRow[0];
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a DefinitionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DefinitionColumns and other values
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static long Count(WhereDelegate<DefinitionColumns> where, Database database = null)
		{
			DefinitionColumns c = new DefinitionColumns();
			IQueryFilter filter = where(c) ;

			Database db = database ?? Db.For<Definition>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<Definition>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
		 
		public static long Count(QiQuery where, Database database = null)
		{
		    Database db = database ?? Db.For<Definition>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<Definition>();
			query.Where(where);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		} 		

		private static Definition CreateFromFilter(IQueryFilter filter, Database database = null)
		{
			Database db = database ?? Db.For<Definition>();			
			var dao = new Definition();
			filter.Parameters.Each(p=>
			{
				dao.Property(p.ColumnName, p.Value);
			});
			dao.Save(db);
			return dao;
		}
		
		private static Definition OneOrThrow(DefinitionCollection c)
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
