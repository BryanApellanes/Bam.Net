/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net;
using Bam.Net.Web;

namespace Bam.Net.Automation
{
    public class FtpUploadWorker: FileCopyWorker
    {
        public FtpUploadWorker() : base() { }

        public FtpUploadWorker(string name) : base(name) { }

        public string UserName { get; set; }
        public string Password { get; set; }
        protected override WorkState Do(WorkState currentWorkState)
        {
            WorkState result = new WorkState(this)
            {
                Message = "({0}) uploaded to ({1}) successfully"._Format(Source, Destination),
                PreviousWorkState = currentWorkState
            };
            Ftp.Server(Destination).UserName(UserName).Password(Password).Upload(Source);

            return result;
        }

        public override string[] RequiredProperties
        {
            get
            {
                List<string> props = new List<string>();
                props.AddRange(new string[] { "UserName", "Password" });
                props.AddRange(base.RequiredProperties);
                return props.ToArray();
            }
        }        

    }
}
