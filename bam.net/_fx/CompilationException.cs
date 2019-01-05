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
	public partial class CompilationException: Exception
	{
        public CompilationException(SerializationInfo info, StreamingContext context) { }
	}
}
