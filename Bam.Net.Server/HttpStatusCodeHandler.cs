using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Server
{
    public class HttpStatusCodeHandler
    {
        public HttpStatusCodeHandler()
        {
            DefaultResponse = "OK";
            Code = 200;
            Handle = () => DefaultResponse;
        }
        public HttpStatusCodeHandler(int code, string defaultResponse)
        {
            DefaultResponse = defaultResponse;
            Code = code;
            Handle = () => DefaultResponse;
        }
        public HttpStatusCodeHandler(int code, Func<string> handler)
        {
            Code = code;
            Handle = handler;
        }
        string _defaultResponse;
        public string DefaultResponse
        {
            get
            {
                if (string.IsNullOrEmpty(_defaultResponse) && Handle != null)
                {
                    _defaultResponse = Handle();
                }
                return _defaultResponse;
            }
            set
            {
                _defaultResponse = value;
            }
        }

        public int Code { get; set; }
        public Func<string> Handle { get; set; }

        public static HttpStatusCodeHandler Get(int code)
        {
            if (Defaults.ContainsKey(code))
            {
                return Defaults[code];
            }
            return new HttpStatusCodeHandler(code, string.Empty);
        }

        static Dictionary<int, HttpStatusCodeHandler> _defaults;
        static object _defaultLock = new object();
        public static Dictionary<int, HttpStatusCodeHandler> Defaults
        {
            get
            {
                return _defaultLock.DoubleCheckLock(ref _defaults, () =>
                {
                    return new Dictionary<int, HttpStatusCodeHandler>
                    {
                        { 200, new HttpStatusCodeHandler() },
                        { 400, new HttpStatusCodeHandler(400, "Bad Request") },
                        { 401, new HttpStatusCodeHandler(401, "Unauthorized") },
                        { 404, new HttpStatusCodeHandler(404, "Not Found") },
                        { 500, new HttpStatusCodeHandler(500, "Server Error") }
                    };
                });
            }
        }
    }
}
