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

namespace Bam.Net.Translation
{
	// schema = Translation 
    public static class TranslationContext
    {
		public static string ConnectionName
		{
			get
			{
				return "Translation";
			}
		}

		public static Database Db
		{
			get
			{
				return Bam.Net.Data.Db.For(ConnectionName);
			}
		}


	public class LanguageQueryContext
	{
			public LanguageCollection Where(WhereDelegate<LanguageColumns> where, Database db = null)
			{
				return Bam.Net.Translation.Language.Where(where, db);
			}
		   
			public LanguageCollection Where(WhereDelegate<LanguageColumns> where, OrderBy<LanguageColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Translation.Language.Where(where, orderBy, db);
			}

			public Language OneWhere(WhereDelegate<LanguageColumns> where, Database db = null)
			{
				return Bam.Net.Translation.Language.OneWhere(where, db);
			}

			public static Language GetOneWhere(WhereDelegate<LanguageColumns> where, Database db = null)
			{
				return Bam.Net.Translation.Language.GetOneWhere(where, db);
			}
		
			public Language FirstOneWhere(WhereDelegate<LanguageColumns> where, Database db = null)
			{
				return Bam.Net.Translation.Language.FirstOneWhere(where, db);
			}

			public LanguageCollection Top(int count, WhereDelegate<LanguageColumns> where, Database db = null)
			{
				return Bam.Net.Translation.Language.Top(count, where, db);
			}

			public LanguageCollection Top(int count, WhereDelegate<LanguageColumns> where, OrderBy<LanguageColumns> orderBy, Database db = null)
			{
				return Bam.Net.Translation.Language.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<LanguageColumns> where, Database db = null)
			{
				return Bam.Net.Translation.Language.Count(where, db);
			}
	}

	static LanguageQueryContext _languages;
	static object _languagesLock = new object();
	public static LanguageQueryContext Languages
	{
		get
		{
			return _languagesLock.DoubleCheckLock<LanguageQueryContext>(ref _languages, () => new LanguageQueryContext());
		}
	}
	public class TextQueryContext
	{
			public TextCollection Where(WhereDelegate<TextColumns> where, Database db = null)
			{
				return Bam.Net.Translation.Text.Where(where, db);
			}
		   
			public TextCollection Where(WhereDelegate<TextColumns> where, OrderBy<TextColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Translation.Text.Where(where, orderBy, db);
			}

			public Text OneWhere(WhereDelegate<TextColumns> where, Database db = null)
			{
				return Bam.Net.Translation.Text.OneWhere(where, db);
			}

			public static Text GetOneWhere(WhereDelegate<TextColumns> where, Database db = null)
			{
				return Bam.Net.Translation.Text.GetOneWhere(where, db);
			}
		
			public Text FirstOneWhere(WhereDelegate<TextColumns> where, Database db = null)
			{
				return Bam.Net.Translation.Text.FirstOneWhere(where, db);
			}

			public TextCollection Top(int count, WhereDelegate<TextColumns> where, Database db = null)
			{
				return Bam.Net.Translation.Text.Top(count, where, db);
			}

			public TextCollection Top(int count, WhereDelegate<TextColumns> where, OrderBy<TextColumns> orderBy, Database db = null)
			{
				return Bam.Net.Translation.Text.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<TextColumns> where, Database db = null)
			{
				return Bam.Net.Translation.Text.Count(where, db);
			}
	}

	static TextQueryContext _texts;
	static object _textsLock = new object();
	public static TextQueryContext Texts
	{
		get
		{
			return _textsLock.DoubleCheckLock<TextQueryContext>(ref _texts, () => new TextQueryContext());
		}
	}
	public class LanguageDetectionQueryContext
	{
			public LanguageDetectionCollection Where(WhereDelegate<LanguageDetectionColumns> where, Database db = null)
			{
				return Bam.Net.Translation.LanguageDetection.Where(where, db);
			}
		   
			public LanguageDetectionCollection Where(WhereDelegate<LanguageDetectionColumns> where, OrderBy<LanguageDetectionColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Translation.LanguageDetection.Where(where, orderBy, db);
			}

			public LanguageDetection OneWhere(WhereDelegate<LanguageDetectionColumns> where, Database db = null)
			{
				return Bam.Net.Translation.LanguageDetection.OneWhere(where, db);
			}

			public static LanguageDetection GetOneWhere(WhereDelegate<LanguageDetectionColumns> where, Database db = null)
			{
				return Bam.Net.Translation.LanguageDetection.GetOneWhere(where, db);
			}
		
			public LanguageDetection FirstOneWhere(WhereDelegate<LanguageDetectionColumns> where, Database db = null)
			{
				return Bam.Net.Translation.LanguageDetection.FirstOneWhere(where, db);
			}

			public LanguageDetectionCollection Top(int count, WhereDelegate<LanguageDetectionColumns> where, Database db = null)
			{
				return Bam.Net.Translation.LanguageDetection.Top(count, where, db);
			}

			public LanguageDetectionCollection Top(int count, WhereDelegate<LanguageDetectionColumns> where, OrderBy<LanguageDetectionColumns> orderBy, Database db = null)
			{
				return Bam.Net.Translation.LanguageDetection.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<LanguageDetectionColumns> where, Database db = null)
			{
				return Bam.Net.Translation.LanguageDetection.Count(where, db);
			}
	}

	static LanguageDetectionQueryContext _languageDetections;
	static object _languageDetectionsLock = new object();
	public static LanguageDetectionQueryContext LanguageDetections
	{
		get
		{
			return _languageDetectionsLock.DoubleCheckLock<LanguageDetectionQueryContext>(ref _languageDetections, () => new LanguageDetectionQueryContext());
		}
	}
	public class TranslationQueryContext
	{
			public TranslationCollection Where(WhereDelegate<TranslationColumns> where, Database db = null)
			{
				return Bam.Net.Translation.Translation.Where(where, db);
			}
		   
			public TranslationCollection Where(WhereDelegate<TranslationColumns> where, OrderBy<TranslationColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Translation.Translation.Where(where, orderBy, db);
			}

			public Translation OneWhere(WhereDelegate<TranslationColumns> where, Database db = null)
			{
				return Bam.Net.Translation.Translation.OneWhere(where, db);
			}

			public static Translation GetOneWhere(WhereDelegate<TranslationColumns> where, Database db = null)
			{
				return Bam.Net.Translation.Translation.GetOneWhere(where, db);
			}
		
			public Translation FirstOneWhere(WhereDelegate<TranslationColumns> where, Database db = null)
			{
				return Bam.Net.Translation.Translation.FirstOneWhere(where, db);
			}

			public TranslationCollection Top(int count, WhereDelegate<TranslationColumns> where, Database db = null)
			{
				return Bam.Net.Translation.Translation.Top(count, where, db);
			}

			public TranslationCollection Top(int count, WhereDelegate<TranslationColumns> where, OrderBy<TranslationColumns> orderBy, Database db = null)
			{
				return Bam.Net.Translation.Translation.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<TranslationColumns> where, Database db = null)
			{
				return Bam.Net.Translation.Translation.Count(where, db);
			}
	}

	static TranslationQueryContext _translations;
	static object _translationsLock = new object();
	public static TranslationQueryContext Translations
	{
		get
		{
			return _translationsLock.DoubleCheckLock<TranslationQueryContext>(ref _translations, () => new TranslationQueryContext());
		}
	}
	public class OtherNameQueryContext
	{
			public OtherNameCollection Where(WhereDelegate<OtherNameColumns> where, Database db = null)
			{
				return Bam.Net.Translation.OtherName.Where(where, db);
			}
		   
			public OtherNameCollection Where(WhereDelegate<OtherNameColumns> where, OrderBy<OtherNameColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Translation.OtherName.Where(where, orderBy, db);
			}

			public OtherName OneWhere(WhereDelegate<OtherNameColumns> where, Database db = null)
			{
				return Bam.Net.Translation.OtherName.OneWhere(where, db);
			}

			public static OtherName GetOneWhere(WhereDelegate<OtherNameColumns> where, Database db = null)
			{
				return Bam.Net.Translation.OtherName.GetOneWhere(where, db);
			}
		
			public OtherName FirstOneWhere(WhereDelegate<OtherNameColumns> where, Database db = null)
			{
				return Bam.Net.Translation.OtherName.FirstOneWhere(where, db);
			}

			public OtherNameCollection Top(int count, WhereDelegate<OtherNameColumns> where, Database db = null)
			{
				return Bam.Net.Translation.OtherName.Top(count, where, db);
			}

			public OtherNameCollection Top(int count, WhereDelegate<OtherNameColumns> where, OrderBy<OtherNameColumns> orderBy, Database db = null)
			{
				return Bam.Net.Translation.OtherName.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<OtherNameColumns> where, Database db = null)
			{
				return Bam.Net.Translation.OtherName.Count(where, db);
			}
	}

	static OtherNameQueryContext _otherNames;
	static object _otherNamesLock = new object();
	public static OtherNameQueryContext OtherNames
	{
		get
		{
			return _otherNamesLock.DoubleCheckLock<OtherNameQueryContext>(ref _otherNames, () => new OtherNameQueryContext());
		}
	}    }
}																								
