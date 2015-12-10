/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Naizari.Helpers;

namespace Naizari.Javascript.JsonControls
{
    public class JavascriptResourceManagerNotFoundException: CustomException
    {
        public JavascriptResourceManagerNotFoundException(string messageFormat, params object[] formatArgs)
            : base(messageFormat, formatArgs)
        { }
    }
}
