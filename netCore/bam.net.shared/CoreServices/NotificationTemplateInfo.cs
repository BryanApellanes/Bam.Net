using Bam.Net.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Bam.Net.Logging;

namespace Bam.Net.CoreServices
{
    public class NotificationTemplateInfo
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Subject { get; set; }
        public bool IsHtml { get; set; }
        /// <summary>
        /// Used to specify template content when adding a new template.
        /// </summary>
        public string Content { get; set; }
        public override string ToString()
        {
            return this.ToJson(true);
        }
        public override int GetHashCode()
        {
            return this.ToJson().ToSha1Int();
        }
        public override bool Equals(object obj)
        {
            if(obj is NotificationTemplateInfo temp)
            {
                return temp.Name.Equals(Name) &&
                    temp.Description.Equals(Description) &&
                    temp.Subject.Equals(Subject) &&
                    temp.IsHtml.Equals(IsHtml);
            }
            return false;
        }

        public void Save(string filePath, bool overwrite = false)
        {
            this.ToJson(true, NullValueHandling.Ignore).SafeWriteToFile(filePath, overwrite);
        }        
    }
}
