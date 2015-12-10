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
    public struct JavascriptHandlerValidationMessages
    {
        static Dictionary<JsonProviderValidationResult, string> messages;
        static JavascriptHandlerValidationMessages()
        {
             messages = new Dictionary<JsonProviderValidationResult, string>();
             messages.Add(JsonProviderValidationResult.NoJsonMethodsFound,
@"{0}:{1} - There were no methods found with the JSONMethod attribute"
             );
             messages.Add(JsonProviderValidationResult.InvalidParameterTypeFound,
 @"{0}:{1} - JSONMethods must take only strings as parameters."
             );
             messages.Add(JsonProviderValidationResult.OverrideFound,
 @"{0}:{1} - JSONMethod names must be unique and cannot be overridden."
 );
             messages.Add(JsonProviderValidationResult.Success,
 @"{0}:{1} - Validation succeeded."
 );
        }

        public static string GetMessage(JsonProviderValidationResult validationResult, MethodInfo method)
        {
            if (validationResult == JsonProviderValidationResult.Invalid)
                throw new InvalidOperationException("Invalid value provided for validationResult");

            string methodName = string.Empty;
            string typeName = string.Empty;
            if (method != null)
            {
                methodName = method.Name;
                typeName = method.ReflectedType.Name;
            }
            return string.Format(messages[validationResult], typeName, methodName);
        }
    }
}
