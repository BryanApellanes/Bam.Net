using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.ServiceProxy
{
    public static class Extensions
    {
        public static string GetClientIp(this IHttpContext context)
        {
            return GetClientIp(context?.Request);
        }

        public static string GetClientIp(this IRequest request)
        {
            return request?.Headers["X-Forwarded-For"]
                        .Or(request?.Headers["Remote-Addr"])
                        .Or(request?.UserHostAddress);
        }
    }
}
