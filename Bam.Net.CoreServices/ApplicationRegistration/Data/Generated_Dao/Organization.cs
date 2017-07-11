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

namespace Bam.Net.CoreServices.ApplicationRegistration.Dao
{
	// schema = ApplicationRegistration
	// connection Name = ApplicationRegistration
	[Serializable]
	[Bam.Net.Data.Table("Organization", "ApplicationRegistration")]
	public partial class Organization: Bam.Net.Data.Dao
	{
		public Organization():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public Organization(DataRow data)
			: base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public Organization(Database db)
			: base(db)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public Organization(Database db, DataRow data)
			: base(db, data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		[Bam.Net.Exclude]
		public static implicit operator Organization(DataRow data)
		{
			return new Organization(data);
		}

		private void SetChildren()
		{
			if(_database != null)
			{
				this.ChildCollections.Add("Application_OrganizationId", new ApplicationCollection(Database.GetQuery<ApplicationColumns, Application>((c) => c.OrganizationId == GetLongValue("Id")), this, "OrganizationId"));				
			}			if(_database != null)
			{
				this.ChildCollections.Add("OrganizationUser_OrganizationId", new OrganizationUserCollection(Database.GetQuery<OrganizationUserColumns, OrganizationUser>((c) => c.OrganizationId == GetLongValue("Id")), this, "OrganizationId"));				
			}			
            this.ChildCollections.Add("Organization_OrganizationUser_User",  new XrefDaoCollection<OrganizationUser, User>(this, false));
							
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



				

	[Bam.Net.Exclude]	
	public ApplicationCollection ApplicationsByOrganizationId
	{
		get
		{
			if (this.IsNew)
			{
				throw new InvalidOperationException("The current instance of type({0}) hasn't been saved and will have no child collections, call Save() or Save(Database) first."._Format(this.GetType().Name));
			}

			if(!this.ChildCollections.ContainsKey("Application_OrganizationId"))
			{
				SetChildren();
			}

			var c = (ApplicationCollection)this.ChildCollections["Application_OrganizationId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
	
	[Bam.Net.Exclude]	
	public OrganizationUserCollection OrganizationUsersByOrganizationId
	{
		get
		{
			if (this.IsNew)
			{
				throw new InvalidOperationException("The current instance of type({0}) hasn't been saved and will have no child collections, call Save() or Save(Database) first."._Format(this.GetType().Name));
			}

			if(!this.ChildCollections.ContainsKey("OrganizationUser_OrganizationId"))
			{
				SetChildren();
			}

			var c = (OrganizationUserCollection)this.ChildCollections["OrganizationUser_OrganizationId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
			

		// Xref       
        public XrefDaoCollection<OrganizationUser, User> Users
        {
            get
            {			
				if (this.IsNew)
				{
					throw new InvalidOperationException("The current instance of type({0}) hasn't been saved and will have no child collections, call Save() or Save(Database) first."._Format(this.GetType().Name));
				}

				if(!this.ChildCollections.ContainsKey("Organization_OrganizationUser_User"))
				{
					SetChildren();
				}

				var xref = (XrefDaoCollection<OrganizationUser, User>)this.ChildCollections["Organization_OrganizationUser_User"];
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
				return UniqueFilterProvider();
			}
			else
			{
				var colFilter = new OrganizationColumns();
				return (colFilter.KeyColumn == IdValue);
			}			
		}

		/// <summary>
		/// Return every record in the Organization table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static OrganizationCollection LoadAll(Database database = null)
		{
			SqlStringBuilder sql = new SqlStringBuilder();
			sql.Select<Organization>();
			Database db = database ?? Db.For<Organization>();
			var results = new OrganizationCollection(db, sql.GetDataTable(db));
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Process all records in batches of the specified size
		/// </summary>
		[Bam.Net.Exclude]
		public static async Task BatchAll(int batchSize, Action<IEnumerable<Organization>> batchProcessor, Database database = null)
		{
			await Task.Run(async ()=>
			{
				OrganizationColumns columns = new OrganizationColumns();
				var orderBy = Bam.Net.Data.Order.By<OrganizationColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
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
		public static async Task BatchQuery(int batchSize, QueryFilter filter, Action<IEnumerable<Organization>> batchProcessor, Database database = null)
		{
			await BatchQuery(batchSize, (c) => filter, batchProcessor, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery(int batchSize, WhereDelegate<OrganizationColumns> where, Action<IEnumerable<Organization>> batchProcessor, Database database = null)
		{
			await Task.Run(async ()=>
			{
				OrganizationColumns columns = new OrganizationColumns();
				var orderBy = Bam.Net.Data.Order.By<OrganizationColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (OrganizationColumns)where(columns) && columns.KeyColumn > topId, orderBy, database);
				}
			});			
		}

		public static Organization GetById(int id, Database database = null)
		{
			return GetById((long)id, database);
		}

		public static Organization GetById(long id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static Organization GetByUuid(string uuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Uuid") == uuid, database);
		}

		public static Organization GetByCuid(string cuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Cuid") == cuid, database);
		}

		[Bam.Net.Exclude]
		public static OrganizationCollection Query(QueryFilter filter, Database database = null)
		{
			return Where(filter, database);
		}

		[Bam.Net.Exclude]		
		public static OrganizationCollection Where(QueryFilter filter, Database database = null)
		{
			WhereDelegate<OrganizationColumns> whereDelegate = (c) => filter;
			return Where(whereDelegate, database);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a OrganizationColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between OrganizationColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static OrganizationCollection Where(Func<OrganizationColumns, QueryFilter<OrganizationColumns>> where, OrderBy<OrganizationColumns> orderBy = null, Database database = null)
		{
			database = database ?? Db.For<Organization>();
			return new OrganizationCollection(database.GetQuery<OrganizationColumns, Organization>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a OrganizationColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between OrganizationColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static OrganizationCollection Where(WhereDelegate<OrganizationColumns> where, Database database = null)
		{		
			database = database ?? Db.For<Organization>();
			var results = new OrganizationCollection(database, database.GetQuery<OrganizationColumns, Organization>(where), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a OrganizationColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between OrganizationColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static OrganizationCollection Where(WhereDelegate<OrganizationColumns> where, OrderBy<OrganizationColumns> orderBy = null, Database database = null)
		{		
			database = database ?? Db.For<Organization>();
			var results = new OrganizationCollection(database, database.GetQuery<OrganizationColumns, Organization>(where, orderBy), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate&lt;OrganizationColumns&gt;.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static OrganizationCollection Where(QiQuery where, Database database = null)
		{
			var results = new OrganizationCollection(database, Select<OrganizationColumns>.From<Organization>().Where(where, database));
			return results;
		}
				
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		[Bam.Net.Exclude]
		public static Organization GetOneWhere(QueryFilter where, Database database = null)
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
		public static Organization OneWhere(QueryFilter where, Database database = null)
		{
			WhereDelegate<OrganizationColumns> whereDelegate = (c) => where;
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
		public static Organization GetOneWhere(WhereDelegate<OrganizationColumns> where, Database database = null)
		{
			var result = OneWhere(where, database);
			if(result == null)
			{
				OrganizationColumns c = new OrganizationColumns();
				IQueryFilter filter = where(c); 
				result = CreateFromFilter(filter, database);
			}

			return result;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single Organization instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a OrganizationColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between OrganizationColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static Organization OneWhere(WhereDelegate<OrganizationColumns> where, Database database = null)
		{
			var result = Top(1, where, database);
			return OneOrThrow(result);
		}
					 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<OrganizationColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static Organization OneWhere(QiQuery where, Database database = null)
		{
			var results = Top(1, where, database);
			return OneOrThrow(results);
		}

		/// <summary>
		/// Execute a query and return the first result.  This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a OrganizationColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between OrganizationColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static Organization FirstOneWhere(WhereDelegate<OrganizationColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a OrganizationColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between OrganizationColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static Organization FirstOneWhere(WhereDelegate<OrganizationColumns> where, OrderBy<OrganizationColumns> orderBy, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a OrganizationColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between OrganizationColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static Organization FirstOneWhere(QueryFilter where, OrderBy<OrganizationColumns> orderBy = null, Database database = null)
		{
			WhereDelegate<OrganizationColumns> whereDelegate = (c) => where;
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
		/// <param name="where">A WhereDelegate that recieves a OrganizationColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between OrganizationColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static OrganizationCollection Top(int count, WhereDelegate<OrganizationColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a OrganizationColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between OrganizationColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static OrganizationCollection Top(int count, WhereDelegate<OrganizationColumns> where, OrderBy<OrganizationColumns> orderBy, Database database = null)
		{
			OrganizationColumns c = new OrganizationColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database ?? Db.For<Organization>();
			QuerySet query = GetQuerySet(db); 
			query.Top<Organization>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<OrganizationColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<OrganizationCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static OrganizationCollection Top(int count, QueryFilter where, Database database)
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
		public static OrganizationCollection Top(int count, QueryFilter where, OrderBy<OrganizationColumns> orderBy = null, Database database = null)
		{
			Database db = database ?? Db.For<Organization>();
			QuerySet query = GetQuerySet(db);
			query.Top<Organization>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy<OrganizationColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<OrganizationCollection>(0);
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
		public static OrganizationCollection Top(int count, QiQuery where, Database database = null)
		{
			Database db = database ?? Db.For<Organization>();
			QuerySet query = GetQuerySet(db);
			query.Top<Organization>(count);
			query.Where(where);
			query.Execute(db);
			var results = query.Results.As<OrganizationCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Return the count of Organizations
		/// </summary>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		public static long Count(Database database = null)
        {
			Database db = database ?? Db.For<Organization>();
            QuerySet query = GetQuerySet(db);
            query.Count<Organization>();
            query.Execute(db);
            return (long)query.Results[0].DataRow[0];
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a OrganizationColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between OrganizationColumns and other values
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static long Count(WhereDelegate<OrganizationColumns> where, Database database = null)
		{
			OrganizationColumns c = new OrganizationColumns();
			IQueryFilter filter = where(c) ;

			Database db = database ?? Db.For<Organization>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<Organization>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
		 
		public static long Count(QiQuery where, Database database = null)
		{
		    Database db = database ?? Db.For<Organization>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<Organization>();
			query.Where(where);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		} 		

		private static Organization CreateFromFilter(IQueryFilter filter, Database database = null)
		{
			Database db = database ?? Db.For<Organization>();			
			var dao = new Organization();
			filter.Parameters.Each(p=>
			{
				dao.Property(p.ColumnName, p.Value);
			});
			dao.Save(db);
			return dao;
		}
		
		private static Organization OneOrThrow(OrganizationCollection c)
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
