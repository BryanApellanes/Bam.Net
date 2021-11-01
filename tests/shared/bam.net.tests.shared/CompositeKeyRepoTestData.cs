using Bam.Net.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bam.Net.Services.Tests
{
    [Serializable]
    public class CompositeKeyRepoThrowsData : CompositeKeyRepoData
    {

    }

    [Serializable]
    public class CompositeKeyRepoTestData : CompositeKeyRepoData
    {
        [CompositeKey]
        public string Name { get; set; }
        [CompositeKey]
        public string SomeOtherUniqueProperty { get; set; }
    }
}
