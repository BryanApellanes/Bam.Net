using Bam.Net.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net
{
    public static class Paths
    { 
        static Paths()
        {
            Root = "C:\\bam";

            SystemDrive = "/b/drive"; // should be mapped to PubRoot (net use b: \\bam\public)
            WindowsBamDrive = "B:\\drive";
        }

        static string _root;
        public static string Root
        {
            get { return _root; }
            set
            {
                _root = value;
                SetPaths();
            }
        }

        static string _pubRoot = @"\\bam\public";
        public static string PubRoot
        {
            get
            {
                return _pubRoot;
            }
            set
            {
                _pubRoot = value;
            }
        }

        public static string SystemDrive { get; set; }
        public static string WindowsBamDrive { get; set; }

        public static string Apps { get; private set; }
        public static string Local { get; private set; }
        public static string Content { get; private set; }
        public static string Conf { get; private set; }
        public static string Sys { get; private set; }
        
        public static string Logs { get; private set; }
        public static string Data { get; private set; }
        public static string Tools { get; private set; }
        
        public static string Tests { get; private set; }
        public static string Builds { get; set; }
        public static string NugetPackages { get; private set; }

        public static string AppData
        {
            get { return RuntimeSettings.AppDataFolder; }
            set { RuntimeSettings.AppDataFolder = value; }
        }

        private static void SetPaths()
        {
            Apps = Path.Combine(Root, "apps");
            Local = Path.Combine(Root, "local");
            Content = Path.Combine(Root, "content");
            Conf = Path.Combine(Root, "conf");
            Sys = Path.Combine(Root, "sys");
            Logs = Path.Combine(Root, "logs");
            Data = Path.Combine(Root, "data");
            Tools = Path.Combine(Root, "tools");
            Tests = Path.Combine(Root, "tests");
            NugetPackages = Path.Combine(Root, "nuget", "packages");

            Builds = Path.Combine(PubRoot, "Builds");            
        }
    }
}
