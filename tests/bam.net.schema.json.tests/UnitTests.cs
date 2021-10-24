using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using Bam.Net;
using Bam.Net.Schema.Json;
using Bam.Net.CommandLine;
using Bam.Net.Data.Schema;
using Bam.Net.Logging;
using Bam.Net.Server.PathHandlers.Attributes;
using Bam.Net.Testing;
using Bam.Net.Testing.Unit;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Serializers;
using Newtonsoft.Json.Schema;

namespace Bam.Net.Schema.Json.Tests
{
    [Serializable]
    public class UnitTests : CommandLineTool
    {
        private HomePath RootData = new HomePath("~/.data/JsonSchema/");
        private HomePath ApplicationSchema = new HomePath("~/.data/JsonSchema/application_v1.yaml");
        private HomePath CensusSchema = new HomePath("~/.data/JsonSchema/census_v1.yaml");
        private HomePath CommonSchema = new HomePath("~/.data/JsonSchema/common_v1.yaml");
        private HomePath OrganizationDataPath => new HomePath(Path.Combine(RootData, "organization_v1.yaml"));
        private HomePath CompanyDataPath => new HomePath(Path.Combine(RootData, "company_v1.yaml"));

        [UnitTest]
        [TestGroup("JSchema")]
        public void CanGenerateDaoSource()
        {
            Workspace workspace = Workspace.ForProcess();
            string testSrcPath = workspace.Path("src", nameof(CanGenerateDaoSource));
            if (Directory.Exists(testSrcPath))
            {
                Directory.Delete(testSrcPath, true);
            }
            DirectoryInfo testSrcDir = new DirectoryInfo(testSrcPath);
            if (!testSrcDir.Exists)
            {
                testSrcDir.Create();
            }
            JSchemaManagementRegistry registry = JSchemaManagementRegistry.CreateForYaml(RootData);
            JSchemaDaoAssemblyGenerator jSchemaDaoAssemblyGenerator = registry.Get<JSchemaDaoAssemblyGenerator>();
            jSchemaDaoAssemblyGenerator.Namespace = "Bam.Net.JSchema.Generated.Classes";
            FileInfo[] files = testSrcDir.GetFiles();
            (files.Length == 0).IsTrue("There were files already in the target directory");
            jSchemaDaoAssemblyGenerator.GenerateSource(testSrcPath);
            files = testSrcDir.GetFiles();
            (files.Length > 0).IsTrue("No files were in the target directory");
        }
        
        [UnitTest]
        [TestGroup("JSchema")]
        public void CanGenerateSchemaDefinition()
        {
            JSchemaManagementRegistry registry = JSchemaManagementRegistry.CreateForYaml(RootData);
            JSchemaSchemaDefinitionGenerator generator = registry.Get<JSchemaSchemaDefinitionGenerator>();
            JSchemaSchemaDefinition jSchemaSchemaDefinition = generator.GenerateSchemaDefinition();
            Expect.IsNotNull(jSchemaSchemaDefinition, "jSchemaSchemaDefinition was null");
        }

        [UnitTest]
        [TestGroup("JSchema")]
        public void CanGetGenerator()
        {
            JSchemaManagementRegistry registry = JSchemaManagementRegistry.CreateForYaml(RootData);
            JSchemaSchemaDefinitionGenerator generator = registry.Get<JSchemaSchemaDefinitionGenerator>();
            Expect.IsNotNull(generator, "generator was null");
            Expect.IsNotNull(generator.SchemaManager, "SchemaManager was null");
            Expect.IsNotNull(generator.JSchemaClassManager, "JSchemaSchemaClassManager was null");
            Expect.IsNotNull(generator.Logger, "Logger was null");
        }

        [UnitTest]
        [TestGroup("JSchema")]
        public void CanLoadJSchemaWithoutExceptions()
        {
            JSchema jSchema = JSchemaLoader.LoadJSchema(OrganizationDataPath, SerializationFormat.Yaml);
            OutLine(jSchema.ToString(), ConsoleColor.Cyan);
        }

        [UnitTest]
        [TestGroup("JSchema")]
        public void CanGetClassManagerFromRegistry()
        {
            JSchemaManagementRegistry registry = new JSchemaManagementRegistry(RootData);
            JSchemaClassManager classManager = registry.Get<JSchemaClassManager>();
            Expect.IsNotNull(classManager.JSchemaResolver);
            (classManager.JSchemaResolver is FileSystemJSchemaResolver).IsTrue("unexpected resolver type");
            Expect.IsNotNull(classManager.JSchemaNameParser);
        }

        [UnitTest]
        [TestGroup("JSchema")]
        public void CanGetClassManagerWithNameParser()
        {
            JSchemaManagementRegistry registry = new JSchemaManagementRegistry(RootData);
            JSchemaClassManager classManager = registry.Get<JSchemaClassManager>();
            Expect.IsNotNull(classManager.JSchemaNameParser);
        }
        
        [UnitTest]
        [TestGroup("JSchema")]
        public void CanLoadDefinitionsFromCommon()
        {
             JSchemaManagementRegistry registry = new JSchemaManagementRegistry(RootData);
             JSchemaClassManager classManager = registry.Get<JSchemaClassManager>();
             JSchemaClass common = classManager.LoadJSchemaClassFile(new HomePath("~/.data/JsonSchema/common_v1.yaml"));
             OutLine(common.ToJson(true));
             IEnumerable<JSchemaClass> definitions = JSchemaClass.FromDefinitions(common.JSchema, classManager);
             OutLine(definitions.ToJson(true), ConsoleColor.Yellow);
        }

        [UnitTest]
        [TestGroup("JSchema")]
        public void CanLoadJSchemaClassWithClassName()
        {
            JSchemaManagementRegistry registry = JSchemaManagementRegistry.CreateForYaml(RootData, "@type", "class", "className");
            JSchemaClassManager classManager = registry.Get<JSchemaClassManager>();
            JSchemaClass app = classManager.LoadJSchemaClassFile(new HomePath("~/.data/JsonSchema/application_v1.yaml"));
            Expect.IsNotNull(app);
            Expect.AreEqual("Application", app.ClassName);
            Expect.AreEqual(22, app.Properties.Count());
            
            Message.PrintLine("Properties: {0}", app.Properties.ToArray().ToDelimited(p=> p.PropertyName));
            Message.PrintLine("Value properties: {0}", app.ValueProperties.ToArray().ToDelimited(p=> p.PropertyName));
            Message.PrintLine("Array properties: {0}", app.ArrayProperties.ToArray().ToDelimited(p=> p.PropertyName + $"[{p.ClassOfArrayItems.ClassName}]"));
            Message.PrintLine("Object properties: {0}", app.ObjectProperties.ToArray().ToDelimited(p=> p.PropertyName));
            Message.PrintLine("Enum properties: \r\n\t{0}", ConsoleColor.Blue, app.EnumProperties.ToArray().ToDelimited(p=> p.PropertyName + ": " + p.GetEnumNames().ToArray().ToDelimited(en=> en, "|"), "\r\n"));

            JSchemaProperty proposalDetail = app["ProposalDetail"];
            
            Expect.IsNotNull(proposalDetail);
            Message.PrintLine(proposalDetail.ToJson(true), ConsoleColor.Yellow);
            Message.PrintLine(proposalDetail.JSchemaOfProperty.ToJson(true), ConsoleColor.DarkYellow);
        }
        
        [UnitTest]
        [TestGroup("JSchema")]
        public void CanLoadJSchemaClassWithClassName2()
        {
            JSchemaManagementRegistry registry = JSchemaManagementRegistry.CreateForYaml(RootData, "@type", "javaType", "class", "className");
            JSchemaClassManager classManager = registry.Get<JSchemaClassManager>();
            classManager.SetClassNameMunger("javaType", javaType =>
            {
                string[] split = javaType.DelimitSplit(".");
                return split[split.Length - 1];
            });
            JSchemaClass census = classManager.LoadJSchemaClassFile(new HomePath("~/.data/JsonSchema/census_v1.yaml"));
            Expect.IsNotNull(census);
            Expect.AreEqual("Census", census.ClassName);
            Expect.AreEqual(4, census.Properties.Count());
            
            Message.PrintLine("Properties: {0}", census.Properties.ToArray().ToDelimited(p=> p.PropertyName));
            Message.PrintLine("Value properties: {0}", census.ValueProperties.ToArray().ToDelimited(p=> p.PropertyName));
            Message.PrintLine("Array properties: {0}", census.ArrayProperties.ToArray().ToDelimited(p=> p.PropertyName));
            Message.PrintLine("Object properties: {0}", census.ObjectProperties.ToArray().ToDelimited(p=> p.PropertyName));
            Message.PrintLine("Enum properties: \r\n\t{0}", ConsoleColor.Blue, census.EnumProperties.ToArray().ToDelimited(p=> p.PropertyName + ": " + p.GetEnumNames().ToArray().ToDelimited(en=> en, "|"), "\r\n"));
        }

        [UnitTest]
        [TestGroup("JSchema")]
        public void CanLoadAllJSchemaClasses()
        {
            JSchemaManagementRegistry registry = JSchemaManagementRegistry.CreateForYaml(RootData, "@type", "javaType", "class", "className");
            JSchemaClassManager classManager = registry.Get<JSchemaClassManager>();
            classManager.SetClassNameMunger("javaType", javaType =>
            {
                string[] split = javaType.DelimitSplit(".");
                string typeName = split[split.Length - 1];
                if (typeName.EndsWith("Entity"))
                {
                    typeName = typeName.Truncate("Entity".Length);
                }

                return typeName;
            });
            HashSet<JSchemaClass> results = classManager.GetAllJSchemaClasses(RootData);
            Expect.AreEqual(35, results.Count);
            Console.WriteLine($"Result count = {results.Count}");
            StringBuilder output = new StringBuilder();
            foreach (JSchemaClass jSchemaClass in results)
            {
                output.AppendFormat("ClassName={0}\r\n", jSchemaClass.ClassName);
                output.AppendLine(jSchemaClass.ToJson(true));
                output.AppendLine("****");
            }
            FileInfo outputFile = new FileInfo(new HomePath("~/.bam/data/testoutput.txt"));
            output.ToString().SafeWriteToFile(outputFile.FullName, true);
            Console.WriteLine("Wrote file {0}", outputFile.FullName);
        }
        
        [UnitTest]
        [TestGroup("JSchema")]
        public void CanLoadAllJSchemaClassesWithJavaJSchemaClassManager()
        {
            JSchemaManagementRegistry registry = JSchemaManagementRegistry.Create(RootData, SerializationFormat.Yaml, "@type", "javaType", "class", "className");
            JSchemaClassManager classManager = registry.Get<JavaJSchemaClassManager>();
            HashSet<JSchemaClass> results = classManager.GetAllJSchemaClasses(RootData);
            
            Expect.AreEqual(35, results.Count);
            Console.WriteLine($"Result count = {results.Count}");
            
            StringBuilder output = new StringBuilder();
            foreach (JSchemaClass jSchemaClass in results)
            {
                output.AppendFormat("ClassName={0}\r\n", jSchemaClass.ClassName);
                output.AppendLine(jSchemaClass.ToJson(true));
                output.AppendLine("****");
            }
            FileInfo outputFile = new FileInfo(new HomePath("~/.bam/data/javaTestOutput.txt"));
            output.ToString().SafeWriteToFile(outputFile.FullName, true);
            Console.WriteLine("Wrote file {0}", outputFile.FullName);
        }
        
        [UnitTest]
        [TestGroup("JSchema")]
        public void CanGetRootClassName()
        {
            // check @type
            // check title
            // check javaType
            // check _tableNameProperties
            JSchema jSchema = JSchemaLoader.LoadYamlJSchema(OrganizationDataPath);
            JSchemaClassManager classManager = new JSchemaClassManager("@type", "title", "javaType");
            JSchemaClass orgClass = classManager.LoadJSchemaClassFile(OrganizationDataPath);
            string className = orgClass.ClassName;
            Expect.IsNotNullOrEmpty(className);
            Expect.AreEqual("Organization", className);
        }

        [UnitTest]
        [TestGroup("JSchema")]
        public void CanGetPropertyNames()
        {
            JSchema jSchema = JSchemaLoader.LoadYamlJSchema(OrganizationDataPath);
            JSchemaClassManager jSchemaClassManager = new JSchemaClassManager("@type", "title", "javaType");
            JSchemaClass jSchemaClass = jSchemaClassManager.LoadJSchemaClassFile(OrganizationDataPath);
            string[] propertyNames = jSchemaClass.Properties.Select(p => p.PropertyName).ToArray();
            HashSet<string> propertyNameHashSet = new HashSet<string>(propertyNames);
            propertyNameHashSet.Contains("FriendlyId").IsTrue("FriendlyId not found");
            propertyNameHashSet.Contains("WebsiteUrl").IsTrue("WebsiteUrl not found");
            
            propertyNameHashSet.Contains("BusinessName").IsTrue("BusinessName not found");
            propertyNameHashSet.Contains("TaxIds").IsTrue("TaxIds");
            propertyNameHashSet.Contains("IndustryCodes").IsTrue("IndustryCodes");
        }
        
        [UnitTest]
        public void CanResolveUnixPath()
        {
            HomePath path = new HomePath("~/src");
            path.Resolve().StartsWith("~").IsFalse();
            path.Path.StartsWith("~/").IsTrue();
            path.Resolve().StartsWith(BamHome.UserHome);
            Message.PrintLine("Unix path: {0}", ConsoleColor.Cyan, path.Resolve());
        }

        private JSchema GetCompanyJSchema(out JSchemaClassManager jSchemaClassManager)
        {
            return GetJSchema(CompanyDataPath, out jSchemaClassManager);
        }
        
        private JSchema GetOrganizationJSchema(out JSchemaClassManager jSchemaClassManager)
        {
            return GetJSchema(OrganizationDataPath, out jSchemaClassManager);
        }
        
        private JSchema GetJSchema(string dataPath, out JSchemaClassManager jSchemaClassManager)
        {
            JSchema jSchema = JSchemaLoader.LoadYamlJSchema(dataPath);
            jSchemaClassManager = GetJSchemaManager();
            return jSchema;
        }

        private JSchemaClassManager GetJSchemaManager<T>() where T : JSchemaClassManager, new()
        {
            T result = new T {JSchemaResolver = GetJSchemaResolver()};
            return result;
        }
        
        private JSchemaResolver GetJSchemaResolver()
        {
            return new FileSystemYamlJSchemaResolver(RootData)
            {
                JSchemaLoader = JSchemaLoader.ForFormat(SerializationFormat.Yaml)
            };
        }
        
        private JSchemaClassManager GetJSchemaManager()
        {
            return new JavaJSchemaClassManager();
        }
    }
}