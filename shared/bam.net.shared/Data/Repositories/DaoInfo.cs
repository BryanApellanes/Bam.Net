using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.ServiceProxy;

namespace Bam.Net.Data.Repositories
{
    public class DaoInfo
    {
        public DaoInfo() { }
        public DaoInfo(string connectionName, CrudMethods method, string daoName)
        {
            ConnectionName = connectionName;
            Method = method;
            DaoName = daoName;
        }
        public string ConnectionName { get; set; }
        public CrudMethods Method { get; set; }
        public string DaoName { get; set; }
        public static Func<Uri, DaoInfo> DefaultUrlParser
        {
            get
            {
                return (uri) => 
                {
                    string[] chunks = uri.AbsolutePath.DelimitSplit("/");
                    if(chunks.Length == 4 && chunks[0].Equals("dao", StringComparison.InvariantCultureIgnoreCase))
                    {
                        if (Enum.TryParse<CrudMethods>(chunks[2], out CrudMethods method))
                        {
                            return new DaoInfo { ConnectionName = chunks[1], Method = method, DaoName = chunks[3] };
                        }
                    }
                    return new DaoInfo();
                };
            }
        }
          
        public static DaoInfo FromContext(IHttpContext context)
        {
            return FromRequest(context.Request);
        }

        public static DaoInfo FromRequest(IRequest request)
        {
            return FromUri(request.Url);
        }

        public static DaoInfo FromUri(Uri uri)
        {
            return DefaultUrlParser(uri);
        }
    }
}
