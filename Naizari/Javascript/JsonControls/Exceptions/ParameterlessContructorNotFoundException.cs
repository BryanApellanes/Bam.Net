/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Naizari.Javascript.JsonControls
{
    public class ParameterlessContructorNotFoundException: JsonException
    {
        public ParameterlessContructorNotFoundException(string name)
            : base(string.Format("The type [{0}] doesn't have a parameterless constructor and can't be instantiated by the  box server.", name))
        {
        }
    }
}
