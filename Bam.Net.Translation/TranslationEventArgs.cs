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
    public class TranslationEventArgs: EventArgs
    {
        public TranslationEventArgs()
        {
            this.Success = true;
        }
        public bool Success { get; set; }
        public Language From { get; set; }
        public Language To { get; set; }
        public string Message { get; set; }
        public string Input { get; set; }
        public string Output { get; set; }
    }
}
