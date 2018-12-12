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

        public void Log(IRequest request)
        {
            Task.Run(() => HttpLoggingRepository.Save(RequestData.FromRequest(request)));
        }
    }
}
