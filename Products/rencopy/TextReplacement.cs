/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rencopy
{
    [Serializable]
    public class TextReplacement
    {
        public TextReplacement() { } // for serialization
        public TextReplacement(string oldTxt, string newTxt)
        {
            this.OldText = oldTxt;
            this.NewText = newTxt;
        }
        
        public string OldText { get; set; }
        public string NewText { get; set; }

        public string GetNewText(string sourceContent)
        {
            return sourceContent.Replace(OldText, NewText);
        }
    }
}
