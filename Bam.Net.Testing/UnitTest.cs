/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bam.Net.CommandLine;
using System.Reflection;

namespace Bam.Net.Testing
{
    /// <summary>
    /// Attribute used to mark a method as a Unit Test
    /// </summary>
    [Serializable]
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
