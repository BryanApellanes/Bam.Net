using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Messaging
{
    public class RequiredSettingsNotSetException: Exception
    {
        public RequiredSettingsNotSetException(string message, Exception innerException) : base(message, innerException) { }
    }
}
