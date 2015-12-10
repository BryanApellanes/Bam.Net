/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Data.Common;
using Naizari.Data;
namespace Naizari.Logging
{
	[DaoTable("LogEventData")]
	public class LogEventDataBase: DaoObject
	{
		public LogEventDataBase(): base()
		{
			this.idColumn = IdColumn;
			this.tableName = TableName;
			this.dataContextName = ContextName;
			propertyToColumnMap.Add("ID","ID");
			columnSizes.Add("ID", 8);
			propertyToColumnMap.Add("Computer","Computer");
			columnSizes.Add("Computer", 255);
			propertyToColumnMap.Add("User","User");
			columnSizes.Add("User", 255);
			propertyToColumnMap.Add("Message","Message");
			columnSizes.Add("Message", 4000);
			propertyToColumnMap.Add("EventID","EventID");
			columnSizes.Add("EventID", 4);
			propertyToColumnMap.Add("Category","Category");
			columnSizes.Add("Category", 255);
			propertyToColumnMap.Add("TimeOccurred","TimeOccurred");
			columnSizes.Add("TimeOccurred", 8);
			propertyToColumnMap.Add("Severity","Severity");
			columnSizes.Add("Severity", 50);
			propertyToColumnMap.Add("Source","Source");
			columnSizes.Add("Source", 255);
			primaryKeyColumns.Add("ID");
		}

		public const string IdColumn = "ID";
		public const string TableName = "LogEventData";
		public const string ContextName = "Logging";


		[DaoPrimaryKeyColumn("ID", 8)]
		[DaoIdColumn("ID", 8)]
		public long ID
		{
			get
			{
				return GetLongValue("ID");
			}

			set
			{
				SetValue("ID", value);
			}

		}

		[DaoColumn("Computer", 255, true)]
		public string Computer
		{
			get
			{
				return (string)GetValue("Computer");
			}

			set
			{
				SetValue("Computer", value);
			}

		}

		[DaoColumn("User", 255, false)]
		public string User
		{
			get
			{
				return (string)GetValue("User");
			}

			set
			{
				SetValue("User", value);
			}

		}

		[DaoColumn("Message", 4000, false)]
		public string Message
		{
			get
			{
				return (string)GetValue("Message");
			}

			set
			{
				SetValue("Message", value);
			}

		}

		[DaoColumn("EventID", 4, false)]
		public int EventID
		{
			get
			{
				return GetIntValue("EventID");
			}

			set
			{
				SetValue("EventID", value);
			}

		}

		[DaoColumn("Category", 255, true)]
		public string Category
		{
			get
			{
				return (string)GetValue("Category");
			}

			set
			{
				SetValue("Category", value);
			}

		}

		[DaoColumn("TimeOccurred", 8, false)]
		public DateTime TimeOccurred
		{
			get
			{
				return GetDateTimeValue("TimeOccurred");
			}

			set
			{
				SetValue("TimeOccurred", value);
			}

		}

		[DaoColumn("Severity", 50, false)]
		public string Severity
		{
			get
			{
				return (string)GetValue("Severity");
			}

			set
			{
				SetValue("Severity", value);
			}

		}

		[DaoColumn("Source", 255, true)]
		public string Source
		{
			get
			{
				return (string)GetValue("Source");
			}

			set
			{
				SetValue("Source", value);
			}

		}

		public static T[] SelectPropertyList<T>(LogEventDataFields field, DaoSearchFilter filter)
		{
			return LogEventData.SelectPropertyList<LogEventData, T>(field, filter);
		}

		public static T[] SelectPropertyList<T>(LogEventDataFields field, DaoSearchFilter filter, DatabaseAgent agent)
		{
			return LogEventData.SelectPropertyList<LogEventData, T>(field, filter, agent);
		}

		public static LogEventData[] SelectByPropertyList(LogEventDataFields field, params object[] propertyValues)
		{
			return LogEventData.SelectByPropertyList<LogEventData>(field, propertyValues);
		}

		public static LogEventData[] SelectByPropertyList(LogEventDataFields field, DatabaseAgent agent, params object[] propertyValues)
		{
			return LogEventData.SelectByPropertyList<LogEventData>(field, agent, propertyValues);
		}

		public static LogEventData SelectById(int id)
		{
			return LogEventData.SelectById<LogEventData>(id);
		}

		public static LogEventData SelectById(int id, DatabaseAgent agent)
		{
			return LogEventData.SelectById<LogEventData>(id, agent);
		}

		public static LogEventData SelectById(long id)
		{
			return LogEventData.SelectById<LogEventData>(id);
		}

		public static LogEventData SelectById(long id, DatabaseAgent agent)
		{
			return LogEventData.SelectById<LogEventData>(id, agent);
		}

		public static LogEventData[] SelectByIdList(int[] ids)
		{
			return LogEventData.SelectByIdList<LogEventData>(ids);
		}

		public static LogEventData[] SelectByIdList(int[] ids, DatabaseAgent agent)
		{
			return LogEventData.SelectByIdList<LogEventData>(ids, agent);
		}

		public static LogEventData[] SelectListWhere(LogEventDataFields fieldName, object value)
		{
			return LogEventData.SelectListWhere<LogEventData>(fieldName.ToString(), value);
		}

		public static LogEventData[] SelectListWhere(LogEventDataFields fieldName, object value, DatabaseAgent agent)
		{
			return LogEventData.SelectListWhere<LogEventData>(fieldName.ToString(), value, agent);
		}

		public static LogEventData[] Search(LogEventDataSearchFilter filter)
		{
			return LogEventData.Search<LogEventData>(filter);
		}

		public static LogEventData[] Search(LogEventDataSearchFilter filter, DatabaseAgent agent)
		{
			return LogEventData.Search<LogEventData>(filter, agent);
		}

		public static LogEventData[] Search(LogEventDataSearchFilter filter, OrderBy orderBy)
		{
			return LogEventData.Search<LogEventData>(filter, orderBy);
		}

		public static LogEventData[] Search(LogEventDataSearchFilter filter, OrderBy orderBy, DatabaseAgent agent)
		{
			return LogEventData.Search<LogEventData>(filter, orderBy, agent);
		}

		public static LogEventData[] Search(LogEventDataSearchFilter filter, OrderBy orderBy, int count)
		{
			return LogEventData.Search<LogEventData>(filter, orderBy, count);
		}

		public static LogEventData[] Search(LogEventDataSearchFilter filter, OrderBy orderBy, int count, DatabaseAgent agent)
		{
			return LogEventData.Search<LogEventData>(filter, orderBy, count, agent);
		}

		public static LogEventData[] Select(LogEventDataSearchFilter filter)
		{
			return LogEventData.Select<LogEventData>(filter);
		}

		public static LogEventData[] Select(LogEventDataSearchFilter filter, DatabaseAgent agent)
		{
			return LogEventData.Select<LogEventData>(filter, agent);
		}

		public static LogEventData SelectOneWhere(LogEventDataSearchFilter filter)
		{
			return LogEventData.SelectOneWhere<LogEventData>(filter);
		}

		public static LogEventData SelectOneWhere(LogEventDataSearchFilter filter, DatabaseAgent agent)
		{
			return LogEventData.SelectOneWhere<LogEventData>(filter, agent);
		}

		public static LogEventData[] SelectListWhere(LogEventDataSearchFilter filter)
		{
			return LogEventData.Select<LogEventData>(filter);
		}

		public static LogEventData[] SelectListWhere(LogEventDataSearchFilter filter, DatabaseAgent agent)
		{
			return LogEventData.Select<LogEventData>(filter, agent);
		}

		public static LogEventData[] SelectTop(LogEventDataSearchFilter filter, int count)
		{
			return LogEventData.Select<LogEventData>(filter, count);
		}

		public static LogEventData[] SelectTop(LogEventDataSearchFilter filter, int count, DatabaseAgent agent)
		{
			return LogEventData.Select<LogEventData>(filter, count, agent);
		}

		public static LogEventData[] Select()
		{
			return LogEventData.Select<LogEventData>();
		}

		public static LogEventData[] Select(DatabaseAgent agent)
		{
			return LogEventData.Select<LogEventData>(agent);
		}

		public static LogEventData SelectOneWhere(DaoSearchFilter filter)
		{
			return LogEventData.SelectOneWhere<LogEventData>(filter);
		}

		public static LogEventData SelectOneWhere(DaoSearchFilter filter, DatabaseAgent agent)
		{
			return LogEventData.SelectOneWhere<LogEventData>(filter, agent);
		}

		public static LogEventData SelectTop(OrderBy orderBy, DatabaseAgent agent)
		{
			return LogEventData.SelectTop<LogEventData>(orderBy, agent);
		}

		public static LogEventData[] SelectTop(OrderBy orderBy, int count)
		{
			return LogEventData.SelectTop<LogEventData>(orderBy, count);
		}

		public static LogEventData[] SelectTop(OrderBy orderBy, int count, DatabaseAgent agent)
		{
			return LogEventData.SelectTop<LogEventData>(orderBy, count, agent);
		}

		public static LogEventData New()
		{
			return LogEventData.New<LogEventData>();
		}

		public static LogEventData New(DatabaseAgent agent)
		{
			return LogEventData.New<LogEventData>(agent);
		}

		public UpdateResult Update(bool useTransaction)
		{
			return useTransaction ? this.TransactionUpdate<LogEventData>(): this.Update();
		}

		public override UpdateResult UpdateByPrimaryKey()
		{
			return this.UpdateByPrimaryKey<LogEventData>();
		}

		public override UpdateResult UpdateByValues()
		{
			return this.UpdateByValues<LogEventData>();
		}

		public static LogEventData[] SelectAll()
		{
			return LogEventData.SelectAll<LogEventData>();
		}

		public bool RecordIsAltered()
		{
			return this.RecordIsAltered<LogEventData>();
		}

	}
}

