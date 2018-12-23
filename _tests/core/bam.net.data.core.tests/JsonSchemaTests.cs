using Bam.Net.CoreServices.ServiceRegistration.Data.Dao;
using Bam.Net.Data.Schema.Json;
using Bam.Net.Testing;
using Bam.Net.Testing.Unit;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Bam.Net.Data.Repositories;

namespace Bam.Net.Data.Tests
{
    [JsonSchema]
    public class TestContainer
    {
        public string Name { get; set; }
        public DateTime Created { get; set; }
    }

    public class AnotherClass: AuditRepoData
    {
        public string Name { get; set; }
        public string SomeOtherProperty { get; set; }
    }

    public class TestParentRepoData: RepoData
    {
        public string Name { get; set; }
        public TestContainer Container { get; set; }
        public AnotherClass AnotherClass { get; set; }
        public List<TestChild> Children { get; set; }
    }

    public class TestChild
    {
        public string Name { get; set; }
    }

    [Serializable]
    public class JsonSchemaTests : CommandLineTestInterface
    {
        [UnitTest]
        public void CanConvertToJson()
        {
            Type doaType = typeof(MachineRegistries);
            JsonSchema jsonSchema = JsonSchema.FromDao<MachineRegistries>();
            OutLine(jsonSchema.ToJson(true));
        }

        [UnitTest]
        public void ArrayMustHaveType()
        {
            JsonSchema schema = JsonSchema.FromType(typeof(TestParentRepoData));
            OutLine(schema.ToJson(true));
        }
        
        [UnitTest]
        public void JsonSchemaDictionary()
        {
            Dictionary<string, JsonSchemaProperty> props = JsonSchemaProperty.PropertyDictionaryFromType<TestParentRepoData>();
            OutLine(props.ToJson(true));
        }

        [UnitTest]
        public void TypeResolution()
        {
            Type listType = typeof(List<JsonSchemaProperty>);            
            Expect.IsTrue(listType.GetInterfaces().Contains(typeof(IEnumerable)), "isn't IEnumerable");
            Expect.IsTrue(listType.IsForEachable(out Type underlyingType), "list wasn't foreachable");
            Expect.AreEqual(typeof(JsonSchemaProperty), underlyingType, "underlying list type was not as expected");

            Type arrayType = typeof(JsonSchemaProperty[]);
            Expect.IsTrue(arrayType.IsArray, "isn't array");
            Expect.IsTrue(arrayType.IsForEachable(out Type underlyingArrayType), "array type wasn't foreachable");
            Expect.AreEqual(typeof(JsonSchemaProperty), underlyingArrayType, "underlying array type wasn't as expected");
        }
    }
}
