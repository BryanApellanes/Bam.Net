using Bam.Net.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bam.Net.Logging.Counters.Data
{
    [Serializable]
    public class TimerData: KeyHashRepoData
    {
        public TimerData() { }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
