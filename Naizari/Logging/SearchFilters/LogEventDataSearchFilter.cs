/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Data.Common;
using Naizari.Data;

namespace Naizari.Logging
{
	public class LogEventDataSearchFilter : DaoSearchFilter
	{
		public void AddParameter(LogEventDataFields fieldEnum, object value)
		{
			base.AddParameter(fieldEnum, value);
		}

		public void AppendFilter(LogEventDataFields fieldEnum, object value, Comparison whereOperator, WhereAppender appender)
		{
			base.AppendFilter(fieldEnum, value, whereOperator, appender);
		}


	}
}
