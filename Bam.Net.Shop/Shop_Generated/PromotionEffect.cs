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

namespace Bam.Net.Shop
{
	// schema = Shop
	// connection Name = Shop
	[Serializable]
	[Bam.Net.Data.Table("PromotionEffect", "Shop")]
	public partial class PromotionEffect: Dao
	{
		public PromotionEffect():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public PromotionEffect(DataRow data)
			: base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public PromotionEffect(Database db)
			: base(db)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public PromotionEffect(Database db, DataRow data)
			: base(db, data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		[Bam.Net.Exclude]
		public static implicit operator PromotionEffect(DataRow data)
		{
			return new PromotionEffect(data);
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



	// start PromotionId -> PromotionId
	[Bam.Net.Data.ForeignKey(
        Table="PromotionEffect",
		Name="PromotionId", 
		DbDataType="BigInt", 
		MaxLength="",
		AllowNull=true, 
		ReferencedKey="Id",
		ReferencedTable="Promotion",
		Suffix="1")]
	public long? PromotionId
	{
		get
		{
			return GetLongValue("PromotionId");
		}
		set
		{
			SetValue("PromotionId", value);
		}
	}

	Promotion _promotionOfPromotionId;
	public Promotion PromotionOfPromotionId
	{
		get
		{
			if(_promotionOfPromotionId == null)
			{
				_promotionOfPromotionId = Bam.Net.Shop.Promotion.OneWhere(c => c.KeyColumn == this.PromotionId, this.Database);
			}
			return _promotionOfPromotionId;
		}
	}
	
	// start PromotionEffectsId -> PromotionEffectsId
	[Bam.Net.Data.ForeignKey(
        Table="PromotionEffect",
		Name="PromotionEffectsId", 
		DbDataType="BigInt", 
		MaxLength="",
		AllowNull=true, 
		ReferencedKey="Id",
		ReferencedTable="PromotionEffects",
		Suffix="2")]
	public long? PromotionEffectsId
	{
		get
		{
			return GetLongValue("PromotionEffectsId");
		}
		set
		{
			SetValue("PromotionEffectsId", value);
		}
	}

	PromotionEffects _promotionEffectsOfPromotionEffectsId;
	public PromotionEffects PromotionEffectsOfPromotionEffectsId
	{
		get
		{
			if(_promotionEffectsOfPromotionEffectsId == null)
			{
				_promotionEffectsOfPromotionEffectsId = Bam.Net.Shop.PromotionEffects.OneWhere(c => c.KeyColumn == this.PromotionEffectsId, this.Database);
			}
			return _promotionEffectsOfPromotionEffectsId;
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
				var colFilter = new PromotionEffectColumns();
				return (colFilter.KeyColumn == IdValue);
			}			
		}

		/// <summary>
		/// Return every record in the PromotionEffect table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static PromotionEffectCollection LoadAll(Database database = null)
		{
			SqlStringBuilder sql = new SqlStringBuilder();
			sql.Select<PromotionEffect>();
			Database db = database ?? Db.For<PromotionEffect>();
			var results = new PromotionEffectCollection(sql.GetDataTable(db));
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Process all records in batches of the specified size
		/// </summary>
		[Bam.Net.Exclude]
		public static async Task BatchAll(int batchSize, Action<IEnumerable<PromotionEffect>> batchProcessor, Database database = null)
		{
			await Task.Run(async ()=>
			{
				PromotionEffectColumns columns = new PromotionEffectColumns();
				var orderBy = Bam.Net.Data.Order.By<PromotionEffectColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
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
		public static async Task BatchQuery(int batchSize, QueryFilter filter, Action<IEnumerable<PromotionEffect>> batchProcessor, Database database = null)
		{
			await BatchQuery(batchSize, (c) => filter, batchProcessor, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery(int batchSize, WhereDelegate<PromotionEffectColumns> where, Action<IEnumerable<PromotionEffect>> batchProcessor, Database database = null)
		{
			await Task.Run(async ()=>
			{
				PromotionEffectColumns columns = new PromotionEffectColumns();
				var orderBy = Bam.Net.Data.Order.By<PromotionEffectColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (PromotionEffectColumns)where(columns) && columns.KeyColumn > topId, orderBy, database);
				}
			});			
		}

		public static PromotionEffect GetById(int id, Database database = null)
		{
			return GetById((long)id, database);
		}

		public static PromotionEffect GetById(long id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static PromotionEffect GetByUuid(string uuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Uuid") == uuid, database);
		}

		public static PromotionEffect GetByCuid(string cuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Cuid") == cuid, database);
		}

		[Bam.Net.Exclude]
		public static PromotionEffectCollection Query(QueryFilter filter, Database database = null)
		{
			return Where(filter, database);
		}

		[Bam.Net.Exclude]		
		public static PromotionEffectCollection Where(QueryFilter filter, Database database = null)
		{
			WhereDelegate<PromotionEffectColumns> whereDelegate = (c) => filter;
			return Where(whereDelegate, database);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a PromotionEffectColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between PromotionEffectColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static PromotionEffectCollection Where(Func<PromotionEffectColumns, QueryFilter<PromotionEffectColumns>> where, OrderBy<PromotionEffectColumns> orderBy = null, Database database = null)
		{
			database = database ?? Db.For<PromotionEffect>();
			return new PromotionEffectCollection(database.GetQuery<PromotionEffectColumns, PromotionEffect>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PromotionEffectColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PromotionEffectColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static PromotionEffectCollection Where(WhereDelegate<PromotionEffectColumns> where, Database database = null)
		{		
			database = database ?? Db.For<PromotionEffect>();
			var results = new PromotionEffectCollection(database, database.GetQuery<PromotionEffectColumns, PromotionEffect>(where), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PromotionEffectColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PromotionEffectColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static PromotionEffectCollection Where(WhereDelegate<PromotionEffectColumns> where, OrderBy<PromotionEffectColumns> orderBy = null, Database database = null)
		{		
			database = database ?? Db.For<PromotionEffect>();
			var results = new PromotionEffectCollection(database, database.GetQuery<PromotionEffectColumns, PromotionEffect>(where, orderBy), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate&lt;PromotionEffectColumns&gt;.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static PromotionEffectCollection Where(QiQuery where, Database database = null)
		{
			var results = new PromotionEffectCollection(database, Select<PromotionEffectColumns>.From<PromotionEffect>().Where(where, database));
			return results;
		}
				
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		[Bam.Net.Exclude]
		public static PromotionEffect GetOneWhere(QueryFilter where, Database database = null)
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
		public static PromotionEffect OneWhere(QueryFilter where, Database database = null)
		{
			WhereDelegate<PromotionEffectColumns> whereDelegate = (c) => where;
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
		public static PromotionEffect GetOneWhere(WhereDelegate<PromotionEffectColumns> where, Database database = null)
		{
			var result = OneWhere(where, database);
			if(result == null)
			{
				PromotionEffectColumns c = new PromotionEffectColumns();
				IQueryFilter filter = where(c); 
				result = CreateFromFilter(filter, database);
			}

			return result;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single PromotionEffect instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PromotionEffectColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PromotionEffectColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static PromotionEffect OneWhere(WhereDelegate<PromotionEffectColumns> where, Database database = null)
		{
			var result = Top(1, where, database);
			return OneOrThrow(result);
		}
					 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<PromotionEffectColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static PromotionEffect OneWhere(QiQuery where, Database database = null)
		{
			var results = Top(1, where, database);
			return OneOrThrow(results);
		}

		/// <summary>
		/// Execute a query and return the first result.  This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PromotionEffectColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PromotionEffectColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static PromotionEffect FirstOneWhere(WhereDelegate<PromotionEffectColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a PromotionEffectColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PromotionEffectColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static PromotionEffect FirstOneWhere(WhereDelegate<PromotionEffectColumns> where, OrderBy<PromotionEffectColumns> orderBy, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a PromotionEffectColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PromotionEffectColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static PromotionEffect FirstOneWhere(QueryFilter where, OrderBy<PromotionEffectColumns> orderBy = null, Database database = null)
		{
			WhereDelegate<PromotionEffectColumns> whereDelegate = (c) => where;
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
		/// <param name="where">A WhereDelegate that recieves a PromotionEffectColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PromotionEffectColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static PromotionEffectCollection Top(int count, WhereDelegate<PromotionEffectColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a PromotionEffectColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PromotionEffectColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static PromotionEffectCollection Top(int count, WhereDelegate<PromotionEffectColumns> where, OrderBy<PromotionEffectColumns> orderBy, Database database = null)
		{
			PromotionEffectColumns c = new PromotionEffectColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database ?? Db.For<PromotionEffect>();
			QuerySet query = GetQuerySet(db); 
			query.Top<PromotionEffect>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<PromotionEffectColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<PromotionEffectCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static PromotionEffectCollection Top(int count, QueryFilter where, Database database)
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
		public static PromotionEffectCollection Top(int count, QueryFilter where, OrderBy<PromotionEffectColumns> orderBy = null, Database database = null)
		{
			Database db = database ?? Db.For<PromotionEffect>();
			QuerySet query = GetQuerySet(db);
			query.Top<PromotionEffect>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy<PromotionEffectColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<PromotionEffectCollection>(0);
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
		public static PromotionEffectCollection Top(int count, QiQuery where, Database database = null)
		{
			Database db = database ?? Db.For<PromotionEffect>();
			QuerySet query = GetQuerySet(db);
			query.Top<PromotionEffect>(count);
			query.Where(where);
			query.Execute(db);
			var results = query.Results.As<PromotionEffectCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Return the count of PromotionEffects
		/// </summary>
		public static long Count(Database database = null)
        {
			Database db = database ?? Db.For<PromotionEffect>();
            QuerySet query = GetQuerySet(db);
            query.Count<PromotionEffect>();
            query.Execute(db);
            return (long)query.Results[0].DataRow[0];
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PromotionEffectColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PromotionEffectColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static long Count(WhereDelegate<PromotionEffectColumns> where, Database database = null)
		{
			PromotionEffectColumns c = new PromotionEffectColumns();
			IQueryFilter filter = where(c) ;

			Database db = database ?? Db.For<PromotionEffect>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<PromotionEffect>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
		 
		public static long Count(QiQuery where, Database database = null)
		{
		    Database db = database ?? Db.For<PromotionEffect>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<PromotionEffect>();
			query.Where(where);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		} 		

		private static PromotionEffect CreateFromFilter(IQueryFilter filter, Database database = null)
		{
			Database db = database ?? Db.For<PromotionEffect>();			
			var dao = new PromotionEffect();
			filter.Parameters.Each(p=>
			{
				dao.Property(p.ColumnName, p.Value);
			});
			dao.Save(db);
			return dao;
		}
		
		private static PromotionEffect OneOrThrow(PromotionEffectCollection c)
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
