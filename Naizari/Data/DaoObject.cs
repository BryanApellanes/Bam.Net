/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;
using Naizari.Extensions;
using Naizari.Helpers;
using Naizari.AlphaOmega;

namespace Naizari.Data
{
    /// <summary>
    /// Serves as the base class of all generated data objects.
    /// </summary>
    public abstract class DaoObject
    {
        internal Dictionary<string, object> initialValues;
        Dictionary<string, object> newValues;
        DatabaseAgent databaseAgent;

        bool changesPending;
        protected List<string> primaryKeyColumns;

        protected Dictionary<string, string> propertyToColumnMap;
        protected Dictionary<string, int> columnSizes;

        protected string tableName;
        protected string idColumn;
        protected string dataContextName;
        
        protected DaoObject()
        {
            initialValues = new Dictionary<string, object>();
            newValues = new Dictionary<string, object>();
            columnSizes = new Dictionary<string, int>();
            propertyToColumnMap = new Dictionary<string, string>();
            primaryKeyColumns = new List<string>();
            changesPending = false;
            this.BeforeDelete += this.DoBeforeDelete;
        }

        #region events

        public static event DaoValueChangedEventHandler ValueChangedAny;

        /// <summary>
        /// This event will occur when a change is made to the properties of the
        /// object if the new state is not representative of what is
        /// currently in the database.  The intent is to notify any 
        /// listening parent object that child updates should be made 
        /// when updating.  When this event fires the change has not been 
        /// committed to the database, <see cref="DaoObject.ChangesCommitted"/>.
        /// </summary>
        public event DaoValueChangedEventHandler ValueChanged;

        public static event DaoValueChangedEventHandler BeforeValueChangedAny;

        /// <summary>
        /// This event will occur before a column value on the current
        /// instance is set, will happen each time even if the previous
        /// value has not been committed.
        /// </summary>
        public event DaoValueChangedEventHandler BeforeValueChanged;
        
        /// <summary>
        /// The event fired when changes are committed to any DaoObject
        /// </summary>
        public static event DaoObjectEventHandler ChangesCommittedAny;

        /// <summary>
        /// This event will occur each time changes are committed to the 
        /// database.
        /// </summary>
        public event DaoObjectEventHandler ChangesCommitted;

        public static event DaoObjectEventHandler BeforeChangesCommittedAny;

        /// <summary>
        /// This event will occur before changes are committed to the 
        /// database.
        /// </summary>
        public event DaoObjectEventHandler BeforeChangesCommitted;

        /// <summary>
        /// The event fired before inserting any DaoObject
        /// </summary>
        public static event DaoObjectEventHandler BeforeInsertAny;

        /// <summary>
        /// This event will occur before inserting the current instance.
        /// </summary>
        public event DaoObjectEventHandler BeforeInsert;

        /// <summary>
        /// The event fired after any DaoObject is inserted.
        /// </summary>
        public static event DaoObjectEventHandler AfterInsertAny;

        /// <summary>
        /// This event will occur after inserting the current instance.
        /// </summary>
        public event DaoObjectEventHandler AfterInsert;

        /// <summary>
        /// The event fired before any DaoObject is deleted.
        /// </summary>
        public static event DaoObjectEventHandler BeforeDeleteAny;

        /// <summary>
        /// This event will occur before the current instance is
        /// deleted by calling the Delete() method.
        /// </summary>
        public event DaoObjectEventHandler BeforeDelete;


        private void OnBeforeValueChanged(string columnName, object value)
        {
            if (BeforeValueChanged != null)
            {
                BeforeValueChanged(this, new DaoValueChangedEventArgs(columnName, value));
            }

            if (BeforeValueChangedAny != null)
            {
                BeforeValueChangedAny(this, new DaoValueChangedEventArgs(columnName, value));
            }
        }

        private void OnValueChanged(string columnName, object value)
        {
            if (ValueChanged != null)
            {
                ValueChanged(this, new DaoValueChangedEventArgs(columnName, value));
            }

            if (ValueChangedAny != null)
            {
                ValueChangedAny(this, new DaoValueChangedEventArgs(columnName, value));
            }
        }

        private void OnBeforeChangesCommitted()
        {
            if (BeforeChangesCommitted != null)
            {
                BeforeChangesCommitted(this);
            }

            if (BeforeChangesCommittedAny != null)
            {
                BeforeChangesCommittedAny(this);
            }
        }

        private void OnChangesCommitted()
        {
            if (ChangesCommitted != null)
            {
                ChangesCommitted(this);
            }

            if (ChangesCommittedAny != null)
            {
                ChangesCommittedAny(this);
            }
        }

        private void OnBeforeDelete()
        {
            if (BeforeDelete != null)
            {
                BeforeDelete(this);
            }

            if (BeforeDeleteAny != null)
            {
                BeforeDeleteAny(this);
            }
        }

        private void OnBeforeInsert()
        {
            if (BeforeInsert != null)
            {
                BeforeInsert(this);
            }

            if (BeforeInsertAny != null)
            {
                BeforeInsertAny(this);
            }
        }

        private void OnAfterInsert()
        {
            if (AfterInsert != null)
            {
                AfterInsert(this);
            }

            if (AfterInsertAny != null)
            {
                AfterInsertAny(this);
            }
        }
        /// <summary>
        /// When overridden in a derived class should perform any necessary actions required to allow the
        /// deletion of the current instance to succeed. For example, deleting referencing entries from other
        /// tables (represented by the [TableName]List properties of the current instance).
        /// </summary>
        /// <param name="self"></param>
        protected virtual void DoBeforeDelete(DaoObject self)
        {

        }
        #endregion 


        [DaoIgnore]
        public string TableName
        {
            get { return tableName; }
        }

        [DaoIgnore]
        public DatabaseAgent DatabaseAgent
        {
            get
            {
                if (this.databaseAgent == null)
                {
                    this.databaseAgent = DaoContext.Get(this.dataContextName).DatabaseAgent;
                }

                return this.databaseAgent;
            }

            internal set
            {
                this.databaseAgent = value;
            }
        }

        [DaoIgnore]
        public string DataContextName
        {
            get { return dataContextName; }
        }

        public long GetId()
        {
            return this.GetLongValue(DatabaseAgent.GetUniquenessColumn(this.GetType()).ColumnName);
        }

        public T Clone<T>() where T : DaoObject, new()
        {
            T retVal = DaoObject.New<T>();
            PropertyInfo[] properties = CustomAttributeExtension.GetPropertiesWithAttributeOfType<DaoColumn>(typeof(T), false);
            foreach (PropertyInfo property in properties)
            {
                if (property.CanWrite)
                    property.SetValue(retVal, property.GetValue(this, null), null);
            }
            retVal.DatabaseAgent = this.DatabaseAgent;
            return retVal;
        }

        #region sql - select & execute
        public static T[] ExecuteSql<T>(string sqlStatement) where T: DaoObject, new()
        {
            return ExecuteSql<T>(sqlStatement, new DbParameter[] { });
        }

        public static T[] ExecuteSql<T>(string sqlStatement, params DbParameter[] parameters) where T : DaoObject, new()
        {
            T proxy = new T();
            return ExecuteSql<T>(sqlStatement, DaoContext.Get(proxy.DataContextName).DatabaseAgent, parameters);
        }

        public static T[] ExecuteSql<T>(string sqlStatement, DatabaseAgent agent, params DbParameter[] parameters) where T : DaoObject, new()
        {
            return agent.ExecuteSql<T>(sqlStatement, parameters);
        }

        public static T[] ExecuteStoredProcedure<T>(string procedureName) where T : DaoObject, new()
        {
            return ExecuteStoredProcedure<T>(procedureName, new DbParameter[] { });
        }

        public static T[] ExecuteStoredProcedure<T>(string procedureExecStatement, params DbParameter[] parameters) where T : DaoObject, new()
        {
            T proxy = new T();
            return ExecuteStoredProcedure<T>(procedureExecStatement, DaoContext.Get(proxy.DataContextName).DatabaseAgent, parameters);
        }

        public static T[] ExecuteStoredProcedure<T>(string procedureExecStatement, DatabaseAgent agent, params DbParameter[] parameters) where T : DaoObject, new()
        {
            return agent.ExecuteStoredProcedure<T>(procedureExecStatement, parameters);
        }

        public static T[] Search<T>(DaoSearchFilter filter) where T : DaoObject, new()
        {
            return Search<T>(filter, DaoContext.Get(new T().DataContextName).DatabaseAgent);
        }

        public static T[] Search<T>(DaoSearchFilter filter, DatabaseAgent agent) where T : DaoObject, new()
        {
            return Search<T>(filter, -1, agent);
        }

        public static T[] Search<T>(DaoSearchFilter filter, int count) where T : DaoObject, new()
        {
            return Search<T>(filter, count, DaoContext.Get(new T().DataContextName).DatabaseAgent);
        }

        public static T[] Search<T>(DaoSearchFilter filter, int count, DatabaseAgent agent) where T: DaoObject, new()
        {
            return Select<T>(filter, count, agent);
        }

        public static T[] Search<T>(DaoSearchFilter filter, OrderBy orderBy) where T : DaoObject, new()
        {
            return Search<T>(filter, orderBy, DaoContext.Get(new T().DataContextName).DatabaseAgent);
        }

        public static T[] Search<T>(DaoSearchFilter filter, OrderBy orderBy, DatabaseAgent agent) where T : DaoObject, new()
        {
            return agent.Select<T>(filter, orderBy, -1); 
        }

        public static T[] Search<T>(DaoSearchFilter filter, OrderBy orderBy, int count) where T : DaoObject, new()
        {
            return Search<T>(filter, orderBy, count, DaoContext.Get(new T().DataContextName).DatabaseAgent);
        }

        public static T[] Search<T>(DaoSearchFilter filter, OrderBy orderBy, int count, DatabaseAgent agent) where T : DaoObject, new()
        {
            return agent.Select<T>(filter, orderBy, count);
        }

        public static T[] Select<T>(DaoSearchFilter filter) where T : DaoObject, new()
        {
            return Select<T>(filter, DaoContext.Get(new T().DataContextName).DatabaseAgent);
        }

        public static T[] Select<T>(DaoSearchFilter filter, DatabaseAgent agent) where T : DaoObject, new()
        {
            return Select<T>(filter, -1, agent);
        }

        public static T[] Select<T>(DaoSearchFilter filter, int count) where T : DaoObject, new()
        {
            return Select<T>(filter, count, DaoContext.Get(new T().DataContextName).DatabaseAgent);
        }

        public static T[] Select<T>(DaoSearchFilter filter, int count, DatabaseAgent agent) where T: DaoObject, new()
        {
            return Select<T>(filter.ToString(), null, count, agent, filter.DbParameters);
        }

        protected static T[] Select<T>() where T : DaoObject, new()
        {
            return Select<T>(DaoContext.Get(new T().DataContextName).DatabaseAgent);
        }
        protected static T[] Select<T>(DatabaseAgent agent) where T : DaoObject, new()
        {
            return Select<T>(string.Empty, agent);
        }

        public static T[] Select<T>(DaoSearchFilter filter, OrderBy orderBy) where T : DaoObject, new()
        {
            return Select<T>(filter, orderBy, DaoContext.Get(new T().DataContextName).DatabaseAgent);
        }

        public static T[] Select<T>(DaoSearchFilter filter, OrderBy orderBy, DatabaseAgent agent) where T: DaoObject, new()
        {
            return agent.Select<T>(filter, orderBy, -1);
        }

        public static T[] Select<T>(string orderBy) where T : DaoObject, new()
        {
            return Select<T>(orderBy, DaoContext.Get(new T().DataContextName).DatabaseAgent);
        }

        public static T[] Select<T>(string orderBy, DatabaseAgent agent) where T : DaoObject, new()
        {
            return Select<T>(string.Empty, orderBy, agent);
        }

        protected static T[] Select<T>(string whereClause, params DbParameter[] parameters) where T : DaoObject, new()
        {
            return Select<T>(whereClause, DaoContext.Get(new T().DataContextName).DatabaseAgent, parameters);
        }

        protected static T[] Select<T>(string whereClause, DatabaseAgent agent, params DbParameter[] parameters) where T : DaoObject, new()
        {
            return Select<T>(whereClause, string.Empty, agent, parameters);
        }

        protected static T[] Select<T>(string whereClause, string orderBy, params DbParameter[] parameters) where T : DaoObject, new()
        {
            return Select<T>(whereClause, orderBy, DaoContext.Get(new T().DataContextName).DatabaseAgent);
        }

        protected static T[] Select<T>(string whereClause, string orderBy, DatabaseAgent agent, params DbParameter[] parameters) where T : DaoObject, new()
        {
            return Select<T>(whereClause, orderBy, -1, agent, parameters);
        }

        protected static T[] Select<T>(string whereClause, string orderBy, int count, params DbParameter[] parameters) where T : DaoObject, new()
        {
            T proxy = new T();
            return Select<T>(whereClause, orderBy, count, DaoContext.Get(proxy.dataContextName).DatabaseAgent, parameters);
        }

        internal protected static T[] Select<T>(string whereClause, string orderBy, int count, DatabaseAgent agent, params DbParameter[] parameters) where T : DaoObject, new()
        {
            return agent.Select<T>(whereClause, orderBy, count, parameters);

        }

        public static T SelectById<T>(int id) where T : DaoObject, new()
        {
            return SelectById<T>((long)id);
        }

        public static T SelectById<T>(int id, DatabaseAgent agent) where T : DaoObject, new()
        {
            return SelectById<T>((long)id, agent);
        }

        public static T SelectById<T>(long id) where T : DaoObject, new()
        {
            T proxy = new T();
            return SelectById<T>(id, DaoContext.Get(proxy.DataContextName).DatabaseAgent);
        }

        public static T SelectById<T>(long id, DatabaseAgent agent) where T: DaoObject, new()//, string tableName, string columnName, string contextName) where T : DaoObject, new()
        {
            return agent.SelectById<T>(id);
        }

        public static T[] SelectByIdList<T>(int[] ids) where T : DaoObject, new()
        {
            T proxy = new T();
            return SelectByIdList<T>(ids, DaoContext.Get(proxy.DataContextName).DatabaseAgent);
        }

        public static T[] SelectByIdList<T>(int[] ids, DatabaseAgent agent) where T: DaoObject, new()
        {            
            return agent.SelectByIdList<T>(ids);
        }

        public static T[] SelectByIdList<T>(long[] ids) where T : DaoObject, new()
        {
            T proxy = new T();
            return SelectByIdList<T>(ids, DaoContext.Get(proxy.DataContextName).DatabaseAgent);
        }

        public static T[] SelectByIdList<T>(long[] ids, DatabaseAgent agent) where T : DaoObject, new()
        {
            return agent.SelectByIdList<T>(ids);
        }

        public static T[] SelectByIdList<T>(string[] ids) where T : DaoObject, new()
        {
            T proxy = new T();
            return SelectByIdList<T>(ids, DaoContext.Get(proxy.DataContextName).DatabaseAgent);
        }

        public static T[] SelectByIdList<T>(string[] ids, DatabaseAgent agent) where T : DaoObject, new()
        {            
            return agent.SelectByIdList<T>(ids);
        }

        public static R[] SelectDistinct<T, R>(Enum field, DaoSearchFilter filter) where T : DaoObject, new()
        {
            return SelectDistinct<T, R>(field, filter, DaoContext.Get(new T().DataContextName).DatabaseAgent);
        }

        public static R[] SelectDistinct<T, R>(Enum field, DaoSearchFilter filter, DatabaseAgent agent) where T : DaoObject, new()
        {
            return agent.SelectDistinct<T, R>(field, filter);
        }

        public static R[] SelectPropertyList<T, R>(Enum field, DaoSearchFilter filter) where T : DaoObject, new()
        {
            return SelectPropertyList<T, R>(field, filter, DaoContext.Get(new T().DataContextName).DatabaseAgent);
        }

        public static R[] SelectPropertyList<T, R>(Enum field, DaoSearchFilter filter, DatabaseAgent agent) where T : DaoObject, new()
        {
            return agent.SelectPropertyList<T, R>(field, filter);
        }

        public static T[] SelectByPropertyList<T>(Enum field, params object[] propertyValues) where T : DaoObject, new()
        {
            return SelectByPropertyList<T>(field, DaoContext.Get(new T().DataContextName).DatabaseAgent, propertyValues);
        }

        public static T[] SelectByPropertyList<T>(Enum field, DatabaseAgent agent, params object[] propertyValues) where T : DaoObject, new()
        {
            return agent.SelectByPropertyList<T>(field, propertyValues);
        }

        public static object[] SelectByPropertyList(Type type, Enum field, params object[] propertyValues)
        {
            object instance = type.GetConstructor(Type.EmptyTypes).Invoke(null);
            string contextName = (string)type.GetProperty("DataContextName").GetValue(instance, null);
            return SelectByPropertyList(type, field, DaoContext.Get(contextName).DatabaseAgent, propertyValues);
        }

        public static object[] SelectByPropertyList(Type type, Enum field, DatabaseAgent agent, params object[] propertyValues)
        {
            return SelectByPropertyList(type, field, agent, SortOrder.Ascending, propertyValues);
        }

        public static object[] SelectByPropertyList(Type type, Enum field, DatabaseAgent agent, SortOrder sortOrder, params object[] propertyValues)
        {
            return agent.SelectByPropertyList(type, field.ToString(), propertyValues, new OrderBy(field, sortOrder));           
        }

        public static R[] SelectPropertyByPropertyList<T, R>(Enum returnField, Enum compareField, params object[] propertyValues) where T : DaoObject, new()
        {
            return SelectPropertyByPropertyList<T, R>(returnField, compareField, DaoContext.Get(new T().DataContextName).DatabaseAgent, propertyValues);
        }

        public static R[] SelectPropertyByPropertyList<T, R>(Enum returnField, Enum compareField, DatabaseAgent agent, params object[] propertyValues) where T : DaoObject, new()
        {
            return agent.SelectPropertyByPropertyList<T, R>(returnField, compareField, propertyValues);
        }

        /// <summary>
        /// Return all records of the type specified by T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected static T[] SelectAll<T>() where T: DaoObject, new()
        {
            return Select<T>();
        }

        protected static T[] SelectAll<T>(DatabaseAgent agent) where T : DaoObject, new()
        {
            return Select<T>(agent);
        }

        protected static T SelectTop<T>(OrderBy orderBy) where T : DaoObject, new()
        {
            return SelectTop<T>(orderBy, DaoContext.Get(new T().DataContextName).DatabaseAgent);
        }

        protected static T SelectTop<T>(OrderBy orderBy, DatabaseAgent agent) where T: DaoObject, new()
        {
            T[] ret = SelectTop<T>(orderBy, 1, agent);
            if (ret.Length == 1)
                return ret[0];
            return null;
        }
        
        protected static T[] SelectTop<T>(OrderBy orderBy, int count) where T : DaoObject, new()
        {
            T proxy = new T();
            return SelectTop<T>(orderBy, count, DaoContext.Get(proxy.DataContextName).DatabaseAgent);
        }

        protected static T[] SelectTop<T>(OrderBy orderBy, int count, DatabaseAgent agent) where T: DaoObject, new()
        {
            return agent.SelectTop<T>(orderBy, count);
        }

        public static T[] SelectTop<T>(DaoSearchFilter filter, OrderBy orderBy, int count) where T : DaoObject, new()
        {
            T proxy = new T();
            return SelectTop<T>(filter, orderBy, count, DaoContext.Get(proxy.DataContextName).DatabaseAgent);
        }

        public static T[] SelectTop<T>(DaoSearchFilter filter, int count) where T : DaoObject, new()
        {
            T proxy = new T();
            return SelectTop<T>(filter, new OrderBy(proxy.IdColumnName, SortOrder.Ascending), count, DaoContext.Get(proxy.DataContextName).DatabaseAgent);
        }

        public static T[] SelectTop<T>(DaoSearchFilter filter, int count, DatabaseAgent agent) where T : DaoObject, new()
        {
            T proxy = new T();
            return SelectTop<T>(filter, new OrderBy(proxy.IdColumnName, SortOrder.Ascending), count, agent);
        }

        public static T[] SelectTop<T>(DaoSearchFilter filter, OrderBy orderBy, int count, DatabaseAgent agent) where T : DaoObject, new()
        {
            return agent.SelectTop<T>(filter, orderBy, count);
        }

        protected static T[] SelectListWhere<T>(string column, object value) where T : DaoObject, new()
        {
            T proxy = new T();
            return SelectListWhere<T>(column, value, DaoContext.Get(proxy.DataContextName).DatabaseAgent);
        }

        protected static T[] SelectListWhere<T>(string column, object value, DatabaseAgent agent) where T: DaoObject, new()//, string tableName, string column, string contextName) where T : DaoObject, new()
        {
            ThrowIfNull(column, "column");
            ThrowIfNull(value, "value");

            DatabaseAgent db = agent;// 
            DaoSearchFilter filter = new DaoSearchFilter();
            filter.AddParameter(column, value);
            return agent.Select<T>(filter);
        }

        protected static T[] SelectListWhere<T>(params DbSelectParameter[] selectParameters) where T : DaoObject, new()
        {
            return SelectListWhere<T>(WhereAppender.AND, selectParameters);
        }

        protected static T[] SelectListWhere<T>(WhereAppender appender, params DbSelectParameter[] selectParameters) where T : DaoObject, new()
        {
            return SelectListWhere<T>(appender, DaoContext.Get(new T().DataContextName).DatabaseAgent, selectParameters);
        }

        protected static T[] SelectListWhere<T>(WhereAppender appender, DatabaseAgent agent, params DbSelectParameter[] selectParameters) where T : DaoObject, new()
        {
            ThrowIfNull(selectParameters, "selectParameters");
            string whereClause = string.Empty;

            List<DbParameter> parameters = new List<DbParameter>();
            for (int i = 0; i < selectParameters.Length; i++ )
            {
                parameters.Add(selectParameters[i].CreateParameter(agent));

                whereClause += selectParameters[i].ToString();
                if (i != selectParameters.Length - 1)
                    whereClause += " " + appender.ToString() + " ";
            }

            return Select<T>(whereClause, parameters.ToArray());
        }

        public static T SelectOneWhere<T>(params DbSelectParameter[] selectParamaters) where T : DaoObject, new()
        {
            T[] results = SelectListWhere<T>(selectParamaters);
            if (results.Length > 1)
                throw new MultipleEntriesFoundException();
            if (results.Length == 0)
                return null;
            else
                return results[0];
        }

        public static T SelectOneWhere<T>(DaoSearchFilter filter) where T : DaoObject, new()
        {
            return SelectOneWhere<T>(filter, DaoContext.Get(new T().DataContextName).DatabaseAgent);
        }
        public static T SelectOneWhere<T>(DaoSearchFilter filter, DatabaseAgent agent) where T: DaoObject, new()
        {
            return agent.SelectOneWhere<T>(filter);
        }

        protected static T[] SelectXrefJoin<T>(object leftValue,            
            DataRelationshipDefinition parentDefinition,
            DataRelationshipDefinition childDefinition,
            string contextName) where T: DaoObject, new()
        {
            return SelectXrefJoin<T>(leftValue, parentDefinition, childDefinition, contextName, DaoContext.Get(new T().DataContextName).DatabaseAgent);
        }

        protected static T[] SelectXrefJoin<T>(object leftValue,
            DataRelationshipDefinition parentDefinition,
            DataRelationshipDefinition childDefinition,
            string contextName,
            DatabaseAgent agent) where T : DaoObject, new()//string leftTable, string leftColumn, string rightTable, string rightColumn) where T : DaoObject, new()
        {
            ThrowIfNull(leftValue, "parentIdValue");
            ThrowIfNull(parentDefinition, "parentDefintion");
            ThrowIfNull(childDefinition, "childDefinition");
            ThrowIfNotEqual(parentDefinition.ForeignKeyTable, childDefinition.ForeignKeyTable, "ForeignKeyTable of parentDefinition and childDefinition must match");

            string xrefTableName = parentDefinition.ForeignKeyTable;
            string parentTableName = parentDefinition.PrimaryKeyTable;
            string xrefColumnRelatedToParentColumn = parentDefinition.ForeignKeyColumn;
            string parentIdColumn = parentDefinition.PrimaryKeyColumn;
            string childTableName = childDefinition.PrimaryKeyTable;
            string xrefColumnRelatedToChildColumn = childDefinition.ForeignKeyColumn;
            string childIdColumn = childDefinition.PrimaryKeyColumn;

            string join = string.Format("SELECT * FROM "
                + "{0} JOIN "
                + "{1} ON "
                + "{0}.{2} = {1}.{3} JOIN "
                + "{4} ON {0}.{5} = {4}.{6} "
                + "WHERE {1}.{3} = @value",
                xrefTableName,
                parentTableName,
                xrefColumnRelatedToParentColumn,
                parentIdColumn,
                childTableName,
                xrefColumnRelatedToChildColumn,
                childIdColumn);

            DatabaseAgent db = agent;
            DataTable data = db.GetDataTableFromSql(join, new SqlParameter("@value", leftValue));
            return agent.ToObjectArray<T>(data);
        }
        #endregion

        #region value getters
        public object GetInitialValue(string columnName)
        {
            if (initialValues.ContainsKey(columnName))
                return initialValues[columnName];
            return null;
        }

        public object GetValue(string columnName)
        {
            if (!newValues.ContainsKey(columnName))
            {
                if (initialValues.ContainsKey(columnName))
                    return initialValues[columnName];
                else
                    return null;
            }
            else
                return newValues[columnName];
        }

        public int GetIntValue(string columnName)
        {
            object obj = GetValue(columnName);
            if (obj != null)
            {            
                if (obj is int)
                    return (int)obj;

                if (obj is long)
                    return (int)(long)obj;                
            }

            return -1;
        }

        public Guid GetGuidValue(string columnName)
        {
            object obj = GetValue(columnName);
            if (obj != null)
            {
                try
                {
                    return (Guid)obj;
                }
                catch
                {
                    try
                    {
                        return new Guid(obj.ToString());
                    }
                    catch
                    {}

                }
            }

            return Guid.Empty;
        }

        public long GetLongValue(string columnName)
        {
            object obj = GetValue(columnName);
            if (obj != null)
            {
                try
                {
                    return Convert.ToInt64(obj);
                }
                catch
                {
                    return -1;
                }
            }
            else
                return -1;
        }

        public byte[] GetByteValue(string columnName)
        {
            object obj = GetValue(columnName);
            if (obj == null)
                return null;
            else
            {
                return (byte[])obj;             
            }
        }

        public bool GetBoolValue(string columnName)
        {
            object obj = GetValue(columnName);
            if (obj == null)
                return false;
            else
                return (bool)obj;
        }

        public DateTime GetDateTimeValue(string columnName)
        {
            object obj = GetValue(columnName);
            if (obj != null)
            {
                try
                {
                    return (DateTime)obj;
                }
                catch
                {
                    return JulianDate.ToDateTime((double)obj);
                }
            }
            else
                return new DateTime();
        }

        public decimal GetDecimalValue(string columnName)
        {
            object obj = GetValue(columnName);
            if (obj != null)
            {
                try
                {
                    return Convert.ToDecimal(obj);
                }
                catch
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }
        #endregion

        [DaoIgnore]
        public string IdColumnName
        {
            get { return idColumn; }
        }

        public void Delete()
        {
            this.Delete(this.DatabaseAgent);
        }

        public void Delete(DatabaseAgent agent)
        {
            string idCol = this.idColumn;
            DaoSearchFilter filter = new DaoSearchFilter();

            if (string.IsNullOrEmpty(idCol))
            {
                try
                {
                    idCol = DatabaseAgent.GetUniquenessColumn(this.GetType()).ColumnName;
                }
                catch (IdColumnNotDefinedException icnde)
                {
                    DeleteByValues(agent, icnde);
                }
            }

            if (!string.IsNullOrEmpty(idCol))
            {
                filter.AddParameter(idCol, this.GetValue(idCol));
                Delete(filter, agent);
            }
        }

        private void DeleteByValues(DatabaseAgent agent, IdColumnNotDefinedException icnde)
        {
            DaoSearchFilter filter = new DaoSearchFilter();
            foreach (string column in propertyToColumnMap.Values)
            {
                filter.AddParameter(column, this.GetValue(column));
            }
            DataTable existing = this.databaseAgent.GetDataTableFromSql(string.Format(DatabaseAgent.SELECTFORMAT, "", this.TableName, "WHERE " + filter.ToString()), filter.DbParameters);
            if (existing.Rows.Count > 1)
            {
                throw new MultipleEntriesFoundException(icnde);
            }
            else if (existing.Rows.Count == 1)
            {
                Delete(filter, agent);
            }
        }

        internal void Delete(DaoSearchFilter filter, DatabaseAgent agent)
        {
            if (AllowDelete)
            {
                OnBeforeDelete();
                string whereClause = filter.ToString();
                string deleteStatement = string.Format(DatabaseAgent.DELETEFORMAT, this.tableName, whereClause);

                agent.ExecuteSql(deleteStatement, agent.ConvertParameters(filter.DbParameters));
            }
            else
            {
                throw new InvalidOperationException("Attempted to delete a record without setting the AllowDelete flag on the object");
            }
        }

        [XmlIgnore]
        [DaoIgnore]
        public bool AllowDelete { get; set; }

        /// <summary>
        /// Will be true if the current instance was instantiated using the .New() method and not using 
        /// a constructor.
        /// </summary>
        [XmlIgnore]
        [DaoIgnore]
        public bool IsNew { get; internal set; }

        public static T New<T>(DatabaseAgent agent) where T: DaoObject, new()
        {
            T retVal = New<T>();
            retVal.DatabaseAgent = agent;
            return retVal;
        }

        public static T New<T>() where T: DaoObject, new()
        {
            T ret = new T();
            foreach (string column in ret.GetPropertyToColumnMap().Values)
            {
                if (!ret.initialValues.ContainsKey(column))
                    ret.initialValues.Add(column, null);
            }
            ret.IsNew = true;
            return ret;
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() == this.GetType())
            {
                DaoObject compareToObj = (DaoObject)obj;
                string idColumn = "";
                if (this.HasId)
                {
                    idColumn = this.idColumn;
                }

                if (string.IsNullOrEmpty(idColumn))
                {
                    try
                    {
                        idColumn = DatabaseAgent.GetUniquenessColumn(this.GetType()).ColumnName;
                    }
                    catch //(IdColumnNotDefinedException icnse)
                    {
                        return base.Equals(obj);
                    }
                }

                return this.GetLongValue(idColumn) == compareToObj.GetLongValue(idColumn);
            }
            
            return base.Equals(obj);            
        }

        public override int GetHashCode()
        {
            string idColumn;
            if (TryGetUniquenessColumn(out idColumn))
            {
                return this.GetIntValue(idColumn);//base.GetHashCode() + this.GetIntValue(this.idColumn);
            }

            return base.GetHashCode();
        }

        bool allowModifyPrimaryKeyValue = false;
        /// <summary>
        /// Set to true if primary key values need to be set 
        /// manually for some reason.
        /// </summary>
        public bool AllowModifyPrimaryKeyValue
        {
            get { return allowModifyPrimaryKeyValue; }
            set { allowModifyPrimaryKeyValue = value; }
        }

        protected bool TryGetUniquenessColumn(out string idColumn)
        {
            idColumn = string.Empty;
            try
            {
                idColumn = DatabaseAgent.GetUniquenessColumn(this.GetType()).ColumnName;
                return true;
            }
            catch (IdColumnNotDefinedException idns)
            {
                idColumn = idns.Message;
                return false;
            }
        }

        /// <summary>
        /// Set the value for the specified column.  This
        /// method is intended for use by objects that implement
        /// DaoObject in each property set method.
        /// </summary>
        /// <param name="columnName">The name of the column value being
        /// set.</param>
        /// <param name="value">The value to set the column to.</param>
        protected internal void SetValue(string columnName, object value)
        {
            bool isGuidString = false;
            if (value is Guid && this.DatabaseAgent.DbType != DaoDbType.MSSql)
            {
                value = value.ToString();
                isGuidString = true;
            }

            if (value is DateTime && this.DatabaseAgent.DbType != DaoDbType.MSSql)
            {
                value = JulianDate.ToJulianDate((DateTime)value);
            }

            if(value is int)
            {
                int val = (int)value;
                long newVal = val;
                value = newVal;
            }

            if (primaryKeyColumns.Contains(columnName) && !AllowModifyPrimaryKeyValue)
            {
                if (this.GetValue(columnName) == null ||
                    this.GetIntValue(columnName) == -1)
                {
                    if (!initialValues.ContainsKey(columnName))
                    {
                        initialValues.Add(columnName, value);
                    }
                    else
                    {
                        initialValues[columnName] = value;
                    }
                    return;
                }
                else
                {
                    throw new InvalidOperationException(
                        string.Format("Attempted to modify a column value that is part of the primary key for the table {0}.  If this was intential set the AllowModifyPrimaryKeyValue to true, but be sure you know what you're doing!!",
                        this.tableName));
                }
            }   
            // If the value being set is the Id
            // verify that it has not already been set,
            // the value will be -1 for new entries
            // which means that the current instance
            // was just instantiated.  If it's null
            // the value is just being initialized
            if (columnName.Equals(idColumn))
            {
                if (this.GetValue(idColumn) == null ||
                    (long)this.GetValue(idColumn) == -1)
                {
                    initialValues.Add(idColumn, value);
                    return;
                }
                else
                {
                    throw new InvalidOperationException("Attempted to modify the id property");
                }
            }

            if (!initialValues.ContainsKey(columnName))
            {
                initialValues.Add(columnName, value);
            }
            else
            {
                SetNewValue(columnName, value, isGuidString);
            }
        }

        private void SetNewValue(string columnName, object value)
        {
            SetNewValue(columnName, value, false);
        }

        private void SetNewValue(string columnName, object value, bool isGuidString)
        {
            if (!isGuidString)
            {
                if (value is string && 
                    value.ToString().Length > columnSizes[columnName] &&
                    columnSizes[columnName] > 0)
                {
                    value = value.ToString().Substring(0, columnSizes[columnName]);
                }
            }

            if (value is DateTime && this.DatabaseAgent.DbType != DaoDbType.MSSql)
            {
                value = JulianDate.ToJulianDate((DateTime)value);
            }

            // set the new value
            if (!newValues.ContainsKey(columnName))
            {
                OnBeforeValueChanged(columnName, value);
                newValues.Add(columnName, value);
                changesPending = true;
                OnValueChanged(columnName, value);
            }
            else
            {
                OnBeforeValueChanged(columnName, value);
                newValues[columnName] = value;
                changesPending = true;
                OnValueChanged(columnName, value);
            }
        }

        internal bool ChangesPending
        {
            get
            {
                return changesPending;
            }
            set
            {
                changesPending = value;
            }
        }

        public bool HasId { get { return !string.IsNullOrEmpty(idColumn); } }
        public bool HasPrimaryKey { get { return primaryKeyColumns.Count > 0; } }



        /// <summary>
        /// Calls Update() after checking to see if the record has been altered.
        /// If the record has been altered (the state of the database doesn't represent
        /// the state of the current instance) no updates are made.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public UpdateResult TransactionUpdate<T>() where T : DaoObject, new()
        {
            try
            {
                bool recordIsAltered = RecordIsAltered<T>();

                if (recordIsAltered)
                    return UpdateResult.RecordAltered;

                return Update();
            }
            catch (Exception ex)
            {
                LastException = ex;
                return UpdateResult.Error;
            }
        }

        /// <summary>
        /// When implemented by a derived class should update the current 
        /// instance using the primary key to isolate a single record.
        /// </summary>
        /// <returns></returns>
        public abstract UpdateResult UpdateByPrimaryKey();

        /// <summary>
        /// When implemented by a derived class should update the current
        /// instance using the initial property values to isolate a single
        /// record.
        /// </summary>
        /// <returns></returns>
        public abstract UpdateResult UpdateByValues();

        public UpdateResult UpdateByValues<T>() where T : DaoObject, new()
        {
            List<DbSelectParameter> parameters = GetInitialValueSelectParameters();

            return Update<T>(parameters.ToArray());
        }

        private List<DbSelectParameter> GetInitialValueSelectParameters()
        {
            List<DbSelectParameter> parameters = new List<DbSelectParameter>();
            foreach (string column in propertyToColumnMap.Values)
            {
                DbSelectParameter p = new DbSelectParameter(column, this.GetInitialValue(column));
                p.ParameterName = column + "Current";
                parameters.Add(p);
            }
            return parameters;
        }

        public UpdateResult UpdateByPrimaryKey<T>() where T: DaoObject, new()
        {
            List<DbSelectParameter> parameters = new List<DbSelectParameter>();
            foreach (string primaryKeyColumn in primaryKeyColumns)
            {
                DbSelectParameter param = new DbSelectParameter(primaryKeyColumn, GetInitialValue(primaryKeyColumn));
                param.ParameterName = primaryKeyColumn + "Current";
                parameters.Add(param);
            }

            return Update<T>(parameters.ToArray());
        }

        public UpdateResult Update<T>(params DbSelectParameter[] filters) where T: DaoObject, new()
        {
            if (changesPending)
            {
                DatabaseAgent agent = this.DatabaseAgent;//DaoContext.Get(new T().DataContextName).DatabaseAgent;
                DbSelectParameter[] copy = new DbSelectParameter[filters.Length];
                filters.CopyTo(copy, 0);
                T[] objects = SelectListWhere<T>(copy);
                if (objects.Length == 1)
                {
                    StringBuilder update = new StringBuilder();
                    update.AppendFormat("UPDATE {0} SET ", this.tableName);
                    List<DbParameter> parameters = new List<DbParameter>();
                    int i = 0;
                    foreach (string columnName in newValues.Keys)
                    {
                        update.AppendFormat("{0} = @{0}", columnName);
                        parameters.Add(MakeParam(columnName, newValues[columnName], columnSizes[columnName]));
                        if (i != newValues.Keys.Count - 1)
                            update.Append(", ");

                        i++;
                    }

                    update.Append(" WHERE ");
                    for (int ii = 0; ii < filters.Length; ii++)
                    {
                        update.AppendFormat(filters[ii].ToString());
                        parameters.Add(filters[ii].CreateParameter(agent));

                        if (ii != filters.Length - 1)
                        {
                            update.Append(" AND ");
                        }
                    }

                    OnBeforeChangesCommitted();
                    agent.ExecuteSql(update.ToString(), parameters.ToArray());
                    changesPending = false;
                    CopyNewValuesAsInitial();
                    newValues.Clear();
                    OnChangesCommitted();
                    return UpdateResult.Success;
                }
                else
                {
                    this.LastException = new Exception("Unable to isolate single instance using specified filters");
                    return UpdateResult.Error;
                }
            }

            return UpdateResult.NoChangesToCommit;
        }

        private void CopyNewValuesAsInitial()
        {
            foreach (string key in newValues.Keys)
            {
                if (!initialValues.ContainsKey(key))
                    initialValues.Add(key, newValues[key]);
                else
                    initialValues[key] = newValues[key];
            }
        }

        /// <summary>
        /// Commit any changes or throw the exception that caused
        /// the commit to fail.
        /// </summary>
        public void CommitOrDie()
        {
            if (Commit().Exception != null)
            {
                throw this.LastException;
            }
        }

        public DaoObjectCommitResult Commit()
        {
            DaoObjectCommitResult result = new DaoObjectCommitResult();
            if (this.IsNew)
            {
                DaoObjectInsertResult insertResult = this.Insert();
                result.Exception = insertResult.Exception;
                result.Id = insertResult.Id;
            }
            else
            {
                UpdateResult updateResult = this.Update();
                if (updateResult == UpdateResult.Error)
                {
                    result.Exception = this.LastException;
                }
                result.UpdateResult = updateResult;
                if (string.IsNullOrEmpty(this.idColumn))
                {
                    result.Id = 0;
                }
                else
                {
                    result.Id = this.GetLongValue(this.idColumn);
                }
            }

            return result;
        }

        public UpdateResult Update()
        {
            if (newValues.Count == 0)
            {
                OnChangesCommitted();
                return UpdateResult.Success;
            }

            if (!string.IsNullOrEmpty(idColumn))
            {
                return UpdateById();
            }

            if (this.primaryKeyColumns.Count > 0)
            {
                return this.UpdateByPrimaryKey();
            }

            return UpdateByValues();
        }

        public UpdateResult UpdateById()
        {
            if (changesPending)
            {
                if (this.GetValue(idColumn) == null)
                {
                    LastException = new Exception("The ID of the current instance is null use Insert() if you would like to insert a new record.");
                    return UpdateResult.Error;//throw new InvalidOperationException("The ID of the current instance is null use Insert() if you would like to insert a new record.");
                }

                try
                {
                    StringBuilder update = new StringBuilder();
                    update.AppendFormat("UPDATE [{0}] SET ", this.tableName);
                    
                    List<DbParameter> parameters = new List<DbParameter>();
                    int i = 0;
                    foreach (string columnName in newValues.Keys)
                    {
                        update.AppendFormat("{0} = @{0}", columnName);
                        parameters.Add(MakeParam(columnName, newValues[columnName], columnSizes[columnName]));
                        if (i != newValues.Keys.Count - 1)
                            update.Append(", ");

                        i++;
                    }
                    update.AppendFormat(" WHERE {0} = @id", idColumn);
                    parameters.Add(MakeParam("@id", this.GetValue(idColumn), columnSizes[idColumn]));//new SqlParameter("@id", this.GetValue(idColumn)));
                    string sql = update.ToString();
                    
                    //DatabaseUtility db = DaoContext.Get(this.dataContextName).DatabaseUtility;
                    DatabaseAgent db = this.DatabaseAgent;//DaoContext.Get(this.dataContextName).DatabaseAgent;
                    OnBeforeChangesCommitted();
                    db.ExecuteSql(sql, parameters.ToArray());
                    changesPending = false;
                    CopyNewValuesAsInitial();
                    newValues.Clear();
                    OnChangesCommitted();
                    return UpdateResult.Success;
                }
                catch (Exception ex)
                {
                    LastException = ex;
                    return UpdateResult.Error;
                }
            }

            return UpdateResult.NoChangesToCommit;
        }

        /// <summary>
        /// This should be used if errors occur during an Update() attempt.
        /// The LastException property should be checked prior to and after 
        /// calling RollBack to determine whether the state of the last error
        /// has changed.
        /// </summary>
        /// <returns>True on success false otherwise</returns>
        public bool RollBack()
        {
            newValues.Clear();
            foreach (string key in initialValues.Keys)
            {
                if (!key.Equals(idColumn))
                    newValues.Add(key, initialValues[key]);
            }
            changesPending = true;
            UpdateResult result = Update();
            if (result == UpdateResult.Success)
                return true;
            else
                return false;
        }

        protected bool RecordIsAltered<T>() where T : DaoObject, new()
        {
            T check = SelectById<T>((int)GetValue(this.idColumn), this.DatabaseAgent);
            foreach (string columnName in check.initialValues.Keys)
            {
                if (this.initialValues[columnName] != null &&
                    !this.initialValues[columnName].Equals(check.initialValues[columnName]))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Trys to insert a new record with the values of the current instance.
        /// If the insert fails the return value will be -1 you should
        /// check the LastException property for details about the exception that occurred.
        /// The same as TryInsert().
        /// </summary>
        /// <returns>The new id or -1 if the insert fails.</returns>
        public DaoObjectInsertResult Insert()
        {
            return TryInsert();
        }

        /// <summary>
        /// Trys to insert a new record with the values of the current instance.
        /// If the insert fails the return value will be -1 you should
        /// check the LastException property for details about the exception that occurred.
        /// The same as Insert().
        /// </summary>
        /// <returns>The new id or -1 if the insert fails.</returns>
        public DaoObjectInsertResult TryInsert()
        {
            try
            {
                if (!IsNew)
                {
                    throw new InvalidOperationException(string.Format("Use {0}.New() rather than the parameterless constructor to create new entries", this.GetType().Name));
                }

                if (newValues.Count == 0 && this.GetLongValue(idColumn) == -1)
                {
                    throw new InvalidOperationException("No properties have been set.");
                }

                this.OnBeforeInsert();

                DatabaseAgent agent = this.DatabaseAgent;//DaoContext.Get(this.dataContextName).DatabaseAgent;
                List<string> columns = new List<string>();
                List<DbParameter> parameters = new List<DbParameter>();
                foreach (string columnName in newValues.Keys)
                {
                    columns.Add("\"" + columnName + "\"");
                    object newValue = newValues[columnName];
                 
                    DbParameter param = agent.CreateParameter("@" + columnName, newValue, columnSizes[columnName]);
                    parameters.Add(param);
                }

                int id = agent.Insert(this.tableName, columns.ToArray(), this.idColumn, parameters.ToArray()); 

                if (id != -1 && !string.IsNullOrEmpty(this.idColumn) )
                {
                    this.IsNew = false;
                    this.initialValues[idColumn] = id;
                }
                else if (string.IsNullOrEmpty(this.idColumn))
                {
                    this.initialValues[idColumn] = 0;
                }
                else
                {
                    throw new InvalidOperationException("Unexpected result from insert attempt");
                }

                OnAfterInsert();
            }
            catch (Exception ex)
            {
                LastException = ex;
                return new DaoObjectInsertResult(-1, ex);//-1;
            }

            return new DaoObjectInsertResult((int)this.initialValues[idColumn]);
        }

        
        private void AppendColumn(StringBuilder columns, int i, string columnName)
        {
            columns.Append(columnName);
            if (i != newValues.Keys.Count - 1)
                columns.Append(", ");
        }

        private void AppendValue(StringBuilder values, int i, string columnName)
        {
            values.Append("@" + columnName);
            if (i != newValues.Keys.Count - 1)
                values.Append(", ");
        }

        private void AppendParam(List<DbParameter> parameters, string columnName, object value)
        {
            //SqlParameter param = new SqlParameter("@" + columnName, value);
            //param.Size = columnSizes[columnName];
            parameters.Add(MakeParam(columnName, value, columnSizes[columnName]));
        }
        /// <summary>
        /// Used specifically for updates and inserts to limit the 
        /// size of the input parameters.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        private DbParameter MakeParam(string name, object value, int size)
        {
            return this.DatabaseAgent.CreateParameter(name, value, size);            
        }

        internal static List<string> ReservedProperties
        {
            get
            {
                return new List<string>(new string[] { "AllowDelete", "AllowModifyPrimaryKeyValue", "DatabaseAgent", "DataContextName", "HasId", "HasPrimaryKey", "IdColumnName", "IsNew", "LastException", "TableName" });
            }
        }

        [XmlIgnore]
        [DaoIgnore]
        [JsonIgnore]
        public Exception LastException
        {
            get;
            protected set;
        }

        public Dictionary<string, string> GetPropertyToColumnMap()
        {
            if (propertyToColumnMap.Count > 0)
                return propertyToColumnMap;
            else
                return GetDefaultPropertyToColumnMap();
        }

        private Dictionary<string, string> GetDefaultPropertyToColumnMap()
        {
            Dictionary<string, string> retVal = new Dictionary<string, string>();
            PropertyInfo[] columnProperties = CustomAttributeExtension.GetPropertiesWithAttributeOfType<DaoColumn>(this.GetType());
            foreach (PropertyInfo property in columnProperties)
            {
                DaoColumn propAttr = (DaoColumn)(property.GetCustomAttributes(typeof(DaoColumn), true)[0]);
                retVal.Add(property.Name, propAttr.ColumnName);
            }

            return retVal;
        }

        public static Dictionary<string, string> GetPropertyToColumnMap<T>() where T : DaoObject, new()
        {
            return DaoContext.GetPropertyToColumnMap<T>();
        }

        private static void ThrowIfNull(object argument, string argumentName)
        {
            if( argument == null )
                throw new ArgumentNullException(argumentName);

            if (argument.GetType() == typeof(string) &&
                string.IsNullOrEmpty((string)argument))
                throw new ArgumentNullException(argumentName);                
        }

        private static void ThrowIfNotEqual(string argOne, string argTwo, string messageIfNotEqual)
        {
            if (!argOne.Equals(argTwo))
                throw new InvalidOperationException(messageIfNotEqual);
        }
    }
}
