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
        public RequestLog()
        {
            HttpLoggingRepository = new HttpLoggingRepository();
        }

        public RequestLog(IUserResolver userResolver, HttpLoggingRepository repository)
        {
            HttpLoggingRepository = repository;
            UserResolver = userResolver;
        }

        public IUserResolver UserResolver { get; }
        public HttpLoggingRepository HttpLoggingRepository { get; }

        public void LogRequest(IHttpContext context)
        {
            Task.Run(() =>
            {
                IRequest request = context.Request;
                RequestData requestData = HttpLoggingRepository.Save(RequestData.FromRequest(request));
                string userName = UserResolver.GetUser(context);
                ulong userNameHash = userName.ToSha512ULong();
                UserHashData dataMap = HttpLoggingRepository.GetOneUserHashDataWhere(uhd => uhd.UserName == userName && uhd.UserNameHash == userNameHash);
                HttpLoggingRepository.SetOneUserDataWhere(ud => ud.UserNameHash == userNameHash && ud.RequestCuid == requestData.Cuid);
            });
        }
    }
}
