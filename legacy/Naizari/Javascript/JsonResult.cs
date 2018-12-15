/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;

namespace Naizari.Javascript
{
    public class JsonResult
    {
        public JsonResultStatus Status { get; set; }
        public object Data { get; set; }

        public string ErrorMessage { get; set; }
        public string StackTrace { get; set; }

        public string ToJSON()
        {
            //JsonSerializer serializer = new JsonSerializer(typeof(JsonResult));
            return JsonSerializer.ToJson(this);
        }

        public string[] Input { get; set; }
    }
}
