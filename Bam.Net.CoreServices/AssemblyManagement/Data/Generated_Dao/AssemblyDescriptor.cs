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
	[Bam.Net.Data.Table("AssemblyDescriptor", "AssemblyService")]
	public partial class AssemblyDescriptor: Bam.Net.Data.Dao
	{
		public AssemblyDescriptor():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public AssemblyDescriptor(DataRow data)
			: base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public AssemblyDescriptor(Database db)
			: base(db)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public AssemblyDescriptor(Database db, DataRow data)
			: base(db, data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		[Bam.Net.Exclude]
		public static implicit operator AssemblyDescriptor(DataRow data)
		{
			return new AssemblyDescriptor(data);
		}

		private void SetChildren()
		{

			if(_database != null)
			{
				this.ChildCollections.Add("AssemblyDescriptorProcessRuntimeDescriptor_AssemblyDescriptorId", new AssemblyDescriptorProcessRuntimeDescriptorCollection(Database.GetQuery<AssemblyDescriptorProcessRuntimeDescriptorColumns, AssemblyDescriptorProcessRuntimeDescriptor>((c) => c.AssemblyDescriptorId == GetULongValue("Id")), this, "AssemblyDescriptorId"));				
			}
			if(_database != null)
			{
				this.ChildCollections.Add("AssemblyDescriptorAssemblyReferenceDescriptor_AssemblyDescriptorId", new AssemblyDescriptorAssemblyReferenceDescriptorCollection(Database.GetQuery<AssemblyDescriptorAssemblyReferenceDescriptorColumns, AssemblyDescriptorAssemblyReferenceDescriptor>((c) => c.AssemblyDescriptorId == GetULongValue("Id")), this, "AssemblyDescriptorId"));				
			}			
            this.ChildCollections.Add("AssemblyDescriptor_AssemblyDescriptorProcessRuntimeDescriptor_ProcessRuntimeDescriptor",  new XrefDaoCollection<AssemblyDescriptorProcessRuntimeDescriptor, ProcessRuntimeDescriptor>(this, false));
				
            this.ChildCollections.Add("AssemblyDescriptor_AssemblyDescriptorAssemblyReferenceDescriptor_AssemblyReferenceDescriptor",  new XrefDaoCollection<AssemblyDescriptorAssemblyReferenceDescriptor, AssemblyReferenceDescriptor>(this, false));
							
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

	// property:FileHash, columnName:FileHash	
	[Bam.Net.Data.Column(Name="FileHash", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string FileHash
	{
		get
		{
			return GetStringValue("FileHash");
		}
		set
		{
			SetValue("FileHash", value);
		}
	}

	// property:AssemblyFullName, columnName:AssemblyFullName	
	[Bam.Net.Data.Column(Name="AssemblyFullName", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string AssemblyFullName
	{
		get
		{
			return GetStringValue("AssemblyFullName");
		}
		set
		{
			SetValue("AssemblyFullName", value);
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
	public AssemblyDescriptorProcessRuntimeDescriptorCollection AssemblyDescriptorProcessRuntimeDescriptorsByAssemblyDescriptorId
	{
		get
		{
			if (this.IsNew)
			{
				throw new InvalidOperationException("The current instance of type({0}) hasn't been saved and will have no child collections, call Save() or Save(Database) first."._Format(this.GetType().Name));
			}

			if(!this.ChildCollections.ContainsKey("AssemblyDescriptorProcessRuntimeDescriptor_AssemblyDescriptorId"))
			{
				SetChildren();
			}

			var c = (AssemblyDescriptorProcessRuntimeDescriptorCollection)this.ChildCollections["AssemblyDescriptorProcessRuntimeDescriptor_AssemblyDescriptorId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
	
	[Bam.Net.Exclude]	
	public AssemblyDescriptorAssemblyReferenceDescriptorCollection AssemblyDescriptorAssemblyReferenceDescriptorsByAssemblyDescriptorId
	{
		get
		{
			if (this.IsNew)
			{
				throw new InvalidOperationException("The current instance of type({0}) hasn't been saved and will have no child collections, call Save() or Save(Database) first."._Format(this.GetType().Name));
			}

			if(!this.ChildCollections.ContainsKey("AssemblyDescriptorAssemblyReferenceDescriptor_AssemblyDescriptorId"))
			{
				SetChildren();
			}

			var c = (AssemblyDescriptorAssemblyReferenceDescriptorCollection)this.ChildCollections["AssemblyDescriptorAssemblyReferenceDescriptor_AssemblyDescriptorId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
			

		// Xref       
        public XrefDaoCollection<AssemblyDescriptorProcessRuntimeDescriptor, ProcessRuntimeDescriptor> ProcessRuntimeDescriptors
        {
            get
            {			
				if (this.IsNew)
				{
					throw new InvalidOperationException("The current instance of type({0}) hasn't been saved and will have no child collections, call Save() or Save(Database) first."._Format(this.GetType().Name));
				}

				if(!this.ChildCollections.ContainsKey("AssemblyDescriptor_AssemblyDescriptorProcessRuntimeDescriptor_ProcessRuntimeDescriptor"))
				{
					SetChildren();
				}

				var xref = (XrefDaoCollection<AssemblyDescriptorProcessRuntimeDescriptor, ProcessRuntimeDescriptor>)this.ChildCollections["AssemblyDescriptor_AssemblyDescriptorProcessRuntimeDescriptor_ProcessRuntimeDescriptor"];
				if(!xref.Loaded)
				{
					xref.Load(Database);
				}

				return xref;
            }
        }
		// Xref       
        public XrefDaoCollection<AssemblyDescriptorAssemblyReferenceDescriptor, AssemblyReferenceDescriptor> AssemblyReferenceDescriptors
        {
            get
            {			
				if (this.IsNew)
				{
					throw new InvalidOperationException("The current instance of type({0}) hasn't been saved and will have no child collections, call Save() or Save(Database) first."._Format(this.GetType().Name));
				}

				if(!this.ChildCollections.ContainsKey("AssemblyDescriptor_AssemblyDescriptorAssemblyReferenceDescriptor_AssemblyReferenceDescriptor"))
				{
					SetChildren();
				}

				var xref = (XrefDaoCollection<AssemblyDescriptorAssemblyReferenceDescriptor, AssemblyReferenceDescriptor>)this.ChildCollections["AssemblyDescriptor_AssemblyDescriptorAssemblyReferenceDescriptor_AssemblyReferenceDescriptor"];
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
		[Bam.Net.Exclude] 
		public override IQueryFilter GetUniqueFilter()
		{
			if(UniqueFilterProvider != null)
			{
				return UniqueFilterProvider(this);
			}
			else
			{
				var colFilter = new AssemblyDescriptorColumns();
				return (colFilter.KeyColumn == IdValue);
			}			
		}

		/// <summary>
		/// Return every record in the AssemblyDescriptor table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static AssemblyDescriptorCollection LoadAll(Database database = null)
		{
			Database db = database ?? Db.For<AssemblyDescriptor>();
			SqlStringBuilder sql = db.GetSqlStringBuilder();
			sql.Select<AssemblyDescriptor>();
			var results = new AssemblyDescriptorCollection(db, sql.GetDataTable(db))
			{
				Database = db
			};
			return results;
		}

		/// <summary>
		/// Process all records in batches of the specified size
		/// </summary>
		[Bam.Net.Exclude]
		public static async Task BatchAll(int batchSize, Action<IEnumerable<AssemblyDescriptor>> batchProcessor, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				AssemblyDescriptorColumns columns = new AssemblyDescriptorColumns();
				var orderBy = Bam.Net.Data.Order.By<AssemblyDescriptorColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
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
		public static async Task BatchQuery(int batchSize, QueryFilter filter, Action<IEnumerable<AssemblyDescriptor>> batchProcessor, Database database = null)
		{
			await BatchQuery(batchSize, (c) => filter, batchProcessor, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery(int batchSize, WhereDelegate<AssemblyDescriptorColumns> where, Action<IEnumerable<AssemblyDescriptor>> batchProcessor, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				AssemblyDescriptorColumns columns = new AssemblyDescriptorColumns();
				var orderBy = Bam.Net.Data.Order.By<AssemblyDescriptorColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (AssemblyDescriptorColumns)where(columns) && columns.KeyColumn > topId, orderBy, database);
				}
			});			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>			 
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, QueryFilter filter, Action<IEnumerable<AssemblyDescriptor>> batchProcessor, Bam.Net.Data.OrderBy<AssemblyDescriptorColumns> orderBy, Database database = null)
		{
			await BatchQuery<ColType>(batchSize, (c) => filter, batchProcessor, orderBy, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, WhereDelegate<AssemblyDescriptorColumns> where, Action<IEnumerable<AssemblyDescriptor>> batchProcessor, Bam.Net.Data.OrderBy<AssemblyDescriptorColumns> orderBy, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				AssemblyDescriptorColumns columns = new AssemblyDescriptorColumns();
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					ColType top = results.Select(d => d.Property<ColType>(orderBy.Column.ToString())).ToArray().Largest();
					results = Top(batchSize, (AssemblyDescriptorColumns)where(columns) && orderBy.Column > top, orderBy, database);
				}
			});			
		}

		public static AssemblyDescriptor GetById(uint id, Database database = null)
		{
			return GetById((ulong)id, database);
		}

		public static AssemblyDescriptor GetById(int id, Database database = null)
		{
			return GetById((long)id, database);
		}

		public static AssemblyDescriptor GetById(long id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static AssemblyDescriptor GetById(ulong id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static AssemblyDescriptor GetByUuid(string uuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Uuid") == uuid, database);
		}

		public static AssemblyDescriptor GetByCuid(string cuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Cuid") == cuid, database);
		}

		[Bam.Net.Exclude]
		public static AssemblyDescriptorCollection Query(QueryFilter filter, Database database = null)
		{
			return Where(filter, database);
		}

		[Bam.Net.Exclude]		
		public static AssemblyDescriptorCollection Where(QueryFilter filter, Database database = null)
		{
			WhereDelegate<AssemblyDescriptorColumns> whereDelegate = (c) => filter;
			return Where(whereDelegate, database);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a AssemblyDescriptorColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between AssemblyDescriptorColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static AssemblyDescriptorCollection Where(Func<AssemblyDescriptorColumns, QueryFilter<AssemblyDescriptorColumns>> where, OrderBy<AssemblyDescriptorColumns> orderBy = null, Database database = null)
		{
			database = database ?? Db.For<AssemblyDescriptor>();
			return new AssemblyDescriptorCollection(database.GetQuery<AssemblyDescriptorColumns, AssemblyDescriptor>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a AssemblyDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between AssemblyDescriptorColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static AssemblyDescriptorCollection Where(WhereDelegate<AssemblyDescriptorColumns> where, Database database = null)
		{		
			database = database ?? Db.For<AssemblyDescriptor>();
			var results = new AssemblyDescriptorCollection(database, database.GetQuery<AssemblyDescriptorColumns, AssemblyDescriptor>(where), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a AssemblyDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between AssemblyDescriptorColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static AssemblyDescriptorCollection Where(WhereDelegate<AssemblyDescriptorColumns> where, OrderBy<AssemblyDescriptorColumns> orderBy = null, Database database = null)
		{		
			database = database ?? Db.For<AssemblyDescriptor>();
			var results = new AssemblyDescriptorCollection(database, database.GetQuery<AssemblyDescriptorColumns, AssemblyDescriptor>(where, orderBy), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate&lt;AssemblyDescriptorColumns&gt;.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static AssemblyDescriptorCollection Where(QiQuery where, Database database = null)
		{
			var results = new AssemblyDescriptorCollection(database, Select<AssemblyDescriptorColumns>.From<AssemblyDescriptor>().Where(where, database));
			return results;
		}
				
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		[Bam.Net.Exclude]
		public static AssemblyDescriptor GetOneWhere(QueryFilter where, Database database = null)
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
		public static AssemblyDescriptor OneWhere(QueryFilter where, Database database = null)
		{
			WhereDelegate<AssemblyDescriptorColumns> whereDelegate = (c) => where;
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
		public static AssemblyDescriptor GetOneWhere(WhereDelegate<AssemblyDescriptorColumns> where, Database database = null)
		{
			var result = OneWhere(where, database);
			if(result == null)
			{
				AssemblyDescriptorColumns c = new AssemblyDescriptorColumns();
				IQueryFilter filter = where(c); 
				result = CreateFromFilter(filter, database);
			}

			return result;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single AssemblyDescriptor instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a AssemblyDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between AssemblyDescriptorColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static AssemblyDescriptor OneWhere(WhereDelegate<AssemblyDescriptorColumns> where, Database database = null)
		{
			var result = Top(1, where, database);
			return OneOrThrow(result);
		}
					 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<AssemblyDescriptorColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static AssemblyDescriptor OneWhere(QiQuery where, Database database = null)
		{
			var results = Top(1, where, database);
			return OneOrThrow(results);
		}

		/// <summary>
		/// Execute a query and return the first result.  This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a AssemblyDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between AssemblyDescriptorColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static AssemblyDescriptor FirstOneWhere(WhereDelegate<AssemblyDescriptorColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a AssemblyDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between AssemblyDescriptorColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static AssemblyDescriptor FirstOneWhere(WhereDelegate<AssemblyDescriptorColumns> where, OrderBy<AssemblyDescriptorColumns> orderBy, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a AssemblyDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between AssemblyDescriptorColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static AssemblyDescriptor FirstOneWhere(QueryFilter where, OrderBy<AssemblyDescriptorColumns> orderBy = null, Database database = null)
		{
			WhereDelegate<AssemblyDescriptorColumns> whereDelegate = (c) => where;
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
		/// <param name="where">A WhereDelegate that recieves a AssemblyDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between AssemblyDescriptorColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static AssemblyDescriptorCollection Top(int count, WhereDelegate<AssemblyDescriptorColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a AssemblyDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between AssemblyDescriptorColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static AssemblyDescriptorCollection Top(int count, WhereDelegate<AssemblyDescriptorColumns> where, OrderBy<AssemblyDescriptorColumns> orderBy, Database database = null)
		{
			AssemblyDescriptorColumns c = new AssemblyDescriptorColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database ?? Db.For<AssemblyDescriptor>();
			QuerySet query = GetQuerySet(db); 
			query.Top<AssemblyDescriptor>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<AssemblyDescriptorColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<AssemblyDescriptorCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static AssemblyDescriptorCollection Top(int count, QueryFilter where, Database database)
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
		public static AssemblyDescriptorCollection Top(int count, QueryFilter where, OrderBy<AssemblyDescriptorColumns> orderBy = null, Database database = null)
		{
			Database db = database ?? Db.For<AssemblyDescriptor>();
			QuerySet query = GetQuerySet(db);
			query.Top<AssemblyDescriptor>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy<AssemblyDescriptorColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<AssemblyDescriptorCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static AssemblyDescriptorCollection Top(int count, QueryFilter where, string orderBy = null, SortOrder sortOrder = SortOrder.Ascending, Database database = null)
		{
			Database db = database ?? Db.For<AssemblyDescriptor>();
			QuerySet query = GetQuerySet(db);
			query.Top<AssemblyDescriptor>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy(orderBy, sortOrder);
			}

			query.Execute(db);
			var results = query.Results.As<AssemblyDescriptorCollection>(0);
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
		public static AssemblyDescriptorCollection Top(int count, QiQuery where, Database database = null)
		{
			Database db = database ?? Db.For<AssemblyDescriptor>();
			QuerySet query = GetQuerySet(db);
			query.Top<AssemblyDescriptor>(count);
			query.Where(where);
			query.Execute(db);
			var results = query.Results.As<AssemblyDescriptorCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Return the count of AssemblyDescriptors
		/// </summary>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		public static long Count(Database database = null)
        {
			Database db = database ?? Db.For<AssemblyDescriptor>();
            QuerySet query = GetQuerySet(db);
            query.Count<AssemblyDescriptor>();
            query.Execute(db);
            return (long)query.Results[0].DataRow[0];
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a AssemblyDescriptorColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between AssemblyDescriptorColumns and other values
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static long Count(WhereDelegate<AssemblyDescriptorColumns> where, Database database = null)
		{
			AssemblyDescriptorColumns c = new AssemblyDescriptorColumns();
			IQueryFilter filter = where(c) ;

			Database db = database ?? Db.For<AssemblyDescriptor>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<AssemblyDescriptor>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
		 
		public static long Count(QiQuery where, Database database = null)
		{
		    Database db = database ?? Db.For<AssemblyDescriptor>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<AssemblyDescriptor>();
			query.Where(where);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		} 		

		private static AssemblyDescriptor CreateFromFilter(IQueryFilter filter, Database database = null)
		{
			Database db = database ?? Db.For<AssemblyDescriptor>();			
			var dao = new AssemblyDescriptor();
			filter.Parameters.Each(p=>
			{
				dao.Property(p.ColumnName, p.Value);
			});
			dao.Save(db);
			return dao;
		}
		
		private static AssemblyDescriptor OneOrThrow(AssemblyDescriptorCollection c)
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
