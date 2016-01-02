/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net
{
	public class DynamicTypeRecursionLimitReachedException: Exception
	{
		public DynamicTypeRecursionLimitReachedException(int limit)
			: base("The DynamicTypeInfo.RecursionLimit ({0})was reached"._Format(limit))
		{ }
	}
}
