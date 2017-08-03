using Bam.Net.ServiceProxy;
using Bam.Net.Testing.Tracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Testing.Tracking
{
    public class TestTrackerResponse: ServiceResponse
    {
        public string Uuid { get; set; }
        public CreateStatus CreateStatus { get; set; }
    }
}
