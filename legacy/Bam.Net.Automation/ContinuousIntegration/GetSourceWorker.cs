/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Configuration;

namespace Bam.Net.Automation.ContinuousIntegration
{
    public abstract class GetSourceWorker: Worker
    {
        public GetSourceWorker()
            : base()
        {
            this.TagName = "latest";
        }

        public GetSourceWorker(string name)
            : base(name)
        {
            this.TagName = "latest";
        }

        protected internal IBuildSourceControlClient SourceControlClient
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the name of the source control 
        /// Tag to retrieve
        /// </summary>
        public string TagName
        {
            get;
            set;
        }

        public string SourceControlType
        {
            get;
            internal protected set;
        }

        /// <summary>
        /// Gets or sets the path on the server where source 
        /// should be retrieved from.  For Git this is the
        /// repository path, for Tfs this is the team project
        /// branch path
        /// </summary>
        public string ServerSourcePath
        {
            get;
            set;
        }

        public string UserName
        {
            get;
            set;
        }

        public string UserEmail
        {
            get;
            set;
        }

        /// <summary>
        /// Returns true if the TagName is latest
        /// </summary>
        protected bool GetLatest
        {
            get
            {
                return TagName.ToLowerInvariant().Equals("latest");
            }
        }

        public string LocalDirectory
        {
            get;
            set;
        }
        
        protected override WorkState Do()
        {
            WorkState result = new WorkState(this);
            this.CheckRequiredProperties();

            ConfigureClient();

            if (GetLatest)
            {
                SourceControlClient.GetLatest(LocalDirectory);
            }
            else
            {
                SourceControlClient.GetTag(TagName, LocalDirectory);
            }

            return result;
        }

        protected internal abstract void ConfigureClient();

        public override string[] RequiredProperties
        {
            get { return new string[] { "Name", "LocalDirectory", "UserName", "UserEmail", "ServerSourcePath" }; }
        }

    }
}
