/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Analytics
{
    public abstract class DiffReportFormatter
    {
        public DiffReportFormatter() { }
        public DiffReportFormatter(DiffReport diffReport)
        {
            this.DiffReport = diffReport;
        }

        public DiffReport DiffReport { get; set; }

        public bool NumberLines { get; set; }

        public string Format()
        {
            StringBuilder output = new StringBuilder();
            Start(output);
            foreach (DiffReportToken line in DiffReport.Tokens)
            {
                switch (line.Type)
                {
                    case DiffType.None:
                        WriteLine(line.TokenNumber, line.Text, output);
                        break;
                    case DiffType.Deleted:
                        WriteDeletedLine(line.TokenNumber, line.Text, output);
                        break;
                    case DiffType.Inserted:
                        WriteInsertedLine(line.TokenNumber, line.Text, output);
                        break;
                }
            }
            End(output);
            return output.ToString();
        }

        public abstract void WriteLine(int lineNumber, string text, StringBuilder output);
        public abstract void WriteDeletedLine(int lineNumber, string text, StringBuilder output);
        public abstract void WriteInsertedLine(int lineNumber, string text, StringBuilder output);

        public virtual void Start(StringBuilder output) { }
        public virtual void End(StringBuilder output) { }
    }
}
