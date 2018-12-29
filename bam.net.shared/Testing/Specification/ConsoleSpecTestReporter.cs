using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net;
using Bam.Net.Logging;
using Bam.Net.CommandLine;

namespace Bam.Net.Testing.Specification
{
    public class ConsoleSpecTestReporter : SpecTestReporter
    {
        public override LogMessage AddMessage(string format, params string[] args)
        {
            CommandLine.CommandLineInterface.OutLineFormat(format, args);
            return base.AddMessage(format, args);
        }

        public override LogMessage AddWarningMessage(string format, params string[] args)
        {
            CommandLineInterface.OutLineFormat(format, ConsoleColor.DarkYellow, args);
            return base.AddWarningMessage(format, args);
        }

        public override LogMessage AddErrorMessage(string format, Exception ex, params string[] args)
        {
            string message = string.Format(format, args);
            message = $"{message}\r\n {ex.GetMessageAndStackTrace()}";
            CommandLineInterface.OutLine(message, ConsoleColor.DarkMagenta);
            return base.AddErrorMessage(format, ex, args);
        }
    }
}
