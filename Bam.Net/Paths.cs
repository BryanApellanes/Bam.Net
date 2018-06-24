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
            Builds = @"\\core\share\builds";
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

        public static string Content { get; private set; }
        public static string Conf { get; private set; }
        public static string Sys { get; private set; }
        public static string Logs { get; private set; }
        public static string Data { get; private set; }
        public static string Tools { get; private set; }
        public static string NugetPackages { get; private set; }
        public static string Builds { get; set; }

        private static void SetPaths()
        {
            Content = Path.Combine(Root, "content");
            Conf = Path.Combine(Root, "conf");
            Sys = Path.Combine(Root, "sys");
            Logs = Path.Combine(Root, "logs");
            Data = Path.Combine(Root, "data");
            Tools = Path.Combine(Root, "tools");
            NugetPackages = Path.Combine(Root, "nuget", "packages");
        }
    }
}
