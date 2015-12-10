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

namespace Bam.Net.Analytics
{
	// schema = Analytics
	// connection Name = Analytics
	[Serializable]
	[Bam.Net.Data.Table("UrlTag", "Analytics")]
	public partial class UrlTag: Dao
	{
		public UrlTag():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public UrlTag(DataRow data)
			: base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public UrlTag(Database db)
			: base(db)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public UrlTag(Database db, DataRow data)
			: base(db, data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public static implicit operator UrlTag(DataRow data)
		{
			return new UrlTag(data);
		}

		private void SetChildren()
		{
						
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



﻿	// start UrlId -> UrlId
	[Bam.Net.Data.ForeignKey(
        Table="UrlTag",
		Name="UrlId", 
		DbDataType="BigInt", 
		MaxLength="",
		AllowNull=false, 
		ReferencedKey="Id",
		ReferencedTable="Url",
		Suffix="1")]
	public long? UrlId
	{
		get
		{
			return GetLongValue("UrlId");
		}
		set
		{
			SetValue("UrlId", value);
		}
	}

	Url _urlOfUrlId;
	public Url UrlOfUrlId
	{
		get
		{
			if(_urlOfUrlId == null)
			{
				_urlOfUrlId = Bam.Net.Analytics.Url.OneWhere(c => c.KeyColumn == this.UrlId, this.Database);
			}
			return _urlOfUrlId;
		}
	}
	
﻿	// start TagId -> TagId
	[Bam.Net.Data.ForeignKey(
        Table="UrlTag",
		Name="TagId", 
		DbDataType="BigInt", 
		MaxLength="",
		AllowNull=false, 
		ReferencedKey="Id",
		ReferencedTable="Tag",
		Suffix="2")]
	public long? TagId
	{
		get
		{
			return GetLongValue("TagId");
		}
		set
		{
			SetValue("TagId", value);
		}
	}

	Tag _tagOfTagId;
	public Tag TagOfTagId
	{
		get
		{
			if(_tagOfTagId == null)
			{
				_tagOfTagId = Bam.Net.Analytics.Tag.OneWhere(c => c.KeyColumn == this.TagId, this.Database);
			}
			return _tagOfTagId;
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
				var colFilter = new UrlTagColumns();
				return (colFilter.KeyColumn == IdValue);
			}			
		}

		/// <summary>
		/// Return every record in the UrlTag table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static UrlTagCollection LoadAll(Database database = null)
		{
			SqlStringBuilder sql = new SqlStringBuilder();
			sql.Select<UrlTag>();
			Database db = database ?? Db.For<UrlTag>();
			var results = new UrlTagCollection(sql.GetDataTable(db));
			results.Database = db;
			return results;
		}

		public static UrlTag GetById(int id, Database database = null)
		{
			return GetById((long)id, database);
		}

		public static UrlTag GetById(long id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static UrlTag GetByUuid(string uuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Uuid") == uuid, database);
		}

		public static UrlTag GetByCuid(string cuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Cuid") == cuid, database);
		}

		public static UrlTagCollection Query(QueryFilter filter, Database database = null)
		{
			return Where(filter, database);
		}
				
		public static UrlTagCollection Where(QueryFilter filter, Database database = null)
		{
			WhereDelegate<UrlTagColumns> whereDelegate = (c) => filter;
			return Where(whereDelegate, database);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a UrlTagColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between UrlTagColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static UrlTagCollection Where(Func<UrlTagColumns, QueryFilter<UrlTagColumns>> where, OrderBy<UrlTagColumns> orderBy = null, Database database = null)
		{
			database = database ?? Db.For<UrlTag>();
			return new UrlTagCollection(database.GetQuery<UrlTagColumns, UrlTag>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a UrlTagColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between UrlTagColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static UrlTagCollection Where(WhereDelegate<UrlTagColumns> where, Database database = null)
		{		
			database = database ?? Db.For<UrlTag>();
			var results = new UrlTagCollection(database, database.GetQuery<UrlTagColumns, UrlTag>(where), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a UrlTagColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between UrlTagColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		public static UrlTagCollection Where(WhereDelegate<UrlTagColumns> where, OrderBy<UrlTagColumns> orderBy = null, Database database = null)
		{		
			database = database ?? Db.For<UrlTag>();
			var results = new UrlTagCollection(database, database.GetQuery<UrlTagColumns, UrlTag>(where, orderBy), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<UrlTagColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static UrlTagCollection Where(QiQuery where, Database database = null)
		{
			var results = new UrlTagCollection(database, Select<UrlTagColumns>.From<UrlTag>().Where(where, database));
			return results;
		}
				
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		public static UrlTag GetOneWhere(QueryFilter where, Database database = null)
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
		public static UrlTag OneWhere(QueryFilter where, Database database = null)
		{
			WhereDelegate<UrlTagColumns> whereDelegate = (c) => where;
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
		public static UrlTag GetOneWhere(WhereDelegate<UrlTagColumns> where, Database database = null)
		{
			var result = OneWhere(where, database);
			if(result == null)
			{
				UrlTagColumns c = new UrlTagColumns();
				IQueryFilter filter = where(c); 
				result = CreateFromFilter(filter, database);
			}

			return result;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single UrlTag instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a UrlTagColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between UrlTagColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static UrlTag OneWhere(WhereDelegate<UrlTagColumns> where, Database database = null)
		{
			var result = Top(1, where, database);
			return OneOrThrow(result);
		}
					 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<UrlTagColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static UrlTag OneWhere(QiQuery where, Database database = null)
		{
			var results = Top(1, where, database);
			return OneOrThrow(results);
		}

		/// <summary>
		/// Execute a query and return the first result.  This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a UrlTagColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between UrlTagColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static UrlTag FirstOneWhere(WhereDelegate<UrlTagColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a UrlTagColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between UrlTagColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static UrlTag FirstOneWhere(WhereDelegate<UrlTagColumns> where, OrderBy<UrlTagColumns> orderBy, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a UrlTagColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between UrlTagColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static UrlTag FirstOneWhere(QueryFilter where, OrderBy<UrlTagColumns> orderBy = null, Database database = null)
		{
			WhereDelegate<UrlTagColumns> whereDelegate = (c) => where;
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
		/// <param name="where">A WhereDelegate that recieves a UrlTagColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between UrlTagColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static UrlTagCollection Top(int count, WhereDelegate<UrlTagColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a UrlTagColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between UrlTagColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		public static UrlTagCollection Top(int count, WhereDelegate<UrlTagColumns> where, OrderBy<UrlTagColumns> orderBy, Database database = null)
		{
			UrlTagColumns c = new UrlTagColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database ?? Db.For<UrlTag>();
			QuerySet query = GetQuerySet(db); 
			query.Top<UrlTag>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<UrlTagColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<UrlTagCollection>(0);
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
		public static UrlTagCollection Top(int count, QueryFilter where, OrderBy<UrlTagColumns> orderBy = null, Database database = null)
		{
			Database db = database ?? Db.For<UrlTag>();
			QuerySet query = GetQuerySet(db);
			query.Top<UrlTag>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy<UrlTagColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<UrlTagCollection>(0);
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
		public static UrlTagCollection Top(int count, QiQuery where, Database database = null)
		{
			Database db = database ?? Db.For<UrlTag>();
			QuerySet query = GetQuerySet(db);
			query.Top<UrlTag>(count);
			query.Where(where);
			query.Execute(db);
			var results = query.Results.As<UrlTagCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a UrlTagColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between UrlTagColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<UrlTagColumns> where, Database database = null)
		{
			UrlTagColumns c = new UrlTagColumns();
			IQueryFilter filter = where(c) ;

			Database db = database ?? Db.For<UrlTag>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<UrlTag>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}

		private static UrlTag CreateFromFilter(IQueryFilter filter, Database database = null)
		{
			Database db = database ?? Db.For<UrlTag>();			
			var dao = new UrlTag();
			filter.Parameters.Each(p=>
			{
				dao.Property(p.ColumnName, p.Value);
			});
			dao.Save(db);
			return dao;
		}
		
		private static UrlTag OneOrThrow(UrlTagCollection c)
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
