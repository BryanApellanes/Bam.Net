/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Naizari.Javascript.JsonControls
{
    public class InvokerNotSetException: JsonException
    {
        public InvokerNotSetException(JsonCallback callback)
            : base(string.Format(
                @"No invoker was set to use the callback [jsonid: {0}, asp id: {1}]. \r\n-Make sure that " +
            "JsonControls nested in BoxControls have unique JsonIds.  The BoxServer will attempt to handle this in most cases" +
            " but JsonCallbacks are sensitive to this and require user specified, globally unique identifiers.\r\n" +
            "-To add a script to the page use an instance of the JsonFunction control or a standard script tag.", callback.JsonId, callback.ID))
        { }
    }
}
