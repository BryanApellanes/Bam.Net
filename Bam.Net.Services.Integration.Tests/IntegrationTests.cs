/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.CommandLine;
using Bam.Net;
using Bam.Net.Testing;
using Bam.Net.Encryption;
using Bam.Net.ServiceProxy;
using Bam.Net.Data;
using System.Reflection;
using Bam.Net.Testing.Integration;

namespace Bam.Net.Services.Integration.Tests
{
    [Serializable]
    [IntegrationTestContainer]
    public class IntegrationTests : CommandLineTestInterface
    {
        // create a named List
        [IntegrationTest]
        public void CreateNamedListTest()
        {

        }
        // must be logged in
        // should be associated with current user and org
        // should be able to create Item with Attributes [many-many]
        // Item can be another list
        // can add Item to list
    }
}
