using Bam.Net.Logging.Http.Data;
using Bam.Net.Logging.Http.Data.Dao.Repository;
using Bam.Net.ServiceProxy;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Logging.Http
{
    public class RequestLog
    {
        public RequestLog(IUserResolver userResolver, HttpLoggingRepository repository)
        {
            HttpLoggingRepository = repository;
            UserResolver = userResolver;
        }

        public IUserResolver UserResolver { get; }
        public HttpLoggingRepository HttpLoggingRepository { get; }

        public void Log(IHttpContext context)
        {
            Task.Run(() =>
            {
                IRequest request = context.Request;
                RequestData reqeustData = HttpLoggingRepository.Save(RequestData.FromRequest(request));
                UserData user = new UserData { UserName = UserResolver.GetUser(context), RequestCuid = reqeustData.Cuid };
            });
        }
    }
}
