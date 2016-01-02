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
    }
}
