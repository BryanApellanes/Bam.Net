/*
	This file was generated and should not be modified directly
*/
// model is SchemaDefinition
using System;
using System.Data;
using System.Data.Common;
using Bam.Net;
using Bam.Net.Data;
using Bam.Net.Data.Qi;

namespace Bam.Net.Analytics.EnglishDictionary
{
	// schema = EnglishDictionary 
    public static class EnglishDictionaryContext
    {
		public static string ConnectionName
		{
			get
			{
				return "EnglishDictionary";
			}
		}

		public static Database Db
		{
			get
			{
				return Bam.Net.Data.Db.For(ConnectionName);
			}
		}


	public class WordQueryContext
	{
			public WordCollection Where(WhereDelegate<WordColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.EnglishDictionary.Word.Where(where, db);
			}
		   
			public WordCollection Where(WhereDelegate<WordColumns> where, OrderBy<WordColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Analytics.EnglishDictionary.Word.Where(where, orderBy, db);
			}

			public Word OneWhere(WhereDelegate<WordColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.EnglishDictionary.Word.OneWhere(where, db);
			}

			public static Word GetOneWhere(WhereDelegate<WordColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.EnglishDictionary.Word.GetOneWhere(where, db);
			}
		
			public Word FirstOneWhere(WhereDelegate<WordColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.EnglishDictionary.Word.FirstOneWhere(where, db);
			}

			public WordCollection Top(int count, WhereDelegate<WordColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.EnglishDictionary.Word.Top(count, where, db);
			}

			public WordCollection Top(int count, WhereDelegate<WordColumns> where, OrderBy<WordColumns> orderBy, Database db = null)
			{
				return Bam.Net.Analytics.EnglishDictionary.Word.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<WordColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.EnglishDictionary.Word.Count(where, db);
			}
	}

	static WordQueryContext _words;
	static object _wordsLock = new object();
	public static WordQueryContext Words
	{
		get
		{
			return _wordsLock.DoubleCheckLock<WordQueryContext>(ref _words, () => new WordQueryContext());
		}
	}
	public class DefinitionQueryContext
	{
			public DefinitionCollection Where(WhereDelegate<DefinitionColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.EnglishDictionary.Definition.Where(where, db);
			}
		   
			public DefinitionCollection Where(WhereDelegate<DefinitionColumns> where, OrderBy<DefinitionColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Analytics.EnglishDictionary.Definition.Where(where, orderBy, db);
			}

			public Definition OneWhere(WhereDelegate<DefinitionColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.EnglishDictionary.Definition.OneWhere(where, db);
			}

			public static Definition GetOneWhere(WhereDelegate<DefinitionColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.EnglishDictionary.Definition.GetOneWhere(where, db);
			}
		
			public Definition FirstOneWhere(WhereDelegate<DefinitionColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.EnglishDictionary.Definition.FirstOneWhere(where, db);
			}

			public DefinitionCollection Top(int count, WhereDelegate<DefinitionColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.EnglishDictionary.Definition.Top(count, where, db);
			}

			public DefinitionCollection Top(int count, WhereDelegate<DefinitionColumns> where, OrderBy<DefinitionColumns> orderBy, Database db = null)
			{
				return Bam.Net.Analytics.EnglishDictionary.Definition.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<DefinitionColumns> where, Database db = null)
			{
				return Bam.Net.Analytics.EnglishDictionary.Definition.Count(where, db);
			}
	}

	static DefinitionQueryContext _definitions;
	static object _definitionsLock = new object();
	public static DefinitionQueryContext Definitions
	{
		get
		{
			return _definitionsLock.DoubleCheckLock<DefinitionQueryContext>(ref _definitions, () => new DefinitionQueryContext());
		}
	}    }
}																								
