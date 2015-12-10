/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;

namespace Naizari
{
    public class MultiPartMessageBuilder
    {
        List<string> parts;
        public MultiPartMessageBuilder()
        {
            parts = new List<string>();
        }

        public string StartWith { get; set; }

        public void AddPart(string part)
        {
            parts.Add(part);
        }

        public string EndWith { get; set; }

        public override string ToString()
        {
            string retVal = string.Format("{0} {1} {2}", this.StartWith, this.GetParts(), this.EndWith);
      
            return retVal;
        }

        private string GetParts()
        {
            string retVal = string.Empty;
            for(int i = 0; i < parts.Count; i++)
            {
                retVal += parts[i];
                if (i == parts.Count - 2)
                    retVal += useAnd ? " and " : " or ";
                else if (i != parts.Count - 1)
                    retVal += ", ";
            }

            if (this.EndSentenceAtLastPart)
                retVal += ".";
            return retVal;
        }

        bool useAnd;
        public bool UseAnd
        {
            get { return useAnd; }
            set
            {
                useAnd = value;
                useOr = !value;
            }
        }

        bool useOr;
        public bool UseOr
        {
            get { return useOr; }
            set
            {
                useOr = value;
                useAnd = !value;
            }
        }

        public bool EndSentenceAtLastPart
        {
            get;
            set;
        }
    }
}
