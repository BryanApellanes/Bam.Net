/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using Naizari.Extensions;
using Naizari.Helpers;

namespace Naizari.Logging
{
    public class ConsoleOutLogger: LoggerBase
    {
        public ConsoleOutLogger()
        {
            Basic = true;
        }

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

            if (Basic)
            {
                Console.WriteLine("{0}:{1}:{2}:{3}", logEvent.Severity.ToString(), logEvent.User, logEvent.TimeOccurred.ToLocalTime(), logEvent.Message);
            }
            else
            {
                Console.WriteLine(Debug.PropertiesToString(logEvent));
            }

            ConsoleExtensions.SetTextColor();
        }

        bool basic;
        public bool Basic 
        {
            get
            {
                return basic;
            }
            set
            {
                basic = value;
                detail = !value;
            }
        }

        bool detail;
        public bool Detail
        {
            get
            {
                return detail;
            }
            set
            {
                detail = value;
                basic = !value;
            }
        }

        public bool UseColors { get; set; }
     
    }
}
