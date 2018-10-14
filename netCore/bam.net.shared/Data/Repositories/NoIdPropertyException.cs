/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data.Repositories
{
	[Serializable]
	public class NoIdPropertyException: Exception
	{
		public NoIdPropertyException(Type type)
			: base(string.Format("The specified type {0} doesn't have an Id property and no property addorned with the Key attribute", type.FullName))
		{ }
		public NoIdPropertyException(IEnumerable<string> classNames)
			: base("The specified types don't have Id properties: \r\n\t{0}"._Format(classNames.ToArray().ToDelimited(s => s, "\r\n\t")))
		{ }
	}
}
