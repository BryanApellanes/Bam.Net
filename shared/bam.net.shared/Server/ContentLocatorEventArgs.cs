/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Server
{
    public class ContentLocatorEventArgs: EventArgs
    {
        public ContentLocatorEventArgs(string appName, string originalPath, string foundAt)
        {
            this.AppName = appName;
            this.OriginalPath = originalPath;
            this.FoundAtPath = foundAt;
        }

        public string AppName { get; set; }
        public string OriginalPath { get; set; }
        public string FoundAtPath { get; set; }
    }
}
