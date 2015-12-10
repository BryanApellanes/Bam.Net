/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Naizari.Testing
{
    public class TestObject
    {
        public string StringProperty { get; set; }
        public int IntProperty { get; set; }
        public TestObject ClassProperty { get; set; }
        public TestObject[] ArrayProperty { get; set; }
    }
}
