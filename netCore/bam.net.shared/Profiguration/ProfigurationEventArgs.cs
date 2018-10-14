/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Bam.Net.Profiguration
{
    public class ProfigurationEventArgs: EventArgs
    {
        public ProfigurationEventArgs(DirectoryInfo rootDir)
        {
            this.RootDirectory = rootDir;
        }

        public DirectoryInfo RootDirectory { get; set; }
    }
}
