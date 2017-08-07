/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.Repositories;
using Bam.Net.Testing;
using Bam.Net.CommandLine;

namespace Bam.Net.Automation.TestReporting.Data
{
    /// <summary>
    /// Represents a logical set of TestDefinitions
    /// belonging to a common suite of tests
    /// </summary>
	[Serializable]
	public class TestSuiteDefinition: RepoData
	{
		public TestSuiteDefinition()
		{
		}
		public string Title { get; set; }
		public virtual TestDefinition[] TestDefinitions { get; set; }

        public static TestSuiteDefinition FromMethod(ConsoleMethod test)
        {
            return new TestSuiteDefinition { Title = GetSuiteTitle(test) };
        }

        private static string GetSuiteTitle(ConsoleMethod test)
        {
            Args.ThrowIfNull(test);
            Args.ThrowIfNull(test.Method);
            Type containingType = test.Method.DeclaringType;
            string title = $"{containingType.Assembly.FullName}:{containingType.Name}";
            if (containingType.HasCustomAttributeOfType(out TestSuiteAttribute typeAttr))
            {
                title = typeAttr.Title;
            }
            else if (test.Method.HasCustomAttributeOfType(out TestSuiteAttribute methodAttr))
            {
                title = methodAttr.Title;
            }
            return title;
        }
	}
}
