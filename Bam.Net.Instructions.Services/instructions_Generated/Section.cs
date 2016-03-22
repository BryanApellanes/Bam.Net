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
	[Bam.Net.Data.Table("Section", "Instructions")]
	public partial class Section: Dao
	{
		public Section():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public Section(DataRow data)
			: base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public Section(Database db)
			: base(db)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public Section(Database db, DataRow data)
			: base(db, data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public static implicit operator Section(DataRow data)
		{
			return new Section(data);
		}

		private void SetChildren()
		{

            this.ChildCollections.Add("Step_SectionId", new StepCollection(Database.GetQuery<StepColumns, Step>((c) => c.SectionId == GetLongValue("Id")), this, "SectionId"));							
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

	// property:Title, columnName:Title	
	[Bam.Net.Data.Column(Name="Title", DbDataType="VarChar", MaxLength="4000", AllowNull=false)]
	public string Title
	{
		get
		{
			return GetStringValue("Title");
		}
		set
		{
			SetValue("Title", value);
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



	// start InstructionSetId -> InstructionSetId
	[Bam.Net.Data.ForeignKey(
        Table="Section",
		Name="InstructionSetId", 
		DbDataType="BigInt", 
		MaxLength="",
		AllowNull=true, 
		ReferencedKey="Id",
		ReferencedTable="InstructionSet",
		Suffix="1")]
	public long? InstructionSetId
	{
		get
		{
			return GetLongValue("InstructionSetId");
		}
		set
		{
			SetValue("InstructionSetId", value);
		}
	}

	InstructionSet _instructionSetOfInstructionSetId;
	public InstructionSet InstructionSetOfInstructionSetId
	{
		get
		{
			if(_instructionSetOfInstructionSetId == null)
			{
				_instructionSetOfInstructionSetId = Bam.Net.Instructions.InstructionSet.OneWhere(c => c.KeyColumn == this.InstructionSetId, this.Database);
			}
			return _instructionSetOfInstructionSetId;
		}
	}
	
				

	[Exclude]	
	public StepCollection StepsBySectionId
	{
		get
		{
			if (this.IsNew)
			{
				throw new InvalidOperationException("The current instance of type({0}) hasn't been saved and will have no child collections, call Save() or Save(Database) first."._Format(this.GetType().Name));
			}

			if(!this.ChildCollections.ContainsKey("Step_SectionId"))
			{
				SetChildren();
			}

			var c = (StepCollection)this.ChildCollections["Step_SectionId"];
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
				var colFilter = new SectionColumns();
				return (colFilter.KeyColumn == IdValue);
			}			
		}

		/// <summary>
		/// Return every record in the Section table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static SectionCollection LoadAll(Database database = null)
		{
			SqlStringBuilder sql = new SqlStringBuilder();
			sql.Select<Section>();
			Database db = database ?? Db.For<Section>();
			var results = new SectionCollection(sql.GetDataTable(db));
			results.Database = db;
			return results;
		}

		public static async Task BatchAll(int batchSize, Func<SectionCollection, Task> batchProcessor, Database database = null)
		{
			await Task.Run(async ()=>
			{
				SectionColumns columns = new SectionColumns();
				var orderBy = Order.By<SectionColumns>(c => c.KeyColumn, SortOrder.Ascending);
				var results = Top(batchSize, (c) => c.KeyColumn > 0, orderBy, database);
				while(results.Count > 0)
				{
					await batchProcessor(results);
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (c) => c.KeyColumn > topId, orderBy, database);
				}
			});			
		}	 

		public static async Task BatchQuery(int batchSize, QueryFilter filter, Func<SectionCollection, Task> batchProcessor, Database database = null)
		{
			await BatchQuery(batchSize, (c) => filter, batchProcessor, database);			
		}

		public static async Task BatchQuery(int batchSize, WhereDelegate<SectionColumns> where, Func<SectionCollection, Task> batchProcessor, Database database = null)
		{
			await Task.Run(async ()=>
			{
				SectionColumns columns = new SectionColumns();
				var orderBy = Order.By<SectionColumns>(c => c.KeyColumn, SortOrder.Ascending);
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await batchProcessor(results);
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (SectionColumns)where(columns) && columns.KeyColumn > topId, orderBy, database);
				}
			});			
		}

		public static Section GetById(int id, Database database = null)
		{
			return GetById((long)id, database);
		}

		public static Section GetById(long id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static Section GetByUuid(string uuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Uuid") == uuid, database);
		}

		public static Section GetByCuid(string cuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Cuid") == cuid, database);
		}

		public static SectionCollection Query(QueryFilter filter, Database database = null)
		{
			return Where(filter, database);
		}
				
		public static SectionCollection Where(QueryFilter filter, Database database = null)
		{
			WhereDelegate<SectionColumns> whereDelegate = (c) => filter;
			return Where(whereDelegate, database);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a SectionColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between SectionColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static SectionCollection Where(Func<SectionColumns, QueryFilter<SectionColumns>> where, OrderBy<SectionColumns> orderBy = null, Database database = null)
		{
			database = database ?? Db.For<Section>();
			return new SectionCollection(database.GetQuery<SectionColumns, Section>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a SectionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SectionColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static SectionCollection Where(WhereDelegate<SectionColumns> where, Database database = null)
		{		
			database = database ?? Db.For<Section>();
			var results = new SectionCollection(database, database.GetQuery<SectionColumns, Section>(where), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a SectionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SectionColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		public static SectionCollection Where(WhereDelegate<SectionColumns> where, OrderBy<SectionColumns> orderBy = null, Database database = null)
		{		
			database = database ?? Db.For<Section>();
			var results = new SectionCollection(database, database.GetQuery<SectionColumns, Section>(where, orderBy), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate&lt;SectionColumns&gt;.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static SectionCollection Where(QiQuery where, Database database = null)
		{
			var results = new SectionCollection(database, Select<SectionColumns>.From<Section>().Where(where, database));
			return results;
		}
				
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		public static Section GetOneWhere(QueryFilter where, Database database = null)
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
		public static Section OneWhere(QueryFilter where, Database database = null)
		{
			WhereDelegate<SectionColumns> whereDelegate = (c) => where;
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
		public static Section GetOneWhere(WhereDelegate<SectionColumns> where, Database database = null)
		{
			var result = OneWhere(where, database);
			if(result == null)
			{
				SectionColumns c = new SectionColumns();
				IQueryFilter filter = where(c); 
				result = CreateFromFilter(filter, database);
			}

			return result;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single Section instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a SectionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SectionColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static Section OneWhere(WhereDelegate<SectionColumns> where, Database database = null)
		{
			var result = Top(1, where, database);
			return OneOrThrow(result);
		}
					 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<SectionColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static Section OneWhere(QiQuery where, Database database = null)
		{
			var results = Top(1, where, database);
			return OneOrThrow(results);
		}

		/// <summary>
		/// Execute a query and return the first result.  This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a SectionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SectionColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static Section FirstOneWhere(WhereDelegate<SectionColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a SectionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SectionColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static Section FirstOneWhere(WhereDelegate<SectionColumns> where, OrderBy<SectionColumns> orderBy, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a SectionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SectionColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static Section FirstOneWhere(QueryFilter where, OrderBy<SectionColumns> orderBy = null, Database database = null)
		{
			WhereDelegate<SectionColumns> whereDelegate = (c) => where;
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
		/// <param name="where">A WhereDelegate that recieves a SectionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SectionColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static SectionCollection Top(int count, WhereDelegate<SectionColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a SectionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SectionColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		public static SectionCollection Top(int count, WhereDelegate<SectionColumns> where, OrderBy<SectionColumns> orderBy, Database database = null)
		{
			SectionColumns c = new SectionColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database ?? Db.For<Section>();
			QuerySet query = GetQuerySet(db); 
			query.Top<Section>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<SectionColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<SectionCollection>(0);
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
		public static SectionCollection Top(int count, QueryFilter where, OrderBy<SectionColumns> orderBy = null, Database database = null)
		{
			Database db = database ?? Db.For<Section>();
			QuerySet query = GetQuerySet(db);
			query.Top<Section>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy<SectionColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<SectionCollection>(0);
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
		public static SectionCollection Top(int count, QiQuery where, Database database = null)
		{
			Database db = database ?? Db.For<Section>();
			QuerySet query = GetQuerySet(db);
			query.Top<Section>(count);
			query.Where(where);
			query.Execute(db);
			var results = query.Results.As<SectionCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a SectionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SectionColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<SectionColumns> where, Database database = null)
		{
			SectionColumns c = new SectionColumns();
			IQueryFilter filter = where(c) ;

			Database db = database ?? Db.For<Section>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<Section>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}

		private static Section CreateFromFilter(IQueryFilter filter, Database database = null)
		{
			Database db = database ?? Db.For<Section>();			
			var dao = new Section();
			filter.Parameters.Each(p=>
			{
				dao.Property(p.ColumnName, p.Value);
			});
			dao.Save(db);
			return dao;
		}
		
		private static Section OneOrThrow(SectionCollection c)
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
