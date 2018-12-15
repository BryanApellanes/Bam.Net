/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Naizari.Extensions;

namespace Naizari.Javascript.BoxControls
{
    public class InvalidBoxRequestException: BoxException
    {
        public InvalidBoxRequestException(HttpRequest request)
            : base("The box request query string was invalid: " + request.Url.OriginalString)
        { }
    }
}
