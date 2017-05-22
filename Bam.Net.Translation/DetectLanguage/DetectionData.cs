using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Translation.DetectLanguage
{
    public class Detection
    {
        public string language { get; set; }
        public bool isReliable { get; set; }
        public decimal confidence { get; set; }
    }

    public class DetectionData
    {
        public Detection[] detections { get; set; }
    }
}
