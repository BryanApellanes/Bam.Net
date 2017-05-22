using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Translation.MyMemory
{
    public class MyMemoryResponseData
    {
        public string translatedText { get; set; }
        public decimal match { get; set; }
        public int responseStatus { get; set; }
        public bool quotaFinished { get; set; }
    }
}
