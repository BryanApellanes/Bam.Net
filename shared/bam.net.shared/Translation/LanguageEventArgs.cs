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
    public class LanguageEventArgs: EventArgs
    {
        public Language Language { get; set; }
        public string Message { get; set; }
        public OtherName OtherName { get; set; }
    }
}
