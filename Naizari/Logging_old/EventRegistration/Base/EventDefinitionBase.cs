/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Data.Common;
using Naizari.Data;
namespace Naizari.Logging.EventRegistration
{
	[DaoTable("EventDefinition")]
	public class EventDefinitionBase: DaoObject
	{
		public EventDefinitionBase(): base()
		{
			this.idColumn = IdColumn;
			this.tableName = TableName;
			this.dataContextName = ContextName;
			propertyToColumnMap.Add("EventId","EventId");
			columnSizes.Add("EventId", 4);
			propertyToColumnMap.Add("ApplicationName","ApplicationName");
			columnSizes.Add("ApplicationName", 4000);
			propertyToColumnMap.Add("MessageSignature","MessageSignature");
			columnSizes.Add("MessageSignature", 4000);
			primaryKeyColumns.Add("EventId");
		}

		public const string IdColumn = "EventId";
		public const string TableName = "EventDefinition";
		public const string ContextName = "Logging";


		[DaoPrimaryKeyColumn("EventId", 4)]
		[DaoIdColumn("EventId", 4)]
		public int EventId
		{
			get
			{
				return GetIntValue("EventId");
			}

			set
			{
				SetValue("EventId", value);
			}

		}

		[DaoColumn("ApplicationName", 4000, true)]
		public string ApplicationName
		{
			get
			{
				return (string)GetValue("ApplicationName");
			}

			set
			{
				SetValue("ApplicationName", value);
			}

		}

		[DaoColumn("MessageSignature", 4000, true)]
		public string MessageSignature
		{
			get
			{
				return (string)GetValue("MessageSignature");
			}

			set
			{
				SetValue("MessageSignature", value);
			}

		}

		public static T[] SelectPropertyList<T>(EventDefinitionFields field, DaoSearchFilter filter)
		{
			return EventDefinition.SelectPropertyList<EventDefinition, T>(field, filter);
		}

		public static T[] SelectPropertyList<T>(EventDefinitionFields field, DaoSearchFilter filter, DatabaseAgent agent)
		{
			return EventDefinition.SelectPropertyList<EventDefinition, T>(field, filter, agent);
		}

		public static EventDefinition[] SelectByPropertyList(EventDefinitionFields field, params object[] propertyValues)
		{
			return EventDefinition.SelectByPropertyList<EventDefinition>(field, propertyValues);
		}

		public static EventDefinition[] SelectByPropertyList(EventDefinitionFields field, DatabaseAgent agent, params object[] propertyValues)
		{
			return EventDefinition.SelectByPropertyList<EventDefinition>(field, agent, propertyValues);
		}

		public static EventDefinition SelectById(int id)
		{
			return EventDefinition.SelectById<EventDefinition>(id);
		}

		public static EventDefinition SelectById(int id, DatabaseAgent agent)
		{
			return EventDefinition.SelectById<EventDefinition>(id, agent);
		}

		public static EventDefinition SelectById(long id)
		{
			return EventDefinition.SelectById<EventDefinition>(id);
		}

		public static EventDefinition SelectById(long id, DatabaseAgent agent)
		{
			return EventDefinition.SelectById<EventDefinition>(id, agent);
		}

		public static EventDefinition[] SelectByIdList(int[] ids)
		{
			return EventDefinition.SelectByIdList<EventDefinition>(ids);
		}

		public static EventDefinition[] SelectByIdList(int[] ids, DatabaseAgent agent)
		{
			return EventDefinition.SelectByIdList<EventDefinition>(ids, agent);
		}

		public static EventDefinition[] SelectListWhere(EventDefinitionFields fieldName, object value)
		{
			return EventDefinition.SelectListWhere<EventDefinition>(fieldName.ToString(), value);
		}

		public static EventDefinition[] SelectListWhere(EventDefinitionFields fieldName, object value, DatabaseAgent agent)
		{
			return EventDefinition.SelectListWhere<EventDefinition>(fieldName.ToString(), value, agent);
		}

		public static EventDefinition[] Search(EventDefinitionSearchFilter filter)
		{
			return EventDefinition.Search<EventDefinition>(filter);
		}

		public static EventDefinition[] Search(EventDefinitionSearchFilter filter, DatabaseAgent agent)
		{
			return EventDefinition.Search<EventDefinition>(filter, agent);
		}

		public static EventDefinition[] Search(EventDefinitionSearchFilter filter, OrderBy orderBy)
		{
			return EventDefinition.Search<EventDefinition>(filter, orderBy);
		}

		public static EventDefinition[] Search(EventDefinitionSearchFilter filter, OrderBy orderBy, DatabaseAgent agent)
		{
			return EventDefinition.Search<EventDefinition>(filter, orderBy, agent);
		}

		public static EventDefinition[] Search(EventDefinitionSearchFilter filter, OrderBy orderBy, int count)
		{
			return EventDefinition.Search<EventDefinition>(filter, orderBy, count);
		}

		public static EventDefinition[] Search(EventDefinitionSearchFilter filter, OrderBy orderBy, int count, DatabaseAgent agent)
		{
			return EventDefinition.Search<EventDefinition>(filter, orderBy, count, agent);
		}

		public static EventDefinition[] Select(EventDefinitionSearchFilter filter)
		{
			return EventDefinition.Select<EventDefinition>(filter);
		}

		public static EventDefinition[] Select(EventDefinitionSearchFilter filter, DatabaseAgent agent)
		{
			return EventDefinition.Select<EventDefinition>(filter, agent);
		}

		public static EventDefinition SelectOneWhere(EventDefinitionSearchFilter filter)
		{
			return EventDefinition.SelectOneWhere<EventDefinition>(filter);
		}

		public static EventDefinition SelectOneWhere(EventDefinitionSearchFilter filter, DatabaseAgent agent)
		{
			return EventDefinition.SelectOneWhere<EventDefinition>(filter, agent);
		}

		public static EventDefinition[] SelectListWhere(EventDefinitionSearchFilter filter)
		{
			return EventDefinition.Select<EventDefinition>(filter);
		}

		public static EventDefinition[] SelectListWhere(EventDefinitionSearchFilter filter, DatabaseAgent agent)
		{
			return EventDefinition.Select<EventDefinition>(filter, agent);
		}

		public static EventDefinition[] SelectTop(EventDefinitionSearchFilter filter, int count)
		{
			return EventDefinition.Select<EventDefinition>(filter, count);
		}

		public static EventDefinition[] SelectTop(EventDefinitionSearchFilter filter, int count, DatabaseAgent agent)
		{
			return EventDefinition.Select<EventDefinition>(filter, count, agent);
		}

		public static EventDefinition[] Select()
		{
			return EventDefinition.Select<EventDefinition>();
		}

		public static EventDefinition[] Select(DatabaseAgent agent)
		{
			return EventDefinition.Select<EventDefinition>(agent);
		}

		public static EventDefinition SelectOneWhere(DaoSearchFilter filter)
		{
			return EventDefinition.SelectOneWhere<EventDefinition>(filter);
		}

		public static EventDefinition SelectOneWhere(DaoSearchFilter filter, DatabaseAgent agent)
		{
			return EventDefinition.SelectOneWhere<EventDefinition>(filter, agent);
		}

		public static EventDefinition SelectTop(OrderBy orderBy, DatabaseAgent agent)
		{
			return EventDefinition.SelectTop<EventDefinition>(orderBy, agent);
		}

		public static EventDefinition[] SelectTop(OrderBy orderBy, int count)
		{
			return EventDefinition.SelectTop<EventDefinition>(orderBy, count);
		}

		public static EventDefinition[] SelectTop(OrderBy orderBy, int count, DatabaseAgent agent)
		{
			return EventDefinition.SelectTop<EventDefinition>(orderBy, count, agent);
		}

		public static EventDefinition New()
		{
			return EventDefinition.New<EventDefinition>();
		}

		public static EventDefinition New(DatabaseAgent agent)
		{
			return EventDefinition.New<EventDefinition>(agent);
		}

		public UpdateResult Update(bool useTransaction)
		{
			return useTransaction ? this.TransactionUpdate<EventDefinition>(): this.Update();
		}

		public override UpdateResult UpdateByPrimaryKey()
		{
			return this.UpdateByPrimaryKey<EventDefinition>();
		}

		public override UpdateResult UpdateByValues()
		{
			return this.UpdateByValues<EventDefinition>();
		}

		public static EventDefinition[] SelectAll()
		{
			return EventDefinition.SelectAll<EventDefinition>();
		}

		public bool RecordIsAltered()
		{
			return this.RecordIsAltered<EventDefinition>();
		}

	}
}

