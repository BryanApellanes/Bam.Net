/*
	Copyright © Bryan Apellanes 2015  
*/
// Model is Table
using System;
using System.Data;
using System.Data.Common;
using Bam.Net;
using Bam.Net.Data;
using Bam.Net.Data.Qi;

namespace Bam.Net.Logging.Data
{
	// schema = DaoLogger2
	// connection Name = DaoLogger2
	[Serializable]
	[Bam.Net.Data.Table("CategoryName", "DaoLogger2")]
	public partial class CategoryName: Dao
	{
		public CategoryName():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public CategoryName(DataRow data)
			: base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public CategoryName(Database db)
			: base(db)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public CategoryName(Database db, DataRow data)
			: base(db, data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public static implicit operator CategoryName(DataRow data)
		{
			return new CategoryName(data);
		}

		private void SetChildren()
		{
﻿
            this.ChildCollections.Add("Event_CategoryNameId", new EventCollection(Database.GetQuery<EventColumns, Event>((c) => c.CategoryNameId == this.Id), this, "CategoryNameId"));							
		}

﻿	// property:Id, columnName:Id	
	[Exclude]
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

﻿	// property:Uuid, columnName:Uuid	
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

﻿	// property:Value, columnName:Value	
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



				
﻿
	[Exclude]	
	public EventCollection EventsByCategoryNameId
	{
		get
		{
			if (this.IsNew)
			{
				throw new InvalidOperationException("The current instance of type({0}) hasn't been saved and will have no child collections, call Save() or Save(Database) first."._Format(this.GetType().Name));
			}

			if(!this.ChildCollections.ContainsKey("Event_CategoryNameId"))
			{
				SetChildren();
			}

			var c = (EventCollection)this.ChildCollections["Event_CategoryNameId"];
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
		public override IQueryFilter GetUniqueFilter()
		{
			if(UniqueFilterProvider != null)
			{
				return UniqueFilterProvider();
			}
			else
			{
				var colFilter = new CategoryNameColumns();
				return (colFilter.KeyColumn == IdValue);
			}			
		}

		/// <summary>
		/// Return every record in the CategoryName table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static CategoryNameCollection LoadAll(Database database = null)
		{
			SqlStringBuilder sql = new SqlStringBuilder();
			sql.Select<CategoryName>();
			Database db = database ?? Db.For<CategoryName>();
			var results = new CategoryNameCollection(sql.GetDataTable(db));
			results.Database = db;
			return results;
		}

		public static CategoryName GetById(int id, Database database = null)
		{
			return GetById((long)id, database);
		}

		public static CategoryName GetById(long id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static CategoryName GetByUuid(string uuid, Database database = null)
		{
			return OneWhere(c => c.Uuid == uuid, database);
		}

		public static CategoryNameCollection Query(QueryFilter filter, Database database = null)
		{
			return Where(filter, database);
		}
				
		public static CategoryNameCollection Where(QueryFilter filter, Database database = null)
		{
			WhereDelegate<CategoryNameColumns> whereDelegate = (c) => filter;
			return Where(whereDelegate, database);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a CategoryNameColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between CategoryNameColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static CategoryNameCollection Where(Func<CategoryNameColumns, QueryFilter<CategoryNameColumns>> where, OrderBy<CategoryNameColumns> orderBy = null, Database database = null)
		{
			database = database ?? Db.For<CategoryName>();
			return new CategoryNameCollection(database.GetQuery<CategoryNameColumns, CategoryName>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a CategoryNameColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CategoryNameColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static CategoryNameCollection Where(WhereDelegate<CategoryNameColumns> where, Database database = null)
		{		
			database = database ?? Db.For<CategoryName>();
			var results = new CategoryNameCollection(database, database.GetQuery<CategoryNameColumns, CategoryName>(where), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a CategoryNameColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CategoryNameColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		public static CategoryNameCollection Where(WhereDelegate<CategoryNameColumns> where, OrderBy<CategoryNameColumns> orderBy = null, Database database = null)
		{		
			database = database ?? Db.For<CategoryName>();
			var results = new CategoryNameCollection(database, database.GetQuery<CategoryNameColumns, CategoryName>(where, orderBy), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<CategoryNameColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static CategoryNameCollection Where(QiQuery where, Database database = null)
		{
			var results = new CategoryNameCollection(database, Select<CategoryNameColumns>.From<CategoryName>().Where(where, database));
			return results;
		}
				
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		public static CategoryName GetOneWhere(QueryFilter where, Database database = null)
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
		public static CategoryName OneWhere(QueryFilter where, Database database = null)
		{
			WhereDelegate<CategoryNameColumns> whereDelegate = (c) => where;
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
		public static CategoryName GetOneWhere(WhereDelegate<CategoryNameColumns> where, Database database = null)
		{
			var result = OneWhere(where, database);
			if(result == null)
			{
				CategoryNameColumns c = new CategoryNameColumns();
				IQueryFilter filter = where(c); 
				result = CreateFromFilter(filter, database);
			}

			return result;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single CategoryName instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a CategoryNameColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CategoryNameColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static CategoryName OneWhere(WhereDelegate<CategoryNameColumns> where, Database database = null)
		{
			var result = Top(1, where, database);
			return OneOrThrow(result);
		}
					 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<CategoryNameColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static CategoryName OneWhere(QiQuery where, Database database = null)
		{
			var results = Top(1, where, database);
			return OneOrThrow(results);
		}

		/// <summary>
		/// Execute a query and return the first result.  This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a CategoryNameColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CategoryNameColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static CategoryName FirstOneWhere(WhereDelegate<CategoryNameColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a CategoryNameColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CategoryNameColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static CategoryName FirstOneWhere(WhereDelegate<CategoryNameColumns> where, OrderBy<CategoryNameColumns> orderBy, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a CategoryNameColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CategoryNameColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static CategoryName FirstOneWhere(QueryFilter where, OrderBy<CategoryNameColumns> orderBy = null, Database database = null)
		{
			WhereDelegate<CategoryNameColumns> whereDelegate = (c) => where;
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
		/// <param name="where">A WhereDelegate that recieves a CategoryNameColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CategoryNameColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static CategoryNameCollection Top(int count, WhereDelegate<CategoryNameColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a CategoryNameColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CategoryNameColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		public static CategoryNameCollection Top(int count, WhereDelegate<CategoryNameColumns> where, OrderBy<CategoryNameColumns> orderBy, Database database = null)
		{
			CategoryNameColumns c = new CategoryNameColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database ?? Db.For<CategoryName>();
			QuerySet query = GetQuerySet(db); 
			query.Top<CategoryName>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<CategoryNameColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<CategoryNameCollection>(0);
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
		public static CategoryNameCollection Top(int count, QueryFilter where, OrderBy<CategoryNameColumns> orderBy = null, Database database = null)
		{
			Database db = database ?? Db.For<CategoryName>();
			QuerySet query = GetQuerySet(db);
			query.Top<CategoryName>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy<CategoryNameColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<CategoryNameCollection>(0);
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
		public static CategoryNameCollection Top(int count, QiQuery where, Database database = null)
		{
			Database db = database ?? Db.For<CategoryName>();
			QuerySet query = GetQuerySet(db);
			query.Top<CategoryName>(count);
			query.Where(where);
			query.Execute(db);
			var results = query.Results.As<CategoryNameCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a CategoryNameColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CategoryNameColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<CategoryNameColumns> where, Database database = null)
		{
			CategoryNameColumns c = new CategoryNameColumns();
			IQueryFilter filter = where(c) ;

			Database db = database ?? Db.For<CategoryName>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<CategoryName>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}

		private static CategoryName CreateFromFilter(IQueryFilter filter, Database database = null)
		{
			Database db = database ?? Db.For<CategoryName>();			
			var dao = new CategoryName();
			filter.Parameters.Each(p=>
			{
				dao.Property(p.ColumnName, p.Value);
			});
			dao.Save(db);
			return dao;
		}
		
		private static CategoryName OneOrThrow(CategoryNameCollection c)
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
