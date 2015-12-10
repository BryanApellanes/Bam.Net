/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Bam.Net.Analytics
{
    public class HtmlTableDiffReportFormatter : DiffReportFormatter
    {
        public HtmlTableDiffReportFormatter() { }
        public HtmlTableDiffReportFormatter(DiffReport report) : base(report) { }
        
        public override void Start(StringBuilder output)
        {
            output.Append("<table>");
        }

        public override void End(StringBuilder output)
        {
            output.Append("</table>");
        }

        public override void WriteLine(int lineNumber, string text, StringBuilder output)
        {
            WriteLine(lineNumber, DiffType.None, text, output, NumberLines);
        }

        public override void WriteDeletedLine(int lineNumber, string text, StringBuilder output)
        {
            WriteLine(lineNumber, DiffType.Deleted, text, output, NumberLines);
        }

        public override void WriteInsertedLine(int lineNumber, string text, StringBuilder output)
        {
            WriteLine(lineNumber, DiffType.Inserted, text, output, NumberLines);
        }
        private static void WriteLine(int lineNumber, DiffType typ, string text, StringBuilder output, bool numberLines)
        {
            output.Append("<tr><td>");
            if (lineNumber >= 0 && numberLines)
            {
                output.Append((lineNumber).ToString());
            }
            else
            {
                output.Append("&nbsp;");
            }

            output.Append("<td><span xstyle='width:100%'");

            switch (typ)
            {
                case DiffType.None:
                    break;
                case DiffType.Deleted:
                    output.Append(" style='background-color: red; width: 100%;'");
                    break;
                case DiffType.Inserted:
                    output.Append(" style='background-color: green; width: 100%'");
                    break;
                default:
                    break;
            }


            text = HttpUtility.HtmlEncode(text).Replace("\r", "").Replace(" ", "&nbsp;");
            output.Append(">" + text + "</span></td></tr>\n");
        }
    }
}
