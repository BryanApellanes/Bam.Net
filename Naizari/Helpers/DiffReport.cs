/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Naizari.Helpers
{
    public enum DiffType
    {
        None,
        Deleted,
        Inserted
    }
    
    [Serializable]
    public class DiffReport
    {
        public DiffReport()
        {
            tokens = new List<DiffReportToken>();
            inserted = new List<InsertedDiffReportToken>();
            deleted = new List<DeletedDiffReportToken>();
        }

        List<DiffReportToken> tokens;
        public DiffReportToken[] Tokens
        {
            get { return tokens.ToArray(); }
            set
            {
                tokens.Clear();
                tokens.AddRange(value);
            }
        }

        List<InsertedDiffReportToken> inserted;
        public InsertedDiffReportToken[] InsertedLines
        {
            get { return inserted.ToArray(); }
        }

        List<DeletedDiffReportToken> deleted;
        public DeletedDiffReportToken[] DeletedLines
        {
            get { return deleted.ToArray(); }
        }
        /// <summary>
        /// Saves this Report to the specified file overwriting if the file exists.
        /// </summary>
        /// <param name="filePath"></param>
        public void Save(string filePath)
        {
            using (StreamWriter sw = new StreamWriter(filePath, false))
            {
                XmlSerializer ser = new XmlSerializer(this.GetType());
                ser.Serialize(sw, this);
            }
        }

        private void AddLine<T>(int lineNum, string text) where T : DiffReportToken, new()
        {
            T line = new T();
            line.lineNum = lineNum;
            line.text = text;
            tokens.Add(line);
            if (line.Type == DiffType.Deleted)
                deleted.Add(line as DeletedDiffReportToken);
            if (line.Type == DiffType.Inserted)
                inserted.Add(line as InsertedDiffReportToken);

        }

        private static void WriteLine(int nr, DiffType typ, string aText, StringBuilder diffText)
        {
            WriteLine(nr, typ, aText, diffText, true);
        }

        private static void WriteLine(int nr, DiffType typ, string aText, StringBuilder diffText, bool numberLines)
        {
            diffText.Append("<tr><td>");
            if (nr >= 0 && numberLines)
                diffText.Append((nr).ToString());
            else
                diffText.Append("&nbsp;");
            diffText.Append("<td><span xstyle='width:100%'");

            switch (typ)
            {
                case DiffType.None:
                    break;
                case DiffType.Deleted:
                    diffText.Append(" style='background-color: red; width: 100%;'");
                    break;
                case DiffType.Inserted:
                    diffText.Append(" style='background-color: green; width: 100%'");
                    break;
                default:
                    break;
            }
                

            aText =  HttpUtility.HtmlEncode(aText).Replace("\r", "").Replace(" ", "&nbsp;");
            diffText.Append(">" + aText + "</span></td></tr>\n");
        }

        public string ToHtmlTable()
        {
            return ToHtmlTable(true);
        }

        public string ToHtmlTable(bool numberLines)
        {
            StringBuilder text = new StringBuilder();
            text.Append("<table>");
            foreach (DiffReportToken line in this.tokens)
            {
                //WriteLine(line.TokenNumber, line.Type
                if (line.GetType().Equals(typeof(DeletedDiffReportToken)))
                    WriteLine(line.TokenNumber, DiffType.Deleted, line.Text, text, numberLines);
                else if (line.GetType().Equals(typeof(InsertedDiffReportToken)))
                    WriteLine(line.TokenNumber, DiffType.Inserted, line.Text, text, numberLines);
                else
                    WriteLine(line.TokenNumber, DiffType.None, line.Text, text, numberLines);
            }

            text.Append("</table>");
            return text.ToString();
        }

        public static string GetHtmlTable(string a, string b)
        {
            DiffReport dr = Get(a, b);
            return dr.ToHtmlTable();
        }

        public static DiffReport Get(string a, string b)
        {
            return Get(a, b, '\n');
        }

        public static DiffReport Get(string a, string b, params char[] separators)
        {
            //StringBuilder returnText = new StringBuilder();
            //returnText.Append("<table>");
            DiffReport r = new DiffReport();

            Diff.Item[] f = Diff.DiffText(a, b, true, true, false, separators);
            string[] aLines = a.Split(separators);
            string[] bLines = b.Split(separators);

            int n = 1;
            for (int fdx = 0; fdx < f.Length; fdx++)
            {
                Diff.Item aItem = f[fdx];

                // write unchanged lines
                while ((n-1 < aItem.StartB) && (n-1 < bLines.Length))
                {
                    r.AddLine<DiffReportToken>(n, bLines[n-1]);//WriteLine(n, DiffType.None, bLines[n], returnText);
                    n++;
                } // while

                // write deleted lines
                for (int m = 0; m < aItem.deletedA; m++)
                {
                    r.AddLine<DeletedDiffReportToken>(-1, aLines[aItem.StartA + m]);//WriteLine(-1, DiffType.Deleted, aLines[aItem.StartA + m], returnText);
                } // for

                // write inserted lines
                while (n-1 < aItem.StartB + aItem.insertedB)
                {
                    r.AddLine<InsertedDiffReportToken>(n, bLines[n-1]);//WriteLine(n, DiffType.Inserted, bLines[n], returnText);
                    n++;
                } // while
            } // while

            // write rest of unchanged lines
            while (n-1 < bLines.Length)
            {
                r.AddLine<DiffReportToken>(n, bLines[n-1]);//WriteLine(n, DiffType.None, bLines[n], returnText);
                n++;
            } // while

            //returnText.Append("</table>");
            return r;// returnText.ToString();
        }
    }
}
