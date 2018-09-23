using Bam.Net.ServiceProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Server.ErrorHandling
{
    public abstract class ErrorHandler
    {
        public ErrorHandler()
        {
            ErrorContent = new Dictionary<int, ErrorContent>();
        }

        public Dictionary<int, ErrorContent> ErrorContent { get; set; }

        public abstract Task LoadResponseContent { get; }

        public virtual void OnResponding(IResponse response, int code) { }
        public virtual void OnResponded(IResponse response, int code) { }
        public bool TryRespond(IResponse response, int code)
        {
            LoadResponseContent.Wait();
            if (ErrorContent.ContainsKey(code))
            {
                ErrorContent error = ErrorContent[code];
                OnResponding(response, code);
                Responder.SendResponse(response, error.Content, code, error.Headers);
                OnResponded(response, code);
                return true;
            }
            return false;
        }
    }
}
