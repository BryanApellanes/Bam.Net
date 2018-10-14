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

namespace Bam.Net.Services.OpenApi
{
	// schema = OpenApi 
    public static class OpenApiContext
    {
		public static string ConnectionName
		{
			get
			{
				return "OpenApi";
			}
		}

		public static Database Db
		{
			get
			{
				return Bam.Net.Data.Db.For(ConnectionName);
			}
		}


	public class ObjectDescriptorQueryContext
	{
			public ObjectDescriptorCollection Where(WhereDelegate<ObjectDescriptorColumns> where, Database db = null)
			{
				return Bam.Net.Services.OpenApi.ObjectDescriptor.Where(where, db);
			}
		   
			public ObjectDescriptorCollection Where(WhereDelegate<ObjectDescriptorColumns> where, OrderBy<ObjectDescriptorColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Services.OpenApi.ObjectDescriptor.Where(where, orderBy, db);
			}

			public ObjectDescriptor OneWhere(WhereDelegate<ObjectDescriptorColumns> where, Database db = null)
			{
				return Bam.Net.Services.OpenApi.ObjectDescriptor.OneWhere(where, db);
			}

			public static ObjectDescriptor GetOneWhere(WhereDelegate<ObjectDescriptorColumns> where, Database db = null)
			{
				return Bam.Net.Services.OpenApi.ObjectDescriptor.GetOneWhere(where, db);
			}
		
			public ObjectDescriptor FirstOneWhere(WhereDelegate<ObjectDescriptorColumns> where, Database db = null)
			{
				return Bam.Net.Services.OpenApi.ObjectDescriptor.FirstOneWhere(where, db);
			}

			public ObjectDescriptorCollection Top(int count, WhereDelegate<ObjectDescriptorColumns> where, Database db = null)
			{
				return Bam.Net.Services.OpenApi.ObjectDescriptor.Top(count, where, db);
			}

			public ObjectDescriptorCollection Top(int count, WhereDelegate<ObjectDescriptorColumns> where, OrderBy<ObjectDescriptorColumns> orderBy, Database db = null)
			{
				return Bam.Net.Services.OpenApi.ObjectDescriptor.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<ObjectDescriptorColumns> where, Database db = null)
			{
				return Bam.Net.Services.OpenApi.ObjectDescriptor.Count(where, db);
			}
	}

	static ObjectDescriptorQueryContext _objectDescriptors;
	static object _objectDescriptorsLock = new object();
	public static ObjectDescriptorQueryContext ObjectDescriptors
	{
		get
		{
			return _objectDescriptorsLock.DoubleCheckLock<ObjectDescriptorQueryContext>(ref _objectDescriptors, () => new ObjectDescriptorQueryContext());
		}
	}
	public class FixedFieldQueryContext
	{
			public FixedFieldCollection Where(WhereDelegate<FixedFieldColumns> where, Database db = null)
			{
				return Bam.Net.Services.OpenApi.FixedField.Where(where, db);
			}
		   
			public FixedFieldCollection Where(WhereDelegate<FixedFieldColumns> where, OrderBy<FixedFieldColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Services.OpenApi.FixedField.Where(where, orderBy, db);
			}

			public FixedField OneWhere(WhereDelegate<FixedFieldColumns> where, Database db = null)
			{
				return Bam.Net.Services.OpenApi.FixedField.OneWhere(where, db);
			}

			public static FixedField GetOneWhere(WhereDelegate<FixedFieldColumns> where, Database db = null)
			{
				return Bam.Net.Services.OpenApi.FixedField.GetOneWhere(where, db);
			}
		
			public FixedField FirstOneWhere(WhereDelegate<FixedFieldColumns> where, Database db = null)
			{
				return Bam.Net.Services.OpenApi.FixedField.FirstOneWhere(where, db);
			}

			public FixedFieldCollection Top(int count, WhereDelegate<FixedFieldColumns> where, Database db = null)
			{
				return Bam.Net.Services.OpenApi.FixedField.Top(count, where, db);
			}

			public FixedFieldCollection Top(int count, WhereDelegate<FixedFieldColumns> where, OrderBy<FixedFieldColumns> orderBy, Database db = null)
			{
				return Bam.Net.Services.OpenApi.FixedField.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<FixedFieldColumns> where, Database db = null)
			{
				return Bam.Net.Services.OpenApi.FixedField.Count(where, db);
			}
	}

	static FixedFieldQueryContext _fixedFields;
	static object _fixedFieldsLock = new object();
	public static FixedFieldQueryContext FixedFields
	{
		get
		{
			return _fixedFieldsLock.DoubleCheckLock<FixedFieldQueryContext>(ref _fixedFields, () => new FixedFieldQueryContext());
		}
	}
	public class PatternedFieldQueryContext
	{
			public PatternedFieldCollection Where(WhereDelegate<PatternedFieldColumns> where, Database db = null)
			{
				return Bam.Net.Services.OpenApi.PatternedField.Where(where, db);
			}
		   
			public PatternedFieldCollection Where(WhereDelegate<PatternedFieldColumns> where, OrderBy<PatternedFieldColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Services.OpenApi.PatternedField.Where(where, orderBy, db);
			}

			public PatternedField OneWhere(WhereDelegate<PatternedFieldColumns> where, Database db = null)
			{
				return Bam.Net.Services.OpenApi.PatternedField.OneWhere(where, db);
			}

			public static PatternedField GetOneWhere(WhereDelegate<PatternedFieldColumns> where, Database db = null)
			{
				return Bam.Net.Services.OpenApi.PatternedField.GetOneWhere(where, db);
			}
		
			public PatternedField FirstOneWhere(WhereDelegate<PatternedFieldColumns> where, Database db = null)
			{
				return Bam.Net.Services.OpenApi.PatternedField.FirstOneWhere(where, db);
			}

			public PatternedFieldCollection Top(int count, WhereDelegate<PatternedFieldColumns> where, Database db = null)
			{
				return Bam.Net.Services.OpenApi.PatternedField.Top(count, where, db);
			}

			public PatternedFieldCollection Top(int count, WhereDelegate<PatternedFieldColumns> where, OrderBy<PatternedFieldColumns> orderBy, Database db = null)
			{
				return Bam.Net.Services.OpenApi.PatternedField.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<PatternedFieldColumns> where, Database db = null)
			{
				return Bam.Net.Services.OpenApi.PatternedField.Count(where, db);
			}
	}

	static PatternedFieldQueryContext _patternedFields;
	static object _patternedFieldsLock = new object();
	public static PatternedFieldQueryContext PatternedFields
	{
		get
		{
			return _patternedFieldsLock.DoubleCheckLock<PatternedFieldQueryContext>(ref _patternedFields, () => new PatternedFieldQueryContext());
		}
	}    }
}																								
