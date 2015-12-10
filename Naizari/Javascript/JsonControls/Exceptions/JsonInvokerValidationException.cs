/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Naizari.Javascript.JsonControls
{
    public class JsonInvokerValidationException: JsonException
    {
        internal JsonInvokerValidationException(JsonInvoker invoker, JsonInvokerValidationResult result, Type handlerType, MethodInfo method, int parameterSourceCount)
            : base(JsonInvokerValidationMessages.GetMessage(invoker, result, handlerType, method, parameterSourceCount))
        {
        }

        internal JsonInvokerValidationException(JsonInvoker invoker, JsonInvokerValidationResult result, Type handlerType, MethodInfo method, int parameterSourceCount, string methodName)
            :base(JsonInvokerValidationMessages.GetMessage(invoker, result, handlerType, method, parameterSourceCount, methodName))
        {

        }
    }
}
