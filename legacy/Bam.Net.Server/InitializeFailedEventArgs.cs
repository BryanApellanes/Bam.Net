using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Server
{
    public class InitializeFailedEventArgs: EventArgs
    {
        public InitializeFailedEventArgs(Exception ex)
        {
            Exception = ex;
        }

        public string Message
        {
            get
            {
                return Exception.Message;
            }
        }
        public Exception Exception { get; set; }
    }
}
