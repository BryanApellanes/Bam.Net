/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Naizari.Javascript.JsonControls
{
    public class InvalidJsonResultSpecifiedException: JsonException
    {
        public InvalidJsonResultSpecifiedException()
            : base("The JsonResult specified is not in an error state.")
        {
        }
    }
}
