/*
	Copyright © Bryan Apellanes 2015  
*/
using Bam.Net.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Translation.Yandex
{

    public class LanguageDetectResponse
    {
        public ResponseCode code { get; set; }
        public string lang { get; set; }
        public Language GetLanguage(Database languageDb)
        {
            Language language = Language.OneWhere(l => l.ISO6391 == lang, languageDb);
            if (language == null)
            {
                language = Language.OneWhere(l => l.EnglishName == "English", languageDb);
            }
            return language;
        }
    }
}
