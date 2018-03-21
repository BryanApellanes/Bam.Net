/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Bam.Net.Presentation.Html;
using Bam.Net.Configuration;
using Bam.Net.Profiguration;
using Bam.Net.Services;

namespace Bam.Net.Automation
{
    public class EmailWorker: NotificationWorker
    {
        bool _enableSsl;
        bool _isBodyHtml;

        public EmailWorker()
            : base()
        {
            Init();
        }

        public EmailWorker(string name)
            : base(name)
        {
            Init();
        }

        private void Init()
        {
            this._isBodyHtml = true;
            this.SubjectFormat = "Job: {JobName} (Worker: {WorkName})";
            this.BodyFormat = "Status: {Status}<br />Message: {Message}";
        }

        protected override WorkState Do(WorkState currentWorkState)            
        {
            this.CheckRequiredProperties();

            WorkState result = new WorkState(this)
            {
                Status = Status.Succeeded,
                PreviousWorkState = currentWorkState
            };

            object formatValues = new
            {
                JobName = Job.Name.Or("[JobName Not Set]"),
                WorkName = Job.CurrentWorkerName.Or("[WorkName Not Specified]"),
                Status = Job.CurrentWorkState.Status.ToString(),
                Message = Job.CurrentWorkState.Message.Or("&nbsp;")
            };

            Email email = new Email();
            email.Server(SmtpHost)
                .Port(int.Parse(Port))
                .IsBodyHtml(_isBodyHtml)
                .From(From)
                .To(Recipients.DelimitSplit(",", ";"))
                .Subject(SubjectFormat.NamedFormat(formatValues))
                .Body(BodyFormat.NamedFormat(formatValues))
                .UserName(UserName)
                .Password(Password)
                .EnableSsl(_enableSsl)
                .Send();

            return result;
        }

        [Label("From")]
        public string From
        {
            get;
            set;
        }

        public string SmtpUserName
        {
            get;
            set;
        }

        public string EnableSsl
        {
            get
            {
                return _enableSsl.ToString();
            }
            set
            {
                _enableSsl = bool.Parse(value);
            }
        }
        
        public string IsBodyHtml
        {
            get
            {
                return _isBodyHtml.ToString();
            }
            set
            {
                _isBodyHtml = bool.Parse(value);
            }
        }

        [Label("Subject Format")]
        public string SubjectFormat
        {
            get;
            set;
        }

        [Label("Body Format")]
        public string BodyFormat
        {
            get;
            set;
        }

        [Label("Smtp Host")]
        public string SmtpHost
        {
            get;
            set;
        }


        [Label("Port")]
        public string Port
        {
            get;
            set;
        }

        [Label("Email User Name")]
        public string UserName
        {
            get;
            set;
        }

        [Label("Password")]
        [Password]
        public string Password
        {
            get
            {
                return JobConductorService.SecureGet("{0}_{1}"._Format(typeof(EmailWorker).Name, Name));
            }
            set
            {
                JobConductorService.SecureSet("{0}_{1}"._Format(typeof(EmailWorker).Name, Name), value);
            }
        }


        public override string[] RequiredProperties
        {
            get { return new string[] { "From", "SmtpHost", "UserName", "Password" }; }
        }
    }
}
