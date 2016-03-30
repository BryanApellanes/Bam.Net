/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bam.Net.Logging;
using Bam.Net;
using System.Runtime.Serialization;

namespace Bam.Net.CommandLine
{
    public class ConsoleLogger : Logger
    {
        public ConsoleLogger()
            : base()
        {
            AddDetails = true;
            UseColors = true;
            ShowTime = true;
        }
		
        public bool UseColors { get; set; }
        public bool AddDetails { get; set; }

        /// <summary>
        /// If true the Local time will prefix the output
        /// </summary>
        public bool ShowTime { get; set; }

        protected override StringBuilder HandleDetails(LogEvent ev)
        {
            if (AddDetails)
            {
                return base.HandleDetails(ev);
            }
            else
            {
                return new StringBuilder(ev.Message);
            }
        }

        public override void CommitLogEvent(LogEvent logEvent)
        {
            if (AddDetails)
            {
                ShowDetails(logEvent);
            }

            if (UseColors)
            {
                switch (logEvent.Severity)
                {
                    case LogEventType.None:
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        break;
                    case LogEventType.Information:
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        break;
                    case LogEventType.Warning:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        break;
                    case LogEventType.Error:
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        break;
                    case LogEventType.Fatal:
                        Console.ForegroundColor = ConsoleColor.Red;
                        break;
                }
            }
            StringBuilder time = GetTimeString(logEvent);
            Console.WriteLine($"{time.ToString()}{logEvent.Message}");
            Console.ResetColor();
        }

        private StringBuilder GetTimeString(LogEvent logEvent)
        {
            StringBuilder time = new StringBuilder();
            DateTime utc, local;
            GetUtcAndLocalTimes(logEvent.Time, out utc, out local);
            if (ShowTime)
            {
                time.Append($"Time({local.ToString()} ms {local.Millisecond})");
            }
            if (time.Length > 0)
            {
                time.Insert(0, "[");
                time.Append("]:");
            }

            return time;
        }

        private static void GetUtcAndLocalTimes(DateTime input, out DateTime utc, out DateTime local)
        {
            utc = input;
            local = input;
            switch (input.Kind)
            {
                case DateTimeKind.Utc:
                    local = input.ToLocalTime();
                    break;
                case DateTimeKind.Local:
                    utc = input.ToUniversalTime();
                    break;
            }
        }

        private static void ShowDetails(LogEvent logEvent)
        {
            Console.WriteLine(logEvent.Severity.ToString());
            Console.WriteLine("Computer: {0}", logEvent.Computer);
            string[] split = logEvent.Message.Split(new string[] { "~~" }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < split.Length - 1; i++)
            {
                Console.WriteLine(split[i]);
            }
        }
    }
}
