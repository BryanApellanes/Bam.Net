/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Bam.Net;

namespace Bam.Net
{
	public static class GeneratedAssemblies
	{
		static Dictionary<string, GeneratedAssemblyInfo> _generatedAssemblies = new Dictionary<string, GeneratedAssemblyInfo>();
		public static GeneratedAssemblyInfo GetGeneratedAssemblyInfo(string name)
		{
			if (_generatedAssemblies.ContainsKey(name))
			{
				return _generatedAssemblies[name];
			}

			return null;
		}

		public static void SetAssemblyInfo(string name, GeneratedAssemblyInfo assembly)
		{
			_generatedAssemblies[name] = assembly;
		}
	}
}
