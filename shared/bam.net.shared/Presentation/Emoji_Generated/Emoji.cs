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

namespace Bam.Net.Presentation.Unicode
{
	// schema = Emojis
	// connection Name = Emojis
	[Serializable]
	[Bam.Net.Data.Table("Emoji", "Emojis")]
	public partial class Emoji: Bam.Net.Data.Dao
	{
		public Emoji():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public Emoji(DataRow data)
			: base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public Emoji(Database db)
			: base(db)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public Emoji(Database db, DataRow data)
			: base(db, data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		[Bam.Net.Exclude]
		public static implicit operator Emoji(DataRow data)
		{
			return new Emoji(data);
		}

		private void SetChildren()
		{

			if(_database != null)
			{
				this.ChildCollections.Add("Code_EmojiId", new CodeCollection(Database.GetQuery<CodeColumns, Code>((c) => c.EmojiId == GetULongValue("Id")), this, "EmojiId"));				
			}						
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

	// property:Character, columnName:Character	
	[Bam.Net.Data.Column(Name="Character", DbDataType="VarChar", MaxLength="4000", AllowNull=false)]
	public string Character
	{
		get
		{
			return GetStringValue("Character");
		}
		set
		{
			SetValue("Character", value);
		}
	}

	// property:Apple, columnName:Apple	
	[Bam.Net.Data.Column(Name="Apple", DbDataType="VarChar", MaxLength="4000", AllowNull=false)]
	public string Apple
	{
		get
		{
			return GetStringValue("Apple");
		}
		set
		{
			SetValue("Apple", value);
		}
	}

	// property:Google, columnName:Google	
	[Bam.Net.Data.Column(Name="Google", DbDataType="VarChar", MaxLength="4000", AllowNull=false)]
	public string Google
	{
		get
		{
			return GetStringValue("Google");
		}
		set
		{
			SetValue("Google", value);
		}
	}

	// property:Twitter, columnName:Twitter	
	[Bam.Net.Data.Column(Name="Twitter", DbDataType="VarChar", MaxLength="4000", AllowNull=false)]
	public string Twitter
	{
		get
		{
			return GetStringValue("Twitter");
		}
		set
		{
			SetValue("Twitter", value);
		}
	}

	// property:One, columnName:One	
	[Bam.Net.Data.Column(Name="One", DbDataType="VarChar", MaxLength="4000", AllowNull=false)]
	public string One
	{
		get
		{
			return GetStringValue("One");
		}
		set
		{
			SetValue("One", value);
		}
	}

	// property:Facebook, columnName:Facebook	
	[Bam.Net.Data.Column(Name="Facebook", DbDataType="VarChar", MaxLength="4000", AllowNull=false)]
	public string Facebook
	{
		get
		{
			return GetStringValue("Facebook");
		}
		set
		{
			SetValue("Facebook", value);
		}
	}

	// property:Samsung, columnName:Samsung	
	[Bam.Net.Data.Column(Name="Samsung", DbDataType="VarChar", MaxLength="4000", AllowNull=false)]
	public string Samsung
	{
		get
		{
			return GetStringValue("Samsung");
		}
		set
		{
			SetValue("Samsung", value);
		}
	}

	// property:Windows, columnName:Windows	
	[Bam.Net.Data.Column(Name="Windows", DbDataType="VarChar", MaxLength="4000", AllowNull=false)]
	public string Windows
	{
		get
		{
			return GetStringValue("Windows");
		}
		set
		{
			SetValue("Windows", value);
		}
	}

	// property:GMail, columnName:GMail	
	[Bam.Net.Data.Column(Name="GMail", DbDataType="VarChar", MaxLength="4000", AllowNull=false)]
	public string GMail
	{
		get
		{
			return GetStringValue("GMail");
		}
		set
		{
			SetValue("GMail", value);
		}
	}

	// property:SoftBank, columnName:SoftBank	
	[Bam.Net.Data.Column(Name="SoftBank", DbDataType="VarChar", MaxLength="4000", AllowNull=false)]
	public string SoftBank
	{
		get
		{
			return GetStringValue("SoftBank");
		}
		set
		{
			SetValue("SoftBank", value);
		}
	}

	// property:DoCoMo, columnName:DoCoMo	
	[Bam.Net.Data.Column(Name="DoCoMo", DbDataType="VarChar", MaxLength="4000", AllowNull=false)]
	public string DoCoMo
	{
		get
		{
			return GetStringValue("DoCoMo");
		}
		set
		{
			SetValue("DoCoMo", value);
		}
	}

	// property:KDDI, columnName:KDDI	
	[Bam.Net.Data.Column(Name="KDDI", DbDataType="VarChar", MaxLength="4000", AllowNull=false)]
	public string KDDI
	{
		get
		{
			return GetStringValue("KDDI");
		}
		set
		{
			SetValue("KDDI", value);
		}
	}

	// property:ShortName, columnName:ShortName	
	[Bam.Net.Data.Column(Name="ShortName", DbDataType="VarChar", MaxLength="4000", AllowNull=false)]
	public string ShortName
	{
		get
		{
			return GetStringValue("ShortName");
		}
		set
		{
			SetValue("ShortName", value);
		}
	}



	// start CategoryId -> CategoryId
	[Bam.Net.Data.ForeignKey(
        Table="Emoji",
		Name="CategoryId", 
		DbDataType="BigInt", 
		MaxLength="",
		AllowNull=true, 
		ReferencedKey="Id",
		ReferencedTable="Category",
		Suffix="1")]
	public ulong? CategoryId
	{
		get
		{
			return GetULongValue("CategoryId");
		}
		set
		{
			SetValue("CategoryId", value);
		}
	}

	Category _categoryOfCategoryId;
	public Category CategoryOfCategoryId
	{
		get
		{
			if(_categoryOfCategoryId == null)
			{
				_categoryOfCategoryId = Bam.Net.Presentation.Unicode.Category.OneWhere(c => c.KeyColumn == this.CategoryId, this.Database);
			}
			return _categoryOfCategoryId;
		}
	}
	
				

	[Bam.Net.Exclude]	
	public CodeCollection CodesByEmojiId
	{
		get
		{
			if (this.IsNew)
			{
				throw new InvalidOperationException("The current instance of type({0}) hasn't been saved and will have no child collections, call Save() or Save(Database) first."._Format(this.GetType().Name));
			}

			if(!this.ChildCollections.ContainsKey("Code_EmojiId"))
			{
				SetChildren();
			}

			var c = (CodeCollection)this.ChildCollections["Code_EmojiId"];
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
				var colFilter = new EmojiColumns();
				return (colFilter.KeyColumn == IdValue);
			}			
		}

		/// <summary>
		/// Return every record in the Emoji table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static EmojiCollection LoadAll(Database database = null)
		{
			Database db = database ?? Db.For<Emoji>();
			SqlStringBuilder sql = db.GetSqlStringBuilder();
			sql.Select<Emoji>();
			var results = new EmojiCollection(db, sql.GetDataTable(db))
			{
				Database = db
			};
			return results;
		}

		/// <summary>
		/// Process all records in batches of the specified size
		/// </summary>
		[Bam.Net.Exclude]
		public static async Task BatchAll(int batchSize, Action<IEnumerable<Emoji>> batchProcessor, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				EmojiColumns columns = new EmojiColumns();
				var orderBy = Bam.Net.Data.Order.By<EmojiColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
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
		public static async Task BatchQuery(int batchSize, QueryFilter filter, Action<IEnumerable<Emoji>> batchProcessor, Database database = null)
		{
			await BatchQuery(batchSize, (c) => filter, batchProcessor, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery(int batchSize, WhereDelegate<EmojiColumns> where, Action<IEnumerable<Emoji>> batchProcessor, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				EmojiColumns columns = new EmojiColumns();
				var orderBy = Bam.Net.Data.Order.By<EmojiColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (EmojiColumns)where(columns) && columns.KeyColumn > topId, orderBy, database);
				}
			});			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>			 
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, QueryFilter filter, Action<IEnumerable<Emoji>> batchProcessor, Bam.Net.Data.OrderBy<EmojiColumns> orderBy, Database database = null)
		{
			await BatchQuery<ColType>(batchSize, (c) => filter, batchProcessor, orderBy, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery<ColType>(int batchSize, WhereDelegate<EmojiColumns> where, Action<IEnumerable<Emoji>> batchProcessor, Bam.Net.Data.OrderBy<EmojiColumns> orderBy, Database database = null)
		{
			await System.Threading.Tasks.Task.Run(async ()=>
			{
				EmojiColumns columns = new EmojiColumns();
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await System.Threading.Tasks.Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					ColType top = results.Select(d => d.Property<ColType>(orderBy.Column.ToString())).ToArray().Largest();
					results = Top(batchSize, (EmojiColumns)where(columns) && orderBy.Column > top, orderBy, database);
				}
			});			
		}

		public static Emoji GetById(uint id, Database database = null)
		{
			return GetById((ulong)id, database);
		}

		public static Emoji GetById(int id, Database database = null)
		{
			return GetById((long)id, database);
		}

		public static Emoji GetById(long id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static Emoji GetById(ulong id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static Emoji GetByUuid(string uuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Uuid") == uuid, database);
		}

		public static Emoji GetByCuid(string cuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Cuid") == cuid, database);
		}

		[Bam.Net.Exclude]
		public static EmojiCollection Query(QueryFilter filter, Database database = null)
		{
			return Where(filter, database);
		}

		[Bam.Net.Exclude]		
		public static EmojiCollection Where(QueryFilter filter, Database database = null)
		{
			WhereDelegate<EmojiColumns> whereDelegate = (c) => filter;
			return Where(whereDelegate, database);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a EmojiColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between EmojiColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static EmojiCollection Where(Func<EmojiColumns, QueryFilter<EmojiColumns>> where, OrderBy<EmojiColumns> orderBy = null, Database database = null)
		{
			database = database ?? Db.For<Emoji>();
			return new EmojiCollection(database.GetQuery<EmojiColumns, Emoji>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a EmojiColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between EmojiColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static EmojiCollection Where(WhereDelegate<EmojiColumns> where, Database database = null)
		{		
			database = database ?? Db.For<Emoji>();
			var results = new EmojiCollection(database, database.GetQuery<EmojiColumns, Emoji>(where), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a EmojiColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between EmojiColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static EmojiCollection Where(WhereDelegate<EmojiColumns> where, OrderBy<EmojiColumns> orderBy = null, Database database = null)
		{		
			database = database ?? Db.For<Emoji>();
			var results = new EmojiCollection(database, database.GetQuery<EmojiColumns, Emoji>(where, orderBy), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate&lt;EmojiColumns&gt;.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static EmojiCollection Where(QiQuery where, Database database = null)
		{
			var results = new EmojiCollection(database, Select<EmojiColumns>.From<Emoji>().Where(where, database));
			return results;
		}
				
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		[Bam.Net.Exclude]
		public static Emoji GetOneWhere(QueryFilter where, Database database = null)
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
		public static Emoji OneWhere(QueryFilter where, Database database = null)
		{
			WhereDelegate<EmojiColumns> whereDelegate = (c) => where;
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
		public static Emoji GetOneWhere(WhereDelegate<EmojiColumns> where, Database database = null)
		{
			var result = OneWhere(where, database);
			if(result == null)
			{
				EmojiColumns c = new EmojiColumns();
				IQueryFilter filter = where(c); 
				result = CreateFromFilter(filter, database);
			}

			return result;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single Emoji instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a EmojiColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between EmojiColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static Emoji OneWhere(WhereDelegate<EmojiColumns> where, Database database = null)
		{
			var result = Top(1, where, database);
			return OneOrThrow(result);
		}
					 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<EmojiColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static Emoji OneWhere(QiQuery where, Database database = null)
		{
			var results = Top(1, where, database);
			return OneOrThrow(results);
		}

		/// <summary>
		/// Execute a query and return the first result.  This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a EmojiColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between EmojiColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static Emoji FirstOneWhere(WhereDelegate<EmojiColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a EmojiColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between EmojiColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static Emoji FirstOneWhere(WhereDelegate<EmojiColumns> where, OrderBy<EmojiColumns> orderBy, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a EmojiColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between EmojiColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static Emoji FirstOneWhere(QueryFilter where, OrderBy<EmojiColumns> orderBy = null, Database database = null)
		{
			WhereDelegate<EmojiColumns> whereDelegate = (c) => where;
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
		/// <param name="where">A WhereDelegate that recieves a EmojiColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between EmojiColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static EmojiCollection Top(int count, WhereDelegate<EmojiColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a EmojiColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between EmojiColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static EmojiCollection Top(int count, WhereDelegate<EmojiColumns> where, OrderBy<EmojiColumns> orderBy, Database database = null)
		{
			EmojiColumns c = new EmojiColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database ?? Db.For<Emoji>();
			QuerySet query = GetQuerySet(db); 
			query.Top<Emoji>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<EmojiColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<EmojiCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static EmojiCollection Top(int count, QueryFilter where, Database database)
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
		public static EmojiCollection Top(int count, QueryFilter where, OrderBy<EmojiColumns> orderBy = null, Database database = null)
		{
			Database db = database ?? Db.For<Emoji>();
			QuerySet query = GetQuerySet(db);
			query.Top<Emoji>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy<EmojiColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<EmojiCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static EmojiCollection Top(int count, QueryFilter where, string orderBy = null, SortOrder sortOrder = SortOrder.Ascending, Database database = null)
		{
			Database db = database ?? Db.For<Emoji>();
			QuerySet query = GetQuerySet(db);
			query.Top<Emoji>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy(orderBy, sortOrder);
			}

			query.Execute(db);
			var results = query.Results.As<EmojiCollection>(0);
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
		public static EmojiCollection Top(int count, QiQuery where, Database database = null)
		{
			Database db = database ?? Db.For<Emoji>();
			QuerySet query = GetQuerySet(db);
			query.Top<Emoji>(count);
			query.Where(where);
			query.Execute(db);
			var results = query.Results.As<EmojiCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Return the count of Emojis
		/// </summary>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		public static long Count(Database database = null)
        {
			Database db = database ?? Db.For<Emoji>();
            QuerySet query = GetQuerySet(db);
            query.Count<Emoji>();
            query.Execute(db);
            return (long)query.Results[0].DataRow[0];
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a EmojiColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between EmojiColumns and other values
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static long Count(WhereDelegate<EmojiColumns> where, Database database = null)
		{
			EmojiColumns c = new EmojiColumns();
			IQueryFilter filter = where(c) ;

			Database db = database ?? Db.For<Emoji>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<Emoji>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
		 
		public static long Count(QiQuery where, Database database = null)
		{
		    Database db = database ?? Db.For<Emoji>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<Emoji>();
			query.Where(where);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		} 		

		private static Emoji CreateFromFilter(IQueryFilter filter, Database database = null)
		{
			Database db = database ?? Db.For<Emoji>();			
			var dao = new Emoji();
			filter.Parameters.Each(p=>
			{
				dao.Property(p.ColumnName, p.Value);
			});
			dao.Save(db);
			return dao;
		}
		
		private static Emoji OneOrThrow(EmojiCollection c)
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
