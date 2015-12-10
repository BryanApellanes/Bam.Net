/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;

namespace Naizari.Javascript
{
    [AttributeUsage(AttributeTargets.Method)]
    public class JsonMethod: Attribute
    {
        Verbs verb;
        public JsonMethod() { }
        public JsonMethod(Verbs verb)
        {
            this.verb = verb;
        }

        public Verbs Verb
        {
            get { return verb; }
        }

        public bool MultiInvoke { get; set; }
    }
}
