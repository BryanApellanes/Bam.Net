/*
This file was generated and should not be modified directly
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Bam.Net;
using Bam.Net.Data;
using Bam.Net.Data.Repositories;
using Bam.Net.Services.DataReplication.Data;

namespace Bam.Net.Services.DataReplication.Data.Dao.Repository
{
	[Serializable]
	public class DataReplicationRepository: DatabaseRepository
	{
		public DataReplicationRepository()
		{
			SchemaName = "DataReplication";
			BaseNamespace = "Bam.Net.Services.DataReplication.Data";			
﻿			
			AddType<Bam.Net.Services.DataReplication.Data.CreateOperation>();﻿			
			AddType<Bam.Net.Services.DataReplication.Data.DataPoint>();﻿			
			AddType<Bam.Net.Services.DataReplication.Data.DataProperty>();﻿			
			AddType<Bam.Net.Services.DataReplication.Data.DataRelationship>();﻿			
			AddType<Bam.Net.Services.DataReplication.Data.DeleteEvent>();﻿			
			AddType<Bam.Net.Services.DataReplication.Data.DeleteOperation>();﻿			
			AddType<Bam.Net.Services.DataReplication.Data.QueryOperation>();﻿			
			AddType<Bam.Net.Services.DataReplication.Data.ReplicationOperation>();﻿			
			AddType<Bam.Net.Services.DataReplication.Data.RetrieveOperation>();﻿			
			AddType<Bam.Net.Services.DataReplication.Data.SaveOperation>();﻿			
			AddType<Bam.Net.Services.DataReplication.Data.UpdateOperation>();﻿			
			AddType<Bam.Net.Services.DataReplication.Data.WriteEvent>();
			DaoAssembly = typeof(DataReplicationRepository).Assembly;
		}

		object _addLock = new object();
        public override void AddType(Type type)
        {
            lock (_addLock)
            {
                base.AddType(type);
                DaoAssembly = typeof(DataReplicationRepository).Assembly;
            }
        }

﻿		
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		/// <param name="where"></param>
		public Bam.Net.Services.DataReplication.Data.CreateOperation GetOneCreateOperationWhere(WhereDelegate<CreateOperationColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.Services.DataReplication.Data.CreateOperation>();
			return (Bam.Net.Services.DataReplication.Data.CreateOperation)Bam.Net.Services.DataReplication.Data.Dao.CreateOperation.GetOneWhere(where, Database).CopyAs(wrapperType, this);
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single CreateOperation instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a CreateOperationColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CreateOperationColumns and other values
		/// </param>
		public Bam.Net.Services.DataReplication.Data.CreateOperation OneCreateOperationWhere(WhereDelegate<CreateOperationColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.Services.DataReplication.Data.CreateOperation>();
            return (Bam.Net.Services.DataReplication.Data.CreateOperation)Bam.Net.Services.DataReplication.Data.Dao.CreateOperation.OneWhere(where, Database).CopyAs(wrapperType, this);
        }

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a Bam.Net.Services.DataReplication.Data.CreateOperationColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between Bam.Net.Services.DataReplication.Data.CreateOperationColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.Services.DataReplication.Data.CreateOperation> CreateOperationsWhere(WhereDelegate<CreateOperationColumns> where, OrderBy<CreateOperationColumns> orderBy = null)
        {
            return Wrap<Bam.Net.Services.DataReplication.Data.CreateOperation>(Bam.Net.Services.DataReplication.Data.Dao.CreateOperation.Where(where, orderBy, Database));
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
		/// <param name="where">A WhereDelegate that recieves a CreateOperationColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CreateOperationColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.Services.DataReplication.Data.CreateOperation> TopCreateOperationsWhere(int count, WhereDelegate<CreateOperationColumns> where)
        {
            return Wrap<Bam.Net.Services.DataReplication.Data.CreateOperation>(Bam.Net.Services.DataReplication.Data.Dao.CreateOperation.Top(count, where, Database));
        }

		/// <summary>
		/// Return the count of CreateOperations
		/// </summary>
		public long CountCreateOperations()
        {
            return Bam.Net.Services.DataReplication.Data.Dao.CreateOperation.Count(Database);
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a CreateOperationColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CreateOperationColumns and other values
		/// </param>
        public long CountCreateOperationsWhere(WhereDelegate<CreateOperationColumns> where)
        {
            return Bam.Net.Services.DataReplication.Data.Dao.CreateOperation.Count(where, Database);
        }
        
        public async Task BatchQueryCreateOperations(int batchSize, WhereDelegate<CreateOperationColumns> where, Action<IEnumerable<Bam.Net.Services.DataReplication.Data.CreateOperation>> batchProcessor)
        {
            await Bam.Net.Services.DataReplication.Data.Dao.CreateOperation.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.Services.DataReplication.Data.CreateOperation>(batch));
            }, Database);
        }
		
        public async Task BatchAllCreateOperations(int batchSize, Action<IEnumerable<Bam.Net.Services.DataReplication.Data.CreateOperation>> batchProcessor)
        {
            await Bam.Net.Services.DataReplication.Data.Dao.CreateOperation.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.Services.DataReplication.Data.CreateOperation>(batch));
            }, Database);
        }﻿		
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		/// <param name="where"></param>
		public Bam.Net.Services.DataReplication.Data.DataPoint GetOneDataPointWhere(WhereDelegate<DataPointColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.Services.DataReplication.Data.DataPoint>();
			return (Bam.Net.Services.DataReplication.Data.DataPoint)Bam.Net.Services.DataReplication.Data.Dao.DataPoint.GetOneWhere(where, Database).CopyAs(wrapperType, this);
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single DataPoint instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a DataPointColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DataPointColumns and other values
		/// </param>
		public Bam.Net.Services.DataReplication.Data.DataPoint OneDataPointWhere(WhereDelegate<DataPointColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.Services.DataReplication.Data.DataPoint>();
            return (Bam.Net.Services.DataReplication.Data.DataPoint)Bam.Net.Services.DataReplication.Data.Dao.DataPoint.OneWhere(where, Database).CopyAs(wrapperType, this);
        }

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a Bam.Net.Services.DataReplication.Data.DataPointColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between Bam.Net.Services.DataReplication.Data.DataPointColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.Services.DataReplication.Data.DataPoint> DataPointsWhere(WhereDelegate<DataPointColumns> where, OrderBy<DataPointColumns> orderBy = null)
        {
            return Wrap<Bam.Net.Services.DataReplication.Data.DataPoint>(Bam.Net.Services.DataReplication.Data.Dao.DataPoint.Where(where, orderBy, Database));
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
		/// <param name="where">A WhereDelegate that recieves a DataPointColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DataPointColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.Services.DataReplication.Data.DataPoint> TopDataPointsWhere(int count, WhereDelegate<DataPointColumns> where)
        {
            return Wrap<Bam.Net.Services.DataReplication.Data.DataPoint>(Bam.Net.Services.DataReplication.Data.Dao.DataPoint.Top(count, where, Database));
        }

		/// <summary>
		/// Return the count of DataPoints
		/// </summary>
		public long CountDataPoints()
        {
            return Bam.Net.Services.DataReplication.Data.Dao.DataPoint.Count(Database);
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a DataPointColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DataPointColumns and other values
		/// </param>
        public long CountDataPointsWhere(WhereDelegate<DataPointColumns> where)
        {
            return Bam.Net.Services.DataReplication.Data.Dao.DataPoint.Count(where, Database);
        }
        
        public async Task BatchQueryDataPoints(int batchSize, WhereDelegate<DataPointColumns> where, Action<IEnumerable<Bam.Net.Services.DataReplication.Data.DataPoint>> batchProcessor)
        {
            await Bam.Net.Services.DataReplication.Data.Dao.DataPoint.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.Services.DataReplication.Data.DataPoint>(batch));
            }, Database);
        }
		
        public async Task BatchAllDataPoints(int batchSize, Action<IEnumerable<Bam.Net.Services.DataReplication.Data.DataPoint>> batchProcessor)
        {
            await Bam.Net.Services.DataReplication.Data.Dao.DataPoint.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.Services.DataReplication.Data.DataPoint>(batch));
            }, Database);
        }﻿		
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		/// <param name="where"></param>
		public Bam.Net.Services.DataReplication.Data.DataProperty GetOneDataPropertyWhere(WhereDelegate<DataPropertyColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.Services.DataReplication.Data.DataProperty>();
			return (Bam.Net.Services.DataReplication.Data.DataProperty)Bam.Net.Services.DataReplication.Data.Dao.DataProperty.GetOneWhere(where, Database).CopyAs(wrapperType, this);
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single DataProperty instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a DataPropertyColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DataPropertyColumns and other values
		/// </param>
		public Bam.Net.Services.DataReplication.Data.DataProperty OneDataPropertyWhere(WhereDelegate<DataPropertyColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.Services.DataReplication.Data.DataProperty>();
            return (Bam.Net.Services.DataReplication.Data.DataProperty)Bam.Net.Services.DataReplication.Data.Dao.DataProperty.OneWhere(where, Database).CopyAs(wrapperType, this);
        }

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a Bam.Net.Services.DataReplication.Data.DataPropertyColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between Bam.Net.Services.DataReplication.Data.DataPropertyColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.Services.DataReplication.Data.DataProperty> DataPropertiesWhere(WhereDelegate<DataPropertyColumns> where, OrderBy<DataPropertyColumns> orderBy = null)
        {
            return Wrap<Bam.Net.Services.DataReplication.Data.DataProperty>(Bam.Net.Services.DataReplication.Data.Dao.DataProperty.Where(where, orderBy, Database));
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
		/// <param name="where">A WhereDelegate that recieves a DataPropertyColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DataPropertyColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.Services.DataReplication.Data.DataProperty> TopDataPropertiesWhere(int count, WhereDelegate<DataPropertyColumns> where)
        {
            return Wrap<Bam.Net.Services.DataReplication.Data.DataProperty>(Bam.Net.Services.DataReplication.Data.Dao.DataProperty.Top(count, where, Database));
        }

		/// <summary>
		/// Return the count of DataProperties
		/// </summary>
		public long CountDataProperties()
        {
            return Bam.Net.Services.DataReplication.Data.Dao.DataProperty.Count(Database);
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a DataPropertyColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DataPropertyColumns and other values
		/// </param>
        public long CountDataPropertiesWhere(WhereDelegate<DataPropertyColumns> where)
        {
            return Bam.Net.Services.DataReplication.Data.Dao.DataProperty.Count(where, Database);
        }
        
        public async Task BatchQueryDataProperties(int batchSize, WhereDelegate<DataPropertyColumns> where, Action<IEnumerable<Bam.Net.Services.DataReplication.Data.DataProperty>> batchProcessor)
        {
            await Bam.Net.Services.DataReplication.Data.Dao.DataProperty.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.Services.DataReplication.Data.DataProperty>(batch));
            }, Database);
        }
		
        public async Task BatchAllDataProperties(int batchSize, Action<IEnumerable<Bam.Net.Services.DataReplication.Data.DataProperty>> batchProcessor)
        {
            await Bam.Net.Services.DataReplication.Data.Dao.DataProperty.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.Services.DataReplication.Data.DataProperty>(batch));
            }, Database);
        }﻿		
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		/// <param name="where"></param>
		public Bam.Net.Services.DataReplication.Data.DataRelationship GetOneDataRelationshipWhere(WhereDelegate<DataRelationshipColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.Services.DataReplication.Data.DataRelationship>();
			return (Bam.Net.Services.DataReplication.Data.DataRelationship)Bam.Net.Services.DataReplication.Data.Dao.DataRelationship.GetOneWhere(where, Database).CopyAs(wrapperType, this);
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single DataRelationship instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a DataRelationshipColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DataRelationshipColumns and other values
		/// </param>
		public Bam.Net.Services.DataReplication.Data.DataRelationship OneDataRelationshipWhere(WhereDelegate<DataRelationshipColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.Services.DataReplication.Data.DataRelationship>();
            return (Bam.Net.Services.DataReplication.Data.DataRelationship)Bam.Net.Services.DataReplication.Data.Dao.DataRelationship.OneWhere(where, Database).CopyAs(wrapperType, this);
        }

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a Bam.Net.Services.DataReplication.Data.DataRelationshipColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between Bam.Net.Services.DataReplication.Data.DataRelationshipColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.Services.DataReplication.Data.DataRelationship> DataRelationshipsWhere(WhereDelegate<DataRelationshipColumns> where, OrderBy<DataRelationshipColumns> orderBy = null)
        {
            return Wrap<Bam.Net.Services.DataReplication.Data.DataRelationship>(Bam.Net.Services.DataReplication.Data.Dao.DataRelationship.Where(where, orderBy, Database));
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
		/// <param name="where">A WhereDelegate that recieves a DataRelationshipColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DataRelationshipColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.Services.DataReplication.Data.DataRelationship> TopDataRelationshipsWhere(int count, WhereDelegate<DataRelationshipColumns> where)
        {
            return Wrap<Bam.Net.Services.DataReplication.Data.DataRelationship>(Bam.Net.Services.DataReplication.Data.Dao.DataRelationship.Top(count, where, Database));
        }

		/// <summary>
		/// Return the count of DataRelationships
		/// </summary>
		public long CountDataRelationships()
        {
            return Bam.Net.Services.DataReplication.Data.Dao.DataRelationship.Count(Database);
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a DataRelationshipColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DataRelationshipColumns and other values
		/// </param>
        public long CountDataRelationshipsWhere(WhereDelegate<DataRelationshipColumns> where)
        {
            return Bam.Net.Services.DataReplication.Data.Dao.DataRelationship.Count(where, Database);
        }
        
        public async Task BatchQueryDataRelationships(int batchSize, WhereDelegate<DataRelationshipColumns> where, Action<IEnumerable<Bam.Net.Services.DataReplication.Data.DataRelationship>> batchProcessor)
        {
            await Bam.Net.Services.DataReplication.Data.Dao.DataRelationship.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.Services.DataReplication.Data.DataRelationship>(batch));
            }, Database);
        }
		
        public async Task BatchAllDataRelationships(int batchSize, Action<IEnumerable<Bam.Net.Services.DataReplication.Data.DataRelationship>> batchProcessor)
        {
            await Bam.Net.Services.DataReplication.Data.Dao.DataRelationship.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.Services.DataReplication.Data.DataRelationship>(batch));
            }, Database);
        }﻿		
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		/// <param name="where"></param>
		public Bam.Net.Services.DataReplication.Data.DeleteEvent GetOneDeleteEventWhere(WhereDelegate<DeleteEventColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.Services.DataReplication.Data.DeleteEvent>();
			return (Bam.Net.Services.DataReplication.Data.DeleteEvent)Bam.Net.Services.DataReplication.Data.Dao.DeleteEvent.GetOneWhere(where, Database).CopyAs(wrapperType, this);
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single DeleteEvent instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a DeleteEventColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DeleteEventColumns and other values
		/// </param>
		public Bam.Net.Services.DataReplication.Data.DeleteEvent OneDeleteEventWhere(WhereDelegate<DeleteEventColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.Services.DataReplication.Data.DeleteEvent>();
            return (Bam.Net.Services.DataReplication.Data.DeleteEvent)Bam.Net.Services.DataReplication.Data.Dao.DeleteEvent.OneWhere(where, Database).CopyAs(wrapperType, this);
        }

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a Bam.Net.Services.DataReplication.Data.DeleteEventColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between Bam.Net.Services.DataReplication.Data.DeleteEventColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.Services.DataReplication.Data.DeleteEvent> DeleteEventsWhere(WhereDelegate<DeleteEventColumns> where, OrderBy<DeleteEventColumns> orderBy = null)
        {
            return Wrap<Bam.Net.Services.DataReplication.Data.DeleteEvent>(Bam.Net.Services.DataReplication.Data.Dao.DeleteEvent.Where(where, orderBy, Database));
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
		/// <param name="where">A WhereDelegate that recieves a DeleteEventColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DeleteEventColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.Services.DataReplication.Data.DeleteEvent> TopDeleteEventsWhere(int count, WhereDelegate<DeleteEventColumns> where)
        {
            return Wrap<Bam.Net.Services.DataReplication.Data.DeleteEvent>(Bam.Net.Services.DataReplication.Data.Dao.DeleteEvent.Top(count, where, Database));
        }

		/// <summary>
		/// Return the count of DeleteEvents
		/// </summary>
		public long CountDeleteEvents()
        {
            return Bam.Net.Services.DataReplication.Data.Dao.DeleteEvent.Count(Database);
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a DeleteEventColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DeleteEventColumns and other values
		/// </param>
        public long CountDeleteEventsWhere(WhereDelegate<DeleteEventColumns> where)
        {
            return Bam.Net.Services.DataReplication.Data.Dao.DeleteEvent.Count(where, Database);
        }
        
        public async Task BatchQueryDeleteEvents(int batchSize, WhereDelegate<DeleteEventColumns> where, Action<IEnumerable<Bam.Net.Services.DataReplication.Data.DeleteEvent>> batchProcessor)
        {
            await Bam.Net.Services.DataReplication.Data.Dao.DeleteEvent.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.Services.DataReplication.Data.DeleteEvent>(batch));
            }, Database);
        }
		
        public async Task BatchAllDeleteEvents(int batchSize, Action<IEnumerable<Bam.Net.Services.DataReplication.Data.DeleteEvent>> batchProcessor)
        {
            await Bam.Net.Services.DataReplication.Data.Dao.DeleteEvent.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.Services.DataReplication.Data.DeleteEvent>(batch));
            }, Database);
        }﻿		
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		/// <param name="where"></param>
		public Bam.Net.Services.DataReplication.Data.DeleteOperation GetOneDeleteOperationWhere(WhereDelegate<DeleteOperationColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.Services.DataReplication.Data.DeleteOperation>();
			return (Bam.Net.Services.DataReplication.Data.DeleteOperation)Bam.Net.Services.DataReplication.Data.Dao.DeleteOperation.GetOneWhere(where, Database).CopyAs(wrapperType, this);
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single DeleteOperation instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a DeleteOperationColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DeleteOperationColumns and other values
		/// </param>
		public Bam.Net.Services.DataReplication.Data.DeleteOperation OneDeleteOperationWhere(WhereDelegate<DeleteOperationColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.Services.DataReplication.Data.DeleteOperation>();
            return (Bam.Net.Services.DataReplication.Data.DeleteOperation)Bam.Net.Services.DataReplication.Data.Dao.DeleteOperation.OneWhere(where, Database).CopyAs(wrapperType, this);
        }

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a Bam.Net.Services.DataReplication.Data.DeleteOperationColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between Bam.Net.Services.DataReplication.Data.DeleteOperationColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.Services.DataReplication.Data.DeleteOperation> DeleteOperationsWhere(WhereDelegate<DeleteOperationColumns> where, OrderBy<DeleteOperationColumns> orderBy = null)
        {
            return Wrap<Bam.Net.Services.DataReplication.Data.DeleteOperation>(Bam.Net.Services.DataReplication.Data.Dao.DeleteOperation.Where(where, orderBy, Database));
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
		/// <param name="where">A WhereDelegate that recieves a DeleteOperationColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DeleteOperationColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.Services.DataReplication.Data.DeleteOperation> TopDeleteOperationsWhere(int count, WhereDelegate<DeleteOperationColumns> where)
        {
            return Wrap<Bam.Net.Services.DataReplication.Data.DeleteOperation>(Bam.Net.Services.DataReplication.Data.Dao.DeleteOperation.Top(count, where, Database));
        }

		/// <summary>
		/// Return the count of DeleteOperations
		/// </summary>
		public long CountDeleteOperations()
        {
            return Bam.Net.Services.DataReplication.Data.Dao.DeleteOperation.Count(Database);
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a DeleteOperationColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between DeleteOperationColumns and other values
		/// </param>
        public long CountDeleteOperationsWhere(WhereDelegate<DeleteOperationColumns> where)
        {
            return Bam.Net.Services.DataReplication.Data.Dao.DeleteOperation.Count(where, Database);
        }
        
        public async Task BatchQueryDeleteOperations(int batchSize, WhereDelegate<DeleteOperationColumns> where, Action<IEnumerable<Bam.Net.Services.DataReplication.Data.DeleteOperation>> batchProcessor)
        {
            await Bam.Net.Services.DataReplication.Data.Dao.DeleteOperation.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.Services.DataReplication.Data.DeleteOperation>(batch));
            }, Database);
        }
		
        public async Task BatchAllDeleteOperations(int batchSize, Action<IEnumerable<Bam.Net.Services.DataReplication.Data.DeleteOperation>> batchProcessor)
        {
            await Bam.Net.Services.DataReplication.Data.Dao.DeleteOperation.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.Services.DataReplication.Data.DeleteOperation>(batch));
            }, Database);
        }﻿		
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		/// <param name="where"></param>
		public Bam.Net.Services.DataReplication.Data.QueryOperation GetOneQueryOperationWhere(WhereDelegate<QueryOperationColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.Services.DataReplication.Data.QueryOperation>();
			return (Bam.Net.Services.DataReplication.Data.QueryOperation)Bam.Net.Services.DataReplication.Data.Dao.QueryOperation.GetOneWhere(where, Database).CopyAs(wrapperType, this);
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single QueryOperation instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a QueryOperationColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between QueryOperationColumns and other values
		/// </param>
		public Bam.Net.Services.DataReplication.Data.QueryOperation OneQueryOperationWhere(WhereDelegate<QueryOperationColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.Services.DataReplication.Data.QueryOperation>();
            return (Bam.Net.Services.DataReplication.Data.QueryOperation)Bam.Net.Services.DataReplication.Data.Dao.QueryOperation.OneWhere(where, Database).CopyAs(wrapperType, this);
        }

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a Bam.Net.Services.DataReplication.Data.QueryOperationColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between Bam.Net.Services.DataReplication.Data.QueryOperationColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.Services.DataReplication.Data.QueryOperation> QueryOperationsWhere(WhereDelegate<QueryOperationColumns> where, OrderBy<QueryOperationColumns> orderBy = null)
        {
            return Wrap<Bam.Net.Services.DataReplication.Data.QueryOperation>(Bam.Net.Services.DataReplication.Data.Dao.QueryOperation.Where(where, orderBy, Database));
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
		/// <param name="where">A WhereDelegate that recieves a QueryOperationColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between QueryOperationColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.Services.DataReplication.Data.QueryOperation> TopQueryOperationsWhere(int count, WhereDelegate<QueryOperationColumns> where)
        {
            return Wrap<Bam.Net.Services.DataReplication.Data.QueryOperation>(Bam.Net.Services.DataReplication.Data.Dao.QueryOperation.Top(count, where, Database));
        }

		/// <summary>
		/// Return the count of QueryOperations
		/// </summary>
		public long CountQueryOperations()
        {
            return Bam.Net.Services.DataReplication.Data.Dao.QueryOperation.Count(Database);
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a QueryOperationColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between QueryOperationColumns and other values
		/// </param>
        public long CountQueryOperationsWhere(WhereDelegate<QueryOperationColumns> where)
        {
            return Bam.Net.Services.DataReplication.Data.Dao.QueryOperation.Count(where, Database);
        }
        
        public async Task BatchQueryQueryOperations(int batchSize, WhereDelegate<QueryOperationColumns> where, Action<IEnumerable<Bam.Net.Services.DataReplication.Data.QueryOperation>> batchProcessor)
        {
            await Bam.Net.Services.DataReplication.Data.Dao.QueryOperation.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.Services.DataReplication.Data.QueryOperation>(batch));
            }, Database);
        }
		
        public async Task BatchAllQueryOperations(int batchSize, Action<IEnumerable<Bam.Net.Services.DataReplication.Data.QueryOperation>> batchProcessor)
        {
            await Bam.Net.Services.DataReplication.Data.Dao.QueryOperation.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.Services.DataReplication.Data.QueryOperation>(batch));
            }, Database);
        }﻿		
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		/// <param name="where"></param>
		public Bam.Net.Services.DataReplication.Data.ReplicationOperation GetOneReplicationOperationWhere(WhereDelegate<ReplicationOperationColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.Services.DataReplication.Data.ReplicationOperation>();
			return (Bam.Net.Services.DataReplication.Data.ReplicationOperation)Bam.Net.Services.DataReplication.Data.Dao.ReplicationOperation.GetOneWhere(where, Database).CopyAs(wrapperType, this);
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single ReplicationOperation instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ReplicationOperationColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ReplicationOperationColumns and other values
		/// </param>
		public Bam.Net.Services.DataReplication.Data.ReplicationOperation OneReplicationOperationWhere(WhereDelegate<ReplicationOperationColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.Services.DataReplication.Data.ReplicationOperation>();
            return (Bam.Net.Services.DataReplication.Data.ReplicationOperation)Bam.Net.Services.DataReplication.Data.Dao.ReplicationOperation.OneWhere(where, Database).CopyAs(wrapperType, this);
        }

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a Bam.Net.Services.DataReplication.Data.ReplicationOperationColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between Bam.Net.Services.DataReplication.Data.ReplicationOperationColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.Services.DataReplication.Data.ReplicationOperation> ReplicationOperationsWhere(WhereDelegate<ReplicationOperationColumns> where, OrderBy<ReplicationOperationColumns> orderBy = null)
        {
            return Wrap<Bam.Net.Services.DataReplication.Data.ReplicationOperation>(Bam.Net.Services.DataReplication.Data.Dao.ReplicationOperation.Where(where, orderBy, Database));
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
		/// <param name="where">A WhereDelegate that recieves a ReplicationOperationColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ReplicationOperationColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.Services.DataReplication.Data.ReplicationOperation> TopReplicationOperationsWhere(int count, WhereDelegate<ReplicationOperationColumns> where)
        {
            return Wrap<Bam.Net.Services.DataReplication.Data.ReplicationOperation>(Bam.Net.Services.DataReplication.Data.Dao.ReplicationOperation.Top(count, where, Database));
        }

		/// <summary>
		/// Return the count of ReplicationOperations
		/// </summary>
		public long CountReplicationOperations()
        {
            return Bam.Net.Services.DataReplication.Data.Dao.ReplicationOperation.Count(Database);
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ReplicationOperationColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ReplicationOperationColumns and other values
		/// </param>
        public long CountReplicationOperationsWhere(WhereDelegate<ReplicationOperationColumns> where)
        {
            return Bam.Net.Services.DataReplication.Data.Dao.ReplicationOperation.Count(where, Database);
        }
        
        public async Task BatchQueryReplicationOperations(int batchSize, WhereDelegate<ReplicationOperationColumns> where, Action<IEnumerable<Bam.Net.Services.DataReplication.Data.ReplicationOperation>> batchProcessor)
        {
            await Bam.Net.Services.DataReplication.Data.Dao.ReplicationOperation.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.Services.DataReplication.Data.ReplicationOperation>(batch));
            }, Database);
        }
		
        public async Task BatchAllReplicationOperations(int batchSize, Action<IEnumerable<Bam.Net.Services.DataReplication.Data.ReplicationOperation>> batchProcessor)
        {
            await Bam.Net.Services.DataReplication.Data.Dao.ReplicationOperation.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.Services.DataReplication.Data.ReplicationOperation>(batch));
            }, Database);
        }﻿		
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		/// <param name="where"></param>
		public Bam.Net.Services.DataReplication.Data.RetrieveOperation GetOneRetrieveOperationWhere(WhereDelegate<RetrieveOperationColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.Services.DataReplication.Data.RetrieveOperation>();
			return (Bam.Net.Services.DataReplication.Data.RetrieveOperation)Bam.Net.Services.DataReplication.Data.Dao.RetrieveOperation.GetOneWhere(where, Database).CopyAs(wrapperType, this);
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single RetrieveOperation instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a RetrieveOperationColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between RetrieveOperationColumns and other values
		/// </param>
		public Bam.Net.Services.DataReplication.Data.RetrieveOperation OneRetrieveOperationWhere(WhereDelegate<RetrieveOperationColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.Services.DataReplication.Data.RetrieveOperation>();
            return (Bam.Net.Services.DataReplication.Data.RetrieveOperation)Bam.Net.Services.DataReplication.Data.Dao.RetrieveOperation.OneWhere(where, Database).CopyAs(wrapperType, this);
        }

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a Bam.Net.Services.DataReplication.Data.RetrieveOperationColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between Bam.Net.Services.DataReplication.Data.RetrieveOperationColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.Services.DataReplication.Data.RetrieveOperation> RetrieveOperationsWhere(WhereDelegate<RetrieveOperationColumns> where, OrderBy<RetrieveOperationColumns> orderBy = null)
        {
            return Wrap<Bam.Net.Services.DataReplication.Data.RetrieveOperation>(Bam.Net.Services.DataReplication.Data.Dao.RetrieveOperation.Where(where, orderBy, Database));
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
		/// <param name="where">A WhereDelegate that recieves a RetrieveOperationColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between RetrieveOperationColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.Services.DataReplication.Data.RetrieveOperation> TopRetrieveOperationsWhere(int count, WhereDelegate<RetrieveOperationColumns> where)
        {
            return Wrap<Bam.Net.Services.DataReplication.Data.RetrieveOperation>(Bam.Net.Services.DataReplication.Data.Dao.RetrieveOperation.Top(count, where, Database));
        }

		/// <summary>
		/// Return the count of RetrieveOperations
		/// </summary>
		public long CountRetrieveOperations()
        {
            return Bam.Net.Services.DataReplication.Data.Dao.RetrieveOperation.Count(Database);
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a RetrieveOperationColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between RetrieveOperationColumns and other values
		/// </param>
        public long CountRetrieveOperationsWhere(WhereDelegate<RetrieveOperationColumns> where)
        {
            return Bam.Net.Services.DataReplication.Data.Dao.RetrieveOperation.Count(where, Database);
        }
        
        public async Task BatchQueryRetrieveOperations(int batchSize, WhereDelegate<RetrieveOperationColumns> where, Action<IEnumerable<Bam.Net.Services.DataReplication.Data.RetrieveOperation>> batchProcessor)
        {
            await Bam.Net.Services.DataReplication.Data.Dao.RetrieveOperation.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.Services.DataReplication.Data.RetrieveOperation>(batch));
            }, Database);
        }
		
        public async Task BatchAllRetrieveOperations(int batchSize, Action<IEnumerable<Bam.Net.Services.DataReplication.Data.RetrieveOperation>> batchProcessor)
        {
            await Bam.Net.Services.DataReplication.Data.Dao.RetrieveOperation.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.Services.DataReplication.Data.RetrieveOperation>(batch));
            }, Database);
        }﻿		
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		/// <param name="where"></param>
		public Bam.Net.Services.DataReplication.Data.SaveOperation GetOneSaveOperationWhere(WhereDelegate<SaveOperationColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.Services.DataReplication.Data.SaveOperation>();
			return (Bam.Net.Services.DataReplication.Data.SaveOperation)Bam.Net.Services.DataReplication.Data.Dao.SaveOperation.GetOneWhere(where, Database).CopyAs(wrapperType, this);
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single SaveOperation instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a SaveOperationColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SaveOperationColumns and other values
		/// </param>
		public Bam.Net.Services.DataReplication.Data.SaveOperation OneSaveOperationWhere(WhereDelegate<SaveOperationColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.Services.DataReplication.Data.SaveOperation>();
            return (Bam.Net.Services.DataReplication.Data.SaveOperation)Bam.Net.Services.DataReplication.Data.Dao.SaveOperation.OneWhere(where, Database).CopyAs(wrapperType, this);
        }

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a Bam.Net.Services.DataReplication.Data.SaveOperationColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between Bam.Net.Services.DataReplication.Data.SaveOperationColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.Services.DataReplication.Data.SaveOperation> SaveOperationsWhere(WhereDelegate<SaveOperationColumns> where, OrderBy<SaveOperationColumns> orderBy = null)
        {
            return Wrap<Bam.Net.Services.DataReplication.Data.SaveOperation>(Bam.Net.Services.DataReplication.Data.Dao.SaveOperation.Where(where, orderBy, Database));
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
		/// <param name="where">A WhereDelegate that recieves a SaveOperationColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SaveOperationColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.Services.DataReplication.Data.SaveOperation> TopSaveOperationsWhere(int count, WhereDelegate<SaveOperationColumns> where)
        {
            return Wrap<Bam.Net.Services.DataReplication.Data.SaveOperation>(Bam.Net.Services.DataReplication.Data.Dao.SaveOperation.Top(count, where, Database));
        }

		/// <summary>
		/// Return the count of SaveOperations
		/// </summary>
		public long CountSaveOperations()
        {
            return Bam.Net.Services.DataReplication.Data.Dao.SaveOperation.Count(Database);
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a SaveOperationColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SaveOperationColumns and other values
		/// </param>
        public long CountSaveOperationsWhere(WhereDelegate<SaveOperationColumns> where)
        {
            return Bam.Net.Services.DataReplication.Data.Dao.SaveOperation.Count(where, Database);
        }
        
        public async Task BatchQuerySaveOperations(int batchSize, WhereDelegate<SaveOperationColumns> where, Action<IEnumerable<Bam.Net.Services.DataReplication.Data.SaveOperation>> batchProcessor)
        {
            await Bam.Net.Services.DataReplication.Data.Dao.SaveOperation.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.Services.DataReplication.Data.SaveOperation>(batch));
            }, Database);
        }
		
        public async Task BatchAllSaveOperations(int batchSize, Action<IEnumerable<Bam.Net.Services.DataReplication.Data.SaveOperation>> batchProcessor)
        {
            await Bam.Net.Services.DataReplication.Data.Dao.SaveOperation.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.Services.DataReplication.Data.SaveOperation>(batch));
            }, Database);
        }﻿		
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		/// <param name="where"></param>
		public Bam.Net.Services.DataReplication.Data.UpdateOperation GetOneUpdateOperationWhere(WhereDelegate<UpdateOperationColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.Services.DataReplication.Data.UpdateOperation>();
			return (Bam.Net.Services.DataReplication.Data.UpdateOperation)Bam.Net.Services.DataReplication.Data.Dao.UpdateOperation.GetOneWhere(where, Database).CopyAs(wrapperType, this);
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single UpdateOperation instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a UpdateOperationColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between UpdateOperationColumns and other values
		/// </param>
		public Bam.Net.Services.DataReplication.Data.UpdateOperation OneUpdateOperationWhere(WhereDelegate<UpdateOperationColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.Services.DataReplication.Data.UpdateOperation>();
            return (Bam.Net.Services.DataReplication.Data.UpdateOperation)Bam.Net.Services.DataReplication.Data.Dao.UpdateOperation.OneWhere(where, Database).CopyAs(wrapperType, this);
        }

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a Bam.Net.Services.DataReplication.Data.UpdateOperationColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between Bam.Net.Services.DataReplication.Data.UpdateOperationColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.Services.DataReplication.Data.UpdateOperation> UpdateOperationsWhere(WhereDelegate<UpdateOperationColumns> where, OrderBy<UpdateOperationColumns> orderBy = null)
        {
            return Wrap<Bam.Net.Services.DataReplication.Data.UpdateOperation>(Bam.Net.Services.DataReplication.Data.Dao.UpdateOperation.Where(where, orderBy, Database));
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
		/// <param name="where">A WhereDelegate that recieves a UpdateOperationColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between UpdateOperationColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.Services.DataReplication.Data.UpdateOperation> TopUpdateOperationsWhere(int count, WhereDelegate<UpdateOperationColumns> where)
        {
            return Wrap<Bam.Net.Services.DataReplication.Data.UpdateOperation>(Bam.Net.Services.DataReplication.Data.Dao.UpdateOperation.Top(count, where, Database));
        }

		/// <summary>
		/// Return the count of UpdateOperations
		/// </summary>
		public long CountUpdateOperations()
        {
            return Bam.Net.Services.DataReplication.Data.Dao.UpdateOperation.Count(Database);
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a UpdateOperationColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between UpdateOperationColumns and other values
		/// </param>
        public long CountUpdateOperationsWhere(WhereDelegate<UpdateOperationColumns> where)
        {
            return Bam.Net.Services.DataReplication.Data.Dao.UpdateOperation.Count(where, Database);
        }
        
        public async Task BatchQueryUpdateOperations(int batchSize, WhereDelegate<UpdateOperationColumns> where, Action<IEnumerable<Bam.Net.Services.DataReplication.Data.UpdateOperation>> batchProcessor)
        {
            await Bam.Net.Services.DataReplication.Data.Dao.UpdateOperation.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.Services.DataReplication.Data.UpdateOperation>(batch));
            }, Database);
        }
		
        public async Task BatchAllUpdateOperations(int batchSize, Action<IEnumerable<Bam.Net.Services.DataReplication.Data.UpdateOperation>> batchProcessor)
        {
            await Bam.Net.Services.DataReplication.Data.Dao.UpdateOperation.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.Services.DataReplication.Data.UpdateOperation>(batch));
            }, Database);
        }﻿		
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		/// <param name="where"></param>
		public Bam.Net.Services.DataReplication.Data.WriteEvent GetOneWriteEventWhere(WhereDelegate<WriteEventColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.Services.DataReplication.Data.WriteEvent>();
			return (Bam.Net.Services.DataReplication.Data.WriteEvent)Bam.Net.Services.DataReplication.Data.Dao.WriteEvent.GetOneWhere(where, Database).CopyAs(wrapperType, this);
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single WriteEvent instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a WriteEventColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between WriteEventColumns and other values
		/// </param>
		public Bam.Net.Services.DataReplication.Data.WriteEvent OneWriteEventWhere(WhereDelegate<WriteEventColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.Services.DataReplication.Data.WriteEvent>();
            return (Bam.Net.Services.DataReplication.Data.WriteEvent)Bam.Net.Services.DataReplication.Data.Dao.WriteEvent.OneWhere(where, Database).CopyAs(wrapperType, this);
        }

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a Bam.Net.Services.DataReplication.Data.WriteEventColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between Bam.Net.Services.DataReplication.Data.WriteEventColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.Services.DataReplication.Data.WriteEvent> WriteEventsWhere(WhereDelegate<WriteEventColumns> where, OrderBy<WriteEventColumns> orderBy = null)
        {
            return Wrap<Bam.Net.Services.DataReplication.Data.WriteEvent>(Bam.Net.Services.DataReplication.Data.Dao.WriteEvent.Where(where, orderBy, Database));
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
		/// <param name="where">A WhereDelegate that recieves a WriteEventColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between WriteEventColumns and other values
		/// </param>
		public IEnumerable<Bam.Net.Services.DataReplication.Data.WriteEvent> TopWriteEventsWhere(int count, WhereDelegate<WriteEventColumns> where)
        {
            return Wrap<Bam.Net.Services.DataReplication.Data.WriteEvent>(Bam.Net.Services.DataReplication.Data.Dao.WriteEvent.Top(count, where, Database));
        }

		/// <summary>
		/// Return the count of WriteEvents
		/// </summary>
		public long CountWriteEvents()
        {
            return Bam.Net.Services.DataReplication.Data.Dao.WriteEvent.Count(Database);
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a WriteEventColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between WriteEventColumns and other values
		/// </param>
        public long CountWriteEventsWhere(WhereDelegate<WriteEventColumns> where)
        {
            return Bam.Net.Services.DataReplication.Data.Dao.WriteEvent.Count(where, Database);
        }
        
        public async Task BatchQueryWriteEvents(int batchSize, WhereDelegate<WriteEventColumns> where, Action<IEnumerable<Bam.Net.Services.DataReplication.Data.WriteEvent>> batchProcessor)
        {
            await Bam.Net.Services.DataReplication.Data.Dao.WriteEvent.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.Services.DataReplication.Data.WriteEvent>(batch));
            }, Database);
        }
		
        public async Task BatchAllWriteEvents(int batchSize, Action<IEnumerable<Bam.Net.Services.DataReplication.Data.WriteEvent>> batchProcessor)
        {
            await Bam.Net.Services.DataReplication.Data.Dao.WriteEvent.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.Services.DataReplication.Data.WriteEvent>(batch));
            }, Database);
        }
	}
}																								
