/*
	Copyright © Bryan Apellanes 2015  
*/
// Model is Table
using System;
using System.Data;
using System.Data.Common;
using Bam.Net;
using Bam.Net.Data;
using Bam.Net.Data.Qi;

namespace Bam.Net.Data.Tests
{
	// schema = Shop
	// connection Name = Shop
	[Bam.Net.Data.Table("List", "Shop")]
	public partial class List: Dao
	{
		public List():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public List(DataRow data): base(data)
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public static implicit operator List(DataRow data)
		{
			return new List(data);
		}

		private void SetChildren()
		{

            this.ChildCollections.Add("ListItem_ListId", new ListItemCollection(new Query<ListItemColumns, ListItem>((c) => c.ListId == this.Id), this, "ListId"));				
            this.ChildCollections.Add("List_ListItem_Item",  new XrefDaoCollection<ListItem, Item>(this, false));
							
		}

	// property:Id, columnName:Id	
	[Exclude]
	[Bam.Net.Data.KeyColumn(Name="Id", DbDataType="BigInt", MaxLength="8")]
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



				

	[Exclude]	
	public ListItemCollection ListItemsByListId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("ListItem_ListId"))
			{
				SetChildren();
			}

			var c = (ListItemCollection)this.ChildCollections["ListItem_ListId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
			

		// Xref       
        public XrefDaoCollection<ListItem, Item> Items
        {
            get
            {
				if(!this.ChildCollections.ContainsKey("List_ListItem_Item"))
				{
					SetChildren();
				}

				var xref = (XrefDaoCollection<ListItem, Item>)this.ChildCollections["List_ListItem_Item"];
				if(!xref.Loaded)
				{
					xref.Load();
				}

				return xref;
            }
        }
		/// <summary>
		/// Gets a query filter that should uniquely identify
		/// the current instance.  The default implementation
		/// compares the Id/key field to the current instance.
		/// </summary> 
		public override IQueryFilter GetUniqueFilter()
		{
			var colFilter = new ListColumns();
			return (colFilter.Id == IdValue);
		}
		/// <summary>
		/// Return every record in the List table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static ListCollection LoadAll(Database database = null)
		{
			SqlStringBuilder sql = new SqlStringBuilder();
			sql.Select<List>();
			Database db = database == null ? Db.For<List>(): database;
			var results = new ListCollection(sql.GetDataTable(db));
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a ListColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between ListColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static ListCollection Where(Func<ListColumns, QueryFilter<ListColumns>> where, OrderBy<ListColumns> orderBy = null)
		{
			return new ListCollection(new Query<ListColumns, List>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ListColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ListColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static ListCollection Where(WhereDelegate<ListColumns> where, Database db = null)
		{
			var results = new ListCollection(db, new Query<ListColumns, List>(where, db), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ListColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ListColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static ListCollection Where(WhereDelegate<ListColumns> where, OrderBy<ListColumns> orderBy = null, Database db = null)
		{
			var results = new ListCollection(db, new Query<ListColumns, List>(where, orderBy, db), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<ListColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static ListCollection Where(QiQuery where, Database db = null)
		{
			var results = new ListCollection(db, Select<ListColumns>.From<List>().Where(where, db));
			return results;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single List instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ListColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ListColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static List OneWhere(WhereDelegate<ListColumns> where, Database db = null)
		{
			var results = new ListCollection(db, Select<ListColumns>.From<List>().Where(where, db));
			return OneOrThrow(results);
		}
			 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<ListColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static List OneWhere(QiQuery where, Database db = null)
		{
			var results = new ListCollection(db, Select<ListColumns>.From<List>().Where(where, db));
			return OneOrThrow(results);
		}

		private static List OneOrThrow(ListCollection c)
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

		/// <summary>
		/// Execute a query and return the first result
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ListColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ListColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static List FirstOneWhere(WhereDelegate<ListColumns> where, Database db = null)
		{
			var results = new ListCollection(db, Select<ListColumns>.From<List>().Where(where, db));
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
		/// of values
		/// </summary>
		/// <param name="count">The number of values to return.
		/// This value is used in the sql query so no more than this 
		/// number of values will be returned by the database.
		/// </param>
		/// <param name="where">A WhereDelegate that recieves a ListColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ListColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static ListCollection Top(int count, WhereDelegate<ListColumns> where, Database db = null)
		{
			return Top(count, where, null, db);
		}

		/// <summary>
		/// Execute a query and return the specified count
		/// of values
		/// </summary>
		/// <param name="count">The number of values to return.
		/// This value is used in the sql query so no more than this 
		/// number of values will be returned by the database.
		/// </param>
		/// <param name="where">A WhereDelegate that recieves a ListColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ListColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static ListCollection Top(int count, WhereDelegate<ListColumns> where, OrderBy<ListColumns> orderBy, Database database = null)
		{
			ListColumns c = new ListColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database == null ? Db.For<List>(): database;
			QuerySet query = GetQuerySet(db); 
			query.Top<List>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<ListColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<ListCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ListColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ListColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<ListColumns> where, Database database = null)
		{
			ListColumns c = new ListColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? Db.For<List>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<List>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
	}
}																								
