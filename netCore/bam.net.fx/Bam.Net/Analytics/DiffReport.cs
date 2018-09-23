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

namespace Bam.Net.Analytics
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
        Dictionary<string, Action<string>> _savers;
        public DiffReport()
        {
            tokens = new List<DiffReportToken>();
            inserted = new List<InsertedDiffReportToken>();
            deleted = new List<DeletedDiffReportToken>();
            Dictionary<string, Action<string>> savers = new Dictionary<string, Action<string>>();
            savers.Add(".json", (s) => this.ToJsonFile(s));
            savers.Add(".xml", (s) => this.ToXmlFile(s));
            this._savers = savers;
            
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
        public InsertedDiffReportToken[] Inserted
        {
            get { return inserted.ToArray(); }
        }

        List<DeletedDiffReportToken> deleted;
        public DeletedDiffReportToken[] Deleted
        {
            get { return deleted.ToArray(); }
        }
        /// <summary>
        /// Saves this Report to the specified file overwriting if the file exists.
        /// </summary>
        /// <param name="filePath"></param>
        public void Save(string filePath)
        {
            string ext = System.IO.Path.GetExtension(filePath);
            if (_savers.ContainsKey(ext))
            {
                _savers[ext](filePath);
            }
            else
            {
                this.ToXmlFile(filePath);
            }
        }

        private void AddLine<T>(int lineNum, string text) where T : DiffReportToken, new()
        {
            T line = new T();
            line.lineNum = lineNum;
            line.text = text;
            tokens.Add(line);
            if (line.Type == DiffType.Deleted)
            {
                deleted.Add(line as DeletedDiffReportToken);
            }
            if (line.Type == DiffType.Inserted)
            {
                inserted.Add(line as InsertedDiffReportToken);
            }

        }
                
        public static DiffReport Create(string a, string b)
        {
            return Create(a, b, '\n');
        }

        public static DiffReport Create(string a, string b, params char[] separators)
        {
            DiffReport report = new DiffReport();

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
                    report.AddLine<DiffReportToken>(n, bLines[n-1]);
                    n++;
                } // while

                // write deleted lines
                for (int m = 0; m < aItem.deletedA; m++)
                {
                    report.AddLine<DeletedDiffReportToken>(-1, aLines[aItem.StartA + m]);
                } // for

                // write inserted lines
                while (n-1 < aItem.StartB + aItem.insertedB)
                {
                    report.AddLine<InsertedDiffReportToken>(n, bLines[n-1]);
                    n++;
                } // while
            } // while

            // write rest of unchanged lines
            while (n-1 < bLines.Length)
            {
                report.AddLine<DiffReportToken>(n, bLines[n-1]);
                n++;
            } // while

            return report;
        }
    }
}
