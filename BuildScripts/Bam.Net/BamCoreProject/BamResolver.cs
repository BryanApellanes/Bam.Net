﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;
using System.Diagnostics;

public static class BamResolver
{
    public static void Register(bool downloadIfNotFound = true)
    {
        AppDomain.CurrentDomain.AssemblyResolve += (sender, args) =>
        {            
            WriteLog($"Resolving assembly {args.Name}");
            string resourceName = $"BamCore.{new AssemblyName(args.Name).Name}.dll";
            try
            {
                using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
                {
                    byte[] assemblyData = new byte[stream.Length];
                    stream.Read(assemblyData, 0, assemblyData.Length);
                    return Assembly.Load(assemblyData);
                }
            }
            catch (Exception ex)
            {
                WriteTrace($"Failed to load embedded resource: {args.Name}", ex);
                if (downloadIfNotFound)
                {
                    return Assembly.Load(Download(args.Name));
                }
                return null;
            }
        };     
    }

    public static byte[] Download(string resourceName)
    {
        // download from BamApps.net; not yet implemented
        WriteLog($"Download not yet implemented: {resourceName}");
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
            DateTime now = DateTime.Now;
            FileInfo logFile = new FileInfo(".\\BamCore.log");
            string line = $"[Time({now.ToString()} ms {now.Millisecond})]::BamCore::{message}";
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
