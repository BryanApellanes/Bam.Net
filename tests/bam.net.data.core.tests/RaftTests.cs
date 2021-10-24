using System;
using System.Linq;
using Bam.Net.CoreServices.AssemblyManagement.Data;
using Bam.Net.Data.Repositories;
using Bam.Net.Services.DataReplication.Data;
using Bam.Net.Testing;
using Bam.Net.Testing.Unit;

namespace Bam.Net.Data.Tests
{
    public class RaftTestData : KeyedAuditRepoData
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Dob { get; set; }
        public int Age { get; set; }
    }
    
    public class RaftTests : CommandLineTool
    {
        [UnitTest]
        [TestGroup("Raft")]
        public void CanCreateSaveOperation()
        {
            RaftTestData testObject = new RaftTestData()
            {
                FirstName = "TestFirst",
                LastName = "Test Last",
                Dob = new DateTime(1976, 1, 1)
            };
            SaveOperation operation = SaveOperation.For(testObject);
            Expect.AreEqual(4, operation.Properties.Count);
            foreach (DataProperty prop in operation.Properties)
            {
                OutLine(prop.ToJson(true), ConsoleColor.Yellow);
            }
        }

        [UnitTest]
        [TestGroup("Raft")]
        public void CreateOperationForSetsKeys()
        {
            AssemblyQualifiedTypeDescriptor typeDescriptor = new AssemblyQualifiedTypeDescriptor();
            CreateOperation createOperation = CreateOperation.For(typeDescriptor);
            Expect.IsGreaterThan(createOperation.Properties.Count, 0, "Property count should have been greater than 0");
            Expect.IsFalse(createOperation.Properties.Any(p => string.IsNullOrEmpty(p.InstanceIdentifier)));
            createOperation.Properties.All(p=> p.InstanceIdentifier.Equals(typeDescriptor.Key().ToString())).IsTrue("The keys set did not match the expected value");
        }
    }
}