/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Naizari.Javascript.JsonControls
{
    public class JsonEmptyDataItem: JsonDataItem
    {
        public JsonEmptyDataItem()
        {
        }

        public JsonEmptyDataItem(string text, string value)
            : base(text, value)
        {
        }
    }
}
