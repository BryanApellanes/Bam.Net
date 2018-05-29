/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.ServiceProxy.Secure
{
    /// <summary>
    /// Attribute used to addorn classes or methods that require
    /// authentication or authorization.  Implicity requires
    /// application level encryption.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ApiKeyRequiredAttribute: EncryptAttribute
    {
    }
}
