using Bam.Net.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net
{
    public class BamPaths
    {
        public BamPaths()
        {
            Root = Paths.Root;
            PubRoot = Paths.PubRoot;
            BamDrive = Paths.BamDrive;
            WindowsBamDrive = Paths.WindowsBamDrive;
            Apps = Paths.Apps;
            Local = Paths.Local;
            Content = Paths.Content;
            Conf = Paths.Conf;
            Sys = Paths.Sys;
            Logs = Paths.Logs;
            Tools = Paths.Tools;
        }

        public static BamPaths Get(IDataDirectoryProvider dataProvider)
        {
            return new BamPaths()
            {
                Data = DataPaths.Get(dataProvider)
            };
        }

        public DataPaths Data { get; set; }

        public string Root { get; set; }
        public string PubRoot { get; set; }
        public string BamDrive { get; set; }
        public string WindowsBamDrive { get; set; }

        public string Apps { get; set; }
        public string Local { get; set; }
        public string Content { get; set; }
        public string Conf { get; set; }
        public string Sys { get; set; }
        public string Logs { get; set; }
        public string Tools { get; set; }
        public string NugetPackages { get; set; }

        public string Tests { get; set; }
        public string Builds { get; set; }
    }
}
