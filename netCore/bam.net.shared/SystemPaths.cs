using Bam.Net.Data;
using Bam.Net.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net
{
    /// <summary>
    /// A class referencing all file system paths of importance to the bam system.
    /// </summary>
    public class SystemPaths
    {
        public SystemPaths()
        {
            Root = Paths.Root;
            PubRoot = Paths.PubRoot;
            SystemDrive = Paths.SystemDrive;
            WindowsBamDrive = Paths.WindowsDrive;
            Apps = Paths.Apps;
            Local = Paths.Local;
            Content = Paths.Content;
            Conf = Paths.Conf;
            Sys = Paths.Sys;
            Generated = Paths.Generated;
            Proxies = Paths.Proxies;
            Logs = Paths.Logs;
            Tools = Paths.Tools;
        }

        public static SystemPaths Get(IDataDirectoryProvider dataProvider)
        {
            return new SystemPaths()
            {
                Data = DataPaths.Get(dataProvider)
            };
        }

        public static SystemPaths Current
        {
            get
            {
                return Get(DefaultDataDirectoryProvider.Current);
            }
        }

        public DataPaths Data { get; set; }

        public string Root { get; set; }
        public string PubRoot { get; set; }
        public string SystemDrive { get; set; }
        public string WindowsBamDrive { get; set; }

        public string Apps { get; set; }
        public string Local { get; set; }
        public string Content { get; set; }
        public string Conf { get; set; }
        public string Sys { get; set; }
        public string Generated { get; set; }
        public string Proxies { get; set; }
        public string Logs { get; set; }
        public string Tools { get; set; }
        public string NugetPackages { get; set; }

        public string Tests { get; set; }
        public string Builds { get; set; }
    }
}
