/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
//using Naizari.Interfaces;
using Naizari.Implementation;

namespace Naizari.Javascript
{
    public class JSONTestObject
    {
        [JsonMethod]
        public string Eat()
        {
            return "Yummy";
        }

        [JSONInitMethod]
        public JSONTestObject returntest()
        {
            return new JSONTestObject();
        }

        [JsonMethod]
        public JSONTestObject returntestwithparams(string stuffVal)
        {
            JSONTestObject r = new JSONTestObject();
            r.Stuff = stuffVal;
            return r;
        }

        string stuff = "stuff";
        public string Stuff
        {
            get { return stuff; }
            set { stuff = value; }
        }
    }
}
