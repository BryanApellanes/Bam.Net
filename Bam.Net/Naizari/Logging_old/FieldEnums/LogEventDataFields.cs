/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Data.Common;
using Naizari.Data;

namespace Naizari.Logging
{
	public enum LogEventDataFields
	{
		None,
		ID,
		Computer,
		User,
		Message,
		EventID,
		Category,
		TimeOccurred,
		Severity,
		Source
	}
}
