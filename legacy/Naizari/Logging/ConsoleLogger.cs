/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Naizari.Extensions;

namespace Naizari.Logging
{
    public class ConsoleLogger: Logger
    {
        public ConsoleLogger() : base() { }
        public bool UseColors { get; set; }

        public override void CommitLogEvent(LogEvent logEvent)
        {
            if (UseColors)
            {
                switch (logEvent.Severity)
                {
                    case LogEventType.None:
                        ConsoleExtensions.SetTextColor(ConsoleTextColor.Grey);
                        break;
                    case LogEventType.Information:
                        ConsoleExtensions.SetTextColor(ConsoleTextColor.Grey);
                        break;
                    case LogEventType.Warning:
                        ConsoleExtensions.SetTextColor(ConsoleTextColor.Yellow);
                        break;
                    case LogEventType.Error:
                        ConsoleExtensions.SetTextColor(ConsoleTextColor.Red, false);
                        break;
                    case LogEventType.Fatal:
                        ConsoleExtensions.SetTextColor(ConsoleTextColor.Red, true);
                        break;
                    default:
                        break;
                }
            }
            Console.WriteLine(logEvent.PropertiesToString());
        }
    }
}
