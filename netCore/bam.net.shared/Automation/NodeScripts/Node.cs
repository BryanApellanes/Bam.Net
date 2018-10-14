using Bam.Net.CommandLine;
using Bam.Net.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Automation.NodeScripts
{
    public class Node
    {
        static string _path;
        static object _pathLock = new object();
        public static string Path
        {
            get
            {
                return _pathLock.DoubleCheckLock(ref _path, () => DefaultConfiguration.GetAppSetting("NodePath", "C:\\Program Files\\nodejs\\node.exe"));
            }
        }

        public static ProcessOutput Run(string script, params string[] args)
        {
            return Run(script, new DirectoryInfo("."), args);
        }

        public static ProcessOutput Run(string script, DirectoryInfo directory, params string[] args)
        {
            return $"{Path} {script}".ToStartInfo(string.Join(" ", args), directory.FullName).RunAndWait();
        }
    }
}
