/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Data.Common;
using Naizari.Data;

namespace Naizari.Logging.EventRegistration
{
	public class EventDefinitionSearchFilter : DaoSearchFilter
	{
		public void AddParameter(EventDefinitionFields fieldEnum, object value)
		{
			base.AddParameter(fieldEnum, value);
		}

		public void AppendFilter(EventDefinitionFields fieldEnum, object value, Comparison whereOperator, WhereAppender appender)
		{
			base.AppendFilter(fieldEnum, value, whereOperator, appender);
		}


	}
}
