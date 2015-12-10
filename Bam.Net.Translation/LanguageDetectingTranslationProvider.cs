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
    public abstract class LanguageDetectingTranslationProvider: TranslationProvider, IDetectLanguage
    {
        public abstract Language DetectLanguage(string text);

        public string Translate(string input, string languageIdentifier)
        {
            Language toLanguage = null;
            if (languageIdentifier.Length == 2)
            {
                toLanguage = Language.OneWhere(c => c.ISO6391 == languageIdentifier, LanguageDatabase);
            }
            else if (languageIdentifier.Length == 3)
            {
                toLanguage = Language.OneWhere(c => c.ISO6392 == languageIdentifier, LanguageDatabase);
            }
            else
            {
                toLanguage = Language.OneWhere(c => c.EnglishName == languageIdentifier, LanguageDatabase);
                if (toLanguage == null)
                {
                    OtherName otherName = OtherName.FirstOneWhere(c => c.Value == languageIdentifier, LanguageDatabase);
                    if (otherName != null)
                    {
                        toLanguage = otherName.LanguageOfLanguageId;
                    }
                }
            }

            Args.ThrowIf<ArgumentException>(toLanguage == null, "Unable to identify specified language: {0}", languageIdentifier);
            
            return Translate(input, toLanguage);
        }

        public string Translate(string input, Language toLanguage)
        {
            Text text = Text.OneWhere(t => t.Value == input, TranslationDatabase);
            Language from = null;
            LanguageDetection detection = null;
            if (text != null && text.LanguageOfLanguageId != null)
            {
                from = text.LanguageOfLanguageId;
            }
            else
            {
                from = DetectLanguage(input);
                detection = new LanguageDetection();
                detection.LanguageId = from.Id;

                text = new Text();
                text.Value = input;
                text.LanguageId = from.Id;
                text.Save(TranslationDatabase);
                detection.TextId = text.Id;
                detection.Detector = this.GetType().Name;
                detection.Save(TranslationDatabase);
            }
            
            return Translate(from, toLanguage, input);
        }
    }
}
