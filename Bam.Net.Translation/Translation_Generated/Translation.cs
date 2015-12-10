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

namespace Bam.Net.Translation
{
	// schema = Translation
	// connection Name = Translation
	[Serializable]
	[Bam.Net.Data.Table("Translation", "Translation")]
	public partial class Translation: Dao
	{
		public Translation():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public Translation(DataRow data)
			: base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public Translation(Database db)
			: base(db)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public Translation(Database db, DataRow data)
			: base(db, data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public static implicit operator Translation(DataRow data)
		{
			return new Translation(data);
		}

		private void SetChildren()
		{
						
		}

﻿	// property:Id, columnName:Id	
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

﻿	// property:Uuid, columnName:Uuid	
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

﻿	// property:TranslatorUuid, columnName:TranslatorUuid	
	[Bam.Net.Data.Column(Name="TranslatorUuid", DbDataType="VarChar", MaxLength="4000", AllowNull=false)]
	public string TranslatorUuid
	{
		get
		{
			return GetStringValue("TranslatorUuid");
		}
		set
		{
			SetValue("TranslatorUuid", value);
		}
	}

﻿	// property:Value, columnName:Value	
	[Bam.Net.Data.Column(Name="Value", DbDataType="VarChar", MaxLength="4000", AllowNull=false)]
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



﻿	// start TextId -> TextId
	[Bam.Net.Data.ForeignKey(
        Table="Translation",
		Name="TextId", 
		DbDataType="BigInt", 
		MaxLength="",
		AllowNull=true, 
		ReferencedKey="Id",
		ReferencedTable="Text",
		Suffix="1")]
	public long? TextId
	{
		get
		{
			return GetLongValue("TextId");
		}
		set
		{
			SetValue("TextId", value);
		}
	}

	Text _textOfTextId;
	public Text TextOfTextId
	{
		get
		{
			if(_textOfTextId == null)
			{
				_textOfTextId = Bam.Net.Translation.Text.OneWhere(c => c.KeyColumn == this.TextId, this.Database);
			}
			return _textOfTextId;
		}
	}
	
﻿	// start LanguageId -> LanguageId
	[Bam.Net.Data.ForeignKey(
        Table="Translation",
		Name="LanguageId", 
		DbDataType="BigInt", 
		MaxLength="",
		AllowNull=true, 
		ReferencedKey="Id",
		ReferencedTable="Language",
		Suffix="2")]
	public long? LanguageId
	{
		get
		{
			return GetLongValue("LanguageId");
		}
		set
		{
			SetValue("LanguageId", value);
		}
	}

	Language _languageOfLanguageId;
	public Language LanguageOfLanguageId
	{
		get
		{
			if(_languageOfLanguageId == null)
			{
				_languageOfLanguageId = Bam.Net.Translation.Language.OneWhere(c => c.KeyColumn == this.LanguageId, this.Database);
			}
			return _languageOfLanguageId;
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
				var colFilter = new TranslationColumns();
				return (colFilter.KeyColumn == IdValue);
			}			
		}

		/// <summary>
		/// Return every record in the Translation table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static TranslationCollection LoadAll(Database database = null)
		{
			SqlStringBuilder sql = new SqlStringBuilder();
			sql.Select<Translation>();
			Database db = database ?? Db.For<Translation>();
			var results = new TranslationCollection(sql.GetDataTable(db));
			results.Database = db;
			return results;
		}

		public static Translation GetById(int id, Database database = null)
		{
			return GetById((long)id, database);
		}

		public static Translation GetById(long id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static Translation GetByUuid(string uuid, Database database = null)
		{
			return OneWhere(c => c.Uuid == uuid, database);
		}

		public static TranslationCollection Query(QueryFilter filter, Database database = null)
		{
			return Where(filter, database);
		}
				
		public static TranslationCollection Where(QueryFilter filter, Database database = null)
		{
			WhereDelegate<TranslationColumns> whereDelegate = (c) => filter;
			return Where(whereDelegate, database);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a TranslationColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between TranslationColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static TranslationCollection Where(Func<TranslationColumns, QueryFilter<TranslationColumns>> where, OrderBy<TranslationColumns> orderBy = null, Database database = null)
		{
			database = database ?? Db.For<Translation>();
			return new TranslationCollection(database.GetQuery<TranslationColumns, Translation>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a TranslationColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between TranslationColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static TranslationCollection Where(WhereDelegate<TranslationColumns> where, Database database = null)
		{		
			database = database ?? Db.For<Translation>();
			var results = new TranslationCollection(database, database.GetQuery<TranslationColumns, Translation>(where), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a TranslationColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between TranslationColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		public static TranslationCollection Where(WhereDelegate<TranslationColumns> where, OrderBy<TranslationColumns> orderBy = null, Database database = null)
		{		
			database = database ?? Db.For<Translation>();
			var results = new TranslationCollection(database, database.GetQuery<TranslationColumns, Translation>(where, orderBy), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<TranslationColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static TranslationCollection Where(QiQuery where, Database database = null)
		{
			var results = new TranslationCollection(database, Select<TranslationColumns>.From<Translation>().Where(where, database));
			return results;
		}
				
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		public static Translation GetOneWhere(QueryFilter where, Database database = null)
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
		public static Translation OneWhere(QueryFilter where, Database database = null)
		{
			WhereDelegate<TranslationColumns> whereDelegate = (c) => where;
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
		public static Translation GetOneWhere(WhereDelegate<TranslationColumns> where, Database database = null)
		{
			var result = OneWhere(where, database);
			if(result == null)
			{
				TranslationColumns c = new TranslationColumns();
				IQueryFilter filter = where(c); 
				result = CreateFromFilter(filter, database);
			}

			return result;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single Translation instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a TranslationColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between TranslationColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static Translation OneWhere(WhereDelegate<TranslationColumns> where, Database database = null)
		{
			var result = Top(1, where, database);
			return OneOrThrow(result);
		}
					 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<TranslationColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static Translation OneWhere(QiQuery where, Database database = null)
		{
			var results = Top(1, where, database);
			return OneOrThrow(results);
		}

		/// <summary>
		/// Execute a query and return the first result.  This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a TranslationColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between TranslationColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static Translation FirstOneWhere(WhereDelegate<TranslationColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a TranslationColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between TranslationColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static Translation FirstOneWhere(WhereDelegate<TranslationColumns> where, OrderBy<TranslationColumns> orderBy, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a TranslationColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between TranslationColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static Translation FirstOneWhere(QueryFilter where, OrderBy<TranslationColumns> orderBy = null, Database database = null)
		{
			WhereDelegate<TranslationColumns> whereDelegate = (c) => where;
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
		/// <param name="where">A WhereDelegate that recieves a TranslationColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between TranslationColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static TranslationCollection Top(int count, WhereDelegate<TranslationColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a TranslationColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between TranslationColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		public static TranslationCollection Top(int count, WhereDelegate<TranslationColumns> where, OrderBy<TranslationColumns> orderBy, Database database = null)
		{
			TranslationColumns c = new TranslationColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database ?? Db.For<Translation>();
			QuerySet query = GetQuerySet(db); 
			query.Top<Translation>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<TranslationColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<TranslationCollection>(0);
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
		public static TranslationCollection Top(int count, QueryFilter where, OrderBy<TranslationColumns> orderBy = null, Database database = null)
		{
			Database db = database ?? Db.For<Translation>();
			QuerySet query = GetQuerySet(db);
			query.Top<Translation>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy<TranslationColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<TranslationCollection>(0);
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
		public static TranslationCollection Top(int count, QiQuery where, Database database = null)
		{
			Database db = database ?? Db.For<Translation>();
			QuerySet query = GetQuerySet(db);
			query.Top<Translation>(count);
			query.Where(where);
			query.Execute(db);
			var results = query.Results.As<TranslationCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a TranslationColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between TranslationColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<TranslationColumns> where, Database database = null)
		{
			TranslationColumns c = new TranslationColumns();
			IQueryFilter filter = where(c) ;

			Database db = database ?? Db.For<Translation>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<Translation>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}

		private static Translation CreateFromFilter(IQueryFilter filter, Database database = null)
		{
			Database db = database ?? Db.For<Translation>();			
			var dao = new Translation();
			filter.Parameters.Each(p=>
			{
				dao.Property(p.ColumnName, p.Value);
			});
			dao.Save(db);
			return dao;
		}
		
		private static Translation OneOrThrow(TranslationCollection c)
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
