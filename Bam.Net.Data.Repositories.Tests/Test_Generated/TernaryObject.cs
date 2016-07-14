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

namespace Bam.Net.Data.Repositories.Tests
{
	// schema = RepoTests
	// connection Name = RepoTests
	[Serializable]
	[Bam.Net.Data.Table("TernaryObject", "RepoTests")]
	public partial class TernaryObject: Dao
	{
		public TernaryObject():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public TernaryObject(DataRow data)
			: base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public TernaryObject(Database db)
			: base(db)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public TernaryObject(Database db, DataRow data)
			: base(db, data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public static implicit operator TernaryObject(DataRow data)
		{
			return new TernaryObject(data);
		}

		private void SetChildren()
		{

            this.ChildCollections.Add("SecondaryObjectTernaryObject_TernaryObjectId", new SecondaryObjectTernaryObjectCollection(Database.GetQuery<SecondaryObjectTernaryObjectColumns, SecondaryObjectTernaryObject>((c) => c.TernaryObjectId == GetLongValue("Id")), this, "TernaryObjectId"));							
            this.ChildCollections.Add("TernaryObject_SecondaryObjectTernaryObject_SecondaryObject",  new XrefDaoCollection<SecondaryObjectTernaryObject, SecondaryObject>(this, false));
				
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

	// property:Created, columnName:Created	
	[Bam.Net.Data.Column(Name="Created", DbDataType="DateTime", MaxLength="8", AllowNull=false)]
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



				

	[Exclude]	
	public SecondaryObjectTernaryObjectCollection SecondaryObjectTernaryObjectsByTernaryObjectId
	{
		get
		{
			if (this.IsNew)
			{
				throw new InvalidOperationException("The current instance of type({0}) hasn't been saved and will have no child collections, call Save() or Save(Database) first."._Format(this.GetType().Name));
			}

			if(!this.ChildCollections.ContainsKey("SecondaryObjectTernaryObject_TernaryObjectId"))
			{
				SetChildren();
			}

			var c = (SecondaryObjectTernaryObjectCollection)this.ChildCollections["SecondaryObjectTernaryObject_TernaryObjectId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
			


		// Xref       
        public XrefDaoCollection<SecondaryObjectTernaryObject, SecondaryObject> SecondaryObjects
        {
            get
            {			
				if (this.IsNew)
				{
					throw new InvalidOperationException("The current instance of type({0}) hasn't been saved and will have no child collections, call Save() or Save(Database) first."._Format(this.GetType().Name));
				}

				if(!this.ChildCollections.ContainsKey("TernaryObject_SecondaryObjectTernaryObject_SecondaryObject"))
				{
					SetChildren();
				}

				var xref = (XrefDaoCollection<SecondaryObjectTernaryObject, SecondaryObject>)this.ChildCollections["TernaryObject_SecondaryObjectTernaryObject_SecondaryObject"];
				if(!xref.Loaded)
				{
					xref.Load(Database);
				}

				return xref;
            }
        }		/// <summary>
		/// Gets a query filter that should uniquely identify
		/// the current instance.  The default implementation
		/// compares the Id/key field to the current instance's.
		/// </summary> 
		public override IQueryFilter GetUniqueFilter()
		{
			if(UniqueFilterProvider != null)
			{
				return UniqueFilterProvider();
			}
			else
			{
				var colFilter = new TernaryObjectColumns();
				return (colFilter.KeyColumn == IdValue);
			}			
		}

		/// <summary>
		/// Return every record in the TernaryObject table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static TernaryObjectCollection LoadAll(Database database = null)
		{
			SqlStringBuilder sql = new SqlStringBuilder();
			sql.Select<TernaryObject>();
			Database db = database ?? Db.For<TernaryObject>();
			var results = new TernaryObjectCollection(sql.GetDataTable(db));
			results.Database = db;
			return results;
		}

		public static async Task BatchAll(int batchSize, Action<IEnumerable<TernaryObject>> batchProcessor, Database database = null)
		{
			await Task.Run(async ()=>
			{
				TernaryObjectColumns columns = new TernaryObjectColumns();
				var orderBy = Order.By<TernaryObjectColumns>(c => c.KeyColumn, SortOrder.Ascending);
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

		public static async Task BatchQuery(int batchSize, QueryFilter filter, Action<IEnumerable<TernaryObject>> batchProcessor, Database database = null)
		{
			await BatchQuery(batchSize, (c) => filter, batchProcessor, database);			
		}

		public static async Task BatchQuery(int batchSize, WhereDelegate<TernaryObjectColumns> where, Action<IEnumerable<TernaryObject>> batchProcessor, Database database = null)
		{
			await Task.Run(async ()=>
			{
				TernaryObjectColumns columns = new TernaryObjectColumns();
				var orderBy = Order.By<TernaryObjectColumns>(c => c.KeyColumn, SortOrder.Ascending);
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (TernaryObjectColumns)where(columns) && columns.KeyColumn > topId, orderBy, database);
				}
			});			
		}

		public static TernaryObject GetById(int id, Database database = null)
		{
			return GetById((long)id, database);
		}

		public static TernaryObject GetById(long id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static TernaryObject GetByUuid(string uuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Uuid") == uuid, database);
		}

		public static TernaryObject GetByCuid(string cuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Cuid") == cuid, database);
		}

		public static TernaryObjectCollection Query(QueryFilter filter, Database database = null)
		{
			return Where(filter, database);
		}
				
		public static TernaryObjectCollection Where(QueryFilter filter, Database database = null)
		{
			WhereDelegate<TernaryObjectColumns> whereDelegate = (c) => filter;
			return Where(whereDelegate, database);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a TernaryObjectColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between TernaryObjectColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static TernaryObjectCollection Where(Func<TernaryObjectColumns, QueryFilter<TernaryObjectColumns>> where, OrderBy<TernaryObjectColumns> orderBy = null, Database database = null)
		{
			database = database ?? Db.For<TernaryObject>();
			return new TernaryObjectCollection(database.GetQuery<TernaryObjectColumns, TernaryObject>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a TernaryObjectColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between TernaryObjectColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static TernaryObjectCollection Where(WhereDelegate<TernaryObjectColumns> where, Database database = null)
		{		
			database = database ?? Db.For<TernaryObject>();
			var results = new TernaryObjectCollection(database, database.GetQuery<TernaryObjectColumns, TernaryObject>(where), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a TernaryObjectColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between TernaryObjectColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		public static TernaryObjectCollection Where(WhereDelegate<TernaryObjectColumns> where, OrderBy<TernaryObjectColumns> orderBy = null, Database database = null)
		{		
			database = database ?? Db.For<TernaryObject>();
			var results = new TernaryObjectCollection(database, database.GetQuery<TernaryObjectColumns, TernaryObject>(where, orderBy), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate&lt;TernaryObjectColumns&gt;.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static TernaryObjectCollection Where(QiQuery where, Database database = null)
		{
			var results = new TernaryObjectCollection(database, Select<TernaryObjectColumns>.From<TernaryObject>().Where(where, database));
			return results;
		}
				
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		public static TernaryObject GetOneWhere(QueryFilter where, Database database = null)
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
		public static TernaryObject OneWhere(QueryFilter where, Database database = null)
		{
			WhereDelegate<TernaryObjectColumns> whereDelegate = (c) => where;
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
		public static TernaryObject GetOneWhere(WhereDelegate<TernaryObjectColumns> where, Database database = null)
		{
			var result = OneWhere(where, database);
			if(result == null)
			{
				TernaryObjectColumns c = new TernaryObjectColumns();
				IQueryFilter filter = where(c); 
				result = CreateFromFilter(filter, database);
			}

			return result;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single TernaryObject instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a TernaryObjectColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between TernaryObjectColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static TernaryObject OneWhere(WhereDelegate<TernaryObjectColumns> where, Database database = null)
		{
			var result = Top(1, where, database);
			return OneOrThrow(result);
		}
					 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<TernaryObjectColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static TernaryObject OneWhere(QiQuery where, Database database = null)
		{
			var results = Top(1, where, database);
			return OneOrThrow(results);
		}

		/// <summary>
		/// Execute a query and return the first result.  This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a TernaryObjectColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between TernaryObjectColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static TernaryObject FirstOneWhere(WhereDelegate<TernaryObjectColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a TernaryObjectColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between TernaryObjectColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static TernaryObject FirstOneWhere(WhereDelegate<TernaryObjectColumns> where, OrderBy<TernaryObjectColumns> orderBy, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a TernaryObjectColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between TernaryObjectColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static TernaryObject FirstOneWhere(QueryFilter where, OrderBy<TernaryObjectColumns> orderBy = null, Database database = null)
		{
			WhereDelegate<TernaryObjectColumns> whereDelegate = (c) => where;
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
		/// <param name="where">A WhereDelegate that recieves a TernaryObjectColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between TernaryObjectColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static TernaryObjectCollection Top(int count, WhereDelegate<TernaryObjectColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a TernaryObjectColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between TernaryObjectColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		public static TernaryObjectCollection Top(int count, WhereDelegate<TernaryObjectColumns> where, OrderBy<TernaryObjectColumns> orderBy, Database database = null)
		{
			TernaryObjectColumns c = new TernaryObjectColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database ?? Db.For<TernaryObject>();
			QuerySet query = GetQuerySet(db); 
			query.Top<TernaryObject>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<TernaryObjectColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<TernaryObjectCollection>(0);
			results.Database = db;
			return results;
		}

		public static TernaryObjectCollection Top(int count, QueryFilter where, Database database)
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
		public static TernaryObjectCollection Top(int count, QueryFilter where, OrderBy<TernaryObjectColumns> orderBy = null, Database database = null)
		{
			Database db = database ?? Db.For<TernaryObject>();
			QuerySet query = GetQuerySet(db);
			query.Top<TernaryObject>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy<TernaryObjectColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<TernaryObjectCollection>(0);
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
		public static TernaryObjectCollection Top(int count, QiQuery where, Database database = null)
		{
			Database db = database ?? Db.For<TernaryObject>();
			QuerySet query = GetQuerySet(db);
			query.Top<TernaryObject>(count);
			query.Where(where);
			query.Execute(db);
			var results = query.Results.As<TernaryObjectCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Return the count of TernaryObjects
		/// </summary>
		public static long Count(Database database = null)
        {
			Database db = database ?? Db.For<TernaryObject>();
            QuerySet query = GetQuerySet(db);
            query.Count<TernaryObject>();
            query.Execute(db);
            return (long)query.Results[0].DataRow[0];
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a TernaryObjectColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between TernaryObjectColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<TernaryObjectColumns> where, Database database = null)
		{
			TernaryObjectColumns c = new TernaryObjectColumns();
			IQueryFilter filter = where(c) ;

			Database db = database ?? Db.For<TernaryObject>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<TernaryObject>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}

		private static TernaryObject CreateFromFilter(IQueryFilter filter, Database database = null)
		{
			Database db = database ?? Db.For<TernaryObject>();			
			var dao = new TernaryObject();
			filter.Parameters.Each(p=>
			{
				dao.Property(p.ColumnName, p.Value);
			});
			dao.Save(db);
			return dao;
		}
		
		private static TernaryObject OneOrThrow(TernaryObjectCollection c)
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
