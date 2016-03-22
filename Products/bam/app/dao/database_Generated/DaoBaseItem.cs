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

namespace Bam.Net.Data
{
	// schema = DaoTestData
	// connection Name = DaoTestData
	[Serializable]
	[Bam.Net.Data.Table("DaoBaseItem", "DaoTestData")]
	public partial class DaoBaseItem: Dao
	{
		public DaoBaseItem():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public DaoBaseItem(DataRow data)
			: base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public DaoBaseItem(Database db)
			: base(db)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public DaoBaseItem(Database db, DataRow data)
			: base(db, data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public static implicit operator DaoBaseItem(DataRow data)
		{
			return new DaoBaseItem(data);
		}

		private void SetChildren()
		{

            this.ChildCollections.Add("DaoSubItem_DaoBaseItemId", new DaoSubItemCollection(Database.GetQuery<DaoSubItemColumns, DaoSubItem>((c) => c.DaoBaseItemId == GetLongValue("Id")), this, "DaoBaseItemId"));							
		}

	// property:Id, columnName:Id	
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

	// property:IsCool, columnName:IsCool	
	[Bam.Net.Data.Column(Name="IsCool", DbDataType="Bit", MaxLength="1", AllowNull=true)]
	public bool? IsCool
	{
		get
		{
			return GetBooleanValue("IsCool");
		}
		set
		{
			SetValue("IsCool", value);
		}
	}

	// property:IntValue, columnName:IntValue	
	[Bam.Net.Data.Column(Name="IntValue", DbDataType="Int", MaxLength="10", AllowNull=true)]
	public int? IntValue
	{
		get
		{
			return GetIntValue("IntValue");
		}
		set
		{
			SetValue("IntValue", value);
		}
	}

	// property:LongValue, columnName:LongValue	
	[Bam.Net.Data.Column(Name="LongValue", DbDataType="BigInt", MaxLength="19", AllowNull=true)]
	public long? LongValue
	{
		get
		{
			return GetLongValue("LongValue");
		}
		set
		{
			SetValue("LongValue", value);
		}
	}

	// property:DecimalValue, columnName:DecimalValue	
	[Bam.Net.Data.Column(Name="DecimalValue", DbDataType="Decimal", MaxLength="28", AllowNull=true)]
	public decimal? DecimalValue
	{
		get
		{
			return GetDecimalValue("DecimalValue");
		}
		set
		{
			SetValue("DecimalValue", value);
		}
	}

	// property:ByteArrayValue, columnName:ByteArrayValue	
	[Bam.Net.Data.Column(Name="ByteArrayValue", DbDataType="VarBinary", MaxLength="8000", AllowNull=true)]
	public byte[] ByteArrayValue
	{
		get
		{
			return GetByteArrayValue("ByteArrayValue");
		}
		set
		{
			SetValue("ByteArrayValue", value);
		}
	}



				

	[Exclude]	
	public DaoSubItemCollection DaoSubItemsByDaoBaseItemId
	{
		get
		{
			if (this.IsNew)
			{
				throw new InvalidOperationException("The current instance of type({0}) hasn't been saved and will have no child collections, call Save() or Save(Database) first."._Format(this.GetType().Name));
			}

			if(!this.ChildCollections.ContainsKey("DaoSubItem_DaoBaseItemId"))
			{
				SetChildren();
			}

			var c = (DaoSubItemCollection)this.ChildCollections["DaoSubItem_DaoBaseItemId"];
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
				var colFilter = new DaoBaseItemColumns();
				return (colFilter.KeyColumn == IdValue);
			}			
		}

		/// <summary>
		/// Return every record in the DaoBaseItem table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static DaoBaseItemCollection LoadAll(Database database = null)
		{
			SqlStringBuilder sql = new SqlStringBuilder();
			sql.Select<DaoBaseItem>();
			Database db = database ?? Db.For<DaoBaseItem>();
			var results = new DaoBaseItemCollection(sql.GetDataTable(db));
			results.Database = db;
			return results;
		}

		public static async Task BatchAll(int batchSize, Func<DaoBaseItemCollection, Task> batchProcessor, Database database = null)
		{
			await Task.Run(async ()=>
			{
				DaoBaseItemColumns columns = new DaoBaseItemColumns();
				var orderBy = Order.By<DaoBaseItemColumns>(c => c.KeyColumn, SortOrder.Ascending);
				var results = Top(batchSize, (c) => c.KeyColumn > 0, orderBy, database);
				while(results.Count > 0)
				{
					await batchProcessor(results);
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (c) => c.KeyColumn > topId, orderBy, database);
				}
			});			
		}	 

		public static async Task BatchQuery(int batchSize, QueryFilter filter, Func<DaoBaseItemCollection, Task> batchProcessor, Database database = null)
		{
			await BatchQuery(batchSize, (c) => filter, batchProcessor, database);			
		}

		public static async Task BatchQuery(int batchSize, WhereDelegate<DaoBaseItemColumns> where, Func<DaoBaseItemCollection, Task> batchProcessor, Database database = null)
		{
			await Task.Run(async ()=>
			{
				DaoBaseItemColumns columns = new DaoBaseItemColumns();
				var orderBy = Order.By<DaoBaseItemColumns>(c => c.KeyColumn, SortOrder.Ascending);
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await batchProcessor(results);
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (DaoBaseItemColumns)where(columns) && columns.KeyColumn > topId, orderBy, database);
				}
			});			
		}

		public static DaoBaseItem GetById(int id, Database database = null)
		{
			return GetById((long)id, database);
		}

		public static DaoBaseItem GetById(long id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static DaoBaseItem GetByUuid(string uuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Uuid") == uuid, database);
		}

		public static DaoBaseItem GetByCuid(string cuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Cuid") == cuid, database);
		}

		public static DaoBaseItemCollection Query(QueryFilter filter, Database database = null)
		{
			return Where(filter, database);
		}
				
		public static DaoBaseItemCollection Where(QueryFilter filter, Database database = null)
		{
			WhereDelegate<DaoBaseItemColumns> whereDelegate = (c) => filter;
			return Where(whereDelegate, database);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a DaoBaseItemColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between DaoBaseItemColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static DaoBaseItemCollection Where(Func<DaoBaseItemColumns, QueryFilter<DaoBaseItemColumns>> where, OrderBy<DaoBaseItemColumns> orderBy = null, Database database = null)
		{
			database = database ?? Db.For<DaoBaseItem>();
			return new DaoBaseItemCollection(database.GetQuery<DaoBaseItemColumns, DaoBaseItem>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a DaoBaseItemColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DaoBaseItemColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static DaoBaseItemCollection Where(WhereDelegate<DaoBaseItemColumns> where, Database database = null)
		{		
			database = database ?? Db.For<DaoBaseItem>();
			var results = new DaoBaseItemCollection(database, database.GetQuery<DaoBaseItemColumns, DaoBaseItem>(where), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a DaoBaseItemColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DaoBaseItemColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		public static DaoBaseItemCollection Where(WhereDelegate<DaoBaseItemColumns> where, OrderBy<DaoBaseItemColumns> orderBy = null, Database database = null)
		{		
			database = database ?? Db.For<DaoBaseItem>();
			var results = new DaoBaseItemCollection(database, database.GetQuery<DaoBaseItemColumns, DaoBaseItem>(where, orderBy), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate&lt;DaoBaseItemColumns&gt;.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static DaoBaseItemCollection Where(QiQuery where, Database database = null)
		{
			var results = new DaoBaseItemCollection(database, Select<DaoBaseItemColumns>.From<DaoBaseItem>().Where(where, database));
			return results;
		}
				
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		public static DaoBaseItem GetOneWhere(QueryFilter where, Database database = null)
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
		public static DaoBaseItem OneWhere(QueryFilter where, Database database = null)
		{
			WhereDelegate<DaoBaseItemColumns> whereDelegate = (c) => where;
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
		public static DaoBaseItem GetOneWhere(WhereDelegate<DaoBaseItemColumns> where, Database database = null)
		{
			var result = OneWhere(where, database);
			if(result == null)
			{
				DaoBaseItemColumns c = new DaoBaseItemColumns();
				IQueryFilter filter = where(c); 
				result = CreateFromFilter(filter, database);
			}

			return result;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single DaoBaseItem instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a DaoBaseItemColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DaoBaseItemColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static DaoBaseItem OneWhere(WhereDelegate<DaoBaseItemColumns> where, Database database = null)
		{
			var result = Top(1, where, database);
			return OneOrThrow(result);
		}
					 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<DaoBaseItemColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static DaoBaseItem OneWhere(QiQuery where, Database database = null)
		{
			var results = Top(1, where, database);
			return OneOrThrow(results);
		}

		/// <summary>
		/// Execute a query and return the first result.  This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a DaoBaseItemColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DaoBaseItemColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static DaoBaseItem FirstOneWhere(WhereDelegate<DaoBaseItemColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a DaoBaseItemColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DaoBaseItemColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static DaoBaseItem FirstOneWhere(WhereDelegate<DaoBaseItemColumns> where, OrderBy<DaoBaseItemColumns> orderBy, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a DaoBaseItemColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DaoBaseItemColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static DaoBaseItem FirstOneWhere(QueryFilter where, OrderBy<DaoBaseItemColumns> orderBy = null, Database database = null)
		{
			WhereDelegate<DaoBaseItemColumns> whereDelegate = (c) => where;
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
		/// <param name="where">A WhereDelegate that recieves a DaoBaseItemColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DaoBaseItemColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static DaoBaseItemCollection Top(int count, WhereDelegate<DaoBaseItemColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a DaoBaseItemColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DaoBaseItemColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		public static DaoBaseItemCollection Top(int count, WhereDelegate<DaoBaseItemColumns> where, OrderBy<DaoBaseItemColumns> orderBy, Database database = null)
		{
			DaoBaseItemColumns c = new DaoBaseItemColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database ?? Db.For<DaoBaseItem>();
			QuerySet query = GetQuerySet(db); 
			query.Top<DaoBaseItem>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<DaoBaseItemColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<DaoBaseItemCollection>(0);
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
		public static DaoBaseItemCollection Top(int count, QueryFilter where, OrderBy<DaoBaseItemColumns> orderBy = null, Database database = null)
		{
			Database db = database ?? Db.For<DaoBaseItem>();
			QuerySet query = GetQuerySet(db);
			query.Top<DaoBaseItem>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy<DaoBaseItemColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<DaoBaseItemCollection>(0);
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
		public static DaoBaseItemCollection Top(int count, QiQuery where, Database database = null)
		{
			Database db = database ?? Db.For<DaoBaseItem>();
			QuerySet query = GetQuerySet(db);
			query.Top<DaoBaseItem>(count);
			query.Where(where);
			query.Execute(db);
			var results = query.Results.As<DaoBaseItemCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a DaoBaseItemColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DaoBaseItemColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<DaoBaseItemColumns> where, Database database = null)
		{
			DaoBaseItemColumns c = new DaoBaseItemColumns();
			IQueryFilter filter = where(c) ;

			Database db = database ?? Db.For<DaoBaseItem>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<DaoBaseItem>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}

		private static DaoBaseItem CreateFromFilter(IQueryFilter filter, Database database = null)
		{
			Database db = database ?? Db.For<DaoBaseItem>();			
			var dao = new DaoBaseItem();
			filter.Parameters.Each(p=>
			{
				dao.Property(p.ColumnName, p.Value);
			});
			dao.Save(db);
			return dao;
		}
		
		private static DaoBaseItem OneOrThrow(DaoBaseItemCollection c)
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
