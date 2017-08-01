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
    public class UnitTestAttribute: ConsoleActionAttribute
    {
        public UnitTestAttribute()
            : base()
        {
        }

        public UnitTestAttribute(string description)
            : base(description)
        {
        }

        public static List<UnitTestMethod> FromAssembly(Assembly assembly)
        {
            List<UnitTestMethod> tests = new List<UnitTestMethod>();
            tests.AddRange(ConsoleMethod.FromAssembly<UnitTestMethod>(assembly, typeof(UnitTestAttribute)));
            tests.Sort((l, r) => l.Information.CompareTo(r.Information));
            return tests;
        }
    }
}
