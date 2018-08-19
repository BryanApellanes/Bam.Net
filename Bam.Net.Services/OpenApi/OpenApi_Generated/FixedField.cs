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

namespace Bam.Net.Services.OpenApi
{
	// schema = OpenApi
	// connection Name = OpenApi
	[Serializable]
	[Bam.Net.Data.Table("FixedField", "OpenApi")]
	public partial class FixedField: Bam.Net.Data.Dao
	{
		public FixedField():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public FixedField(DataRow data)
			: base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public FixedField(Database db)
			: base(db)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public FixedField(Database db, DataRow data)
			: base(db, data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		[Bam.Net.Exclude]
		public static implicit operator FixedField(DataRow data)
		{
			return new FixedField(data);
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

	// property:FieldName, columnName:FieldName	
	[Bam.Net.Data.Column(Name="FieldName", DbDataType="VarChar", MaxLength="4000", AllowNull=false)]
	public string FieldName
	{
		get
		{
			return GetStringValue("FieldName");
		}
		set
		{
			SetValue("FieldName", value);
		}
	}

	// property:Type, columnName:Type	
	[Bam.Net.Data.Column(Name="Type", DbDataType="VarChar", MaxLength="4000", AllowNull=false)]
	public string Type
	{
		get
		{
			return GetStringValue("Type");
		}
		set
		{
			SetValue("Type", value);
		}
	}

	// property:AppliesTo, columnName:AppliesTo	
	[Bam.Net.Data.Column(Name="AppliesTo", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string AppliesTo
	{
		get
		{
			return GetStringValue("AppliesTo");
		}
		set
		{
			SetValue("AppliesTo", value);
		}
	}

	// property:Description, columnName:Description	
	[Bam.Net.Data.Column(Name="Description", DbDataType="VarChar", MaxLength="4000", AllowNull=false)]
	public string Description
	{
		get
		{
			return GetStringValue("Description");
		}
		set
		{
			SetValue("Description", value);
		}
	}



	// start ObjectDescriptorId -> ObjectDescriptorId
	[Bam.Net.Data.ForeignKey(
        Table="FixedField",
		Name="ObjectDescriptorId", 
		DbDataType="BigInt", 
		MaxLength="",
		AllowNull=true, 
		ReferencedKey="Id",
		ReferencedTable="ObjectDescriptor",
		Suffix="1")]
	public ulong? ObjectDescriptorId
	{
		get
		{
			return GetULongValue("ObjectDescriptorId");
		}
		set
		{
			SetValue("ObjectDescriptorId", value);
		}
	}

	ObjectDescriptor _objectDescriptorOfObjectDescriptorId;
	public ObjectDescriptor ObjectDescriptorOfObjectDescriptorId
	{
		get
		{
			if(_objectDescriptorOfObjectDescriptorId == null)
			{
				_objectDescriptorOfObjectDescriptorId = Bam.Net.Services.OpenApi.ObjectDescriptor.OneWhere(c => c.KeyColumn == this.ObjectDescriptorId, this.Database);
			}
			return _objectDescriptorOfObjectDescriptorId;
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
				var colFilter = new FixedFieldColumns();
				return (colFilter.KeyColumn == IdValue);
			}			
		}

		/// <summary>
		/// Return every record in the FixedField table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static FixedFieldCollection LoadAll(Database database = null)
		{
			Database db = database ?? Db.For<FixedField>();
			SqlStringBuilder sql = db.GetSqlStringBuilder();
			sql.Select<FixedField>();
			var results = new FixedFieldCollection(db, sql.GetDataTable(db))
			{
				Database = db
			};
			return results;
		}

		/// <summary>
		/// Process all records in batches of the specified size
		/// </summary>
		[Bam.Net.Exclude]
		public static async Task BatchAll(int batchSize, Action<IEnumerable<FixedField>> batchProcessor, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				FixedFieldColumns columns = new FixedFieldColumns();
				var orderBy = Bam.Net.Data.Order.By<FixedFieldColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
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
		public static async Task BatchQuery(int batchSize, QueryFilter filter, Action<IEnumerable<FixedField>> batchProcessor, Database database = null)
		{
			await BatchQuery(batchSize, (c) => filter, batchProcessor, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery(int batchSize, WhereDelegate<FixedFieldColumns> where, Action<IEnumerable<FixedField>> batchProcessor, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				FixedFieldColumns columns = new FixedFieldColumns();
				var orderBy = Bam.Net.Data.Order.By<FixedFieldColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (FixedFieldColumns)where(columns) && columns.KeyColumn > topId, orderBy, database);
				}
			});			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>			 
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, QueryFilter filter, Action<IEnumerable<FixedField>> batchProcessor, Bam.Net.Data.OrderBy<FixedFieldColumns> orderBy, Database database = null)
		{
			await BatchQuery<ColType>(batchSize, (c) => filter, batchProcessor, orderBy, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, WhereDelegate<FixedFieldColumns> where, Action<IEnumerable<FixedField>> batchProcessor, Bam.Net.Data.OrderBy<FixedFieldColumns> orderBy, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				FixedFieldColumns columns = new FixedFieldColumns();
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					ColType top = results.Select(d => d.Property<ColType>(orderBy.Column.ToString())).ToArray().Largest();
					results = Top(batchSize, (FixedFieldColumns)where(columns) && orderBy.Column > top, orderBy, database);
				}
			});			
		}

		public static FixedField GetById(uint id, Database database = null)
		{
			return GetById((ulong)id, database);
		}

		public static FixedField GetById(int id, Database database = null)
		{
			return GetById((long)id, database);
		}

		public static FixedField GetById(long id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static FixedField GetById(ulong id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static FixedField GetByUuid(string uuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Uuid") == uuid, database);
		}

		public static FixedField GetByCuid(string cuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Cuid") == cuid, database);
		}

		[Bam.Net.Exclude]
		public static FixedFieldCollection Query(QueryFilter filter, Database database = null)
		{
			return Where(filter, database);
		}

		[Bam.Net.Exclude]		
		public static FixedFieldCollection Where(QueryFilter filter, Database database = null)
		{
			WhereDelegate<FixedFieldColumns> whereDelegate = (c) => filter;
			return Where(whereDelegate, database);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a FixedFieldColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between FixedFieldColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static FixedFieldCollection Where(Func<FixedFieldColumns, QueryFilter<FixedFieldColumns>> where, OrderBy<FixedFieldColumns> orderBy = null, Database database = null)
		{
			database = database ?? Db.For<FixedField>();
			return new FixedFieldCollection(database.GetQuery<FixedFieldColumns, FixedField>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a FixedFieldColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between FixedFieldColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static FixedFieldCollection Where(WhereDelegate<FixedFieldColumns> where, Database database = null)
		{		
			database = database ?? Db.For<FixedField>();
			var results = new FixedFieldCollection(database, database.GetQuery<FixedFieldColumns, FixedField>(where), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a FixedFieldColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between FixedFieldColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static FixedFieldCollection Where(WhereDelegate<FixedFieldColumns> where, OrderBy<FixedFieldColumns> orderBy = null, Database database = null)
		{		
			database = database ?? Db.For<FixedField>();
			var results = new FixedFieldCollection(database, database.GetQuery<FixedFieldColumns, FixedField>(where, orderBy), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate&lt;FixedFieldColumns&gt;.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static FixedFieldCollection Where(QiQuery where, Database database = null)
		{
			var results = new FixedFieldCollection(database, Select<FixedFieldColumns>.From<FixedField>().Where(where, database));
			return results;
		}
				
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		[Bam.Net.Exclude]
		public static FixedField GetOneWhere(QueryFilter where, Database database = null)
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
		public static FixedField OneWhere(QueryFilter where, Database database = null)
		{
			WhereDelegate<FixedFieldColumns> whereDelegate = (c) => where;
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
		public static FixedField GetOneWhere(WhereDelegate<FixedFieldColumns> where, Database database = null)
		{
			var result = OneWhere(where, database);
			if(result == null)
			{
				FixedFieldColumns c = new FixedFieldColumns();
				IQueryFilter filter = where(c); 
				result = CreateFromFilter(filter, database);
			}

			return result;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single FixedField instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a FixedFieldColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between FixedFieldColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static FixedField OneWhere(WhereDelegate<FixedFieldColumns> where, Database database = null)
		{
			var result = Top(1, where, database);
			return OneOrThrow(result);
		}
					 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<FixedFieldColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static FixedField OneWhere(QiQuery where, Database database = null)
		{
			var results = Top(1, where, database);
			return OneOrThrow(results);
		}

		/// <summary>
		/// Execute a query and return the first result.  This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a FixedFieldColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between FixedFieldColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static FixedField FirstOneWhere(WhereDelegate<FixedFieldColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a FixedFieldColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between FixedFieldColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static FixedField FirstOneWhere(WhereDelegate<FixedFieldColumns> where, OrderBy<FixedFieldColumns> orderBy, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a FixedFieldColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between FixedFieldColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static FixedField FirstOneWhere(QueryFilter where, OrderBy<FixedFieldColumns> orderBy = null, Database database = null)
		{
			WhereDelegate<FixedFieldColumns> whereDelegate = (c) => where;
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
		/// <param name="where">A WhereDelegate that recieves a FixedFieldColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between FixedFieldColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static FixedFieldCollection Top(int count, WhereDelegate<FixedFieldColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a FixedFieldColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between FixedFieldColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static FixedFieldCollection Top(int count, WhereDelegate<FixedFieldColumns> where, OrderBy<FixedFieldColumns> orderBy, Database database = null)
		{
			FixedFieldColumns c = new FixedFieldColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database ?? Db.For<FixedField>();
			QuerySet query = GetQuerySet(db); 
			query.Top<FixedField>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<FixedFieldColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<FixedFieldCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static FixedFieldCollection Top(int count, QueryFilter where, Database database)
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
		public static FixedFieldCollection Top(int count, QueryFilter where, OrderBy<FixedFieldColumns> orderBy = null, Database database = null)
		{
			Database db = database ?? Db.For<FixedField>();
			QuerySet query = GetQuerySet(db);
			query.Top<FixedField>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy<FixedFieldColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<FixedFieldCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static FixedFieldCollection Top(int count, QueryFilter where, string orderBy = null, SortOrder sortOrder = SortOrder.Ascending, Database database = null)
		{
			Database db = database ?? Db.For<FixedField>();
			QuerySet query = GetQuerySet(db);
			query.Top<FixedField>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy(orderBy, sortOrder);
			}

			query.Execute(db);
			var results = query.Results.As<FixedFieldCollection>(0);
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
		public static FixedFieldCollection Top(int count, QiQuery where, Database database = null)
		{
			Database db = database ?? Db.For<FixedField>();
			QuerySet query = GetQuerySet(db);
			query.Top<FixedField>(count);
			query.Where(where);
			query.Execute(db);
			var results = query.Results.As<FixedFieldCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Return the count of FixedFields
		/// </summary>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		public static long Count(Database database = null)
        {
			Database db = database ?? Db.For<FixedField>();
            QuerySet query = GetQuerySet(db);
            query.Count<FixedField>();
            query.Execute(db);
            return (long)query.Results[0].DataRow[0];
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a FixedFieldColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between FixedFieldColumns and other values
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static long Count(WhereDelegate<FixedFieldColumns> where, Database database = null)
		{
			FixedFieldColumns c = new FixedFieldColumns();
			IQueryFilter filter = where(c) ;

			Database db = database ?? Db.For<FixedField>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<FixedField>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
		 
		public static long Count(QiQuery where, Database database = null)
		{
		    Database db = database ?? Db.For<FixedField>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<FixedField>();
			query.Where(where);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		} 		

		private static FixedField CreateFromFilter(IQueryFilter filter, Database database = null)
		{
			Database db = database ?? Db.For<FixedField>();			
			var dao = new FixedField();
			filter.Parameters.Each(p=>
			{
				dao.Property(p.ColumnName, p.Value);
			});
			dao.Save(db);
			return dao;
		}
		
		private static FixedField OneOrThrow(FixedFieldCollection c)
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
