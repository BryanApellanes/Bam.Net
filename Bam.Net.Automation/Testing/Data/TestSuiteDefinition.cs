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
using System.Reflection;

namespace Bam.Net.Automation.Testing.Data
{
    /// <summary>
    /// Represents a logical set of TestDefinitions
    /// belonging to a common suite of tests
    /// </summary>
	[Serializable]
	public class TestSuiteDefinition: AuditRepoData
	{
		public TestSuiteDefinition()
		{
		}
        public static string DefaultTestSuiteTitle => "Default Test Suite";
        public string Title { get; set; }
		public virtual TestDefinition[] TestDefinitions { get; set; }

        public static TestSuiteDefinition FromMethod(ConsoleMethod test)
        {
            return new TestSuiteDefinition { Title = GetSuiteTitle(test) };
        }

        private static string GetSuiteTitle(ConsoleMethod test)
        {
            if(test == null || test.Method == null)
            {
                return DefaultTestSuiteTitle;
            }
            string title = GetSuiteTitle(test.Method.DeclaringType);
            if (test.Method.HasCustomAttributeOfType(out TestSuiteAttribute methodAttr))
            {
                title = methodAttr.Title;
            }
            return title;
        }

        private static string GetSuiteTitle(Type containingType)
        {            
            string title = $"{containingType.Assembly.FullName}:{containingType.Name}";
            if (containingType.HasCustomAttributeOfType(out TestSuiteAttribute typeAttr))
            {
                title = typeAttr.Title;
            }
            return title;
        }
	}
}
