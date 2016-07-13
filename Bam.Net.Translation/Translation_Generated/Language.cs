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

namespace Bam.Net.Translation
{
	// schema = Translation
	// connection Name = Translation
	[Serializable]
	[Bam.Net.Data.Table("Language", "Translation")]
	public partial class Language: Dao
	{
		public Language():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public Language(DataRow data)
			: base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public Language(Database db)
			: base(db)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public Language(Database db, DataRow data)
			: base(db, data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public static implicit operator Language(DataRow data)
		{
			return new Language(data);
		}

		private void SetChildren()
		{

            this.ChildCollections.Add("Text_LanguageId", new TextCollection(Database.GetQuery<TextColumns, Text>((c) => c.LanguageId == GetLongValue("Id")), this, "LanguageId"));	
            this.ChildCollections.Add("LanguageDetection_LanguageId", new LanguageDetectionCollection(Database.GetQuery<LanguageDetectionColumns, LanguageDetection>((c) => c.LanguageId == GetLongValue("Id")), this, "LanguageId"));	
            this.ChildCollections.Add("Translation_LanguageId", new TranslationCollection(Database.GetQuery<TranslationColumns, Translation>((c) => c.LanguageId == GetLongValue("Id")), this, "LanguageId"));	
            this.ChildCollections.Add("OtherName_LanguageId", new OtherNameCollection(Database.GetQuery<OtherNameColumns, OtherName>((c) => c.LanguageId == GetLongValue("Id")), this, "LanguageId"));							
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

	// property:EnglishName, columnName:EnglishName	
	[Bam.Net.Data.Column(Name="EnglishName", DbDataType="VarChar", MaxLength="4000", AllowNull=false)]
	public string EnglishName
	{
		get
		{
			return GetStringValue("EnglishName");
		}
		set
		{
			SetValue("EnglishName", value);
		}
	}

	// property:FrenchName, columnName:FrenchName	
	[Bam.Net.Data.Column(Name="FrenchName", DbDataType="VarChar", MaxLength="4000", AllowNull=false)]
	public string FrenchName
	{
		get
		{
			return GetStringValue("FrenchName");
		}
		set
		{
			SetValue("FrenchName", value);
		}
	}

	// property:GermanName, columnName:GermanName	
	[Bam.Net.Data.Column(Name="GermanName", DbDataType="VarChar", MaxLength="4000", AllowNull=false)]
	public string GermanName
	{
		get
		{
			return GetStringValue("GermanName");
		}
		set
		{
			SetValue("GermanName", value);
		}
	}

	// property:ISO6392, columnName:ISO639_2	
	[Bam.Net.Data.Column(Name="ISO639_2", DbDataType="VarChar", MaxLength="4000", AllowNull=false)]
	public string ISO6392
	{
		get
		{
			return GetStringValue("ISO639_2");
		}
		set
		{
			SetValue("ISO639_2", value);
		}
	}

	// property:ISO6391, columnName:ISO639_1	
	[Bam.Net.Data.Column(Name="ISO639_1", DbDataType="VarChar", MaxLength="4000", AllowNull=false)]
	public string ISO6391
	{
		get
		{
			return GetStringValue("ISO639_1");
		}
		set
		{
			SetValue("ISO639_1", value);
		}
	}



				

	[Exclude]	
	public TextCollection TextsByLanguageId
	{
		get
		{
			if (this.IsNew)
			{
				throw new InvalidOperationException("The current instance of type({0}) hasn't been saved and will have no child collections, call Save() or Save(Database) first."._Format(this.GetType().Name));
			}

			if(!this.ChildCollections.ContainsKey("Text_LanguageId"))
			{
				SetChildren();
			}

			var c = (TextCollection)this.ChildCollections["Text_LanguageId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
	
	[Exclude]	
	public LanguageDetectionCollection LanguageDetectionsByLanguageId
	{
		get
		{
			if (this.IsNew)
			{
				throw new InvalidOperationException("The current instance of type({0}) hasn't been saved and will have no child collections, call Save() or Save(Database) first."._Format(this.GetType().Name));
			}

			if(!this.ChildCollections.ContainsKey("LanguageDetection_LanguageId"))
			{
				SetChildren();
			}

			var c = (LanguageDetectionCollection)this.ChildCollections["LanguageDetection_LanguageId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
	
	[Exclude]	
	public TranslationCollection TranslationsByLanguageId
	{
		get
		{
			if (this.IsNew)
			{
				throw new InvalidOperationException("The current instance of type({0}) hasn't been saved and will have no child collections, call Save() or Save(Database) first."._Format(this.GetType().Name));
			}

			if(!this.ChildCollections.ContainsKey("Translation_LanguageId"))
			{
				SetChildren();
			}

			var c = (TranslationCollection)this.ChildCollections["Translation_LanguageId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
	
	[Exclude]	
	public OtherNameCollection OtherNamesByLanguageId
	{
		get
		{
			if (this.IsNew)
			{
				throw new InvalidOperationException("The current instance of type({0}) hasn't been saved and will have no child collections, call Save() or Save(Database) first."._Format(this.GetType().Name));
			}

			if(!this.ChildCollections.ContainsKey("OtherName_LanguageId"))
			{
				SetChildren();
			}

			var c = (OtherNameCollection)this.ChildCollections["OtherName_LanguageId"];
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
				var colFilter = new LanguageColumns();
				return (colFilter.KeyColumn == IdValue);
			}			
		}

		/// <summary>
		/// Return every record in the Language table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static LanguageCollection LoadAll(Database database = null)
		{
			SqlStringBuilder sql = new SqlStringBuilder();
			sql.Select<Language>();
			Database db = database ?? Db.For<Language>();
			var results = new LanguageCollection(sql.GetDataTable(db));
			results.Database = db;
			return results;
		}

		public static async Task BatchAll(int batchSize, Action<IEnumerable<Language>> batchProcessor, Database database = null)
		{
			await Task.Run(async ()=>
			{
				LanguageColumns columns = new LanguageColumns();
				var orderBy = Order.By<LanguageColumns>(c => c.KeyColumn, SortOrder.Ascending);
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

		public static async Task BatchQuery(int batchSize, QueryFilter filter, Action<IEnumerable<Language>> batchProcessor, Database database = null)
		{
			await BatchQuery(batchSize, (c) => filter, batchProcessor, database);			
		}

		public static async Task BatchQuery(int batchSize, WhereDelegate<LanguageColumns> where, Action<IEnumerable<Language>> batchProcessor, Database database = null)
		{
			await Task.Run(async ()=>
			{
				LanguageColumns columns = new LanguageColumns();
				var orderBy = Order.By<LanguageColumns>(c => c.KeyColumn, SortOrder.Ascending);
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (LanguageColumns)where(columns) && columns.KeyColumn > topId, orderBy, database);
				}
			});			
		}

		public static Language GetById(int id, Database database = null)
		{
			return GetById((long)id, database);
		}

		public static Language GetById(long id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static Language GetByUuid(string uuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Uuid") == uuid, database);
		}

		public static Language GetByCuid(string cuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Cuid") == cuid, database);
		}

		public static LanguageCollection Query(QueryFilter filter, Database database = null)
		{
			return Where(filter, database);
		}
				
		public static LanguageCollection Where(QueryFilter filter, Database database = null)
		{
			WhereDelegate<LanguageColumns> whereDelegate = (c) => filter;
			return Where(whereDelegate, database);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a LanguageColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between LanguageColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static LanguageCollection Where(Func<LanguageColumns, QueryFilter<LanguageColumns>> where, OrderBy<LanguageColumns> orderBy = null, Database database = null)
		{
			database = database ?? Db.For<Language>();
			return new LanguageCollection(database.GetQuery<LanguageColumns, Language>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a LanguageColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between LanguageColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static LanguageCollection Where(WhereDelegate<LanguageColumns> where, Database database = null)
		{		
			database = database ?? Db.For<Language>();
			var results = new LanguageCollection(database, database.GetQuery<LanguageColumns, Language>(where), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a LanguageColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between LanguageColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		public static LanguageCollection Where(WhereDelegate<LanguageColumns> where, OrderBy<LanguageColumns> orderBy = null, Database database = null)
		{		
			database = database ?? Db.For<Language>();
			var results = new LanguageCollection(database, database.GetQuery<LanguageColumns, Language>(where, orderBy), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate&lt;LanguageColumns&gt;.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static LanguageCollection Where(QiQuery where, Database database = null)
		{
			var results = new LanguageCollection(database, Select<LanguageColumns>.From<Language>().Where(where, database));
			return results;
		}
				
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		public static Language GetOneWhere(QueryFilter where, Database database = null)
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
		public static Language OneWhere(QueryFilter where, Database database = null)
		{
			WhereDelegate<LanguageColumns> whereDelegate = (c) => where;
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
		public static Language GetOneWhere(WhereDelegate<LanguageColumns> where, Database database = null)
		{
			var result = OneWhere(where, database);
			if(result == null)
			{
				LanguageColumns c = new LanguageColumns();
				IQueryFilter filter = where(c); 
				result = CreateFromFilter(filter, database);
			}

			return result;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single Language instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a LanguageColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between LanguageColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static Language OneWhere(WhereDelegate<LanguageColumns> where, Database database = null)
		{
			var result = Top(1, where, database);
			return OneOrThrow(result);
		}
					 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<LanguageColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static Language OneWhere(QiQuery where, Database database = null)
		{
			var results = Top(1, where, database);
			return OneOrThrow(results);
		}

		/// <summary>
		/// Execute a query and return the first result.  This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a LanguageColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between LanguageColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static Language FirstOneWhere(WhereDelegate<LanguageColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a LanguageColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between LanguageColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static Language FirstOneWhere(WhereDelegate<LanguageColumns> where, OrderBy<LanguageColumns> orderBy, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a LanguageColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between LanguageColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static Language FirstOneWhere(QueryFilter where, OrderBy<LanguageColumns> orderBy = null, Database database = null)
		{
			WhereDelegate<LanguageColumns> whereDelegate = (c) => where;
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
		/// <param name="where">A WhereDelegate that recieves a LanguageColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between LanguageColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static LanguageCollection Top(int count, WhereDelegate<LanguageColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a LanguageColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between LanguageColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		public static LanguageCollection Top(int count, WhereDelegate<LanguageColumns> where, OrderBy<LanguageColumns> orderBy, Database database = null)
		{
			LanguageColumns c = new LanguageColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database ?? Db.For<Language>();
			QuerySet query = GetQuerySet(db); 
			query.Top<Language>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<LanguageColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<LanguageCollection>(0);
			results.Database = db;
			return results;
		}

		public static LanguageCollection Top(int count, QueryFilter where, Database database)
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
		public static LanguageCollection Top(int count, QueryFilter where, OrderBy<LanguageColumns> orderBy = null, Database database = null)
		{
			Database db = database ?? Db.For<Language>();
			QuerySet query = GetQuerySet(db);
			query.Top<Language>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy<LanguageColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<LanguageCollection>(0);
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
		public static LanguageCollection Top(int count, QiQuery where, Database database = null)
		{
			Database db = database ?? Db.For<Language>();
			QuerySet query = GetQuerySet(db);
			query.Top<Language>(count);
			query.Where(where);
			query.Execute(db);
			var results = query.Results.As<LanguageCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Return the count of Languages
		/// </summary>
		public static long Count(Database database = null)
        {
			Database db = database ?? Db.For<Language>();
            QuerySet query = GetQuerySet(db);
            query.Count<Language>();
            query.Execute(db);
            return (long)query.Results[0].DataRow[0];
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a LanguageColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between LanguageColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<LanguageColumns> where, Database database = null)
		{
			LanguageColumns c = new LanguageColumns();
			IQueryFilter filter = where(c) ;

			Database db = database ?? Db.For<Language>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<Language>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}

		private static Language CreateFromFilter(IQueryFilter filter, Database database = null)
		{
			Database db = database ?? Db.For<Language>();			
			var dao = new Language();
			filter.Parameters.Each(p=>
			{
				dao.Property(p.ColumnName, p.Value);
			});
			dao.Save(db);
			return dao;
		}
		
		private static Language OneOrThrow(LanguageCollection c)
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
