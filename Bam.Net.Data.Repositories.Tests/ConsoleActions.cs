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
using Bam.Net.Schema.Org;
using Bam.Net.Data.Schema;

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
        public void RunRepositoryIntegrationTests()
        {
            IntegrationTestRunner.RunIntegrationTests(typeof(RepositoryIntegrationTests));
        }

        [ConsoleAction]
        public void WriteSchemaScriptTest()
        {
            TypeInheritanceSchemaGenerator gen = new TypeInheritanceSchemaGenerator();
            gen.SchemaManager = new SchemaManager { AutoSave = false };
            SchemaDefinitionCreateResult sd = gen.CreateSchemaDefinition(new Type[] { typeof(Airport) });
            DataTools.Setup(db => { }).Each(sd, (result, db) =>
            {
                SqlStringBuilder schemaScript = db.WriteSchemaScript((SchemaDefinitionCreateResult)result);
                OutLineFormat("Db Type: {0}", ConsoleColor.Cyan, db.GetType().Name);
                Out(schemaScript.ToString(), ConsoleColor.DarkCyan);
            });
        }

        [ConsoleAction]
        public void OutputDeepestChainLength()
        {
            int deepest = 0;
            int outOf = 0;
            Type[] types = typeof(Thing).Assembly.GetTypes();
            TypeInheritanceDescriptor theOne = null;
            OutLineFormat("{0} types", types.Length);
            types.Each(type =>
            {
                outOf++;
                TypeInheritanceDescriptor inheritance = new TypeInheritanceDescriptor(type);
                if (inheritance.Chain.Count > deepest)
                {
                    theOne = inheritance;
                    deepest = inheritance.Chain.Count;
                    OutLineFormat("Deepest so far {0}/{1}", ConsoleColor.Cyan, deepest, outOf);
                    OutLine(inheritance.ToString(), ConsoleColor.DarkCyan);
                }
                else
                {
                    OutLineFormat("{0}: Only {1}", ConsoleColor.Yellow, type.Name, inheritance.Chain.Count);
                }
            });
            OutLineFormat("The deepest is {0} with {1}", ConsoleColor.DarkBlue, theOne.Type.Name, theOne.Chain.Count);
            OutLine(theOne.ToString(), ConsoleColor.Blue);
        }
    }
}
