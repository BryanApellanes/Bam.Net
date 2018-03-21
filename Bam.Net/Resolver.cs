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
            AssemblyResolver = AssemblyResolver ?? ((rea) =>
            {
                WriteLog($"Couldn't resolve assembly: {rea.Name}\r\nRequesting assembly: {rea.RequestingAssembly?.FullName}\r\nRequesting assembly path: {rea.RequestingAssembly?.GetFilePath()}");
                return null;
            });
            AppDomain.CurrentDomain.AssemblyResolve += (sender, args) =>
            {
                WriteLog($"Resolving assembly {args.Name}");
                return Assembly.Load(ResolveAssembly(args));
            };
        }
        
        public static byte[] ResolveAssembly(ResolveEventArgs rea)
        {
            return AssemblyResolver(rea);
        }
        
        public static Func<ResolveEventArgs, byte[]> AssemblyResolver { get; set; }

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
                DateTime local = now.ToLocalTime();
                FileInfo logFile = new FileInfo(".\\Bam.Net.Resolver.log");
                string line = $"[LocalTime({local.ToString()} ms {local.Millisecond}), UtcTime({now.ToString()} ms {now.Millisecond})]::Bam.Net.Resolver::{message}";
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
