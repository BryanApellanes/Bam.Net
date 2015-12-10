/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Naizari.Helpers;
using Naizari.Roles;

namespace Naizari.Javascript.JsonControls.Exceptions
{
    public class JsonNotAuthorizedException: JsonException
    {
        public JsonNotAuthorizedException()
            : this("UNKOWN")
        {
        }

        public JsonNotAuthorizedException(string action)
            : base(string.Format("User '{0}' attempted an unauthorized action '{1}'.", UserUtil.GetCurrentUser(true)))
        { }
    }
}
