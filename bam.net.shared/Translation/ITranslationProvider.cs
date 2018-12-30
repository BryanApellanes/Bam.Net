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
    public interface ITranslationProvider
    {
        string Translate(long languageIdFrom, long languageIdTo, string input);
        string Translate(string uuidFrom, string uuidTo, string input);
        string Translate(Language from, Language to, string input);
    }
}
