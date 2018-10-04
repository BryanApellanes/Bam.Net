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

namespace Bam.Net.Analytics
{
	// schema = Analytics
	// connection Name = Analytics
	[Serializable]
	[Bam.Net.Data.Table("Feature", "Analytics")]
	public partial class Feature: Bam.Net.Data.Dao
	{
		public Feature():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public Feature(DataRow data)
			: base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public Feature(Database db)
			: base(db)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public Feature(Database db, DataRow data)
			: base(db, data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		[Bam.Net.Exclude]
		public static implicit operator Feature(DataRow data)
		{
			return new Feature(data);
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

	// property:FeatureToCategoryCount, columnName:FeatureToCategoryCount	
	[Bam.Net.Data.Column(Name="FeatureToCategoryCount", DbDataType="BigInt", MaxLength="19", AllowNull=false)]
	public long? FeatureToCategoryCount
	{
		get
		{
			return GetLongValue("FeatureToCategoryCount");
		}
		set
		{
			SetValue("FeatureToCategoryCount", value);
		}
	}



	// start CategoryId -> CategoryId
	[Bam.Net.Data.ForeignKey(
        Table="Feature",
		Name="CategoryId", 
		DbDataType="BigInt", 
		MaxLength="",
		AllowNull=true, 
		ReferencedKey="Id",
		ReferencedTable="Category",
		Suffix="1")]
	public ulong? CategoryId
	{
		get
		{
			return GetULongValue("CategoryId");
		}
		set
		{
			SetValue("CategoryId", value);
		}
	}

	Category _categoryOfCategoryId;
	public Category CategoryOfCategoryId
	{
		get
		{
			if(_categoryOfCategoryId == null)
			{
				_categoryOfCategoryId = Bam.Net.Analytics.Category.OneWhere(c => c.KeyColumn == this.CategoryId, this.Database);
			}
			return _categoryOfCategoryId;
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
				var colFilter = new FeatureColumns();
				return (colFilter.KeyColumn == IdValue);
			}			
		}

		/// <summary>
		/// Return every record in the Feature table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static FeatureCollection LoadAll(Database database = null)
		{
			Database db = database ?? Db.For<Feature>();
			SqlStringBuilder sql = db.GetSqlStringBuilder();
			sql.Select<Feature>();
			var results = new FeatureCollection(db, sql.GetDataTable(db))
			{
				Database = db
			};
			return results;
		}

		/// <summary>
		/// Process all records in batches of the specified size
		/// </summary>
		[Bam.Net.Exclude]
		public static async Task BatchAll(int batchSize, Action<IEnumerable<Feature>> batchProcessor, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				FeatureColumns columns = new FeatureColumns();
				var orderBy = Bam.Net.Data.Order.By<FeatureColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
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
		public static async Task BatchQuery(int batchSize, QueryFilter filter, Action<IEnumerable<Feature>> batchProcessor, Database database = null)
		{
			await BatchQuery(batchSize, (c) => filter, batchProcessor, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery(int batchSize, WhereDelegate<FeatureColumns> where, Action<IEnumerable<Feature>> batchProcessor, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				FeatureColumns columns = new FeatureColumns();
				var orderBy = Bam.Net.Data.Order.By<FeatureColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (FeatureColumns)where(columns) && columns.KeyColumn > topId, orderBy, database);
				}
			});			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>			 
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, QueryFilter filter, Action<IEnumerable<Feature>> batchProcessor, Bam.Net.Data.OrderBy<FeatureColumns> orderBy, Database database = null)
		{
			await BatchQuery<ColType>(batchSize, (c) => filter, batchProcessor, orderBy, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, WhereDelegate<FeatureColumns> where, Action<IEnumerable<Feature>> batchProcessor, Bam.Net.Data.OrderBy<FeatureColumns> orderBy, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				FeatureColumns columns = new FeatureColumns();
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					ColType top = results.Select(d => d.Property<ColType>(orderBy.Column.ToString())).ToArray().Largest();
					results = Top(batchSize, (FeatureColumns)where(columns) && orderBy.Column > top, orderBy, database);
				}
			});			
		}

		public static Feature GetById(uint id, Database database = null)
		{
			return GetById((ulong)id, database);
		}

		public static Feature GetById(int id, Database database = null)
		{
			return GetById((long)id, database);
		}

		public static Feature GetById(long id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static Feature GetById(ulong id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static Feature GetByUuid(string uuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Uuid") == uuid, database);
		}

		public static Feature GetByCuid(string cuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Cuid") == cuid, database);
		}

		[Bam.Net.Exclude]
		public static FeatureCollection Query(QueryFilter filter, Database database = null)
		{
			return Where(filter, database);
		}

		[Bam.Net.Exclude]		
		public static FeatureCollection Where(QueryFilter filter, Database database = null)
		{
			WhereDelegate<FeatureColumns> whereDelegate = (c) => filter;
			return Where(whereDelegate, database);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a FeatureColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between FeatureColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static FeatureCollection Where(Func<FeatureColumns, QueryFilter<FeatureColumns>> where, OrderBy<FeatureColumns> orderBy = null, Database database = null)
		{
			database = database ?? Db.For<Feature>();
			return new FeatureCollection(database.GetQuery<FeatureColumns, Feature>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a FeatureColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between FeatureColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static FeatureCollection Where(WhereDelegate<FeatureColumns> where, Database database = null)
		{		
			database = database ?? Db.For<Feature>();
			var results = new FeatureCollection(database, database.GetQuery<FeatureColumns, Feature>(where), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a FeatureColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between FeatureColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static FeatureCollection Where(WhereDelegate<FeatureColumns> where, OrderBy<FeatureColumns> orderBy = null, Database database = null)
		{		
			database = database ?? Db.For<Feature>();
			var results = new FeatureCollection(database, database.GetQuery<FeatureColumns, Feature>(where, orderBy), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate&lt;FeatureColumns&gt;.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static FeatureCollection Where(QiQuery where, Database database = null)
		{
			var results = new FeatureCollection(database, Select<FeatureColumns>.From<Feature>().Where(where, database));
			return results;
		}
				
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		[Bam.Net.Exclude]
		public static Feature GetOneWhere(QueryFilter where, Database database = null)
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
		public static Feature OneWhere(QueryFilter where, Database database = null)
		{
			WhereDelegate<FeatureColumns> whereDelegate = (c) => where;
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
		public static Feature GetOneWhere(WhereDelegate<FeatureColumns> where, Database database = null)
		{
			var result = OneWhere(where, database);
			if(result == null)
			{
				FeatureColumns c = new FeatureColumns();
				IQueryFilter filter = where(c); 
				result = CreateFromFilter(filter, database);
			}

			return result;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single Feature instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a FeatureColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between FeatureColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static Feature OneWhere(WhereDelegate<FeatureColumns> where, Database database = null)
		{
			var result = Top(1, where, database);
			return OneOrThrow(result);
		}
					 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<FeatureColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static Feature OneWhere(QiQuery where, Database database = null)
		{
			var results = Top(1, where, database);
			return OneOrThrow(results);
		}

		/// <summary>
		/// Execute a query and return the first result.  This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a FeatureColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between FeatureColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static Feature FirstOneWhere(WhereDelegate<FeatureColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a FeatureColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between FeatureColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static Feature FirstOneWhere(WhereDelegate<FeatureColumns> where, OrderBy<FeatureColumns> orderBy, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a FeatureColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between FeatureColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static Feature FirstOneWhere(QueryFilter where, OrderBy<FeatureColumns> orderBy = null, Database database = null)
		{
			WhereDelegate<FeatureColumns> whereDelegate = (c) => where;
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
		/// <param name="where">A WhereDelegate that recieves a FeatureColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between FeatureColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static FeatureCollection Top(int count, WhereDelegate<FeatureColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a FeatureColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between FeatureColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static FeatureCollection Top(int count, WhereDelegate<FeatureColumns> where, OrderBy<FeatureColumns> orderBy, Database database = null)
		{
			FeatureColumns c = new FeatureColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database ?? Db.For<Feature>();
			QuerySet query = GetQuerySet(db); 
			query.Top<Feature>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<FeatureColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<FeatureCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static FeatureCollection Top(int count, QueryFilter where, Database database)
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
		public static FeatureCollection Top(int count, QueryFilter where, OrderBy<FeatureColumns> orderBy = null, Database database = null)
		{
			Database db = database ?? Db.For<Feature>();
			QuerySet query = GetQuerySet(db);
			query.Top<Feature>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy<FeatureColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<FeatureCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static FeatureCollection Top(int count, QueryFilter where, string orderBy = null, SortOrder sortOrder = SortOrder.Ascending, Database database = null)
		{
			Database db = database ?? Db.For<Feature>();
			QuerySet query = GetQuerySet(db);
			query.Top<Feature>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy(orderBy, sortOrder);
			}

			query.Execute(db);
			var results = query.Results.As<FeatureCollection>(0);
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
		public static FeatureCollection Top(int count, QiQuery where, Database database = null)
		{
			Database db = database ?? Db.For<Feature>();
			QuerySet query = GetQuerySet(db);
			query.Top<Feature>(count);
			query.Where(where);
			query.Execute(db);
			var results = query.Results.As<FeatureCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Return the count of Features
		/// </summary>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		public static long Count(Database database = null)
        {
			Database db = database ?? Db.For<Feature>();
            QuerySet query = GetQuerySet(db);
            query.Count<Feature>();
            query.Execute(db);
            return (long)query.Results[0].DataRow[0];
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a FeatureColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between FeatureColumns and other values
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static long Count(WhereDelegate<FeatureColumns> where, Database database = null)
		{
			FeatureColumns c = new FeatureColumns();
			IQueryFilter filter = where(c) ;

			Database db = database ?? Db.For<Feature>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<Feature>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
		 
		public static long Count(QiQuery where, Database database = null)
		{
		    Database db = database ?? Db.For<Feature>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<Feature>();
			query.Where(where);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		} 		

		private static Feature CreateFromFilter(IQueryFilter filter, Database database = null)
		{
			Database db = database ?? Db.For<Feature>();			
			var dao = new Feature();
			filter.Parameters.Each(p=>
			{
				dao.Property(p.ColumnName, p.Value);
			});
			dao.Save(db);
			return dao;
		}
		
		private static Feature OneOrThrow(FeatureCollection c)
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
