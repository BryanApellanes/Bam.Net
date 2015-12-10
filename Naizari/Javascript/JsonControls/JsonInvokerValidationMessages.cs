/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Web.UI.WebControls;
using System.Web.UI;

[assembly: TagPrefix("Naizari.Javascript.JsonControls", "json")]
namespace Naizari.Javascript.JsonControls
{
    internal struct JsonInvokerValidationMessages
    {
        static Dictionary<JsonInvokerValidationResult, string> messages;
        static JsonInvokerValidationMessages()
        {
            messages = new Dictionary<JsonInvokerValidationResult, string>();
            messages.Add(JsonInvokerValidationResult.MethodNotSpecified,
                @"{0}: The invoker [JsonId: {1}, asp ID: {2}] specified no MethodName.  The MethodName property of the invoker should be set to the name of a method that has the JsonMethod attribute in the parent JavascriptPage or UserControl.");

            messages.Add(JsonInvokerValidationResult.MethodNotFound,
                 @"{0}.{1}: The method specified by invoker [JsonId: {2}, asp ID: {3}] was not found ");

            messages.Add(JsonInvokerValidationResult.ParameterCountMismatch,
                @"{0}.{1}: The method specified by invoker [JsonId: {2}, asp ID: {3}] takes {4} parameter(s) and {5} were added to the invoker");

            messages.Add(JsonInvokerValidationResult.CallbackNotSpecified,
                @"{0}: The invoker [JsonId: {1} asp ID: {2}] specified no CallbackJsonId and the Callback property was null");

            messages.Add(JsonInvokerValidationResult.Invalid,
                @"{0}.{1}: Invalid result, something is seriously wrong since this should never happen :(");
        }

        internal static string GetMessage(JsonInvoker invoker, JsonInvokerValidationResult result, Type handlerType, MethodInfo method, int parameterSourceCount)
        {
            string methodName = "UNAVAILABLE";
            methodName = method == null ? methodName: method.Name;
            return GetMessage(invoker, result, handlerType, method, parameterSourceCount, methodName);
        }

        internal static string GetMessage(JsonInvoker invoker, JsonInvokerValidationResult result, Type handlerType, MethodInfo method, int parameterSourceCount, string methodName)
        {
            if (result == JsonInvokerValidationResult.MethodNotFound)
                return string.Format(messages[result], handlerType.Name, methodName, invoker.JsonId, invoker.ID);

            if (result == JsonInvokerValidationResult.ParameterCountMismatch)
                return string.Format(messages[result], handlerType.Name, method.Name, invoker.JsonId, invoker.ID, method.GetParameters().Length, parameterSourceCount);

            if (result == JsonInvokerValidationResult.CallbackNotSpecified)
                return string.Format(messages[result], handlerType.Name, invoker.JsonId, invoker.ID);

            if (result == JsonInvokerValidationResult.MethodNotSpecified)
                return string.Format(messages[result], handlerType.Name, invoker.JsonId, invoker.ID);

            return string.Format(messages[JsonInvokerValidationResult.Invalid], handlerType.Name, method.Name);
            
        }
    }
}
