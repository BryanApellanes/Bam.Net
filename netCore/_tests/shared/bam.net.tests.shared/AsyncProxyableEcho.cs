using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Services.Tests
{
    public class AsyncProxyableEcho : AsyncProxyableService
    {
        public AsyncProxyableEcho()
        {
            AppConf = new Server.AppConf();
        }
        public virtual string Send(string value)
        {
            return value;
        }
        public override object Clone()
        {
            AsyncProxyableEcho clone = new AsyncProxyableEcho();
            clone.CopyProperties(this);
            clone.CopyEventHandlers(this);
            return clone;
        }
    }
}
