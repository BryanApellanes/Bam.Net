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

namespace Bam.Net.CoreServices.AssemblyManagement.Data.Dao
{
	// schema = AssemblyService
	// connection Name = AssemblyService
	[Serializable]
	[Bam.Net.Data.Table("AssemblyReferenceDescriptor", "AssemblyService")]
	public partial class AssemblyReferenceDescriptor: Bam.Net.Data.Dao
	{
		public AssemblyReferenceDescriptor():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public AssemblyReferenceDescriptor(DataRow data)
			: base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public AssemblyReferenceDescriptor(Database db)
			: base(db)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public AssemblyReferenceDescriptor(Database db, DataRow data)
			: base(db, data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		[Bam.Net.Exclude]
		public static implicit operator AssemblyReferenceDescriptor(DataRow data)
		{
			return new AssemblyReferenceDescriptor(data);
		}

		private void SetChildren()
		{

			if(_database != null)
			{
				this.ChildCollections.Add("AssemblyDescriptorAssemblyReferenceDescriptor_AssemblyReferenceDescriptorId", new AssemblyDescriptorAssemblyReferenceDescriptorCollection(Database.GetQuery<AssemblyDescriptorAssemblyReferenceDescriptorColumns, AssemblyDescriptorAssemblyReferenceDescriptor>((c) => c.AssemblyReferenceDescriptorId == GetULongValue("Id")), this, "AssemblyReferenceDescriptorId"));				
			}						
            this.ChildCollections.Add("AssemblyReferenceDescriptor_AssemblyDescriptorAssemblyReferenceDescriptor_AssemblyDescriptor",  new XrefDaoCollection<AssemblyDescriptorAssemblyReferenceDescriptor, AssemblyDescriptor>(this, false));
				
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

	// property:ReferencerHash, columnName:ReferencerHash	
	[Bam.Net.Data.Column(Name="ReferencerHash", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string ReferencerHash
	{
		get
		{
			return GetStringValue("ReferencerHash");
		}
		set
		{
			SetValue("ReferencerHash", value);
		}
	}

	// property:ReferencedHash, columnName:ReferencedHash	
	[Bam.Net.Data.Column(Name="ReferencedHash", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string ReferencedHash
	{
		get
		{
			return GetStringValue("ReferencedHash");
		}
		set
		{
			SetValue("ReferencedHash", value);
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



				

	[Bam.Net.Exclude]	
	public AssemblyDescriptorAssemblyReferenceDescriptorCollection AssemblyDescriptorAssemblyReferenceDescriptorsByAssemblyReferenceDescriptorId
	{
		get
		{
			if (this.IsNew)
			{
				throw new InvalidOperationException("The current instance of type({0}) hasn't been saved and will have no child collections, call Save() or Save(Database) first."._Format(this.GetType().Name));
			}

			if(!this.ChildCollections.ContainsKey("AssemblyDescriptorAssemblyReferenceDescriptor_AssemblyReferenceDescriptorId"))
			{
				SetChildren();
			}

			var c = (AssemblyDescriptorAssemblyReferenceDescriptorCollection)this.ChildCollections["AssemblyDescriptorAssemblyReferenceDescriptor_AssemblyReferenceDescriptorId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
			


		// Xref       
        public XrefDaoCollection<AssemblyDescriptorAssemblyReferenceDescriptor, AssemblyDescriptor> AssemblyDescriptors
        {
            get
            {			
				if (this.IsNew)
				{
					throw new InvalidOperationException("The current instance of type({0}) hasn't been saved and will have no child collections, call Save() or Save(Database) first."._Format(this.GetType().Name));
				}

				if(!this.ChildCollections.ContainsKey("AssemblyReferenceDescriptor_AssemblyDescriptorAssemblyReferenceDescriptor_AssemblyDescriptor"))
				{
					SetChildren();
				}

				var xref = (XrefDaoCollection<AssemblyDescriptorAssemblyReferenceDescriptor, AssemblyDescriptor>)this.ChildCollections["AssemblyReferenceDescriptor_AssemblyDescriptorAssemblyReferenceDescriptor_AssemblyDescriptor"];
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
		[Bam.Net.Exclude] 
		public override IQueryFilter GetUniqueFilter()
		{
			if(UniqueFilterProvider != null)
			{
				return UniqueFilterProvider(this);
			}
			else
			{
				var colFilter = new AssemblyReferenceDescriptorColumns();
				return (colFilter.KeyColumn == IdValue);
			}			
		}

		/// <summary>
		/// Return every record in the AssemblyReferenceDescriptor table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static AssemblyReferenceDescriptorCollection LoadAll(Database database = null)
		{
			Database db = database ?? Db.For<AssemblyReferenceDescriptor>();
			SqlStringBuilder sql = db.GetSqlStringBuilder();
			sql.Select<AssemblyReferenceDescriptor>();
			var results = new AssemblyReferenceDescriptorCollection(db, sql.GetDataTable(db))
			{
				Database = db
			};
			return results;
		}

		/// <summary>
		/// Process all records in batches of the specified size
		/// </summary>
		[Bam.Net.Exclude]
		public static async Task BatchAll(int batchSize, Action<IEnumerable<AssemblyReferenceDescriptor>> batchProcessor, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				AssemblyReferenceDescriptorColumns columns = new AssemblyReferenceDescriptorColumns();
				var orderBy = Bam.Net.Data.Order.By<AssemblyReferenceDescriptorColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
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
		public static async Task BatchQuery(int batchSize, QueryFilter filter, Action<IEnumerable<AssemblyReferenceDescriptor>> batchProcessor, Database database = null)
		{
			await BatchQuery(batchSize, (c) => filter, batchProcessor, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery(int batchSize, WhereDelegate<AssemblyReferenceDescriptorColumns> where, Action<IEnumerable<AssemblyReferenceDescriptor>> batchProcessor, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				AssemblyReferenceDescriptorColumns columns = new AssemblyReferenceDescriptorColumns();
				var orderBy = Bam.Net.Data.Order.By<AssemblyReferenceDescriptorColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (AssemblyReferenceDescriptorColumns)where(columns) && columns.KeyColumn > topId, orderBy, database);
				}
			});			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>			 
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, QueryFilter filter, Action<IEnumerable<AssemblyReferenceDescriptor>> batchProcessor, Bam.Net.Data.OrderBy<AssemblyReferenceDescriptorColumns> orderBy, Database database = null)
		{
			await BatchQuery<ColType>(batchSize, (c) => filter, batchProcessor, orderBy, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, WhereDelegate<AssemblyReferenceDescriptorColumns> where, Action<IEnumerable<AssemblyReferenceDescriptor>> batchProcessor, Bam.Net.Data.OrderBy<AssemblyReferenceDescriptorColumns> orderBy, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				AssemblyReferenceDescriptorColumns columns = new AssemblyReferenceDescriptorColumns();
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					ColType top = results.Select(d => d.Property<ColType>(orderBy.Column.ToString())).ToArray().Largest();
					results = Top(batchSize, (AssemblyReferenceDescriptorColumns)where(columns) && orderBy.Column > top, orderBy, database);
				}
			});			
		}

		public static AssemblyReferenceDescriptor GetById(uint id, Database database = null)
		{
			return GetById((ulong)id, database);
		}

		public static AssemblyReferenceDescriptor GetById(int id, Database database = null)
		{
			return GetById((long)id, database);
		}

		public static AssemblyReferenceDescriptor GetById(long id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static AssemblyReferenceDescriptor GetById(ulong id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static AssemblyReferenceDescriptor GetByUuid(string uuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Uuid") == uuid, database);
		}

		public static AssemblyReferenceDescriptor GetByCuid(string cuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Cuid") == cuid, database);
		}

		[Bam.Net.Exclude]
		public static AssemblyReferenceDescriptorCollection Query(QueryFilter filter, Database database = null)
		{
			return Where(filter, database);
		}

		[Bam.Net.Exclude]		
		public static AssemblyReferenceDescriptorCollection Where(QueryFilter filter, Database database = null)
		{
			WhereDelegate<AssemblyReferenceDescriptorColumns> whereDelegate = (c) => filter;
			return Where(whereDelegate, database);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a AssemblyReferenceDescriptorColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between AssemblyReferenceDescriptorColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static AssemblyReferenceDescriptorCollection Where(Func<AssemblyReferenceDescriptorColumns, QueryFilter<AssemblyReferenceDescriptorColumns>> where, OrderBy<AssemblyReferenceDescriptorColumns> orderBy = null, Database database = null)
		{
			database = database ?? Db.For<AssemblyReferenceDescriptor>();
			return new AssemblyReferenceDescriptorCollection(database.GetQuery<AssemblyReferenceDescriptorColumns, AssemblyReferenceDescriptor>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a AssemblyReferenceDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between AssemblyReferenceDescriptorColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static AssemblyReferenceDescriptorCollection Where(WhereDelegate<AssemblyReferenceDescriptorColumns> where, Database database = null)
		{		
			database = database ?? Db.For<AssemblyReferenceDescriptor>();
			var results = new AssemblyReferenceDescriptorCollection(database, database.GetQuery<AssemblyReferenceDescriptorColumns, AssemblyReferenceDescriptor>(where), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a AssemblyReferenceDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between AssemblyReferenceDescriptorColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static AssemblyReferenceDescriptorCollection Where(WhereDelegate<AssemblyReferenceDescriptorColumns> where, OrderBy<AssemblyReferenceDescriptorColumns> orderBy = null, Database database = null)
		{		
			database = database ?? Db.For<AssemblyReferenceDescriptor>();
			var results = new AssemblyReferenceDescriptorCollection(database, database.GetQuery<AssemblyReferenceDescriptorColumns, AssemblyReferenceDescriptor>(where, orderBy), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate&lt;AssemblyReferenceDescriptorColumns&gt;.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static AssemblyReferenceDescriptorCollection Where(QiQuery where, Database database = null)
		{
			var results = new AssemblyReferenceDescriptorCollection(database, Select<AssemblyReferenceDescriptorColumns>.From<AssemblyReferenceDescriptor>().Where(where, database));
			return results;
		}
				
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		[Bam.Net.Exclude]
		public static AssemblyReferenceDescriptor GetOneWhere(QueryFilter where, Database database = null)
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
		public static AssemblyReferenceDescriptor OneWhere(QueryFilter where, Database database = null)
		{
			WhereDelegate<AssemblyReferenceDescriptorColumns> whereDelegate = (c) => where;
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
		public static AssemblyReferenceDescriptor GetOneWhere(WhereDelegate<AssemblyReferenceDescriptorColumns> where, Database database = null)
		{
			var result = OneWhere(where, database);
			if(result == null)
			{
				AssemblyReferenceDescriptorColumns c = new AssemblyReferenceDescriptorColumns();
				IQueryFilter filter = where(c); 
				result = CreateFromFilter(filter, database);
			}

			return result;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single AssemblyReferenceDescriptor instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a AssemblyReferenceDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between AssemblyReferenceDescriptorColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static AssemblyReferenceDescriptor OneWhere(WhereDelegate<AssemblyReferenceDescriptorColumns> where, Database database = null)
		{
			var result = Top(1, where, database);
			return OneOrThrow(result);
		}
					 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<AssemblyReferenceDescriptorColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static AssemblyReferenceDescriptor OneWhere(QiQuery where, Database database = null)
		{
			var results = Top(1, where, database);
			return OneOrThrow(results);
		}

		/// <summary>
		/// Execute a query and return the first result.  This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a AssemblyReferenceDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between AssemblyReferenceDescriptorColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static AssemblyReferenceDescriptor FirstOneWhere(WhereDelegate<AssemblyReferenceDescriptorColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a AssemblyReferenceDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between AssemblyReferenceDescriptorColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static AssemblyReferenceDescriptor FirstOneWhere(WhereDelegate<AssemblyReferenceDescriptorColumns> where, OrderBy<AssemblyReferenceDescriptorColumns> orderBy, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a AssemblyReferenceDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between AssemblyReferenceDescriptorColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static AssemblyReferenceDescriptor FirstOneWhere(QueryFilter where, OrderBy<AssemblyReferenceDescriptorColumns> orderBy = null, Database database = null)
		{
			WhereDelegate<AssemblyReferenceDescriptorColumns> whereDelegate = (c) => where;
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
		/// <param name="where">A WhereDelegate that recieves a AssemblyReferenceDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between AssemblyReferenceDescriptorColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static AssemblyReferenceDescriptorCollection Top(int count, WhereDelegate<AssemblyReferenceDescriptorColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a AssemblyReferenceDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between AssemblyReferenceDescriptorColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static AssemblyReferenceDescriptorCollection Top(int count, WhereDelegate<AssemblyReferenceDescriptorColumns> where, OrderBy<AssemblyReferenceDescriptorColumns> orderBy, Database database = null)
		{
			AssemblyReferenceDescriptorColumns c = new AssemblyReferenceDescriptorColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database ?? Db.For<AssemblyReferenceDescriptor>();
			QuerySet query = GetQuerySet(db); 
			query.Top<AssemblyReferenceDescriptor>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<AssemblyReferenceDescriptorColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<AssemblyReferenceDescriptorCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static AssemblyReferenceDescriptorCollection Top(int count, QueryFilter where, Database database)
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
		public static AssemblyReferenceDescriptorCollection Top(int count, QueryFilter where, OrderBy<AssemblyReferenceDescriptorColumns> orderBy = null, Database database = null)
		{
			Database db = database ?? Db.For<AssemblyReferenceDescriptor>();
			QuerySet query = GetQuerySet(db);
			query.Top<AssemblyReferenceDescriptor>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy<AssemblyReferenceDescriptorColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<AssemblyReferenceDescriptorCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static AssemblyReferenceDescriptorCollection Top(int count, QueryFilter where, string orderBy = null, SortOrder sortOrder = SortOrder.Ascending, Database database = null)
		{
			Database db = database ?? Db.For<AssemblyReferenceDescriptor>();
			QuerySet query = GetQuerySet(db);
			query.Top<AssemblyReferenceDescriptor>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy(orderBy, sortOrder);
			}

			query.Execute(db);
			var results = query.Results.As<AssemblyReferenceDescriptorCollection>(0);
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
		public static AssemblyReferenceDescriptorCollection Top(int count, QiQuery where, Database database = null)
		{
			Database db = database ?? Db.For<AssemblyReferenceDescriptor>();
			QuerySet query = GetQuerySet(db);
			query.Top<AssemblyReferenceDescriptor>(count);
			query.Where(where);
			query.Execute(db);
			var results = query.Results.As<AssemblyReferenceDescriptorCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Return the count of AssemblyReferenceDescriptors
		/// </summary>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		public static long Count(Database database = null)
        {
			Database db = database ?? Db.For<AssemblyReferenceDescriptor>();
            QuerySet query = GetQuerySet(db);
            query.Count<AssemblyReferenceDescriptor>();
            query.Execute(db);
            return (long)query.Results[0].DataRow[0];
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a AssemblyReferenceDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between AssemblyReferenceDescriptorColumns and other values
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static long Count(WhereDelegate<AssemblyReferenceDescriptorColumns> where, Database database = null)
		{
			AssemblyReferenceDescriptorColumns c = new AssemblyReferenceDescriptorColumns();
			IQueryFilter filter = where(c) ;

			Database db = database ?? Db.For<AssemblyReferenceDescriptor>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<AssemblyReferenceDescriptor>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
		 
		public static long Count(QiQuery where, Database database = null)
		{
		    Database db = database ?? Db.For<AssemblyReferenceDescriptor>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<AssemblyReferenceDescriptor>();
			query.Where(where);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		} 		

		private static AssemblyReferenceDescriptor CreateFromFilter(IQueryFilter filter, Database database = null)
		{
			Database db = database ?? Db.For<AssemblyReferenceDescriptor>();			
			var dao = new AssemblyReferenceDescriptor();
			filter.Parameters.Each(p=>
			{
				dao.Property(p.ColumnName, p.Value);
			});
			dao.Save(db);
			return dao;
		}
		
		private static AssemblyReferenceDescriptor OneOrThrow(AssemblyReferenceDescriptorCollection c)
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
