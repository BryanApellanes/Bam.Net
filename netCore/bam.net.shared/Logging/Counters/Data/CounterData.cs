using Bam.Net.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bam.Net.Logging.Counters.Data
{
    [Serializable]
    public class CounterData : KeyHashRepoData
    {
        public CounterData() { }

        [CompositeKey]
        public string UserName { get; set; }

        [CompositeKey]
        public string CounterName { get; set; }

        public string Value { get; set; }
    }
}
