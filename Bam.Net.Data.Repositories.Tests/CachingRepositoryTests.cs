using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Caching;
using Bam.Net.CommandLine;
using Bam.Net.Data.Repositories.Tests.TestDtos;
using Bam.Net.Data.SQLite;
using Bam.Net.Logging;
using Bam.Net.Testing;

namespace Bam.Net.Data.Repositories.Tests
{
    [Serializable]
    public class TestData: KeyHashRepoData
    {
        [CompositeKey]
        public string Name { get; set; }
        [CompositeKey]
        public string SerialNumber { get; set; }
        public string OtherInformation { get; set; }
    }

    [Serializable]
    public class CachingRepositoryTests : CommandLineTestInterface
    {
        [UnitTest]
        public void ShouldQueryCacheTest()
        {
            ConsoleLogger logger = new ConsoleLogger { AddDetails = false, UseColors = true };
            logger.StartLoggingThread();
            string schemaName = "TheSchemaName_".RandomLetters(5);
            DaoRepository repo = new DaoRepository(new SQLiteDatabase(".", nameof(ShouldQueryCacheTest)), logger, schemaName);
            repo.AddType(typeof(TestData));
            CachingRepository cachingRepo = new CachingRepository(repo, logger);

            TestData data = new TestData { Name = "Bryan", SerialNumber = "12345A", OtherInformation = "is having an existential crisis" };
            data.Save<TestData>(cachingRepo);

            bool? queriedSource = false;
            bool? queriedCache = false;
            cachingRepo.SubscribeOnce(nameof(CachingRepository.QueriedSource), (o, a) =>
            {
                OutLineFormat("Queried source: {0}", ConsoleColor.Yellow, a.PropertiesToLine());
                queriedSource = true;
            });
            cachingRepo.SubscribeOnce(nameof(CachingRepository.QueriedCache), (o, a) =>
            {
                OutLineFormat("Queried cache: {0}", ConsoleColor.Green, a.PropertiesToLine());
                queriedCache = true;
            });
            IEnumerable<TestData> result = cachingRepo.Query<TestData>(td => td.SerialNumber.Equals(data.SerialNumber));
            Expect.AreEqual(1, result.Count());            
            result = cachingRepo.Query<TestData>(td => td.SerialNumber.Equals(data.SerialNumber));

            Expect.AreEqual(1, result.Count());
            Expect.IsTrue(queriedSource.Value, "didn't query source");
            Expect.IsTrue(queriedCache.Value, "didn't query cache");
        }
    }
}
