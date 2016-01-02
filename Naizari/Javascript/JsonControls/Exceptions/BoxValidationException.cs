/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Naizari.Javascript.JsonControls
{
    public class BoxValidationException: JsonException
    {
        public BoxValidationException(string boxTemplateName)
            : base(string.Format("The Box ['{0}'] contains another box, this has the potential to cause an infinite loop of asynchronous requests and is not allowed.", boxTemplateName))
        {
        }
    }
}
