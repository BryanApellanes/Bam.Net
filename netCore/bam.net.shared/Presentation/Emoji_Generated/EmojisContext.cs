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

namespace Bam.Net.Presentation.Unicode
{
	// schema = Emojis 
    public static class EmojisContext
    {
		public static string ConnectionName
		{
			get
			{
				return "Emojis";
			}
		}

		public static Database Db
		{
			get
			{
				return Bam.Net.Data.Db.For(ConnectionName);
			}
		}


	public class CategoryQueryContext
	{
			public CategoryCollection Where(WhereDelegate<CategoryColumns> where, Database db = null)
			{
				return Bam.Net.Presentation.Unicode.Category.Where(where, db);
			}
		   
			public CategoryCollection Where(WhereDelegate<CategoryColumns> where, OrderBy<CategoryColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Presentation.Unicode.Category.Where(where, orderBy, db);
			}

			public Category OneWhere(WhereDelegate<CategoryColumns> where, Database db = null)
			{
				return Bam.Net.Presentation.Unicode.Category.OneWhere(where, db);
			}

			public static Category GetOneWhere(WhereDelegate<CategoryColumns> where, Database db = null)
			{
				return Bam.Net.Presentation.Unicode.Category.GetOneWhere(where, db);
			}
		
			public Category FirstOneWhere(WhereDelegate<CategoryColumns> where, Database db = null)
			{
				return Bam.Net.Presentation.Unicode.Category.FirstOneWhere(where, db);
			}

			public CategoryCollection Top(int count, WhereDelegate<CategoryColumns> where, Database db = null)
			{
				return Bam.Net.Presentation.Unicode.Category.Top(count, where, db);
			}

			public CategoryCollection Top(int count, WhereDelegate<CategoryColumns> where, OrderBy<CategoryColumns> orderBy, Database db = null)
			{
				return Bam.Net.Presentation.Unicode.Category.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<CategoryColumns> where, Database db = null)
			{
				return Bam.Net.Presentation.Unicode.Category.Count(where, db);
			}
	}

	static CategoryQueryContext _categories;
	static object _categoriesLock = new object();
	public static CategoryQueryContext Categories
	{
		get
		{
			return _categoriesLock.DoubleCheckLock<CategoryQueryContext>(ref _categories, () => new CategoryQueryContext());
		}
	}
	public class EmojiQueryContext
	{
			public EmojiCollection Where(WhereDelegate<EmojiColumns> where, Database db = null)
			{
				return Bam.Net.Presentation.Unicode.Emoji.Where(where, db);
			}
		   
			public EmojiCollection Where(WhereDelegate<EmojiColumns> where, OrderBy<EmojiColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Presentation.Unicode.Emoji.Where(where, orderBy, db);
			}

			public Emoji OneWhere(WhereDelegate<EmojiColumns> where, Database db = null)
			{
				return Bam.Net.Presentation.Unicode.Emoji.OneWhere(where, db);
			}

			public static Emoji GetOneWhere(WhereDelegate<EmojiColumns> where, Database db = null)
			{
				return Bam.Net.Presentation.Unicode.Emoji.GetOneWhere(where, db);
			}
		
			public Emoji FirstOneWhere(WhereDelegate<EmojiColumns> where, Database db = null)
			{
				return Bam.Net.Presentation.Unicode.Emoji.FirstOneWhere(where, db);
			}

			public EmojiCollection Top(int count, WhereDelegate<EmojiColumns> where, Database db = null)
			{
				return Bam.Net.Presentation.Unicode.Emoji.Top(count, where, db);
			}

			public EmojiCollection Top(int count, WhereDelegate<EmojiColumns> where, OrderBy<EmojiColumns> orderBy, Database db = null)
			{
				return Bam.Net.Presentation.Unicode.Emoji.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<EmojiColumns> where, Database db = null)
			{
				return Bam.Net.Presentation.Unicode.Emoji.Count(where, db);
			}
	}

	static EmojiQueryContext _emojis;
	static object _emojisLock = new object();
	public static EmojiQueryContext Emojis
	{
		get
		{
			return _emojisLock.DoubleCheckLock<EmojiQueryContext>(ref _emojis, () => new EmojiQueryContext());
		}
	}
	public class CodeQueryContext
	{
			public CodeCollection Where(WhereDelegate<CodeColumns> where, Database db = null)
			{
				return Bam.Net.Presentation.Unicode.Code.Where(where, db);
			}
		   
			public CodeCollection Where(WhereDelegate<CodeColumns> where, OrderBy<CodeColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Presentation.Unicode.Code.Where(where, orderBy, db);
			}

			public Code OneWhere(WhereDelegate<CodeColumns> where, Database db = null)
			{
				return Bam.Net.Presentation.Unicode.Code.OneWhere(where, db);
			}

			public static Code GetOneWhere(WhereDelegate<CodeColumns> where, Database db = null)
			{
				return Bam.Net.Presentation.Unicode.Code.GetOneWhere(where, db);
			}
		
			public Code FirstOneWhere(WhereDelegate<CodeColumns> where, Database db = null)
			{
				return Bam.Net.Presentation.Unicode.Code.FirstOneWhere(where, db);
			}

			public CodeCollection Top(int count, WhereDelegate<CodeColumns> where, Database db = null)
			{
				return Bam.Net.Presentation.Unicode.Code.Top(count, where, db);
			}

			public CodeCollection Top(int count, WhereDelegate<CodeColumns> where, OrderBy<CodeColumns> orderBy, Database db = null)
			{
				return Bam.Net.Presentation.Unicode.Code.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<CodeColumns> where, Database db = null)
			{
				return Bam.Net.Presentation.Unicode.Code.Count(where, db);
			}
	}

	static CodeQueryContext _codes;
	static object _codesLock = new object();
	public static CodeQueryContext Codes
	{
		get
		{
			return _codesLock.DoubleCheckLock<CodeQueryContext>(ref _codes, () => new CodeQueryContext());
		}
	}    }
}																								
