/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using M = Microsoft.Build.Framework;
using B = Bam.Net.Logging;

namespace Bam.Net.Automation.ContinuousIntegration
{
    public class BuildEventInfo
    {
        public BuildEventInfo() { }
        public BuildEventInfo(M.BuildEventArgs args)
        {
            this.ThreadId = args.ThreadId.ToString();
            this.Timestamp = args.Timestamp.ToString();
            this.SenderName = args.SenderName;
            this.HelpKeyword = args.HelpKeyword;
            this.Message = args.Message;


            if (args.BuildEventContext != null)
            {
                this.BuildRequestId = args.BuildEventContext.BuildRequestId.ToString();
                this.NodeId = args.BuildEventContext.NodeId.ToString();
                this.ProjectContextId = args.BuildEventContext.ProjectContextId.ToString();
                this.ProjectInstanceId = args.BuildEventContext.ProjectInstanceId.ToString();
                this.SubmissionId = args.BuildEventContext.SubmissionId.ToString();
                this.TargetId = args.BuildEventContext.TargetId.ToString();
                this.TaskId = args.BuildEventContext.TaskId.ToString();
            }
        }

        // ba.ThreadId.ToString(),
        public string ThreadId { get; set; }
        //        ba.Timestamp.ToString(),
        public string Timestamp { get; set; }
        //        ba.SenderName,
        public string SenderName { get; set; }
        //        ba.HelpKeyword,
        public string HelpKeyword { get; set; }
        //        ba.Message
        public string Message { get; set; }
        //        ba.BuildEventContext.BuildRequestId.ToString(),
        public string BuildRequestId { get; set; }
        //        ba.BuildEventContext.NodeId.ToString(),
        public string NodeId { get; set; }
        //        ba.BuildEventContext.ProjectContextId.ToString(),
        public string ProjectContextId { get; set; }
        //        ba.BuildEventContext.ProjectInstanceId.ToString(),
        public string ProjectInstanceId { get; set; }
        //        ba.BuildEventContext.SubmissionId.ToString(),
        public string SubmissionId { get; set; }
        //        ba.BuildEventContext.TargetId.ToString(),
        public string TargetId { get; set; }
        //        ba.BuildEventContext.TaskId.ToString(),
        public string TaskId { get; set; }

        
    }
}
