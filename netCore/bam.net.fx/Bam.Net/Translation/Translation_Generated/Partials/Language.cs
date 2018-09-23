/*
	Copyright © Bryan Apellanes 2015  
*/
// Model is Table
using System;
using System.Data;
using System.Data.Common;
using Bam.Net;
using Bam.Net.Data;
using Bam.Net.Data.Qi;

namespace Bam.Net.Translation
{
	public partial class Language
	{
        static Language _default;
        static object _defaultLock = new object();
        public static Language Default
        {
            get
            {
                return _defaultLock.DoubleCheckLock(ref _default, () => OneWhere(c => c.ISO6391 == "en", LanguageDatabase.Default));
            }
        }

        public static implicit operator string(Language lang)
        {
            return lang.EnglishName;
        }

        public static implicit operator Language(string languageIdentifier)
        {
            return TranslationProvider.FindLanguage(languageIdentifier, LanguageDatabase.Default);
        }
    }
}																								
