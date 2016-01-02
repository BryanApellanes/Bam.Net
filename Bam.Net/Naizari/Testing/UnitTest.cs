/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Naizari.Extensions;

namespace Naizari.Testing
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple=false, Inherited=false)]
    public class UnitTest: ConsoleAction
    {
        public UnitTest()
            : base()
        {
        }

        public UnitTest(string description)
            : base(description)
        {
        }

    }
}
