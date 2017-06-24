/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data.Repositories
{
    [Serializable]
	public class DaoGenerationException: Exception
	{
        protected DaoGenerationException(SerializationInfo info, StreamingContext context) { }
		public DaoGenerationException(string schemaName, string schemaHash, Type[] types, CompilationException compilationEx) : base(GetMessage(schemaName, schemaHash, types, compilationEx)) { }

		private static string GetMessage(string schemaName, string schemaHash, Type[] types, CompilationException compilationEx)
		{
			StringBuilder builder = new StringBuilder();
			builder.AppendFormat("Unable to Generate Dao Assembly for {0} ({1}).\r\nSpecified Types:\r\n", schemaName, schemaHash);
			foreach(Type type in types)
			{
				builder.AppendFormat("\t{0}\r\n", type.FullName);
			}

			builder.AppendLine();
			builder.AppendFormat("\t{0}", compilationEx.Message);

			return builder.ToString();
		}
	}
}
