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
	public class DaoGenerationException: Exception
	{
		public DaoGenerationException(string schemaName, Type[] types, CompilationException compilationEx) : base(GetMessage(schemaName, types, compilationEx)) { }

		private static string GetMessage(string schemaName, Type[] types, CompilationException compilationEx)
		{
			StringBuilder builder = new StringBuilder();
			builder.AppendFormat("Unable to Generate Dao Assembly for {0}.\r\nSpecified Types:\r\n", schemaName);
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
