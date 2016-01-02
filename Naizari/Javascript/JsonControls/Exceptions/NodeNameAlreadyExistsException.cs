/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Naizari.Javascript.JsonControls.Exceptions
{
    public class  NodeNameAlreadyExistsException: JsonInvalidOperationException
    {
        public NodeNameAlreadyExistsException() : base("An item with the specified name already exists.") { }
        public NodeNameAlreadyExistsException(string message) : base(message) { }
        
    }
}
