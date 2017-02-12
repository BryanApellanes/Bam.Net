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

namespace Bam.Net.Instructions
{
	// schema = Instructions
	// connection Name = Instructions
	[Serializable]
	[Bam.Net.Data.Table("Step", "Instructions")]
	public partial class Step: Dao
	{
		public Step():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public Step(DataRow data)
			: base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public Step(Database db)
			: base(db)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public Step(Database db, DataRow data)
			: base(db, data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		[Bam.Net.Exclude]
		public static implicit operator Step(DataRow data)
		{
			return new Step(data);
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

	// property:Number, columnName:Number	
	[Bam.Net.Data.Column(Name="Number", DbDataType="Int", MaxLength="10", AllowNull=false)]
	public int? Number
	{
		get
		{
			return GetIntValue("Number");
		}
		set
		{
			SetValue("Number", value);
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

	// property:Detail, columnName:Detail	
	[Bam.Net.Data.Column(Name="Detail", DbDataType="VarChar", MaxLength="4000", AllowNull=false)]
	public string Detail
	{
		get
		{
			return GetStringValue("Detail");
		}
		set
		{
			SetValue("Detail", value);
		}
	}



	// start SectionId -> SectionId
	[Bam.Net.Data.ForeignKey(
        Table="Step",
		Name="SectionId", 
		DbDataType="BigInt", 
		MaxLength="",
		AllowNull=true, 
		ReferencedKey="Id",
		ReferencedTable="Section",
		Suffix="1")]
	public long? SectionId
	{
		get
		{
			return GetLongValue("SectionId");
		}
		set
		{
			SetValue("SectionId", value);
		}
	}

	Section _sectionOfSectionId;
	public Section SectionOfSectionId
	{
		get
		{
			if(_sectionOfSectionId == null)
			{
				_sectionOfSectionId = Bam.Net.Instructions.Section.OneWhere(c => c.KeyColumn == this.SectionId, this.Database);
			}
			return _sectionOfSectionId;
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
				var colFilter = new StepColumns();
				return (colFilter.KeyColumn == IdValue);
			}			
		}

		/// <summary>
		/// Return every record in the Step table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static StepCollection LoadAll(Database database = null)
		{
			SqlStringBuilder sql = new SqlStringBuilder();
			sql.Select<Step>();
			Database db = database ?? Db.For<Step>();
			var results = new StepCollection(sql.GetDataTable(db));
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Process all records in batches of the specified size
		/// </summary>
		[Bam.Net.Exclude]
		public static async Task BatchAll(int batchSize, Action<IEnumerable<Step>> batchProcessor, Database database = null)
		{
			await Task.Run(async ()=>
			{
				StepColumns columns = new StepColumns();
				var orderBy = Bam.Net.Data.Order.By<StepColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
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
		public static async Task BatchQuery(int batchSize, QueryFilter filter, Action<IEnumerable<Step>> batchProcessor, Database database = null)
		{
			await BatchQuery(batchSize, (c) => filter, batchProcessor, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery(int batchSize, WhereDelegate<StepColumns> where, Action<IEnumerable<Step>> batchProcessor, Database database = null)
		{
			await Task.Run(async ()=>
			{
				StepColumns columns = new StepColumns();
				var orderBy = Bam.Net.Data.Order.By<StepColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (StepColumns)where(columns) && columns.KeyColumn > topId, orderBy, database);
				}
			});			
		}

		public static Step GetById(int id, Database database = null)
		{
			return GetById((long)id, database);
		}

		public static Step GetById(long id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static Step GetByUuid(string uuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Uuid") == uuid, database);
		}

		public static Step GetByCuid(string cuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Cuid") == cuid, database);
		}

		[Bam.Net.Exclude]
		public static StepCollection Query(QueryFilter filter, Database database = null)
		{
			return Where(filter, database);
		}

		[Bam.Net.Exclude]		
		public static StepCollection Where(QueryFilter filter, Database database = null)
		{
			WhereDelegate<StepColumns> whereDelegate = (c) => filter;
			return Where(whereDelegate, database);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a StepColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between StepColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static StepCollection Where(Func<StepColumns, QueryFilter<StepColumns>> where, OrderBy<StepColumns> orderBy = null, Database database = null)
		{
			database = database ?? Db.For<Step>();
			return new StepCollection(database.GetQuery<StepColumns, Step>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a StepColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between StepColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static StepCollection Where(WhereDelegate<StepColumns> where, Database database = null)
		{		
			database = database ?? Db.For<Step>();
			var results = new StepCollection(database, database.GetQuery<StepColumns, Step>(where), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a StepColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between StepColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static StepCollection Where(WhereDelegate<StepColumns> where, OrderBy<StepColumns> orderBy = null, Database database = null)
		{		
			database = database ?? Db.For<Step>();
			var results = new StepCollection(database, database.GetQuery<StepColumns, Step>(where, orderBy), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate&lt;StepColumns&gt;.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static StepCollection Where(QiQuery where, Database database = null)
		{
			var results = new StepCollection(database, Select<StepColumns>.From<Step>().Where(where, database));
			return results;
		}
				
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		[Bam.Net.Exclude]
		public static Step GetOneWhere(QueryFilter where, Database database = null)
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
		public static Step OneWhere(QueryFilter where, Database database = null)
		{
			WhereDelegate<StepColumns> whereDelegate = (c) => where;
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
		public static Step GetOneWhere(WhereDelegate<StepColumns> where, Database database = null)
		{
			var result = OneWhere(where, database);
			if(result == null)
			{
				StepColumns c = new StepColumns();
				IQueryFilter filter = where(c); 
				result = CreateFromFilter(filter, database);
			}

			return result;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single Step instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a StepColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between StepColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static Step OneWhere(WhereDelegate<StepColumns> where, Database database = null)
		{
			var result = Top(1, where, database);
			return OneOrThrow(result);
		}
					 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<StepColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static Step OneWhere(QiQuery where, Database database = null)
		{
			var results = Top(1, where, database);
			return OneOrThrow(results);
		}

		/// <summary>
		/// Execute a query and return the first result.  This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a StepColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between StepColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static Step FirstOneWhere(WhereDelegate<StepColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a StepColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between StepColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static Step FirstOneWhere(WhereDelegate<StepColumns> where, OrderBy<StepColumns> orderBy, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a StepColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between StepColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static Step FirstOneWhere(QueryFilter where, OrderBy<StepColumns> orderBy = null, Database database = null)
		{
			WhereDelegate<StepColumns> whereDelegate = (c) => where;
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
		/// <param name="where">A WhereDelegate that recieves a StepColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between StepColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static StepCollection Top(int count, WhereDelegate<StepColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a StepColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between StepColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static StepCollection Top(int count, WhereDelegate<StepColumns> where, OrderBy<StepColumns> orderBy, Database database = null)
		{
			StepColumns c = new StepColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database ?? Db.For<Step>();
			QuerySet query = GetQuerySet(db); 
			query.Top<Step>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<StepColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<StepCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static StepCollection Top(int count, QueryFilter where, Database database)
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
		public static StepCollection Top(int count, QueryFilter where, OrderBy<StepColumns> orderBy = null, Database database = null)
		{
			Database db = database ?? Db.For<Step>();
			QuerySet query = GetQuerySet(db);
			query.Top<Step>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy<StepColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<StepCollection>(0);
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
		public static StepCollection Top(int count, QiQuery where, Database database = null)
		{
			Database db = database ?? Db.For<Step>();
			QuerySet query = GetQuerySet(db);
			query.Top<Step>(count);
			query.Where(where);
			query.Execute(db);
			var results = query.Results.As<StepCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Return the count of Steps
		/// </summary>
		public static long Count(Database database = null)
        {
			Database db = database ?? Db.For<Step>();
            QuerySet query = GetQuerySet(db);
            query.Count<Step>();
            query.Execute(db);
            return (long)query.Results[0].DataRow[0];
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a StepColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between StepColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static long Count(WhereDelegate<StepColumns> where, Database database = null)
		{
			StepColumns c = new StepColumns();
			IQueryFilter filter = where(c) ;

			Database db = database ?? Db.For<Step>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<Step>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
		 
		public static long Count(QiQuery where, Database database = null)
		{
		    Database db = database ?? Db.For<Step>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<Step>();
			query.Where(where);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		} 		

		private static Step CreateFromFilter(IQueryFilter filter, Database database = null)
		{
			Database db = database ?? Db.For<Step>();			
			var dao = new Step();
			filter.Parameters.Each(p=>
			{
				dao.Property(p.ColumnName, p.Value);
			});
			dao.Save(db);
			return dao;
		}
		
		private static Step OneOrThrow(StepCollection c)
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
