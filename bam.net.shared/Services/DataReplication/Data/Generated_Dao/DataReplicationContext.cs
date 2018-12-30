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

namespace Bam.Net.Services.DataReplication.Data.Dao
{
	// schema = DataReplication 
    public static class DataReplicationContext
    {
		public static string ConnectionName
		{
			get
			{
				return "DataReplication";
			}
		}

		public static Database Db
		{
			get
			{
				return Bam.Net.Data.Db.For(ConnectionName);
			}
		}


	public class CreateOperationQueryContext
	{
			public CreateOperationCollection Where(WhereDelegate<CreateOperationColumns> where, Database db = null)
			{
				return Bam.Net.Services.DataReplication.Data.Dao.CreateOperation.Where(where, db);
			}
		   
			public CreateOperationCollection Where(WhereDelegate<CreateOperationColumns> where, OrderBy<CreateOperationColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Services.DataReplication.Data.Dao.CreateOperation.Where(where, orderBy, db);
			}

			public CreateOperation OneWhere(WhereDelegate<CreateOperationColumns> where, Database db = null)
			{
				return Bam.Net.Services.DataReplication.Data.Dao.CreateOperation.OneWhere(where, db);
			}

			public static CreateOperation GetOneWhere(WhereDelegate<CreateOperationColumns> where, Database db = null)
			{
				return Bam.Net.Services.DataReplication.Data.Dao.CreateOperation.GetOneWhere(where, db);
			}
		
			public CreateOperation FirstOneWhere(WhereDelegate<CreateOperationColumns> where, Database db = null)
			{
				return Bam.Net.Services.DataReplication.Data.Dao.CreateOperation.FirstOneWhere(where, db);
			}

			public CreateOperationCollection Top(int count, WhereDelegate<CreateOperationColumns> where, Database db = null)
			{
				return Bam.Net.Services.DataReplication.Data.Dao.CreateOperation.Top(count, where, db);
			}

			public CreateOperationCollection Top(int count, WhereDelegate<CreateOperationColumns> where, OrderBy<CreateOperationColumns> orderBy, Database db = null)
			{
				return Bam.Net.Services.DataReplication.Data.Dao.CreateOperation.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<CreateOperationColumns> where, Database db = null)
			{
				return Bam.Net.Services.DataReplication.Data.Dao.CreateOperation.Count(where, db);
			}
	}

	static CreateOperationQueryContext _createOperations;
	static object _createOperationsLock = new object();
	public static CreateOperationQueryContext CreateOperations
	{
		get
		{
			return _createOperationsLock.DoubleCheckLock<CreateOperationQueryContext>(ref _createOperations, () => new CreateOperationQueryContext());
		}
	}
	public class DataPropertyQueryContext
	{
			public DataPropertyCollection Where(WhereDelegate<DataPropertyColumns> where, Database db = null)
			{
				return Bam.Net.Services.DataReplication.Data.Dao.DataProperty.Where(where, db);
			}
		   
			public DataPropertyCollection Where(WhereDelegate<DataPropertyColumns> where, OrderBy<DataPropertyColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Services.DataReplication.Data.Dao.DataProperty.Where(where, orderBy, db);
			}

			public DataProperty OneWhere(WhereDelegate<DataPropertyColumns> where, Database db = null)
			{
				return Bam.Net.Services.DataReplication.Data.Dao.DataProperty.OneWhere(where, db);
			}

			public static DataProperty GetOneWhere(WhereDelegate<DataPropertyColumns> where, Database db = null)
			{
				return Bam.Net.Services.DataReplication.Data.Dao.DataProperty.GetOneWhere(where, db);
			}
		
			public DataProperty FirstOneWhere(WhereDelegate<DataPropertyColumns> where, Database db = null)
			{
				return Bam.Net.Services.DataReplication.Data.Dao.DataProperty.FirstOneWhere(where, db);
			}

			public DataPropertyCollection Top(int count, WhereDelegate<DataPropertyColumns> where, Database db = null)
			{
				return Bam.Net.Services.DataReplication.Data.Dao.DataProperty.Top(count, where, db);
			}

			public DataPropertyCollection Top(int count, WhereDelegate<DataPropertyColumns> where, OrderBy<DataPropertyColumns> orderBy, Database db = null)
			{
				return Bam.Net.Services.DataReplication.Data.Dao.DataProperty.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<DataPropertyColumns> where, Database db = null)
			{
				return Bam.Net.Services.DataReplication.Data.Dao.DataProperty.Count(where, db);
			}
	}

	static DataPropertyQueryContext _dataProperties;
	static object _dataPropertiesLock = new object();
	public static DataPropertyQueryContext DataProperties
	{
		get
		{
			return _dataPropertiesLock.DoubleCheckLock<DataPropertyQueryContext>(ref _dataProperties, () => new DataPropertyQueryContext());
		}
	}
	public class DataPointQueryContext
	{
			public DataPointCollection Where(WhereDelegate<DataPointColumns> where, Database db = null)
			{
				return Bam.Net.Services.DataReplication.Data.Dao.DataPoint.Where(where, db);
			}
		   
			public DataPointCollection Where(WhereDelegate<DataPointColumns> where, OrderBy<DataPointColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Services.DataReplication.Data.Dao.DataPoint.Where(where, orderBy, db);
			}

			public DataPoint OneWhere(WhereDelegate<DataPointColumns> where, Database db = null)
			{
				return Bam.Net.Services.DataReplication.Data.Dao.DataPoint.OneWhere(where, db);
			}

			public static DataPoint GetOneWhere(WhereDelegate<DataPointColumns> where, Database db = null)
			{
				return Bam.Net.Services.DataReplication.Data.Dao.DataPoint.GetOneWhere(where, db);
			}
		
			public DataPoint FirstOneWhere(WhereDelegate<DataPointColumns> where, Database db = null)
			{
				return Bam.Net.Services.DataReplication.Data.Dao.DataPoint.FirstOneWhere(where, db);
			}

			public DataPointCollection Top(int count, WhereDelegate<DataPointColumns> where, Database db = null)
			{
				return Bam.Net.Services.DataReplication.Data.Dao.DataPoint.Top(count, where, db);
			}

			public DataPointCollection Top(int count, WhereDelegate<DataPointColumns> where, OrderBy<DataPointColumns> orderBy, Database db = null)
			{
				return Bam.Net.Services.DataReplication.Data.Dao.DataPoint.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<DataPointColumns> where, Database db = null)
			{
				return Bam.Net.Services.DataReplication.Data.Dao.DataPoint.Count(where, db);
			}
	}

	static DataPointQueryContext _dataPoints;
	static object _dataPointsLock = new object();
	public static DataPointQueryContext DataPoints
	{
		get
		{
			return _dataPointsLock.DoubleCheckLock<DataPointQueryContext>(ref _dataPoints, () => new DataPointQueryContext());
		}
	}
	public class DataRelationshipQueryContext
	{
			public DataRelationshipCollection Where(WhereDelegate<DataRelationshipColumns> where, Database db = null)
			{
				return Bam.Net.Services.DataReplication.Data.Dao.DataRelationship.Where(where, db);
			}
		   
			public DataRelationshipCollection Where(WhereDelegate<DataRelationshipColumns> where, OrderBy<DataRelationshipColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Services.DataReplication.Data.Dao.DataRelationship.Where(where, orderBy, db);
			}

			public DataRelationship OneWhere(WhereDelegate<DataRelationshipColumns> where, Database db = null)
			{
				return Bam.Net.Services.DataReplication.Data.Dao.DataRelationship.OneWhere(where, db);
			}

			public static DataRelationship GetOneWhere(WhereDelegate<DataRelationshipColumns> where, Database db = null)
			{
				return Bam.Net.Services.DataReplication.Data.Dao.DataRelationship.GetOneWhere(where, db);
			}
		
			public DataRelationship FirstOneWhere(WhereDelegate<DataRelationshipColumns> where, Database db = null)
			{
				return Bam.Net.Services.DataReplication.Data.Dao.DataRelationship.FirstOneWhere(where, db);
			}

			public DataRelationshipCollection Top(int count, WhereDelegate<DataRelationshipColumns> where, Database db = null)
			{
				return Bam.Net.Services.DataReplication.Data.Dao.DataRelationship.Top(count, where, db);
			}

			public DataRelationshipCollection Top(int count, WhereDelegate<DataRelationshipColumns> where, OrderBy<DataRelationshipColumns> orderBy, Database db = null)
			{
				return Bam.Net.Services.DataReplication.Data.Dao.DataRelationship.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<DataRelationshipColumns> where, Database db = null)
			{
				return Bam.Net.Services.DataReplication.Data.Dao.DataRelationship.Count(where, db);
			}
	}

	static DataRelationshipQueryContext _dataRelationships;
	static object _dataRelationshipsLock = new object();
	public static DataRelationshipQueryContext DataRelationships
	{
		get
		{
			return _dataRelationshipsLock.DoubleCheckLock<DataRelationshipQueryContext>(ref _dataRelationships, () => new DataRelationshipQueryContext());
		}
	}
	public class DeleteEventQueryContext
	{
			public DeleteEventCollection Where(WhereDelegate<DeleteEventColumns> where, Database db = null)
			{
				return Bam.Net.Services.DataReplication.Data.Dao.DeleteEvent.Where(where, db);
			}
		   
			public DeleteEventCollection Where(WhereDelegate<DeleteEventColumns> where, OrderBy<DeleteEventColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Services.DataReplication.Data.Dao.DeleteEvent.Where(where, orderBy, db);
			}

			public DeleteEvent OneWhere(WhereDelegate<DeleteEventColumns> where, Database db = null)
			{
				return Bam.Net.Services.DataReplication.Data.Dao.DeleteEvent.OneWhere(where, db);
			}

			public static DeleteEvent GetOneWhere(WhereDelegate<DeleteEventColumns> where, Database db = null)
			{
				return Bam.Net.Services.DataReplication.Data.Dao.DeleteEvent.GetOneWhere(where, db);
			}
		
			public DeleteEvent FirstOneWhere(WhereDelegate<DeleteEventColumns> where, Database db = null)
			{
				return Bam.Net.Services.DataReplication.Data.Dao.DeleteEvent.FirstOneWhere(where, db);
			}

			public DeleteEventCollection Top(int count, WhereDelegate<DeleteEventColumns> where, Database db = null)
			{
				return Bam.Net.Services.DataReplication.Data.Dao.DeleteEvent.Top(count, where, db);
			}

			public DeleteEventCollection Top(int count, WhereDelegate<DeleteEventColumns> where, OrderBy<DeleteEventColumns> orderBy, Database db = null)
			{
				return Bam.Net.Services.DataReplication.Data.Dao.DeleteEvent.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<DeleteEventColumns> where, Database db = null)
			{
				return Bam.Net.Services.DataReplication.Data.Dao.DeleteEvent.Count(where, db);
			}
	}

	static DeleteEventQueryContext _deleteEvents;
	static object _deleteEventsLock = new object();
	public static DeleteEventQueryContext DeleteEvents
	{
		get
		{
			return _deleteEventsLock.DoubleCheckLock<DeleteEventQueryContext>(ref _deleteEvents, () => new DeleteEventQueryContext());
		}
	}
	public class DeleteOperationQueryContext
	{
			public DeleteOperationCollection Where(WhereDelegate<DeleteOperationColumns> where, Database db = null)
			{
				return Bam.Net.Services.DataReplication.Data.Dao.DeleteOperation.Where(where, db);
			}
		   
			public DeleteOperationCollection Where(WhereDelegate<DeleteOperationColumns> where, OrderBy<DeleteOperationColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Services.DataReplication.Data.Dao.DeleteOperation.Where(where, orderBy, db);
			}

			public DeleteOperation OneWhere(WhereDelegate<DeleteOperationColumns> where, Database db = null)
			{
				return Bam.Net.Services.DataReplication.Data.Dao.DeleteOperation.OneWhere(where, db);
			}

			public static DeleteOperation GetOneWhere(WhereDelegate<DeleteOperationColumns> where, Database db = null)
			{
				return Bam.Net.Services.DataReplication.Data.Dao.DeleteOperation.GetOneWhere(where, db);
			}
		
			public DeleteOperation FirstOneWhere(WhereDelegate<DeleteOperationColumns> where, Database db = null)
			{
				return Bam.Net.Services.DataReplication.Data.Dao.DeleteOperation.FirstOneWhere(where, db);
			}

			public DeleteOperationCollection Top(int count, WhereDelegate<DeleteOperationColumns> where, Database db = null)
			{
				return Bam.Net.Services.DataReplication.Data.Dao.DeleteOperation.Top(count, where, db);
			}

			public DeleteOperationCollection Top(int count, WhereDelegate<DeleteOperationColumns> where, OrderBy<DeleteOperationColumns> orderBy, Database db = null)
			{
				return Bam.Net.Services.DataReplication.Data.Dao.DeleteOperation.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<DeleteOperationColumns> where, Database db = null)
			{
				return Bam.Net.Services.DataReplication.Data.Dao.DeleteOperation.Count(where, db);
			}
	}

	static DeleteOperationQueryContext _deleteOperations;
	static object _deleteOperationsLock = new object();
	public static DeleteOperationQueryContext DeleteOperations
	{
		get
		{
			return _deleteOperationsLock.DoubleCheckLock<DeleteOperationQueryContext>(ref _deleteOperations, () => new DeleteOperationQueryContext());
		}
	}
	public class QueryOperationQueryContext
	{
			public QueryOperationCollection Where(WhereDelegate<QueryOperationColumns> where, Database db = null)
			{
				return Bam.Net.Services.DataReplication.Data.Dao.QueryOperation.Where(where, db);
			}
		   
			public QueryOperationCollection Where(WhereDelegate<QueryOperationColumns> where, OrderBy<QueryOperationColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Services.DataReplication.Data.Dao.QueryOperation.Where(where, orderBy, db);
			}

			public QueryOperation OneWhere(WhereDelegate<QueryOperationColumns> where, Database db = null)
			{
				return Bam.Net.Services.DataReplication.Data.Dao.QueryOperation.OneWhere(where, db);
			}

			public static QueryOperation GetOneWhere(WhereDelegate<QueryOperationColumns> where, Database db = null)
			{
				return Bam.Net.Services.DataReplication.Data.Dao.QueryOperation.GetOneWhere(where, db);
			}
		
			public QueryOperation FirstOneWhere(WhereDelegate<QueryOperationColumns> where, Database db = null)
			{
				return Bam.Net.Services.DataReplication.Data.Dao.QueryOperation.FirstOneWhere(where, db);
			}

			public QueryOperationCollection Top(int count, WhereDelegate<QueryOperationColumns> where, Database db = null)
			{
				return Bam.Net.Services.DataReplication.Data.Dao.QueryOperation.Top(count, where, db);
			}

			public QueryOperationCollection Top(int count, WhereDelegate<QueryOperationColumns> where, OrderBy<QueryOperationColumns> orderBy, Database db = null)
			{
				return Bam.Net.Services.DataReplication.Data.Dao.QueryOperation.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<QueryOperationColumns> where, Database db = null)
			{
				return Bam.Net.Services.DataReplication.Data.Dao.QueryOperation.Count(where, db);
			}
	}

	static QueryOperationQueryContext _queryOperations;
	static object _queryOperationsLock = new object();
	public static QueryOperationQueryContext QueryOperations
	{
		get
		{
			return _queryOperationsLock.DoubleCheckLock<QueryOperationQueryContext>(ref _queryOperations, () => new QueryOperationQueryContext());
		}
	}
	public class ReplicationOperationQueryContext
	{
			public ReplicationOperationCollection Where(WhereDelegate<ReplicationOperationColumns> where, Database db = null)
			{
				return Bam.Net.Services.DataReplication.Data.Dao.ReplicationOperation.Where(where, db);
			}
		   
			public ReplicationOperationCollection Where(WhereDelegate<ReplicationOperationColumns> where, OrderBy<ReplicationOperationColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Services.DataReplication.Data.Dao.ReplicationOperation.Where(where, orderBy, db);
			}

			public ReplicationOperation OneWhere(WhereDelegate<ReplicationOperationColumns> where, Database db = null)
			{
				return Bam.Net.Services.DataReplication.Data.Dao.ReplicationOperation.OneWhere(where, db);
			}

			public static ReplicationOperation GetOneWhere(WhereDelegate<ReplicationOperationColumns> where, Database db = null)
			{
				return Bam.Net.Services.DataReplication.Data.Dao.ReplicationOperation.GetOneWhere(where, db);
			}
		
			public ReplicationOperation FirstOneWhere(WhereDelegate<ReplicationOperationColumns> where, Database db = null)
			{
				return Bam.Net.Services.DataReplication.Data.Dao.ReplicationOperation.FirstOneWhere(where, db);
			}

			public ReplicationOperationCollection Top(int count, WhereDelegate<ReplicationOperationColumns> where, Database db = null)
			{
				return Bam.Net.Services.DataReplication.Data.Dao.ReplicationOperation.Top(count, where, db);
			}

			public ReplicationOperationCollection Top(int count, WhereDelegate<ReplicationOperationColumns> where, OrderBy<ReplicationOperationColumns> orderBy, Database db = null)
			{
				return Bam.Net.Services.DataReplication.Data.Dao.ReplicationOperation.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<ReplicationOperationColumns> where, Database db = null)
			{
				return Bam.Net.Services.DataReplication.Data.Dao.ReplicationOperation.Count(where, db);
			}
	}

	static ReplicationOperationQueryContext _replicationOperations;
	static object _replicationOperationsLock = new object();
	public static ReplicationOperationQueryContext ReplicationOperations
	{
		get
		{
			return _replicationOperationsLock.DoubleCheckLock<ReplicationOperationQueryContext>(ref _replicationOperations, () => new ReplicationOperationQueryContext());
		}
	}
	public class RetrieveOperationQueryContext
	{
			public RetrieveOperationCollection Where(WhereDelegate<RetrieveOperationColumns> where, Database db = null)
			{
				return Bam.Net.Services.DataReplication.Data.Dao.RetrieveOperation.Where(where, db);
			}
		   
			public RetrieveOperationCollection Where(WhereDelegate<RetrieveOperationColumns> where, OrderBy<RetrieveOperationColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Services.DataReplication.Data.Dao.RetrieveOperation.Where(where, orderBy, db);
			}

			public RetrieveOperation OneWhere(WhereDelegate<RetrieveOperationColumns> where, Database db = null)
			{
				return Bam.Net.Services.DataReplication.Data.Dao.RetrieveOperation.OneWhere(where, db);
			}

			public static RetrieveOperation GetOneWhere(WhereDelegate<RetrieveOperationColumns> where, Database db = null)
			{
				return Bam.Net.Services.DataReplication.Data.Dao.RetrieveOperation.GetOneWhere(where, db);
			}
		
			public RetrieveOperation FirstOneWhere(WhereDelegate<RetrieveOperationColumns> where, Database db = null)
			{
				return Bam.Net.Services.DataReplication.Data.Dao.RetrieveOperation.FirstOneWhere(where, db);
			}

			public RetrieveOperationCollection Top(int count, WhereDelegate<RetrieveOperationColumns> where, Database db = null)
			{
				return Bam.Net.Services.DataReplication.Data.Dao.RetrieveOperation.Top(count, where, db);
			}

			public RetrieveOperationCollection Top(int count, WhereDelegate<RetrieveOperationColumns> where, OrderBy<RetrieveOperationColumns> orderBy, Database db = null)
			{
				return Bam.Net.Services.DataReplication.Data.Dao.RetrieveOperation.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<RetrieveOperationColumns> where, Database db = null)
			{
				return Bam.Net.Services.DataReplication.Data.Dao.RetrieveOperation.Count(where, db);
			}
	}

	static RetrieveOperationQueryContext _retrieveOperations;
	static object _retrieveOperationsLock = new object();
	public static RetrieveOperationQueryContext RetrieveOperations
	{
		get
		{
			return _retrieveOperationsLock.DoubleCheckLock<RetrieveOperationQueryContext>(ref _retrieveOperations, () => new RetrieveOperationQueryContext());
		}
	}
	public class SaveOperationQueryContext
	{
			public SaveOperationCollection Where(WhereDelegate<SaveOperationColumns> where, Database db = null)
			{
				return Bam.Net.Services.DataReplication.Data.Dao.SaveOperation.Where(where, db);
			}
		   
			public SaveOperationCollection Where(WhereDelegate<SaveOperationColumns> where, OrderBy<SaveOperationColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Services.DataReplication.Data.Dao.SaveOperation.Where(where, orderBy, db);
			}

			public SaveOperation OneWhere(WhereDelegate<SaveOperationColumns> where, Database db = null)
			{
				return Bam.Net.Services.DataReplication.Data.Dao.SaveOperation.OneWhere(where, db);
			}

			public static SaveOperation GetOneWhere(WhereDelegate<SaveOperationColumns> where, Database db = null)
			{
				return Bam.Net.Services.DataReplication.Data.Dao.SaveOperation.GetOneWhere(where, db);
			}
		
			public SaveOperation FirstOneWhere(WhereDelegate<SaveOperationColumns> where, Database db = null)
			{
				return Bam.Net.Services.DataReplication.Data.Dao.SaveOperation.FirstOneWhere(where, db);
			}

			public SaveOperationCollection Top(int count, WhereDelegate<SaveOperationColumns> where, Database db = null)
			{
				return Bam.Net.Services.DataReplication.Data.Dao.SaveOperation.Top(count, where, db);
			}

			public SaveOperationCollection Top(int count, WhereDelegate<SaveOperationColumns> where, OrderBy<SaveOperationColumns> orderBy, Database db = null)
			{
				return Bam.Net.Services.DataReplication.Data.Dao.SaveOperation.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<SaveOperationColumns> where, Database db = null)
			{
				return Bam.Net.Services.DataReplication.Data.Dao.SaveOperation.Count(where, db);
			}
	}

	static SaveOperationQueryContext _saveOperations;
	static object _saveOperationsLock = new object();
	public static SaveOperationQueryContext SaveOperations
	{
		get
		{
			return _saveOperationsLock.DoubleCheckLock<SaveOperationQueryContext>(ref _saveOperations, () => new SaveOperationQueryContext());
		}
	}
	public class UpdateOperationQueryContext
	{
			public UpdateOperationCollection Where(WhereDelegate<UpdateOperationColumns> where, Database db = null)
			{
				return Bam.Net.Services.DataReplication.Data.Dao.UpdateOperation.Where(where, db);
			}
		   
			public UpdateOperationCollection Where(WhereDelegate<UpdateOperationColumns> where, OrderBy<UpdateOperationColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Services.DataReplication.Data.Dao.UpdateOperation.Where(where, orderBy, db);
			}

			public UpdateOperation OneWhere(WhereDelegate<UpdateOperationColumns> where, Database db = null)
			{
				return Bam.Net.Services.DataReplication.Data.Dao.UpdateOperation.OneWhere(where, db);
			}

			public static UpdateOperation GetOneWhere(WhereDelegate<UpdateOperationColumns> where, Database db = null)
			{
				return Bam.Net.Services.DataReplication.Data.Dao.UpdateOperation.GetOneWhere(where, db);
			}
		
			public UpdateOperation FirstOneWhere(WhereDelegate<UpdateOperationColumns> where, Database db = null)
			{
				return Bam.Net.Services.DataReplication.Data.Dao.UpdateOperation.FirstOneWhere(where, db);
			}

			public UpdateOperationCollection Top(int count, WhereDelegate<UpdateOperationColumns> where, Database db = null)
			{
				return Bam.Net.Services.DataReplication.Data.Dao.UpdateOperation.Top(count, where, db);
			}

			public UpdateOperationCollection Top(int count, WhereDelegate<UpdateOperationColumns> where, OrderBy<UpdateOperationColumns> orderBy, Database db = null)
			{
				return Bam.Net.Services.DataReplication.Data.Dao.UpdateOperation.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<UpdateOperationColumns> where, Database db = null)
			{
				return Bam.Net.Services.DataReplication.Data.Dao.UpdateOperation.Count(where, db);
			}
	}

	static UpdateOperationQueryContext _updateOperations;
	static object _updateOperationsLock = new object();
	public static UpdateOperationQueryContext UpdateOperations
	{
		get
		{
			return _updateOperationsLock.DoubleCheckLock<UpdateOperationQueryContext>(ref _updateOperations, () => new UpdateOperationQueryContext());
		}
	}
	public class WriteEventQueryContext
	{
			public WriteEventCollection Where(WhereDelegate<WriteEventColumns> where, Database db = null)
			{
				return Bam.Net.Services.DataReplication.Data.Dao.WriteEvent.Where(where, db);
			}
		   
			public WriteEventCollection Where(WhereDelegate<WriteEventColumns> where, OrderBy<WriteEventColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Services.DataReplication.Data.Dao.WriteEvent.Where(where, orderBy, db);
			}

			public WriteEvent OneWhere(WhereDelegate<WriteEventColumns> where, Database db = null)
			{
				return Bam.Net.Services.DataReplication.Data.Dao.WriteEvent.OneWhere(where, db);
			}

			public static WriteEvent GetOneWhere(WhereDelegate<WriteEventColumns> where, Database db = null)
			{
				return Bam.Net.Services.DataReplication.Data.Dao.WriteEvent.GetOneWhere(where, db);
			}
		
			public WriteEvent FirstOneWhere(WhereDelegate<WriteEventColumns> where, Database db = null)
			{
				return Bam.Net.Services.DataReplication.Data.Dao.WriteEvent.FirstOneWhere(where, db);
			}

			public WriteEventCollection Top(int count, WhereDelegate<WriteEventColumns> where, Database db = null)
			{
				return Bam.Net.Services.DataReplication.Data.Dao.WriteEvent.Top(count, where, db);
			}

			public WriteEventCollection Top(int count, WhereDelegate<WriteEventColumns> where, OrderBy<WriteEventColumns> orderBy, Database db = null)
			{
				return Bam.Net.Services.DataReplication.Data.Dao.WriteEvent.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<WriteEventColumns> where, Database db = null)
			{
				return Bam.Net.Services.DataReplication.Data.Dao.WriteEvent.Count(where, db);
			}
	}

	static WriteEventQueryContext _writeEvents;
	static object _writeEventsLock = new object();
	public static WriteEventQueryContext WriteEvents
	{
		get
		{
			return _writeEventsLock.DoubleCheckLock<WriteEventQueryContext>(ref _writeEvents, () => new WriteEventQueryContext());
		}
	}    }
}																								
