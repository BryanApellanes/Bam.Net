/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Naizari.Javascript.JsonControls
{
    public class JsonFileNotFoundException: JsonException
    {
        public JsonFileNotFoundException(string filePath)
            : base(string.Format("The file '{0}' could not be found.", filePath))
        { }
       
    }
}
