/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Naizari.Helpers
{
    [Serializable]
    [XmlInclude(typeof(DeletedDiffReportToken))]
    [XmlInclude(typeof(InsertedDiffReportToken))]
    public class DiffReportToken
    {
        internal string text;
        internal int lineNum;
        public DiffReportToken() { }
        public DiffReportToken(string text, int tokenNumber)
        {
            this.text = text;
            this.lineNum = tokenNumber;
        }

        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        public int TokenNumber
        {
            get { return lineNum; }
            set { lineNum = value; }
        }

        public override string ToString()
        {
            return text;
        }

        public virtual DiffType Type
        {
            get { return DiffType.None; }
        }

        public static DiffReportToken[] FromArray(string[] tokens)
        {
            List<DiffReportToken> retVal = new List<DiffReportToken>();
            for (int i = 1; i <= tokens.Length; i++)
            {
                retVal.Add(new DiffReportToken(tokens[i], i));
            }

            return retVal.ToArray();
        }
    }
}
