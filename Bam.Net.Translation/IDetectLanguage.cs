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
    public interface IDetectLanguage
    {
        Language DetectLanguage(string text);
        string Translate(string input, string languageIdentifier);
    }
}
