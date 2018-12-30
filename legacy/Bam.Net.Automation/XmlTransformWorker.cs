/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Bam.Net.Automation
{
    public class XmlTransformWorker: Worker
    {
        public XmlTransformWorker() : base() { }
        public XmlTransformWorker(string name) : base(name) { }

        public string SourceFile { get; set; }
        public string TransformFile { get; set; }
        public string DestinationFile { get; set; }

        public override string[] RequiredProperties
        {
            get
            {
                return new string[] { "SourceFile", "TransformFile", "DestinationFile" };
            }
        }

        protected override WorkState Do(WorkState previousWorkState)
        {
            WorkState workstate = new WorkState(this) { PreviousWorkState = previousWorkState };
            bool success = new FileInfo(SourceFile).TransformXml(TransformFile, DestinationFile);
            workstate.Status = success ? Status.Succeeded : Status.Failed;
            return workstate;
        }
    }
}
