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

    public class YandexLanguageDetectResponse
    {
        public YandexResponseCode code { get; set; }
        public string lang { get; set; }
        public Language GetLanguage(Database languageDb)
        {
            return TranslationProvider.FindLanguage(lang, languageDb);
        }
    }
}
