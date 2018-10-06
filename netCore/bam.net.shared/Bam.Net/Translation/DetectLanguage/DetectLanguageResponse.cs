using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data;

namespace Bam.Net.Translation.DetectLanguage
{
    public class DetectLanguageResponse
    {
        public DetectionData data { get; set; }

        public Language GetLanguage(Database languageDatabase = null)
        {
            if(data?.detections?.Length > 0)
            {
                Detection detection = data.detections[0];
                return TranslationProvider.FindLanguage(detection.language, languageDatabase ?? LanguageDatabase.Default);
            }
            return Language.Default;
        }
    }
}
