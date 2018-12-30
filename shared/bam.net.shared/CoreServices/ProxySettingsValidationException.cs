using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.CoreServices
{
    [Serializable]
    public class ProxySettingsValidationException: Exception
    {
        public ProxySettingsValidationException(SerializationInfo info, StreamingContext context) { }
        public ProxySettingsValidationException(ProxySettingsValidation validation) : base(validation.Message) { }
    }
}
