/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;

namespace Naizari.Extensions.Office
{
    public class ExcelToDbStaticValue
    {
        public ExcelToDbStaticValue(string asName, object value)
        {
            AsName = asName;
            Value = value;
        }

        public string AsName { get; set; }
        public object Value { get; set; }
    }
}
