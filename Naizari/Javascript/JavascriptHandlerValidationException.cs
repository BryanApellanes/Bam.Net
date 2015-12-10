/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Naizari.Javascript
{
    public class JsonProviderValidationException: Exception
    {
        public JsonProviderValidationException(JsonProviderValidationResult result, MethodInfo method)
            : base(JavascriptHandlerValidationMessages.GetMessage(result, method))
        {}
    }
}
