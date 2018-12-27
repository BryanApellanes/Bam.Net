using Bam.Net.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bam.Net.Testing.Specification
{
    public class SpecTestReporter
    {
        Queue<LogMessage> _logMessages;
        public SpecTestReporter()
        {
            _logMessages = new Queue<LogMessage>();
        }

        public LogMessage AddMessage(string format, params string[] args)
        {
            LogMessage message = new LogMessage(format, args);
            _logMessages.Enqueue(message);
            return message;
        }

        public WarningMessage AddWarningMessage(string format, params string[] args)
        {
            WarningMessage message = new WarningMessage(format, args);
            _logMessages.Enqueue(message);
            return message;
        }

        public ErrorMessage AddErrorMessage(string format, Exception ex, params string[] args)
        {
            ErrorMessage message = new ErrorMessage(format, ex, args);
            _logMessages.Enqueue(message);
            return message;
        }
    }
}
