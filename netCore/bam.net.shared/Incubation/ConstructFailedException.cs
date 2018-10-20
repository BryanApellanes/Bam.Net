/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bam.Net.Incubation
{
    [Serializable]
    public class ConstructFailedException: Exception
    {
		public ConstructFailedException(Type type, Type[] ctorTypes)
			: base(string.Format("{0}({1}):: The constructor wasn't found for the specified type and parameter combination",
					type.Name, ctorTypes == null || ctorTypes.Length == 0 ? "" : ctorTypes.ToDelimited<Type>(t => t.Name)))
		{ }
    }
}
