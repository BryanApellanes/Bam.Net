using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.Repositories;

namespace Bam.Net.Services.Tests
{
    [Serializable]
    public class KeyHashRepoThrowsData : KeyHashRepoData
    {

    }

    [Serializable]
    public class KeyHashRepoTestData : KeyHashRepoData
    {
        [CompositeKey]
        public string Name { get; set; }
        [CompositeKey]
        public string SomeOtherUniqueProperty { get; set; }
    }

}
