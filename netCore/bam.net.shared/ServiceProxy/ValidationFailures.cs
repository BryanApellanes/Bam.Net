using System;
using System.Collections.Generic;
using System.Text;

namespace Bam.Net.ServiceProxy
{
    public enum ValidationFailures
    {
        None,
        TokenValidationError,
        UnknownTokenValidationResult,
        TokenHashFailed,
        TokenNonceFailed,
        ClassNameNotSpecified,
        ClassNotRegistered,
        MethodNameNotSpecified,
        MethodNotFound,
        MethodNotProxied,
        ParameterCountMismatch,
        PermissionDenied,
        InvalidApiKeyToken,
        RemoteExecutionNotAllowed,
        /// <summary>
        /// An attribute addorned the class or
        /// method and the specified RequestFilter Type therein
        /// determined the request was not valid
        /// </summary>
        AttributeFilterFailed
    }
}
