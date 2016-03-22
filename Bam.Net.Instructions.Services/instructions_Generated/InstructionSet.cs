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

namespace Bam.Net.Instructions
{
	// schema = Instructions
	// connection Name = Instructions
	[Serializable]
	[Bam.Net.Data.Table("InstructionSet", "Instructions")]
	public partial class InstructionSet: Dao
	{
		public InstructionSet():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public InstructionSet(DataRow data)
			: base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public InstructionSet(Database db)
			: base(db)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public InstructionSet(Database db, DataRow data)
			: base(db, data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public static implicit operator InstructionSet(DataRow data)
		{
			return new InstructionSet(data);
		}

		private void SetChildren()
		{

            this.ChildCollections.Add("Section_InstructionSetId", new SectionCollection(Database.GetQuery<SectionColumns, Section>((c) => c.InstructionSetId == GetLongValue("Id")), this, "InstructionSetId"));							
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

	// property:Description, columnName:Description	
	[Bam.Net.Data.Column(Name="Description", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
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

	// property:Author, columnName:Author	
	[Bam.Net.Data.Column(Name="Author", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string Author
	{
		get
		{
			return GetStringValue("Author");
		}
		set
		{
			SetValue("Author", value);
		}
	}



				

	[Exclude]	
	public SectionCollection SectionsByInstructionSetId
	{
		get
		{
			if (this.IsNew)
			{
				throw new InvalidOperationException("The current instance of type({0}) hasn't been saved and will have no child collections, call Save() or Save(Database) first."._Format(this.GetType().Name));
			}

			if(!this.ChildCollections.ContainsKey("Section_InstructionSetId"))
			{
				SetChildren();
			}

			var c = (SectionCollection)this.ChildCollections["Section_InstructionSetId"];
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
				var colFilter = new InstructionSetColumns();
				return (colFilter.KeyColumn == IdValue);
			}			
		}

		/// <summary>
		/// Return every record in the InstructionSet table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static InstructionSetCollection LoadAll(Database database = null)
		{
			SqlStringBuilder sql = new SqlStringBuilder();
			sql.Select<InstructionSet>();
			Database db = database ?? Db.For<InstructionSet>();
			var results = new InstructionSetCollection(sql.GetDataTable(db));
			results.Database = db;
			return results;
		}

		public static async Task BatchAll(int batchSize, Func<InstructionSetCollection, Task> batchProcessor, Database database = null)
		{
			await Task.Run(async ()=>
			{
				InstructionSetColumns columns = new InstructionSetColumns();
				var orderBy = Order.By<InstructionSetColumns>(c => c.KeyColumn, SortOrder.Ascending);
				var results = Top(batchSize, (c) => c.KeyColumn > 0, orderBy, database);
				while(results.Count > 0)
				{
					await batchProcessor(results);
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (c) => c.KeyColumn > topId, orderBy, database);
				}
			});			
		}	 

		public static async Task BatchQuery(int batchSize, QueryFilter filter, Func<InstructionSetCollection, Task> batchProcessor, Database database = null)
		{
			await BatchQuery(batchSize, (c) => filter, batchProcessor, database);			
		}

		public static async Task BatchQuery(int batchSize, WhereDelegate<InstructionSetColumns> where, Func<InstructionSetCollection, Task> batchProcessor, Database database = null)
		{
			await Task.Run(async ()=>
			{
				InstructionSetColumns columns = new InstructionSetColumns();
				var orderBy = Order.By<InstructionSetColumns>(c => c.KeyColumn, SortOrder.Ascending);
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await batchProcessor(results);
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (InstructionSetColumns)where(columns) && columns.KeyColumn > topId, orderBy, database);
				}
			});			
		}

		public static InstructionSet GetById(int id, Database database = null)
		{
			return GetById((long)id, database);
		}

		public static InstructionSet GetById(long id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static InstructionSet GetByUuid(string uuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Uuid") == uuid, database);
		}

		public static InstructionSet GetByCuid(string cuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Cuid") == cuid, database);
		}

		public static InstructionSetCollection Query(QueryFilter filter, Database database = null)
		{
			return Where(filter, database);
		}
				
		public static InstructionSetCollection Where(QueryFilter filter, Database database = null)
		{
			WhereDelegate<InstructionSetColumns> whereDelegate = (c) => filter;
			return Where(whereDelegate, database);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a InstructionSetColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between InstructionSetColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static InstructionSetCollection Where(Func<InstructionSetColumns, QueryFilter<InstructionSetColumns>> where, OrderBy<InstructionSetColumns> orderBy = null, Database database = null)
		{
			database = database ?? Db.For<InstructionSet>();
			return new InstructionSetCollection(database.GetQuery<InstructionSetColumns, InstructionSet>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a InstructionSetColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between InstructionSetColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static InstructionSetCollection Where(WhereDelegate<InstructionSetColumns> where, Database database = null)
		{		
			database = database ?? Db.For<InstructionSet>();
			var results = new InstructionSetCollection(database, database.GetQuery<InstructionSetColumns, InstructionSet>(where), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a InstructionSetColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between InstructionSetColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		public static InstructionSetCollection Where(WhereDelegate<InstructionSetColumns> where, OrderBy<InstructionSetColumns> orderBy = null, Database database = null)
		{		
			database = database ?? Db.For<InstructionSet>();
			var results = new InstructionSetCollection(database, database.GetQuery<InstructionSetColumns, InstructionSet>(where, orderBy), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate&lt;InstructionSetColumns&gt;.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static InstructionSetCollection Where(QiQuery where, Database database = null)
		{
			var results = new InstructionSetCollection(database, Select<InstructionSetColumns>.From<InstructionSet>().Where(where, database));
			return results;
		}
				
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		public static InstructionSet GetOneWhere(QueryFilter where, Database database = null)
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
		public static InstructionSet OneWhere(QueryFilter where, Database database = null)
		{
			WhereDelegate<InstructionSetColumns> whereDelegate = (c) => where;
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
		public static InstructionSet GetOneWhere(WhereDelegate<InstructionSetColumns> where, Database database = null)
		{
			var result = OneWhere(where, database);
			if(result == null)
			{
				InstructionSetColumns c = new InstructionSetColumns();
				IQueryFilter filter = where(c); 
				result = CreateFromFilter(filter, database);
			}

			return result;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single InstructionSet instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a InstructionSetColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between InstructionSetColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static InstructionSet OneWhere(WhereDelegate<InstructionSetColumns> where, Database database = null)
		{
			var result = Top(1, where, database);
			return OneOrThrow(result);
		}
					 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<InstructionSetColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static InstructionSet OneWhere(QiQuery where, Database database = null)
		{
			var results = Top(1, where, database);
			return OneOrThrow(results);
		}

		/// <summary>
		/// Execute a query and return the first result.  This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a InstructionSetColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between InstructionSetColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static InstructionSet FirstOneWhere(WhereDelegate<InstructionSetColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a InstructionSetColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between InstructionSetColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static InstructionSet FirstOneWhere(WhereDelegate<InstructionSetColumns> where, OrderBy<InstructionSetColumns> orderBy, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a InstructionSetColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between InstructionSetColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static InstructionSet FirstOneWhere(QueryFilter where, OrderBy<InstructionSetColumns> orderBy = null, Database database = null)
		{
			WhereDelegate<InstructionSetColumns> whereDelegate = (c) => where;
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
		/// <param name="where">A WhereDelegate that recieves a InstructionSetColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between InstructionSetColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static InstructionSetCollection Top(int count, WhereDelegate<InstructionSetColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a InstructionSetColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between InstructionSetColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		public static InstructionSetCollection Top(int count, WhereDelegate<InstructionSetColumns> where, OrderBy<InstructionSetColumns> orderBy, Database database = null)
		{
			InstructionSetColumns c = new InstructionSetColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database ?? Db.For<InstructionSet>();
			QuerySet query = GetQuerySet(db); 
			query.Top<InstructionSet>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<InstructionSetColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<InstructionSetCollection>(0);
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
		public static InstructionSetCollection Top(int count, QueryFilter where, OrderBy<InstructionSetColumns> orderBy = null, Database database = null)
		{
			Database db = database ?? Db.For<InstructionSet>();
			QuerySet query = GetQuerySet(db);
			query.Top<InstructionSet>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy<InstructionSetColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<InstructionSetCollection>(0);
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
		public static InstructionSetCollection Top(int count, QiQuery where, Database database = null)
		{
			Database db = database ?? Db.For<InstructionSet>();
			QuerySet query = GetQuerySet(db);
			query.Top<InstructionSet>(count);
			query.Where(where);
			query.Execute(db);
			var results = query.Results.As<InstructionSetCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a InstructionSetColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between InstructionSetColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<InstructionSetColumns> where, Database database = null)
		{
			InstructionSetColumns c = new InstructionSetColumns();
			IQueryFilter filter = where(c) ;

			Database db = database ?? Db.For<InstructionSet>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<InstructionSet>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}

		private static InstructionSet CreateFromFilter(IQueryFilter filter, Database database = null)
		{
			Database db = database ?? Db.For<InstructionSet>();			
			var dao = new InstructionSet();
			filter.Parameters.Each(p=>
			{
				dao.Property(p.ColumnName, p.Value);
			});
			dao.Save(db);
			return dao;
		}
		
		private static InstructionSet OneOrThrow(InstructionSetCollection c)
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
