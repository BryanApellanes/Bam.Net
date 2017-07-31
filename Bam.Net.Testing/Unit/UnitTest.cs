/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bam.Net.CommandLine;
using System.Reflection;

namespace Bam.Net.Testing.Unit
{
    /// <summary>
    /// Attribute used to mark a method as a Unit Test
    /// </summary>
    [Serializable]
    [AttributeUsage(AttributeTargets.Method, AllowMultiple=false, Inherited=false)]
    public class UnitTest: ConsoleActionAttribute
    {
        public UnitTest()
            : base()
        {
        }

        public UnitTest(string description)
            : base(description)
        {
        }

        public static List<ConsoleMethod> FromAssembly(Assembly assembly)
        {
            List<ConsoleMethod> tests = new List<ConsoleMethod>();
            tests.AddRange(ConsoleMethod.FromAssembly(assembly, typeof(UnitTest)));
            tests.Sort((l, r) => l.Information.CompareTo(r.Information));
            return tests;
        }
    }
}
