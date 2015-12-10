/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wrapify
{
    public class WrapifyConfig
    {
        public WrapifyConfig()
        {
            this.RootFolder = ".";
            this.Prefix = string.Empty;
            this.Suffix = string.Empty;
            this.IgnoreFilePatterns = new string[] { };
            this.IgnoreFolderPatterns = new string[] { };
        }
        public string Prefix { get; set; }
        public string Suffix { get; set; }
        public string RootFolder { get; set; }
        public string TargetFilePattern { get; set; }
        public string[] IgnoreFilePatterns { get; set; }
        public string[] IgnoreFolderPatterns { get; set; }
    }
}
