/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net
{
    [Serializable]
	public class CompilationException: Exception
	{
        public CompilationException(SerializationInfo info, StreamingContext context) { }
		public CompilationException(CompilerResults compilerResults)
			: base(AdHocCSharpCompiler.GetMessage(compilerResults))
		{
			this.Results = compilerResults;
		}

		public CompilerResults Results { get; set; }

		
	}
}
