using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Server.ErrorHandling
{
    // TODO: revisit this and read related headers to determine what format
    // to return
    public class NotFoundHtmlResponse: ErrorContent
    {
        public NotFoundHtmlResponse() : base(404)
        {
            Content = "<h1>Not Found</h1>".ToBytes();
        }
    }
}
