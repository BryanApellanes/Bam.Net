using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.Qi;
using Bam.Net.ServiceProxy;

namespace Bam.Net.Data.Repositories
{
    public class DaoCrudResponseProvider : ICrudResponseProvider
    {
        public DaoCrudResponseProvider(DaoProxyRegistration daoProxyRegistration, IHttpContext httpContext)
        {
            DaoProxyRegistration = daoProxyRegistration;
            HttpContext = httpContext;
            DaoInfo = DaoInfo.FromContext(httpContext);
            Methods = new Dictionary<CrudMethods, Func<CrudResponse>>
            {
                { CrudMethods.Invalid, () => new CrudResponse { Success = false, Message = "Invalid CrudMethod specified" } },
                { CrudMethods.Create, () => Try(Create).Result },
                { CrudMethods.Retrieve, () => Try(Retrieve).Result },
                { CrudMethods.Update, () => Try(Update).Result },
                { CrudMethods.Delete, () => Try(Delete).Result },
                { CrudMethods.Query, () => Try(Query).Result },
                { CrudMethods.SaveCollection, () => Try(SaveCollection).Result }
            };
        }

        public CrudResponse Create()
        {
            return Create(this);
        }

        public CrudResponse Retrieve()
        {
            return Retrieve(this);
        }

        public CrudResponse Update()
        {
            return Update(this);
        }

        public CrudResponse Delete()
        {
            return Delete(this);
        }

        public CrudResponse Query()
        {
            return Query(this);
        }

        public CrudResponse SaveCollection()
        {
            return SaveCollection(this);
        }

        public CrudResponse Create(ICrudResponseProvider dcp)
        {
            Type daoType = GetDaoType(dcp);
            Database database = dcp.DaoProxyRegistration.Database;
            MethodInfo daoMethod = daoType.GetMethod("Insert");
            object instance = GetRequestBody(HttpContext.Request).FromJson(daoType);
            daoMethod.Invoke(instance, new object[] { database });
            return new CrudResponse { CxName = Dao.ConnectionName(daoType), Success = true, Dao = instance.ToJsonSafe() };
        }

        public CrudResponse Retrieve(ICrudResponseProvider dcp)
        {
            Type daoType = GetDaoType(dcp);
            Database database = dcp.DaoProxyRegistration.Database;
            MethodInfo daoMethod = daoType.GetMethod("GetById", new Type[] { typeof(long), typeof(Database) });
            long id = GetRequestBody(HttpContext.Request).FromJson<long>();
            return new CrudResponse { CxName = Dao.ConnectionName(daoType), Success = true, Dao = daoMethod.Invoke(null, new object[] { id, database }).ToJsonSafe() };
        }

        public CrudResponse Update(ICrudResponseProvider dcp)
        {
            Type daoType = GetDaoType(dcp);
            Database database = dcp.DaoProxyRegistration.Database;
            MethodInfo daoMethod = daoType.GetMethod("Update", new Type[] { typeof(Database) });
            object instance = GetRequestBody(HttpContext.Request).FromJson(daoType);
            daoMethod.Invoke(instance, new object[] { database });
            return new CrudResponse { CxName = Dao.ConnectionName(daoType), Success = true, Dao = instance.ToJsonSafe() };
        }

        public CrudResponse Delete(ICrudResponseProvider dcp)
        {
            Type daoType = GetDaoType(dcp);
            Database database = dcp.DaoProxyRegistration.Database;
            MethodInfo daoMethod = daoType.GetMethod("Delete", new Type[] { typeof(Database) });
            object instance = GetRequestBody(HttpContext.Request).FromJson(daoType);
            daoMethod.Invoke(instance, new object[] { database });
            return new CrudResponse { CxName = Dao.ConnectionName(daoType), Success = true, Dao = instance.ToJsonSafe() };
        }

        public CrudResponse Query(ICrudResponseProvider dcp)
        {
            Type daoType = GetDaoType(dcp);
            Database database = dcp.DaoProxyRegistration.Database;
            MethodInfo daoMethod = daoType.GetMethod("Where", new Type[] { typeof(QiQuery), typeof(Database)});
            QiQuery query = GetRequestBody(HttpContext.Request).FromJson<QiQuery>();
            IEnumerable results = (IEnumerable)daoMethod.Invoke(null, new object[] { query, database });
            return new CrudResponse { CxName = Dao.ConnectionName(daoType), Success = true, Dao = results.ToJsonSafe() };
        }

        public CrudResponse SaveCollection(ICrudResponseProvider dcp)
        {
            Type daoType = GetDaoType(dcp);
            Database database = dcp.DaoProxyRegistration.Database;
            Type collectionType = daoType.Assembly.GetType("{0}.{1}Collection"._Format(daoType.Namespace, daoType.Name));
            IEnumerable values = (IEnumerable)GetRequestBody(HttpContext.Request).FromJson(daoType.MakeArrayType());
            object collection = collectionType.Construct();
            collection.Invoke("AddRange", values);
            MethodInfo saveMethod = collectionType.GetMethod("Save", new Type[] { typeof(Database) });
            saveMethod.Invoke(collection, new object[] { database });
            return new CrudResponse { CxName = Dao.ConnectionName(daoType), Success = true, Dao = collection.ToJsonSafe() };
        }

        protected Dictionary<CrudMethods, Func<CrudResponse>> Methods { get; }

        public IHttpContext HttpContext
        {
            get;
        }

        public DaoProxyRegistration DaoProxyRegistration
        {
            get;
        }

        public DaoInfo DaoInfo
        {
            get;
        }
        
        public CrudResponse Execute()
        {
            return Methods[DaoInfo.Method]();
        }

        private string GetRequestBody(IRequest request)
        {
            string result = string.Empty;
            using (StreamReader sr = new StreamReader(request.InputStream))
            {
                result = sr.ReadToEnd();
            }

            return result;
        }

        private static Type GetDaoType(ICrudResponseProvider dcp)
        {
            string daoName = dcp.DaoInfo.DaoName;
            Type daoType = dcp.DaoProxyRegistration.ServiceProvider[daoName];
            return daoType;
        }

        private Task<CrudResponse> Try(Func<CrudResponse> func)
        {
            try
            {
                return Task.Run(() => func());
            }
            catch (Exception ex)
            {
                return Task.FromResult(new CrudResponse { Success = false, Message = ex.Message });
            }
        }
    }
}
