using Bam.Net.ServiceProxy;
using Bam.Net.Web;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Bam.Net.CoreServices.Tests
{
    [ServiceRegistryContainer]
    public class CoreServicesTestRegistry
    {
        static Dictionary<ProcessModes, Func<ServiceRegistry>> _factories;
        static CoreServicesTestRegistry()
        {
            _factories = new Dictionary<ProcessModes, Func<ServiceRegistry>>
            {
                { ProcessModes.Dev, Dev },
                { ProcessModes.Test, Test },
                { ProcessModes.Prod, Prod }
            };
        }

        [ServiceRegistryLoader(nameof(CoreServicesTestRegistry))]
        public static ServiceRegistry GetServiceRegistry()
        {
            return _factories[ProcessMode.Current.Mode]();
        }

        public static ServiceRegistry Dev()
        {
            ServiceRegistry registry = Base();

            // customize for dev

            return registry;
        }

        public static ServiceRegistry Test()
        {
            ServiceRegistry registry = Base();

            // customize for dev

            return registry;
        }

        public static ServiceRegistry Prod()
        {
            ServiceRegistry registry = Base();

            // customize for dev

            return registry;
        }


        public static ServiceRegistry Base()
        {
            ApplicationRegistration.Data.Dao.Repository.ApplicationRegistrationRepository appRepo = new ApplicationRegistration.Data.Dao.Repository.ApplicationRegistrationRepository();
            appRepo.EnsureDaoAssemblyAndSchema();

            Auth.Data.Dao.Repository.AuthRepository oauthRepo = new Auth.Data.Dao.Repository.AuthRepository();
            oauthRepo.EnsureDaoAssemblyAndSchema();

            IHttpContext ctx = Substitute.For<IHttpContext>();
            ctx.Request = Substitute.For<IRequest>();
            ctx.Request.Headers.Returns(new NameValueCollection
            {
                { Headers.ApplicationName, "CoreServicesTests" }
            });
            ctx.Request.Cookies.Returns(new System.Net.CookieCollection());
            ctx.Response = Substitute.For<IResponse>();
            ctx.Response.Cookies.Returns(new System.Net.CookieCollection());

            ServiceRegistry registry = new ServiceRegistry()
                .For<ApplicationRegistration.Data.Dao.Repository.ApplicationRegistrationRepository>().Use(appRepo)
                .For<Auth.Data.Dao.Repository.AuthRepository>().Use<Auth.Data.Dao.Repository.AuthRepository>() // not necessary since it has a parameterless ctor
                .For<AuthSettingsService>().Use<AuthSettingsService>()
                .For<IHttpContext>().Use(ctx);

            return registry;
        }
    }
}
