using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Server.ErrorHandling
{
    // TODO: revisit this and read related headers to determine what format
    // to return
    public class NotFoundJsonResponse: ErrorContent
    {
        public NotFoundJsonResponse() : base(404)
        {
            Content = new { data = "Not Found" }.ToJson().ToBytes();
        }
    }
}
