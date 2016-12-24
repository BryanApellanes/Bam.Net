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

namespace Bam.Net.CoreServices.Data.Daos
{
	// schema = CoreRegistry
	// connection Name = CoreRegistry
	[Serializable]
	[Bam.Net.Data.Table("Nic", "CoreRegistry")]
	public partial class Nic: Dao
	{
		public Nic():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public Nic(DataRow data)
			: base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public Nic(Database db)
			: base(db)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public Nic(Database db, DataRow data)
			: base(db, data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		[Bam.Net.Exclude]
		public static implicit operator Nic(DataRow data)
		{
			return new Nic(data);
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

	// property:AddressFamily, columnName:AddressFamily	
	[Bam.Net.Data.Column(Name="AddressFamily", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string AddressFamily
	{
		get
		{
			return GetStringValue("AddressFamily");
		}
		set
		{
			SetValue("AddressFamily", value);
		}
	}

	// property:Value, columnName:Value	
	[Bam.Net.Data.Column(Name="Value", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
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

	// property:MacAddress, columnName:MacAddress	
	[Bam.Net.Data.Column(Name="MacAddress", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string MacAddress
	{
		get
		{
			return GetStringValue("MacAddress");
		}
		set
		{
			SetValue("MacAddress", value);
		}
	}

	// property:CreatedBy, columnName:CreatedBy	
	[Bam.Net.Data.Column(Name="CreatedBy", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string CreatedBy
	{
		get
		{
			return GetStringValue("CreatedBy");
		}
		set
		{
			SetValue("CreatedBy", value);
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

	// property:ModifiedBy, columnName:ModifiedBy	
	[Bam.Net.Data.Column(Name="ModifiedBy", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string ModifiedBy
	{
		get
		{
			return GetStringValue("ModifiedBy");
		}
		set
		{
			SetValue("ModifiedBy", value);
		}
	}

	// property:Modified, columnName:Modified	
	[Bam.Net.Data.Column(Name="Modified", DbDataType="DateTime", MaxLength="8", AllowNull=true)]
	public DateTime? Modified
	{
		get
		{
			return GetDateTimeValue("Modified");
		}
		set
		{
			SetValue("Modified", value);
		}
	}

	// property:Deleted, columnName:Deleted	
	[Bam.Net.Data.Column(Name="Deleted", DbDataType="DateTime", MaxLength="8", AllowNull=true)]
	public DateTime? Deleted
	{
		get
		{
			return GetDateTimeValue("Deleted");
		}
		set
		{
			SetValue("Deleted", value);
		}
	}



	// start MachineId -> MachineId
	[Bam.Net.Data.ForeignKey(
        Table="Nic",
		Name="MachineId", 
		DbDataType="BigInt", 
		MaxLength="",
		AllowNull=true, 
		ReferencedKey="Id",
		ReferencedTable="Machine",
		Suffix="1")]
	public long? MachineId
	{
		get
		{
			return GetLongValue("MachineId");
		}
		set
		{
			SetValue("MachineId", value);
		}
	}

	Machine _machineOfMachineId;
	public Machine MachineOfMachineId
	{
		get
		{
			if(_machineOfMachineId == null)
			{
				_machineOfMachineId = Bam.Net.CoreServices.Data.Daos.Machine.OneWhere(c => c.KeyColumn == this.MachineId, this.Database);
			}
			return _machineOfMachineId;
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
				var colFilter = new NicColumns();
				return (colFilter.KeyColumn == IdValue);
			}			
		}

		/// <summary>
		/// Return every record in the Nic table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static NicCollection LoadAll(Database database = null)
		{
			SqlStringBuilder sql = new SqlStringBuilder();
			sql.Select<Nic>();
			Database db = database ?? Db.For<Nic>();
			var results = new NicCollection(sql.GetDataTable(db));
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Process all records in batches of the specified size
		/// </summary>
		[Bam.Net.Exclude]
		public static async Task BatchAll(int batchSize, Action<IEnumerable<Nic>> batchProcessor, Database database = null)
		{
			await Task.Run(async ()=>
			{
				NicColumns columns = new NicColumns();
				var orderBy = Bam.Net.Data.Order.By<NicColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
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
		public static async Task BatchQuery(int batchSize, QueryFilter filter, Action<IEnumerable<Nic>> batchProcessor, Database database = null)
		{
			await BatchQuery(batchSize, (c) => filter, batchProcessor, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery(int batchSize, WhereDelegate<NicColumns> where, Action<IEnumerable<Nic>> batchProcessor, Database database = null)
		{
			await Task.Run(async ()=>
			{
				NicColumns columns = new NicColumns();
				var orderBy = Bam.Net.Data.Order.By<NicColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (NicColumns)where(columns) && columns.KeyColumn > topId, orderBy, database);
				}
			});			
		}

		public static Nic GetById(int id, Database database = null)
		{
			return GetById((long)id, database);
		}

		public static Nic GetById(long id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static Nic GetByUuid(string uuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Uuid") == uuid, database);
		}

		public static Nic GetByCuid(string cuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Cuid") == cuid, database);
		}

		[Bam.Net.Exclude]
		public static NicCollection Query(QueryFilter filter, Database database = null)
		{
			return Where(filter, database);
		}

		[Bam.Net.Exclude]		
		public static NicCollection Where(QueryFilter filter, Database database = null)
		{
			WhereDelegate<NicColumns> whereDelegate = (c) => filter;
			return Where(whereDelegate, database);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a NicColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between NicColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static NicCollection Where(Func<NicColumns, QueryFilter<NicColumns>> where, OrderBy<NicColumns> orderBy = null, Database database = null)
		{
			database = database ?? Db.For<Nic>();
			return new NicCollection(database.GetQuery<NicColumns, Nic>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a NicColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between NicColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static NicCollection Where(WhereDelegate<NicColumns> where, Database database = null)
		{		
			database = database ?? Db.For<Nic>();
			var results = new NicCollection(database, database.GetQuery<NicColumns, Nic>(where), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a NicColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between NicColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static NicCollection Where(WhereDelegate<NicColumns> where, OrderBy<NicColumns> orderBy = null, Database database = null)
		{		
			database = database ?? Db.For<Nic>();
			var results = new NicCollection(database, database.GetQuery<NicColumns, Nic>(where, orderBy), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate&lt;NicColumns&gt;.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static NicCollection Where(QiQuery where, Database database = null)
		{
			var results = new NicCollection(database, Select<NicColumns>.From<Nic>().Where(where, database));
			return results;
		}
				
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		[Bam.Net.Exclude]
		public static Nic GetOneWhere(QueryFilter where, Database database = null)
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
		public static Nic OneWhere(QueryFilter where, Database database = null)
		{
			WhereDelegate<NicColumns> whereDelegate = (c) => where;
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
		public static Nic GetOneWhere(WhereDelegate<NicColumns> where, Database database = null)
		{
			var result = OneWhere(where, database);
			if(result == null)
			{
				NicColumns c = new NicColumns();
				IQueryFilter filter = where(c); 
				result = CreateFromFilter(filter, database);
			}

			return result;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single Nic instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a NicColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between NicColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static Nic OneWhere(WhereDelegate<NicColumns> where, Database database = null)
		{
			var result = Top(1, where, database);
			return OneOrThrow(result);
		}
					 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<NicColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static Nic OneWhere(QiQuery where, Database database = null)
		{
			var results = Top(1, where, database);
			return OneOrThrow(results);
		}

		/// <summary>
		/// Execute a query and return the first result.  This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a NicColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between NicColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static Nic FirstOneWhere(WhereDelegate<NicColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a NicColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between NicColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static Nic FirstOneWhere(WhereDelegate<NicColumns> where, OrderBy<NicColumns> orderBy, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a NicColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between NicColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static Nic FirstOneWhere(QueryFilter where, OrderBy<NicColumns> orderBy = null, Database database = null)
		{
			WhereDelegate<NicColumns> whereDelegate = (c) => where;
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
		/// <param name="where">A WhereDelegate that recieves a NicColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between NicColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static NicCollection Top(int count, WhereDelegate<NicColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a NicColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between NicColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static NicCollection Top(int count, WhereDelegate<NicColumns> where, OrderBy<NicColumns> orderBy, Database database = null)
		{
			NicColumns c = new NicColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database ?? Db.For<Nic>();
			QuerySet query = GetQuerySet(db); 
			query.Top<Nic>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<NicColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<NicCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static NicCollection Top(int count, QueryFilter where, Database database)
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
		public static NicCollection Top(int count, QueryFilter where, OrderBy<NicColumns> orderBy = null, Database database = null)
		{
			Database db = database ?? Db.For<Nic>();
			QuerySet query = GetQuerySet(db);
			query.Top<Nic>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy<NicColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<NicCollection>(0);
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
		public static NicCollection Top(int count, QiQuery where, Database database = null)
		{
			Database db = database ?? Db.For<Nic>();
			QuerySet query = GetQuerySet(db);
			query.Top<Nic>(count);
			query.Where(where);
			query.Execute(db);
			var results = query.Results.As<NicCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Return the count of Nics
		/// </summary>
		public static long Count(Database database = null)
        {
			Database db = database ?? Db.For<Nic>();
            QuerySet query = GetQuerySet(db);
            query.Count<Nic>();
            query.Execute(db);
            return (long)query.Results[0].DataRow[0];
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a NicColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between NicColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static long Count(WhereDelegate<NicColumns> where, Database database = null)
		{
			NicColumns c = new NicColumns();
			IQueryFilter filter = where(c) ;

			Database db = database ?? Db.For<Nic>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<Nic>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
		 
		public static long Count(QiQuery where, Database database = null)
		{
		    Database db = database ?? Db.For<Nic>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<Nic>();
			query.Where(where);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		} 		

		private static Nic CreateFromFilter(IQueryFilter filter, Database database = null)
		{
			Database db = database ?? Db.For<Nic>();			
			var dao = new Nic();
			filter.Parameters.Each(p=>
			{
				dao.Property(p.ColumnName, p.Value);
			});
			dao.Save(db);
			return dao;
		}
		
		private static Nic OneOrThrow(NicCollection c)
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
