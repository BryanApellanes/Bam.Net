/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Naizari.Javascript.JsonControls
{
    public class ParameterSourceNotFoundException: JsonException
    {
        public ParameterSourceNotFoundException(string jsonId)
            : base(string.Format("The ParameterSource control with jsonid '{0}' was not found.", jsonId))
        { }
    }
}
