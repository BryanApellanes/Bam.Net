using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using System.Diagnostics;

namespace Bam.Net
{
    public static class Resolver
    {
        public static void Register()
        {
            AppDomain.CurrentDomain.AssemblyResolve += (sender, args) =>
            {
                WriteLog($"Resolving assembly {args.Name}");
                return Assembly.Load(ResolveAssembly(args));
            };
        }
        
        public static byte[] ResolveAssembly(ResolveEventArgs rea)
        {
            // download from BamApps.net; not yet implemented

            WriteLog($"Download not yet implemented: \r\n\t{rea.RequestingAssembly.FullName} =>  \r\n\t\t{rea.Name}");
            return null;
        }
        
        private static void WriteTrace(string message, Exception ex, bool writeLog = true)
        {
            Trace.WriteLine(ex.Message);
            Trace.WriteLine(message);
            Trace.WriteLine(ex.StackTrace);
            if (writeLog)
            {
                WriteLog(message);
            }
        }

        private static void WriteLog(string message)
        {
            try
            {
                DateTime now = DateTime.UtcNow;
                FileInfo logFile = new FileInfo(".\\Bam.Net.Resolver.log");
                string line = $"[Time({now.ToString()} ms {now.Millisecond})]::Bam.Net.Resolver::{message}";
                using (StreamWriter sw = new StreamWriter(logFile.FullName))
                {
                    sw.WriteLine(line);
                }
                Console.WriteLine($"{logFile.FullName}::{line}");
            }
            catch (Exception ex)
            {
                WriteTrace(message, ex, false);
            }
        }
    }

}
