/*
	This file was generated and should not be modified directly
*/
// Model is Table
using System;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Bam.Net;
using Bam.Net.Data;
using Bam.Net.Data.Qi;

namespace Bam.Net.Data.Repositories.Tests.ClrTypes._Dao_
{
	// schema = TestDaoSchema
	// connection Name = TestDaoSchema
	[Serializable]
	[Bam.Net.Data.Table("HouseDao", "TestDaoSchema")]
	public partial class HouseDao: Dao
	{
		public HouseDao():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public HouseDao(DataRow data)
			: base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public HouseDao(Database db)
			: base(db)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public HouseDao(Database db, DataRow data)
			: base(db, data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public static implicit operator HouseDao(DataRow data)
		{
			return new HouseDao(data);
		}

		private void SetChildren()
		{

            this.ChildCollections.Add("HouseDaoParentDao_HouseDaoId", new HouseDaoParentDaoCollection(Database.GetQuery<HouseDaoParentDaoColumns, HouseDaoParentDao>((c) => c.HouseDaoId == GetLongValue("Id")), this, "HouseDaoId"));				
            this.ChildCollections.Add("HouseDao_HouseDaoParentDao_ParentDao",  new XrefDaoCollection<HouseDaoParentDao, ParentDao>(this, false));
							
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

	// property:Name, columnName:Name	
	[Bam.Net.Data.Column(Name="Name", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
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
	public HouseDaoParentDaoCollection HouseDaoParentDaosByHouseDaoId
	{
		get
		{
			if (this.IsNew)
			{
				throw new InvalidOperationException("The current instance of type({0}) hasn't been saved and will have no child collections, call Save() or Save(Database) first."._Format(this.GetType().Name));
			}

			if(!this.ChildCollections.ContainsKey("HouseDaoParentDao_HouseDaoId"))
			{
				SetChildren();
			}

			var c = (HouseDaoParentDaoCollection)this.ChildCollections["HouseDaoParentDao_HouseDaoId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
			

		// Xref       
        public XrefDaoCollection<HouseDaoParentDao, ParentDao> ParentDaos
        {
            get
            {			
				if (this.IsNew)
				{
					throw new InvalidOperationException("The current instance of type({0}) hasn't been saved and will have no child collections, call Save() or Save(Database) first."._Format(this.GetType().Name));
				}

				if(!this.ChildCollections.ContainsKey("HouseDao_HouseDaoParentDao_ParentDao"))
				{
					SetChildren();
				}

				var xref = (XrefDaoCollection<HouseDaoParentDao, ParentDao>)this.ChildCollections["HouseDao_HouseDaoParentDao_ParentDao"];
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
		public override IQueryFilter GetUniqueFilter()
		{
			if(UniqueFilterProvider != null)
			{
				return UniqueFilterProvider();
			}
			else
			{
				var colFilter = new HouseDaoColumns();
				return (colFilter.KeyColumn == IdValue);
			}			
		}

		/// <summary>
		/// Return every record in the HouseDao table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static HouseDaoCollection LoadAll(Database database = null)
		{
			SqlStringBuilder sql = new SqlStringBuilder();
			sql.Select<HouseDao>();
			Database db = database ?? Db.For<HouseDao>();
			var results = new HouseDaoCollection(sql.GetDataTable(db));
			results.Database = db;
			return results;
		}

		public static async Task BatchAll(int batchSize, Func<HouseDaoCollection, Task> batchProcessor, Database database = null)
		{
			await Task.Run(async ()=>
			{
				HouseDaoColumns columns = new HouseDaoColumns();
				var orderBy = Order.By<HouseDaoColumns>(c => c.KeyColumn, SortOrder.Ascending);
				var results = Top(batchSize, (c) => c.KeyColumn > 0, orderBy, database);
				while(results.Count > 0)
				{
					await batchProcessor(results);
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (c) => c.KeyColumn > topId, orderBy, database);
				}
			});			
		}	 

		public static async Task BatchQuery(int batchSize, QueryFilter filter, Func<HouseDaoCollection, Task> batchProcessor, Database database = null)
		{
			await BatchQuery(batchSize, (c) => filter, batchProcessor, database);			
		}

		public static async Task BatchQuery(int batchSize, WhereDelegate<HouseDaoColumns> where, Func<HouseDaoCollection, Task> batchProcessor, Database database = null)
		{
			await Task.Run(async ()=>
			{
				HouseDaoColumns columns = new HouseDaoColumns();
				var orderBy = Order.By<HouseDaoColumns>(c => c.KeyColumn, SortOrder.Ascending);
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await batchProcessor(results);
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (HouseDaoColumns)where(columns) && columns.KeyColumn > topId, orderBy, database);
				}
			});			
		}

		public static HouseDao GetById(int id, Database database = null)
		{
			return GetById((long)id, database);
		}

		public static HouseDao GetById(long id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static HouseDao GetByUuid(string uuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Uuid") == uuid, database);
		}

		public static HouseDao GetByCuid(string cuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Cuid") == cuid, database);
		}

		public static HouseDaoCollection Query(QueryFilter filter, Database database = null)
		{
			return Where(filter, database);
		}
				
		public static HouseDaoCollection Where(QueryFilter filter, Database database = null)
		{
			WhereDelegate<HouseDaoColumns> whereDelegate = (c) => filter;
			return Where(whereDelegate, database);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a HouseDaoColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between HouseDaoColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static HouseDaoCollection Where(Func<HouseDaoColumns, QueryFilter<HouseDaoColumns>> where, OrderBy<HouseDaoColumns> orderBy = null, Database database = null)
		{
			database = database ?? Db.For<HouseDao>();
			return new HouseDaoCollection(database.GetQuery<HouseDaoColumns, HouseDao>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a HouseDaoColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between HouseDaoColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static HouseDaoCollection Where(WhereDelegate<HouseDaoColumns> where, Database database = null)
		{		
			database = database ?? Db.For<HouseDao>();
			var results = new HouseDaoCollection(database, database.GetQuery<HouseDaoColumns, HouseDao>(where), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a HouseDaoColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between HouseDaoColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		public static HouseDaoCollection Where(WhereDelegate<HouseDaoColumns> where, OrderBy<HouseDaoColumns> orderBy = null, Database database = null)
		{		
			database = database ?? Db.For<HouseDao>();
			var results = new HouseDaoCollection(database, database.GetQuery<HouseDaoColumns, HouseDao>(where, orderBy), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate&lt;HouseDaoColumns&gt;.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static HouseDaoCollection Where(QiQuery where, Database database = null)
		{
			var results = new HouseDaoCollection(database, Select<HouseDaoColumns>.From<HouseDao>().Where(where, database));
			return results;
		}
				
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		public static HouseDao GetOneWhere(QueryFilter where, Database database = null)
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
		public static HouseDao OneWhere(QueryFilter where, Database database = null)
		{
			WhereDelegate<HouseDaoColumns> whereDelegate = (c) => where;
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
		public static HouseDao GetOneWhere(WhereDelegate<HouseDaoColumns> where, Database database = null)
		{
			var result = OneWhere(where, database);
			if(result == null)
			{
				HouseDaoColumns c = new HouseDaoColumns();
				IQueryFilter filter = where(c); 
				result = CreateFromFilter(filter, database);
			}

			return result;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single HouseDao instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a HouseDaoColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between HouseDaoColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static HouseDao OneWhere(WhereDelegate<HouseDaoColumns> where, Database database = null)
		{
			var result = Top(1, where, database);
			return OneOrThrow(result);
		}
					 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<HouseDaoColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static HouseDao OneWhere(QiQuery where, Database database = null)
		{
			var results = Top(1, where, database);
			return OneOrThrow(results);
		}

		/// <summary>
		/// Execute a query and return the first result.  This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a HouseDaoColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between HouseDaoColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static HouseDao FirstOneWhere(WhereDelegate<HouseDaoColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a HouseDaoColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between HouseDaoColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static HouseDao FirstOneWhere(WhereDelegate<HouseDaoColumns> where, OrderBy<HouseDaoColumns> orderBy, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a HouseDaoColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between HouseDaoColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static HouseDao FirstOneWhere(QueryFilter where, OrderBy<HouseDaoColumns> orderBy = null, Database database = null)
		{
			WhereDelegate<HouseDaoColumns> whereDelegate = (c) => where;
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
		/// <param name="where">A WhereDelegate that recieves a HouseDaoColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between HouseDaoColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static HouseDaoCollection Top(int count, WhereDelegate<HouseDaoColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a HouseDaoColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between HouseDaoColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		public static HouseDaoCollection Top(int count, WhereDelegate<HouseDaoColumns> where, OrderBy<HouseDaoColumns> orderBy, Database database = null)
		{
			HouseDaoColumns c = new HouseDaoColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database ?? Db.For<HouseDao>();
			QuerySet query = GetQuerySet(db); 
			query.Top<HouseDao>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<HouseDaoColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<HouseDaoCollection>(0);
			results.Database = db;
			return results;
		}

		public static HouseDaoCollection Top(int count, QueryFilter where, Database database)
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
		public static HouseDaoCollection Top(int count, QueryFilter where, OrderBy<HouseDaoColumns> orderBy = null, Database database = null)
		{
			Database db = database ?? Db.For<HouseDao>();
			QuerySet query = GetQuerySet(db);
			query.Top<HouseDao>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy<HouseDaoColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<HouseDaoCollection>(0);
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
		public static HouseDaoCollection Top(int count, QiQuery where, Database database = null)
		{
			Database db = database ?? Db.For<HouseDao>();
			QuerySet query = GetQuerySet(db);
			query.Top<HouseDao>(count);
			query.Where(where);
			query.Execute(db);
			var results = query.Results.As<HouseDaoCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Return the count of HouseDaos
		/// </summary>
		public static long Count(Database database = null)
        {
			Database db = database ?? Db.For<HouseDao>();
            QuerySet query = GetQuerySet(db);
            query.Count<HouseDao>();
            query.Execute(db);
            return (long)query.Results[0].DataRow[0];
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a HouseDaoColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between HouseDaoColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<HouseDaoColumns> where, Database database = null)
		{
			HouseDaoColumns c = new HouseDaoColumns();
			IQueryFilter filter = where(c) ;

			Database db = database ?? Db.For<HouseDao>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<HouseDao>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}

		private static HouseDao CreateFromFilter(IQueryFilter filter, Database database = null)
		{
			Database db = database ?? Db.For<HouseDao>();			
			var dao = new HouseDao();
			filter.Parameters.Each(p=>
			{
				dao.Property(p.ColumnName, p.Value);
			});
			dao.Save(db);
			return dao;
		}
		
		private static HouseDao OneOrThrow(HouseDaoCollection c)
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
