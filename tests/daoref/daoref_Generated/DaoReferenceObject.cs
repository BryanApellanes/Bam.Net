/*
	This file was generated and should not be modified directly (handlebars template)
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

namespace Bam.Net.DaoRef
{
	// schema = DaoRef
	// connection Name = DaoRef
	[Serializable]
	[Bam.Net.Data.Table("DaoReferenceObject", "DaoRef")]
	public partial class DaoReferenceObject: Bam.Net.Data.Dao
	{
		public DaoReferenceObject():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public DaoReferenceObject(DataRow data)
			: base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public DaoReferenceObject(Database db)
			: base(db)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public DaoReferenceObject(Database db, DataRow data)
			: base(db, data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		[Bam.Net.Exclude]
		public static implicit operator DaoReferenceObject(DataRow data)
		{
			return new DaoReferenceObject(data);
		}

		private void SetChildren()
		{


			if(_database != null)
			{
				this.ChildCollections.Add("DaoReferenceObjectWithForeignKey_DaoReferenceObjectId", new DaoReferenceObjectWithForeignKeyCollection(Database.GetQuery<DaoReferenceObjectWithForeignKeyColumns, DaoReferenceObjectWithForeignKey>((c) => c.DaoReferenceObjectId == GetULongValue("Id")), this, "DaoReferenceObjectId"));
			}


		} // end SetChildren

	// property:Id, columnName: Id	
	[Bam.Net.Data.Column(Name="Id", DbDataType="BigInt", MaxLength="19", AllowNull=false)]
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

	// property:Uuid, columnName: Uuid	
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

	// property:Cuid, columnName: Cuid	
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

	// property:IntProperty, columnName: IntProperty	
	[Bam.Net.Data.Column(Name="IntProperty", DbDataType="Int", MaxLength="10", AllowNull=true)]
	public int? IntProperty
	{
		get
		{
			return GetIntValue("IntProperty");
		}
		set
		{
			SetValue("IntProperty", value);
		}
	}

	// property:DecimalProperty, columnName: DecimalProperty	
	[Bam.Net.Data.Column(Name="DecimalProperty", DbDataType="Decimal", MaxLength="28", AllowNull=true)]
	public decimal? DecimalProperty
	{
		get
		{
			return GetDecimalValue("DecimalProperty");
		}
		set
		{
			SetValue("DecimalProperty", value);
		}
	}

	// property:LongProperty, columnName: LongProperty	
	[Bam.Net.Data.Column(Name="LongProperty", DbDataType="BigInt", MaxLength="19", AllowNull=true)]
	public long? LongProperty
	{
		get
		{
			return GetLongValue("LongProperty");
		}
		set
		{
			SetValue("LongProperty", value);
		}
	}

	// property:ULongProperty, columnName: ULongProperty	
	[Bam.Net.Data.Column(Name="ULongProperty", DbDataType="BigInt", MaxLength="19", AllowNull=true)]
	public ulong? ULongProperty
	{
		get
		{
			return GetULongValue("ULongProperty");
		}
		set
		{
			SetValue("ULongProperty", value);
		}
	}

	// property:DateTimeProperty, columnName: DateTimeProperty	
	[Bam.Net.Data.Column(Name="DateTimeProperty", DbDataType="DateTime", MaxLength="8", AllowNull=true)]
	public DateTime? DateTimeProperty
	{
		get
		{
			return GetDateTimeValue("DateTimeProperty");
		}
		set
		{
			SetValue("DateTimeProperty", value);
		}
	}

	// property:BoolProperty, columnName: BoolProperty	
	[Bam.Net.Data.Column(Name="BoolProperty", DbDataType="Bit", MaxLength="1", AllowNull=true)]
	public bool? BoolProperty
	{
		get
		{
			return GetBooleanValue("BoolProperty");
		}
		set
		{
			SetValue("BoolProperty", value);
		}
	}

	// property:ByteArrayProperty, columnName: ByteArrayProperty	
	[Bam.Net.Data.Column(Name="ByteArrayProperty", DbDataType="VarBinary", MaxLength="8000", AllowNull=true)]
	public byte[] ByteArrayProperty
	{
		get
		{
			return GetByteArrayValue("ByteArrayProperty");
		}
		set
		{
			SetValue("ByteArrayProperty", value);
		}
	}

	// property:StringProperty, columnName: StringProperty	
	[Bam.Net.Data.Column(Name="StringProperty", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string StringProperty
	{
		get
		{
			return GetStringValue("StringProperty");
		}
		set
		{
			SetValue("StringProperty", value);
		}
	}



	[Bam.Net.Exclude]	
	public DaoReferenceObjectWithForeignKeyCollection DaoReferenceObjectWithForeignKeysByDaoReferenceObjectId
	{
		get
		{
			if (this.IsNew)
			{
				throw new InvalidOperationException("The current instance of type({0}) hasn't been saved and will have no child collections, call Save() or Save(Database) first."._Format(this.GetType().Name));
			}

			if(!this.ChildCollections.ContainsKey("DaoReferenceObjectWithForeignKey_DaoReferenceObjectId"))
			{
				SetChildren();
			}

			var c = (DaoReferenceObjectWithForeignKeyCollection)this.ChildCollections["DaoReferenceObjectWithForeignKey_DaoReferenceObjectId"];
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
		[Bam.Net.Exclude]
		public override IQueryFilter GetUniqueFilter()
		{
			if(UniqueFilterProvider != null)
			{
				return UniqueFilterProvider(this);
			}
			else
			{
				var colFilter = new DaoReferenceObjectColumns();
				return (colFilter.KeyColumn == IdValue);
			}
		}

		/// <summary>
        /// Return every record in the DaoReferenceObject table.
        /// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static DaoReferenceObjectCollection LoadAll(Database database = null)
		{
			Database db = database ?? Db.For<DaoReferenceObject>();
            SqlStringBuilder sql = db.GetSqlStringBuilder();
            sql.Select<DaoReferenceObject>();
            var results = new DaoReferenceObjectCollection(db, sql.GetDataTable(db))
            {
                Database = db
            };
            return results;
        }

        /// <summary>
        /// Process all records in batches of the specified size
        /// </summary>
        [Bam.Net.Exclude]
        public static async Task BatchAll(int batchSize, Action<IEnumerable<DaoReferenceObject>> batchProcessor, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				DaoReferenceObjectColumns columns = new DaoReferenceObjectColumns();
				var orderBy = Bam.Net.Data.Order.By<DaoReferenceObjectColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
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
		public static async Task BatchQuery(int batchSize, QueryFilter filter, Action<IEnumerable<DaoReferenceObject>> batchProcessor, Database database = null)
		{
			await BatchQuery(batchSize, (c) => filter, batchProcessor, database);
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>
		[Bam.Net.Exclude]
		public static async Task BatchQuery(int batchSize, WhereDelegate<DaoReferenceObjectColumns> where, Action<IEnumerable<DaoReferenceObject>> batchProcessor, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				DaoReferenceObjectColumns columns = new DaoReferenceObjectColumns();
				var orderBy = Bam.Net.Data.Order.By<DaoReferenceObjectColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{
						batchProcessor(results);
					});
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (DaoReferenceObjectColumns)where(columns) && columns.KeyColumn > topId, orderBy, database);
				}
			});
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, QueryFilter filter, Action<IEnumerable<DaoReferenceObject>> batchProcessor, Bam.Net.Data.OrderBy<DaoReferenceObjectColumns> orderBy, Database database = null)
		{
			await BatchQuery<ColType>(batchSize, (c) => filter, batchProcessor, orderBy, database);
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, WhereDelegate<DaoReferenceObjectColumns> where, Action<IEnumerable<DaoReferenceObject>> batchProcessor, Bam.Net.Data.OrderBy<DaoReferenceObjectColumns> orderBy, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				DaoReferenceObjectColumns columns = new DaoReferenceObjectColumns();
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{
						batchProcessor(results);
					});
					ColType top = results.Select(d => d.Property<ColType>(orderBy.Column.ToString())).ToArray().Largest();
					results = Top(batchSize, (DaoReferenceObjectColumns)where(columns) && orderBy.Column > top, orderBy, database);
				}
			});
		}

		public static DaoReferenceObject GetById(uint? id, Database database = null)
		{
			Args.ThrowIfNull(id, "id");
			Args.ThrowIf(!id.HasValue, "specified DaoReferenceObject.Id was null");
			return GetById(id.Value, database);
		}

		public static DaoReferenceObject GetById(uint id, Database database = null)
		{
			return GetById((ulong)id, database);
		}

		public static DaoReferenceObject GetById(int? id, Database database = null)
		{
			Args.ThrowIfNull(id, "id");
			Args.ThrowIf(!id.HasValue, "specified DaoReferenceObject.Id was null");
			return GetById(id.Value, database);
		}                                    
                                    
		public static DaoReferenceObject GetById(int id, Database database = null)
		{
			return GetById((long)id, database);
		}

		public static DaoReferenceObject GetById(long? id, Database database = null)
		{
			Args.ThrowIfNull(id, "id");
			Args.ThrowIf(!id.HasValue, "specified DaoReferenceObject.Id was null");
			return GetById(id.Value, database);
		}
                                    
		public static DaoReferenceObject GetById(long id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static DaoReferenceObject GetById(ulong? id, Database database = null)
		{
			Args.ThrowIfNull(id, "id");
			Args.ThrowIf(!id.HasValue, "specified DaoReferenceObject.Id was null");
			return GetById(id.Value, database);
		}
                                    
		public static DaoReferenceObject GetById(ulong id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static DaoReferenceObject GetByUuid(string uuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Uuid") == uuid, database);
		}

		public static DaoReferenceObject GetByCuid(string cuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Cuid") == cuid, database);
		}

		[Bam.Net.Exclude]
		public static DaoReferenceObjectCollection Query(QueryFilter filter, Database database = null)
		{
			return Where(filter, database);
		}

		[Bam.Net.Exclude]
		public static DaoReferenceObjectCollection Where(QueryFilter filter, Database database = null)
		{
			WhereDelegate<DaoReferenceObjectColumns> whereDelegate = (c) => filter;
			return Where(whereDelegate, database);
		}

		/// <summary>
		/// Execute a query and return the results.
		/// </summary>
		/// <param name="where">A Func delegate that recieves a DaoReferenceObjectColumns
		/// and returns a QueryFilter which is the result of any comparisons
		/// between DaoReferenceObjectColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static DaoReferenceObjectCollection Where(Func<DaoReferenceObjectColumns, QueryFilter<DaoReferenceObjectColumns>> where, OrderBy<DaoReferenceObjectColumns> orderBy = null, Database database = null)
		{
			database = database ?? Db.For<DaoReferenceObject>();
			return new DaoReferenceObjectCollection(database.GetQuery<DaoReferenceObjectColumns, DaoReferenceObject>(where, orderBy), true);
		}

		/// <summary>
		/// Execute a query and return the results.
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a DaoReferenceObjectColumns
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DaoReferenceObjectColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static DaoReferenceObjectCollection Where(WhereDelegate<DaoReferenceObjectColumns> where, Database database = null)
		{
			database = database ?? Db.For<DaoReferenceObject>();
			var results = new DaoReferenceObjectCollection(database, database.GetQuery<DaoReferenceObjectColumns, DaoReferenceObject>(where), true);
			return results;
		}

		/// <summary>
		/// Execute a query and return the results.
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a DaoReferenceObjectColumns
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DaoReferenceObjectColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static DaoReferenceObjectCollection Where(WhereDelegate<DaoReferenceObjectColumns> where, OrderBy<DaoReferenceObjectColumns> orderBy = null, Database database = null)
		{
			database = database ?? Db.For<DaoReferenceObject>();
			var results = new DaoReferenceObjectCollection(database, database.GetQuery<DaoReferenceObjectColumns, DaoReferenceObject>(where, orderBy), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of
		/// one of the methods that take a delegate of type
		/// WhereDelegate`DaoReferenceObjectColumns`.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static DaoReferenceObjectCollection Where(QiQuery where, Database database = null)
		{
			var results = new DaoReferenceObjectCollection(database, Select<DaoReferenceObjectColumns>.From<DaoReferenceObject>().Where(where, database));
			return results;
		}

		/// <summary>
		/// Get one entry matching the specified filter.  If none exists
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		[Bam.Net.Exclude]
		public static DaoReferenceObject GetOneWhere(QueryFilter where, Database database = null)
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
		public static DaoReferenceObject OneWhere(QueryFilter where, Database database = null)
		{
			WhereDelegate<DaoReferenceObjectColumns> whereDelegate = (c) => where;
			var result = Top(1, whereDelegate, database);
			return OneOrThrow(result);
		}

		/// <summary>
		/// Set one entry matching the specified filter.  If none exists
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		[Bam.Net.Exclude]
		public static void SetOneWhere(WhereDelegate<DaoReferenceObjectColumns> where, Database database = null)
		{
			SetOneWhere(where, out DaoReferenceObject ignore, database);
		}

		/// <summary>
		/// Set one entry matching the specified filter.  If none exists
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		[Bam.Net.Exclude]
		public static void SetOneWhere(WhereDelegate<DaoReferenceObjectColumns> where, out DaoReferenceObject result, Database database = null)
		{
			result = GetOneWhere(where, database);
		}

		/// <summary>
		/// Get one entry matching the specified filter.  If none exists
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static DaoReferenceObject GetOneWhere(WhereDelegate<DaoReferenceObjectColumns> where, Database database = null)
		{
			var result = OneWhere(where, database);
			if(result == null)
			{
				DaoReferenceObjectColumns c = new DaoReferenceObjectColumns();
				IQueryFilter filter = where(c);
				result = CreateFromFilter(filter, database);
			}

			return result;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will
		/// be thrown.  This method is most commonly used to retrieve a
		/// single DaoReferenceObject instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a DaoReferenceObjectColumns
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DaoReferenceObjectColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static DaoReferenceObject OneWhere(WhereDelegate<DaoReferenceObjectColumns> where, Database database = null)
		{
			var result = Top(1, where, database);
			return OneOrThrow(result);
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of
		/// one of the methods that take a delegate of type
		/// WhereDelegate`DaoReferenceObjectColumns`.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static DaoReferenceObject OneWhere(QiQuery where, Database database = null)
		{
			var results = Top(1, where, database);
			return OneOrThrow(results);
		}

		/// <summary>
		/// Execute a query and return the first result.  This method will issue a sql TOP clause so only the
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a DaoReferenceObjectColumns
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DaoReferenceObjectColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static DaoReferenceObject FirstOneWhere(WhereDelegate<DaoReferenceObjectColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a DaoReferenceObjectColumns
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DaoReferenceObjectColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static DaoReferenceObject FirstOneWhere(WhereDelegate<DaoReferenceObjectColumns> where, OrderBy<DaoReferenceObjectColumns> orderBy, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a DaoReferenceObjectColumns
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DaoReferenceObjectColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static DaoReferenceObject FirstOneWhere(QueryFilter where, OrderBy<DaoReferenceObjectColumns> orderBy = null, Database database = null)
		{
			WhereDelegate<DaoReferenceObjectColumns> whereDelegate = (c) => where;
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
		/// <param name="where">A WhereDelegate that recieves a DaoReferenceObjectColumns
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DaoReferenceObjectColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static DaoReferenceObjectCollection Top(int count, WhereDelegate<DaoReferenceObjectColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a DaoReferenceObjectColumns
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DaoReferenceObjectColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static DaoReferenceObjectCollection Top(int count, WhereDelegate<DaoReferenceObjectColumns> where, OrderBy<DaoReferenceObjectColumns> orderBy, Database database = null)
		{
			DaoReferenceObjectColumns c = new DaoReferenceObjectColumns();
			IQueryFilter filter = where(c);

			Database db = database ?? Db.For<DaoReferenceObject>();
			QuerySet query = GetQuerySet(db);
			query.Top<DaoReferenceObject>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<DaoReferenceObjectColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<DaoReferenceObjectCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static DaoReferenceObjectCollection Top(int count, QueryFilter where, Database database)
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
		public static DaoReferenceObjectCollection Top(int count, QueryFilter where, OrderBy<DaoReferenceObjectColumns> orderBy = null, Database database = null)
		{
			Database db = database ?? Db.For<DaoReferenceObject>();
			QuerySet query = GetQuerySet(db);
			query.Top<DaoReferenceObject>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy<DaoReferenceObjectColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<DaoReferenceObjectCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static DaoReferenceObjectCollection Top(int count, QueryFilter where, string orderBy = null, SortOrder sortOrder = SortOrder.Ascending, Database database = null)
		{
			Database db = database ?? Db.For<DaoReferenceObject>();
			QuerySet query = GetQuerySet(db);
			query.Top<DaoReferenceObject>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy(orderBy, sortOrder);
			}

			query.Execute(db);
			var results = query.Results.As<DaoReferenceObjectCollection>(0);
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
		public static DaoReferenceObjectCollection Top(int count, QiQuery where, Database database = null)
		{
			Database db = database ?? Db.For<DaoReferenceObject>();
			QuerySet query = GetQuerySet(db);
			query.Top<DaoReferenceObject>(count);
			query.Where(where);
			query.Execute(db);
			var results = query.Results.As<DaoReferenceObjectCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Return the count of @(Model.ClassName.Pluralize())
		/// </summary>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		public static long Count(Database database = null)
        {
			Database db = database ?? Db.For<DaoReferenceObject>();
            QuerySet query = GetQuerySet(db);
            query.Count<DaoReferenceObject>();
            query.Execute(db);
            return (long)query.Results[0].DataRow[0];
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a DaoReferenceObjectColumns
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DaoReferenceObjectColumns and other values
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static long Count(WhereDelegate<DaoReferenceObjectColumns> where, Database database = null)
		{
			DaoReferenceObjectColumns c = new DaoReferenceObjectColumns();
			IQueryFilter filter = where(c) ;

			Database db = database ?? Db.For<DaoReferenceObject>();
			QuerySet query = GetQuerySet(db);
			query.Count<DaoReferenceObject>();
			query.Where(filter);
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}

		public static long Count(QiQuery where, Database database = null)
		{
		    Database db = database ?? Db.For<DaoReferenceObject>();
			QuerySet query = GetQuerySet(db);
			query.Count<DaoReferenceObject>();
			query.Where(where);
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}

		private static DaoReferenceObject CreateFromFilter(IQueryFilter filter, Database database = null)
		{
			Database db = database ?? Db.For<DaoReferenceObject>();
			var dao = new DaoReferenceObject();
			filter.Parameters.Each(p=>
			{
				dao.Property(p.ColumnName, p.Value);
			});
			dao.Save(db);
			return dao;
		}

		private static DaoReferenceObject OneOrThrow(DaoReferenceObjectCollection c)
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
