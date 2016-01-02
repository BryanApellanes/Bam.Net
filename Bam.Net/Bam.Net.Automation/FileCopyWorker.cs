/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net;
using Bam.Net.Configuration;

namespace Bam.Net.Automation
{
    public abstract class FileCopyWorker: Worker
    {
        public FileCopyWorker() : base() { }
        public FileCopyWorker(string name) : base(name) { }

        public string Source { get; set; }
        public string Destination { get; set; }

        #region IHasRequiredProperties Members

        public override string[] RequiredProperties
        {
            get { return new string[] { "Source", "Destination", "Name" }; }
        }

        #endregion
    }
}
