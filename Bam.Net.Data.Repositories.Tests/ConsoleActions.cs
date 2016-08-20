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
using Bam.Net.Testing.Integration;
using Bam.Net.Yaml;
using Bam.Net.Data.Tests.Integration;
using Bam.Net.Schema.Org.Things;
using Bam.Net.Schema.Org.DataTypes;
using Bam.Net.Data.Schema;
using Bam.Net.Data.SQLite;

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
        public void SaveOfInheritingTypeTest()
        {
            CivicStructure toSave = new CivicStructure();
            toSave.OpeningHours = new Text { Value = "Opening hours" };
            toSave.Name = "The name of the civic structure";
            toSave.Photo = new OneOfThese<ImageObject, Photograph>();
            toSave.Photo.Value<Photograph>(new Photograph { Name = "The photograph" });
            DatabaseRepository repo = new DatabaseRepository(new SQLiteDatabase($".\\{nameof(SaveOfInheritingTypeTest)}", "SchemaDotOrg"));
            repo.AddNamespace(typeof(CivicStructure));
            repo.Save(toSave);
        }

        [ConsoleAction]
        public void RunRepositoryIntegrationTests()
        {
            IntegrationTestRunner.RunIntegrationTests(typeof(RepositoryIntegrationTests));
        }
    }
}
