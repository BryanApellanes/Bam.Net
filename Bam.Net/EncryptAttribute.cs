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
    /// <summary>
    /// Denotes a class that requires clients use
    /// encrypted channels for method invocation calls
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class EncryptAttribute: Attribute
    {
    }
}
