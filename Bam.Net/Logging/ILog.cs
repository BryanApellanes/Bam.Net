using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Logging
{
    public interface ILog
    {
        void Info(string messageSignature, params object[] args);
        void Warning(string messageSignature, params object[] args);
        void Error(string messageSignature, params object[] args);
    }
}
