/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Translation
{
    public class EchoTranslationProvider: TranslationProvider
    {
        public override string Translate(Language from, Language to, string input)
        {
            return input;
        }

        public override string Translate(string input, string languageIdentifier)
        {
            return input;
        }

        public override string TranslateLanguages(string input, string inputLanguageIdentifier, string outputLanguageIdentifier)
        {
            return input;
        }

        public override Language DetectLanguage(string text)
        {
            throw new NotImplementedException();
        }

        protected override string GetTranslationFromService(string twoLetterIsoLanguageCodeFrom, string twoLetterIsoLanguageCodeTo, string input)
        {
            throw new NotImplementedException();
        }
    }
}
