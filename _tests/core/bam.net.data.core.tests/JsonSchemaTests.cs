using Bam.Net.CoreServices.ServiceRegistration.Data.Dao;
using Bam.Net.Data.Schema.Json;
using Bam.Net.Testing;
using Bam.Net.Testing.Unit;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bam.Net.Data.Tests
{
    [Serializable]
    public class JsonSchemaTests : CommandLineTestInterface
    {
        [UnitTest]
        public void LookAtMe()
        {
            Type doaType = typeof(MachineRegistries);
            JsonSchema jsonSchema = JsonSchema.FromDao<MachineRegistries>();
            OutLine(jsonSchema.ToJson(true));
        }
    }
}
