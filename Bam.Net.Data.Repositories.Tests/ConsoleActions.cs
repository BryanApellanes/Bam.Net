/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Bam.Net;
using Bam.Net.CommandLine;
using Bam.Net.Testing;
using Bam.Net.Yaml;

namespace Bam.Net.Data.Repositories.Tests
{
	[Serializable]
	public class ConsoleActions: CommandLineTestInterface
	{
		static string _gherkin = @"Feature: Some terse yet descriptive text of what is desired
  In order to realize a named business value
  As an explicit system actor
  I want to gain some beneficial outcome which furthers the goal

  Additional text...

  Scenario: Some determinable business situation
    Given some precondition
    And some other precondition
    When some action by the actor
    And some other action
    And yet another action
    Then some testable outcome is achieved
    And something else we can check happens too";

		public class Spec
		{
			public string Feature { get; set; }
			public dynamic Scenario { get; set; }
		}
		[ConsoleAction]
		public void TryToReadGherkinAsSpecflow()
		{
			Spec spec = new Spec
			{
				Feature = "This is the feature description",
				Scenario = new
				{
					Given = "Some precondition",
					And = "Some other precondition",
					When = "some action",
					Then = "some result"
				}
			};
			string fileName = ".\\test.yaml";
			spec.ToYamlFile(".\\test.yaml");

			"notepad {0}"._Format(fileName).Run();
		}

		[ConsoleAction]
		public void OutputDtoTest()
		{
			MainObject o = new MainObject();
			DtoModel model = new DtoModel(o, "Test");
			OutLine(model.Render());
		}

        [ConsoleAction]
        public void OutputPropertiesAndMethods()
        {
            HashSet<string> daoProperties = new HashSet<string>(typeof(Dao).GetProperties().Select(p => p.Name).ToArray());
            HashSet<string> daoMethods = new HashSet<string>(typeof(Dao).GetMethods().Where(m=> !m.IsProperty() && !m.IsSpecialName).Select(m => m.Name).ToArray());
            HashSet<string> generatedMethods = new HashSet<string>(typeof(MainObject).GetMethods().Where(mi => !mi.IsSpecialName && !mi.IsProperty() && !daoMethods.Contains(mi.Name)).Select(mi => mi.Name).ToArray());
            HashSet<string> queryFilterProperties = new HashSet<string>(typeof(QueryFilter).GetProperties().Select(p => p.Name).ToArray());
            HashSet<string> queryFilterMethods = new HashSet<string>(typeof(QueryFilter).GetMethods().Where(mi=> !mi.IsSpecialName && !mi.IsProperty()).Select(p => p.Name).ToArray());
            OutLine("Dao Props:", ConsoleColor.Cyan);
            using (StreamWriter sw = new StreamWriter(".\\reserved.txt"))
            {
                daoProperties.Each(s =>
                {
                    OutLineFormat("\t{0}", ConsoleColor.Cyan, s);
                    sw.WriteLine($"\"{s}\",");
                });
                OutLine("Dao Methods:", ConsoleColor.DarkBlue);
                daoMethods.Each(s =>
                {
                    OutLineFormat("\t{0}", ConsoleColor.DarkBlue, s);
                    sw.WriteLine($"\"{s}\",");
                });
                OutLine("Generated Methods:", ConsoleColor.DarkCyan);
                generatedMethods.Each(s =>
                {
                    OutLineFormat("\t{0}", ConsoleColor.DarkCyan, s);
                    sw.WriteLine($"\"{s}\",");
                });
                OutLine("Query filter Properties:", ConsoleColor.DarkCyan);
                queryFilterProperties.Each(s =>
                {
                    OutLineFormat("\t{0}", ConsoleColor.DarkCyan, s);
                    sw.WriteLine($"\"{s}\",");
                });
                OutLine("Query filter Methods:", ConsoleColor.DarkCyan);
                queryFilterMethods.Each(s =>
                {
                    OutLineFormat("\t{0}", ConsoleColor.DarkCyan, s);
                    sw.WriteLine($"\"{s}\",");
                });
            }

            "notepad .\\reserved.txt".Run();
        }
    }
}
