using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Translation
{
    public interface IIsoLanguageTranslationProvider
    {
        string Translate(string input, string languageIdentifier);
        string TranslateLanguages(string input, string inputLanguageIdentifier, string outputLanguageIdentifier);
    }
}
