/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

[assembly: TagPrefix("Naizari.Javascript.JsonControls", "json")]
namespace Naizari.Javascript.JsonControls
{
    internal enum JsonInvokerValidationResult
    {
        Invalid,
        MethodNotSpecified,
        MethodNotFound,
        ParameterCountMismatch,
        CallbackNotSpecified,
        Success
    }
}
