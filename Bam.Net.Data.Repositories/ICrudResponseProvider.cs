using Bam.Net.ServiceProxy;

namespace Bam.Net.Data.Repositories
{
    public interface ICrudResponseProvider
    {
        DaoInfo DaoInfo { get; }
        DaoProxyRegistration DaoProxyRegistration { get; }
        IHttpContext HttpContext { get; }

        CrudResponse Create();
        CrudResponse Delete();
        CrudResponse Execute();
        CrudResponse Query();
        CrudResponse Retrieve();
        CrudResponse SaveCollection();
        CrudResponse Update();
    }
}