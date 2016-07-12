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
	[Bam.Net.Data.Table("SonDao", "TestDaoSchema")]
	public partial class SonDao: Dao
	{
		public SonDao():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public SonDao(DataRow data)
			: base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public SonDao(Database db)
			: base(db)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public SonDao(Database db, DataRow data)
			: base(db, data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public static implicit operator SonDao(DataRow data)
		{
			return new SonDao(data);
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



	// start ParentId -> ParentId
	[Bam.Net.Data.ForeignKey(
        Table="SonDao",
		Name="ParentId", 
		DbDataType="BigInt", 
		MaxLength="",
		AllowNull=true, 
		ReferencedKey="Id",
		ReferencedTable="ParentDao",
		Suffix="1")]
	public long? ParentId
	{
		get
		{
			return GetLongValue("ParentId");
		}
		set
		{
			SetValue("ParentId", value);
		}
	}

	ParentDao _parentDaoOfParentId;
	public ParentDao ParentDaoOfParentId
	{
		get
		{
			if(_parentDaoOfParentId == null)
			{
				_parentDaoOfParentId = Bam.Net.Data.Repositories.Tests.ClrTypes._Dao_.ParentDao.OneWhere(c => c.KeyColumn == this.ParentId, this.Database);
			}
			return _parentDaoOfParentId;
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
				var colFilter = new SonDaoColumns();
				return (colFilter.KeyColumn == IdValue);
			}			
		}

		/// <summary>
		/// Return every record in the SonDao table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static SonDaoCollection LoadAll(Database database = null)
		{
			SqlStringBuilder sql = new SqlStringBuilder();
			sql.Select<SonDao>();
			Database db = database ?? Db.For<SonDao>();
			var results = new SonDaoCollection(sql.GetDataTable(db));
			results.Database = db;
			return results;
		}

		public static async Task BatchAll(int batchSize, Func<SonDaoCollection, Task> batchProcessor, Database database = null)
		{
			await Task.Run(async ()=>
			{
				SonDaoColumns columns = new SonDaoColumns();
				var orderBy = Order.By<SonDaoColumns>(c => c.KeyColumn, SortOrder.Ascending);
				var results = Top(batchSize, (c) => c.KeyColumn > 0, orderBy, database);
				while(results.Count > 0)
				{
					await batchProcessor(results);
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (c) => c.KeyColumn > topId, orderBy, database);
				}
			});			
		}	 

		public static async Task BatchQuery(int batchSize, QueryFilter filter, Func<SonDaoCollection, Task> batchProcessor, Database database = null)
		{
			await BatchQuery(batchSize, (c) => filter, batchProcessor, database);			
		}

		public static async Task BatchQuery(int batchSize, WhereDelegate<SonDaoColumns> where, Func<SonDaoCollection, Task> batchProcessor, Database database = null)
		{
			await Task.Run(async ()=>
			{
				SonDaoColumns columns = new SonDaoColumns();
				var orderBy = Order.By<SonDaoColumns>(c => c.KeyColumn, SortOrder.Ascending);
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await batchProcessor(results);
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (SonDaoColumns)where(columns) && columns.KeyColumn > topId, orderBy, database);
				}
			});			
		}

		public static SonDao GetById(int id, Database database = null)
		{
			return GetById((long)id, database);
		}

		public static SonDao GetById(long id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static SonDao GetByUuid(string uuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Uuid") == uuid, database);
		}

		public static SonDao GetByCuid(string cuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Cuid") == cuid, database);
		}

		public static SonDaoCollection Query(QueryFilter filter, Database database = null)
		{
			return Where(filter, database);
		}
				
		public static SonDaoCollection Where(QueryFilter filter, Database database = null)
		{
			WhereDelegate<SonDaoColumns> whereDelegate = (c) => filter;
			return Where(whereDelegate, database);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a SonDaoColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between SonDaoColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static SonDaoCollection Where(Func<SonDaoColumns, QueryFilter<SonDaoColumns>> where, OrderBy<SonDaoColumns> orderBy = null, Database database = null)
		{
			database = database ?? Db.For<SonDao>();
			return new SonDaoCollection(database.GetQuery<SonDaoColumns, SonDao>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a SonDaoColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SonDaoColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static SonDaoCollection Where(WhereDelegate<SonDaoColumns> where, Database database = null)
		{		
			database = database ?? Db.For<SonDao>();
			var results = new SonDaoCollection(database, database.GetQuery<SonDaoColumns, SonDao>(where), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a SonDaoColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SonDaoColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		public static SonDaoCollection Where(WhereDelegate<SonDaoColumns> where, OrderBy<SonDaoColumns> orderBy = null, Database database = null)
		{		
			database = database ?? Db.For<SonDao>();
			var results = new SonDaoCollection(database, database.GetQuery<SonDaoColumns, SonDao>(where, orderBy), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate&lt;SonDaoColumns&gt;.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static SonDaoCollection Where(QiQuery where, Database database = null)
		{
			var results = new SonDaoCollection(database, Select<SonDaoColumns>.From<SonDao>().Where(where, database));
			return results;
		}
				
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		public static SonDao GetOneWhere(QueryFilter where, Database database = null)
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
		public static SonDao OneWhere(QueryFilter where, Database database = null)
		{
			WhereDelegate<SonDaoColumns> whereDelegate = (c) => where;
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
		public static SonDao GetOneWhere(WhereDelegate<SonDaoColumns> where, Database database = null)
		{
			var result = OneWhere(where, database);
			if(result == null)
			{
				SonDaoColumns c = new SonDaoColumns();
				IQueryFilter filter = where(c); 
				result = CreateFromFilter(filter, database);
			}

			return result;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single SonDao instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a SonDaoColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SonDaoColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static SonDao OneWhere(WhereDelegate<SonDaoColumns> where, Database database = null)
		{
			var result = Top(1, where, database);
			return OneOrThrow(result);
		}
					 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<SonDaoColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static SonDao OneWhere(QiQuery where, Database database = null)
		{
			var results = Top(1, where, database);
			return OneOrThrow(results);
		}

		/// <summary>
		/// Execute a query and return the first result.  This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a SonDaoColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SonDaoColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static SonDao FirstOneWhere(WhereDelegate<SonDaoColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a SonDaoColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SonDaoColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static SonDao FirstOneWhere(WhereDelegate<SonDaoColumns> where, OrderBy<SonDaoColumns> orderBy, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a SonDaoColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SonDaoColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static SonDao FirstOneWhere(QueryFilter where, OrderBy<SonDaoColumns> orderBy = null, Database database = null)
		{
			WhereDelegate<SonDaoColumns> whereDelegate = (c) => where;
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
		/// <param name="where">A WhereDelegate that recieves a SonDaoColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SonDaoColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static SonDaoCollection Top(int count, WhereDelegate<SonDaoColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a SonDaoColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SonDaoColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		public static SonDaoCollection Top(int count, WhereDelegate<SonDaoColumns> where, OrderBy<SonDaoColumns> orderBy, Database database = null)
		{
			SonDaoColumns c = new SonDaoColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database ?? Db.For<SonDao>();
			QuerySet query = GetQuerySet(db); 
			query.Top<SonDao>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<SonDaoColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<SonDaoCollection>(0);
			results.Database = db;
			return results;
		}

		public static SonDaoCollection Top(int count, QueryFilter where, Database database)
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
		public static SonDaoCollection Top(int count, QueryFilter where, OrderBy<SonDaoColumns> orderBy = null, Database database = null)
		{
			Database db = database ?? Db.For<SonDao>();
			QuerySet query = GetQuerySet(db);
			query.Top<SonDao>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy<SonDaoColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<SonDaoCollection>(0);
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
		public static SonDaoCollection Top(int count, QiQuery where, Database database = null)
		{
			Database db = database ?? Db.For<SonDao>();
			QuerySet query = GetQuerySet(db);
			query.Top<SonDao>(count);
			query.Where(where);
			query.Execute(db);
			var results = query.Results.As<SonDaoCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Return the count of SonDaos
		/// </summary>
		public static long Count(Database database = null)
        {
			Database db = database ?? Db.For<SonDao>();
            QuerySet query = GetQuerySet(db);
            query.Count<SonDao>();
            query.Execute(db);
            return (long)query.Results[0].DataRow[0];
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a SonDaoColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SonDaoColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<SonDaoColumns> where, Database database = null)
		{
			SonDaoColumns c = new SonDaoColumns();
			IQueryFilter filter = where(c) ;

			Database db = database ?? Db.For<SonDao>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<SonDao>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}

		private static SonDao CreateFromFilter(IQueryFilter filter, Database database = null)
		{
			Database db = database ?? Db.For<SonDao>();			
			var dao = new SonDao();
			filter.Parameters.Each(p=>
			{
				dao.Property(p.ColumnName, p.Value);
			});
			dao.Save(db);
			return dao;
		}
		
		private static SonDao OneOrThrow(SonDaoCollection c)
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
