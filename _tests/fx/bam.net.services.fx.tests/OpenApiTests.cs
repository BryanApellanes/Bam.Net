using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.CommandLine;
using Bam.Net.Testing;
using Bam.Net.Services.OpenApi;
using Newtonsoft.Json;
using Bam.Net.Testing.Unit;

namespace Bam.Net.Services.Tests
{
    [Serializable]
    public class OpenApiTests: CommandLineTestInterface
    {
        [UnitTest]
        public void FixedFieldRenderTest()
        {
            OpenApiFixedFieldModel model = new OpenApiFixedFieldModel
            {
                FieldName = "_malarkey",

                Type = "object"
            };

            OpenApiFixedFieldModel model2 = new OpenApiFixedFieldModel
            {
                FieldName = "_malarkey2",

                Type = "string"
            };

            OpenApiObjectDescriptorModel objModel = new OpenApiObjectDescriptorModel
            {
                Namespace = "Test.This.Out",
                ObjectName = "TheObjectName",
                ObjectDescription = "This is a comprehensive description",
                FixedFields = new List<OpenApiFixedFieldModel>
                {
                    model,
                    model2
                }
            };
            OutLine(objModel.Render());
        }

        [UnitTest]
        public void OpenApiDatabaseHasData()
        {
            OpenApiObjectDatabase db = new OpenApiObjectDatabase();
            ObjectDescriptorCollection objects = ObjectDescriptor.Where(c => c.Id > 0, db);
            Expect.IsGreaterThan(objects.Count, 0);
            OutLine(objects[0].PropertiesToLine(), ConsoleColor.Green);
        }
    }
}
