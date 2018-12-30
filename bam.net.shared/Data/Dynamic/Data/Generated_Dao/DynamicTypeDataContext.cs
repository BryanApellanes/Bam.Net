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

namespace Bam.Net.Data.Dynamic.Data.Dao
{
	// schema = DynamicTypeData 
    public static class DynamicTypeDataContext
    {
		public static string ConnectionName
		{
			get
			{
				return "DynamicTypeData";
			}
		}

		public static Database Db
		{
			get
			{
				return Bam.Net.Data.Db.For(ConnectionName);
			}
		}


	public class DataInstanceQueryContext
	{
			public DataInstanceCollection Where(WhereDelegate<DataInstanceColumns> where, Database db = null)
			{
				return Bam.Net.Data.Dynamic.Data.Dao.DataInstance.Where(where, db);
			}
		   
			public DataInstanceCollection Where(WhereDelegate<DataInstanceColumns> where, OrderBy<DataInstanceColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Data.Dynamic.Data.Dao.DataInstance.Where(where, orderBy, db);
			}

			public DataInstance OneWhere(WhereDelegate<DataInstanceColumns> where, Database db = null)
			{
				return Bam.Net.Data.Dynamic.Data.Dao.DataInstance.OneWhere(where, db);
			}

			public static DataInstance GetOneWhere(WhereDelegate<DataInstanceColumns> where, Database db = null)
			{
				return Bam.Net.Data.Dynamic.Data.Dao.DataInstance.GetOneWhere(where, db);
			}
		
			public DataInstance FirstOneWhere(WhereDelegate<DataInstanceColumns> where, Database db = null)
			{
				return Bam.Net.Data.Dynamic.Data.Dao.DataInstance.FirstOneWhere(where, db);
			}

			public DataInstanceCollection Top(int count, WhereDelegate<DataInstanceColumns> where, Database db = null)
			{
				return Bam.Net.Data.Dynamic.Data.Dao.DataInstance.Top(count, where, db);
			}

			public DataInstanceCollection Top(int count, WhereDelegate<DataInstanceColumns> where, OrderBy<DataInstanceColumns> orderBy, Database db = null)
			{
				return Bam.Net.Data.Dynamic.Data.Dao.DataInstance.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<DataInstanceColumns> where, Database db = null)
			{
				return Bam.Net.Data.Dynamic.Data.Dao.DataInstance.Count(where, db);
			}
	}

	static DataInstanceQueryContext _dataInstances;
	static object _dataInstancesLock = new object();
	public static DataInstanceQueryContext DataInstances
	{
		get
		{
			return _dataInstancesLock.DoubleCheckLock<DataInstanceQueryContext>(ref _dataInstances, () => new DataInstanceQueryContext());
		}
	}
	public class DataInstancePropertyValueQueryContext
	{
			public DataInstancePropertyValueCollection Where(WhereDelegate<DataInstancePropertyValueColumns> where, Database db = null)
			{
				return Bam.Net.Data.Dynamic.Data.Dao.DataInstancePropertyValue.Where(where, db);
			}
		   
			public DataInstancePropertyValueCollection Where(WhereDelegate<DataInstancePropertyValueColumns> where, OrderBy<DataInstancePropertyValueColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Data.Dynamic.Data.Dao.DataInstancePropertyValue.Where(where, orderBy, db);
			}

			public DataInstancePropertyValue OneWhere(WhereDelegate<DataInstancePropertyValueColumns> where, Database db = null)
			{
				return Bam.Net.Data.Dynamic.Data.Dao.DataInstancePropertyValue.OneWhere(where, db);
			}

			public static DataInstancePropertyValue GetOneWhere(WhereDelegate<DataInstancePropertyValueColumns> where, Database db = null)
			{
				return Bam.Net.Data.Dynamic.Data.Dao.DataInstancePropertyValue.GetOneWhere(where, db);
			}
		
			public DataInstancePropertyValue FirstOneWhere(WhereDelegate<DataInstancePropertyValueColumns> where, Database db = null)
			{
				return Bam.Net.Data.Dynamic.Data.Dao.DataInstancePropertyValue.FirstOneWhere(where, db);
			}

			public DataInstancePropertyValueCollection Top(int count, WhereDelegate<DataInstancePropertyValueColumns> where, Database db = null)
			{
				return Bam.Net.Data.Dynamic.Data.Dao.DataInstancePropertyValue.Top(count, where, db);
			}

			public DataInstancePropertyValueCollection Top(int count, WhereDelegate<DataInstancePropertyValueColumns> where, OrderBy<DataInstancePropertyValueColumns> orderBy, Database db = null)
			{
				return Bam.Net.Data.Dynamic.Data.Dao.DataInstancePropertyValue.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<DataInstancePropertyValueColumns> where, Database db = null)
			{
				return Bam.Net.Data.Dynamic.Data.Dao.DataInstancePropertyValue.Count(where, db);
			}
	}

	static DataInstancePropertyValueQueryContext _dataInstancePropertyValues;
	static object _dataInstancePropertyValuesLock = new object();
	public static DataInstancePropertyValueQueryContext DataInstancePropertyValues
	{
		get
		{
			return _dataInstancePropertyValuesLock.DoubleCheckLock<DataInstancePropertyValueQueryContext>(ref _dataInstancePropertyValues, () => new DataInstancePropertyValueQueryContext());
		}
	}
	public class DynamicNamespaceDescriptorQueryContext
	{
			public DynamicNamespaceDescriptorCollection Where(WhereDelegate<DynamicNamespaceDescriptorColumns> where, Database db = null)
			{
				return Bam.Net.Data.Dynamic.Data.Dao.DynamicNamespaceDescriptor.Where(where, db);
			}
		   
			public DynamicNamespaceDescriptorCollection Where(WhereDelegate<DynamicNamespaceDescriptorColumns> where, OrderBy<DynamicNamespaceDescriptorColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Data.Dynamic.Data.Dao.DynamicNamespaceDescriptor.Where(where, orderBy, db);
			}

			public DynamicNamespaceDescriptor OneWhere(WhereDelegate<DynamicNamespaceDescriptorColumns> where, Database db = null)
			{
				return Bam.Net.Data.Dynamic.Data.Dao.DynamicNamespaceDescriptor.OneWhere(where, db);
			}

			public static DynamicNamespaceDescriptor GetOneWhere(WhereDelegate<DynamicNamespaceDescriptorColumns> where, Database db = null)
			{
				return Bam.Net.Data.Dynamic.Data.Dao.DynamicNamespaceDescriptor.GetOneWhere(where, db);
			}
		
			public DynamicNamespaceDescriptor FirstOneWhere(WhereDelegate<DynamicNamespaceDescriptorColumns> where, Database db = null)
			{
				return Bam.Net.Data.Dynamic.Data.Dao.DynamicNamespaceDescriptor.FirstOneWhere(where, db);
			}

			public DynamicNamespaceDescriptorCollection Top(int count, WhereDelegate<DynamicNamespaceDescriptorColumns> where, Database db = null)
			{
				return Bam.Net.Data.Dynamic.Data.Dao.DynamicNamespaceDescriptor.Top(count, where, db);
			}

			public DynamicNamespaceDescriptorCollection Top(int count, WhereDelegate<DynamicNamespaceDescriptorColumns> where, OrderBy<DynamicNamespaceDescriptorColumns> orderBy, Database db = null)
			{
				return Bam.Net.Data.Dynamic.Data.Dao.DynamicNamespaceDescriptor.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<DynamicNamespaceDescriptorColumns> where, Database db = null)
			{
				return Bam.Net.Data.Dynamic.Data.Dao.DynamicNamespaceDescriptor.Count(where, db);
			}
	}

	static DynamicNamespaceDescriptorQueryContext _dynamicNamespaceDescriptors;
	static object _dynamicNamespaceDescriptorsLock = new object();
	public static DynamicNamespaceDescriptorQueryContext DynamicNamespaceDescriptors
	{
		get
		{
			return _dynamicNamespaceDescriptorsLock.DoubleCheckLock<DynamicNamespaceDescriptorQueryContext>(ref _dynamicNamespaceDescriptors, () => new DynamicNamespaceDescriptorQueryContext());
		}
	}
	public class DynamicTypeDescriptorQueryContext
	{
			public DynamicTypeDescriptorCollection Where(WhereDelegate<DynamicTypeDescriptorColumns> where, Database db = null)
			{
				return Bam.Net.Data.Dynamic.Data.Dao.DynamicTypeDescriptor.Where(where, db);
			}
		   
			public DynamicTypeDescriptorCollection Where(WhereDelegate<DynamicTypeDescriptorColumns> where, OrderBy<DynamicTypeDescriptorColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Data.Dynamic.Data.Dao.DynamicTypeDescriptor.Where(where, orderBy, db);
			}

			public DynamicTypeDescriptor OneWhere(WhereDelegate<DynamicTypeDescriptorColumns> where, Database db = null)
			{
				return Bam.Net.Data.Dynamic.Data.Dao.DynamicTypeDescriptor.OneWhere(where, db);
			}

			public static DynamicTypeDescriptor GetOneWhere(WhereDelegate<DynamicTypeDescriptorColumns> where, Database db = null)
			{
				return Bam.Net.Data.Dynamic.Data.Dao.DynamicTypeDescriptor.GetOneWhere(where, db);
			}
		
			public DynamicTypeDescriptor FirstOneWhere(WhereDelegate<DynamicTypeDescriptorColumns> where, Database db = null)
			{
				return Bam.Net.Data.Dynamic.Data.Dao.DynamicTypeDescriptor.FirstOneWhere(where, db);
			}

			public DynamicTypeDescriptorCollection Top(int count, WhereDelegate<DynamicTypeDescriptorColumns> where, Database db = null)
			{
				return Bam.Net.Data.Dynamic.Data.Dao.DynamicTypeDescriptor.Top(count, where, db);
			}

			public DynamicTypeDescriptorCollection Top(int count, WhereDelegate<DynamicTypeDescriptorColumns> where, OrderBy<DynamicTypeDescriptorColumns> orderBy, Database db = null)
			{
				return Bam.Net.Data.Dynamic.Data.Dao.DynamicTypeDescriptor.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<DynamicTypeDescriptorColumns> where, Database db = null)
			{
				return Bam.Net.Data.Dynamic.Data.Dao.DynamicTypeDescriptor.Count(where, db);
			}
	}

	static DynamicTypeDescriptorQueryContext _dynamicTypeDescriptors;
	static object _dynamicTypeDescriptorsLock = new object();
	public static DynamicTypeDescriptorQueryContext DynamicTypeDescriptors
	{
		get
		{
			return _dynamicTypeDescriptorsLock.DoubleCheckLock<DynamicTypeDescriptorQueryContext>(ref _dynamicTypeDescriptors, () => new DynamicTypeDescriptorQueryContext());
		}
	}
	public class DynamicTypePropertyDescriptorQueryContext
	{
			public DynamicTypePropertyDescriptorCollection Where(WhereDelegate<DynamicTypePropertyDescriptorColumns> where, Database db = null)
			{
				return Bam.Net.Data.Dynamic.Data.Dao.DynamicTypePropertyDescriptor.Where(where, db);
			}
		   
			public DynamicTypePropertyDescriptorCollection Where(WhereDelegate<DynamicTypePropertyDescriptorColumns> where, OrderBy<DynamicTypePropertyDescriptorColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Data.Dynamic.Data.Dao.DynamicTypePropertyDescriptor.Where(where, orderBy, db);
			}

			public DynamicTypePropertyDescriptor OneWhere(WhereDelegate<DynamicTypePropertyDescriptorColumns> where, Database db = null)
			{
				return Bam.Net.Data.Dynamic.Data.Dao.DynamicTypePropertyDescriptor.OneWhere(where, db);
			}

			public static DynamicTypePropertyDescriptor GetOneWhere(WhereDelegate<DynamicTypePropertyDescriptorColumns> where, Database db = null)
			{
				return Bam.Net.Data.Dynamic.Data.Dao.DynamicTypePropertyDescriptor.GetOneWhere(where, db);
			}
		
			public DynamicTypePropertyDescriptor FirstOneWhere(WhereDelegate<DynamicTypePropertyDescriptorColumns> where, Database db = null)
			{
				return Bam.Net.Data.Dynamic.Data.Dao.DynamicTypePropertyDescriptor.FirstOneWhere(where, db);
			}

			public DynamicTypePropertyDescriptorCollection Top(int count, WhereDelegate<DynamicTypePropertyDescriptorColumns> where, Database db = null)
			{
				return Bam.Net.Data.Dynamic.Data.Dao.DynamicTypePropertyDescriptor.Top(count, where, db);
			}

			public DynamicTypePropertyDescriptorCollection Top(int count, WhereDelegate<DynamicTypePropertyDescriptorColumns> where, OrderBy<DynamicTypePropertyDescriptorColumns> orderBy, Database db = null)
			{
				return Bam.Net.Data.Dynamic.Data.Dao.DynamicTypePropertyDescriptor.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<DynamicTypePropertyDescriptorColumns> where, Database db = null)
			{
				return Bam.Net.Data.Dynamic.Data.Dao.DynamicTypePropertyDescriptor.Count(where, db);
			}
	}

	static DynamicTypePropertyDescriptorQueryContext _dynamicTypePropertyDescriptors;
	static object _dynamicTypePropertyDescriptorsLock = new object();
	public static DynamicTypePropertyDescriptorQueryContext DynamicTypePropertyDescriptors
	{
		get
		{
			return _dynamicTypePropertyDescriptorsLock.DoubleCheckLock<DynamicTypePropertyDescriptorQueryContext>(ref _dynamicTypePropertyDescriptors, () => new DynamicTypePropertyDescriptorQueryContext());
		}
	}
	public class RootDocumentQueryContext
	{
			public RootDocumentCollection Where(WhereDelegate<RootDocumentColumns> where, Database db = null)
			{
				return Bam.Net.Data.Dynamic.Data.Dao.RootDocument.Where(where, db);
			}
		   
			public RootDocumentCollection Where(WhereDelegate<RootDocumentColumns> where, OrderBy<RootDocumentColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Data.Dynamic.Data.Dao.RootDocument.Where(where, orderBy, db);
			}

			public RootDocument OneWhere(WhereDelegate<RootDocumentColumns> where, Database db = null)
			{
				return Bam.Net.Data.Dynamic.Data.Dao.RootDocument.OneWhere(where, db);
			}

			public static RootDocument GetOneWhere(WhereDelegate<RootDocumentColumns> where, Database db = null)
			{
				return Bam.Net.Data.Dynamic.Data.Dao.RootDocument.GetOneWhere(where, db);
			}
		
			public RootDocument FirstOneWhere(WhereDelegate<RootDocumentColumns> where, Database db = null)
			{
				return Bam.Net.Data.Dynamic.Data.Dao.RootDocument.FirstOneWhere(where, db);
			}

			public RootDocumentCollection Top(int count, WhereDelegate<RootDocumentColumns> where, Database db = null)
			{
				return Bam.Net.Data.Dynamic.Data.Dao.RootDocument.Top(count, where, db);
			}

			public RootDocumentCollection Top(int count, WhereDelegate<RootDocumentColumns> where, OrderBy<RootDocumentColumns> orderBy, Database db = null)
			{
				return Bam.Net.Data.Dynamic.Data.Dao.RootDocument.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<RootDocumentColumns> where, Database db = null)
			{
				return Bam.Net.Data.Dynamic.Data.Dao.RootDocument.Count(where, db);
			}
	}

	static RootDocumentQueryContext _rootDocuments;
	static object _rootDocumentsLock = new object();
	public static RootDocumentQueryContext RootDocuments
	{
		get
		{
			return _rootDocumentsLock.DoubleCheckLock<RootDocumentQueryContext>(ref _rootDocuments, () => new RootDocumentQueryContext());
		}
	}    }
}																								
