/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data;

namespace Bam.Net.ServiceProxy.Secure
{
    /// <summary>
    /// Class used to manage Api Keys.
    /// </summary>
    [Encrypt]
    [Proxy("apiKeyMgr")]
    public class LocalApiKeyManager : IRequiresHttpContext
    {
        public LocalApiKeyManager()
        {
            this.UserResolver = DefaultUserResolver.Instance;
        }

        static LocalApiKeyManager _default;
        static object _defaultLock = new object();
        public static LocalApiKeyManager Default
        {
            get
            {
                return _defaultLock.DoubleCheckLock(ref _default, () =>
                {
                    return new LocalApiKeyManager();
                });
            }
            set
            {
                _default = value;
            }
        }
        [Exclude]
        public object Clone()
        {
            LocalApiKeyManager clone = new LocalApiKeyManager();
            clone.CopyProperties(this);
            return clone;
        }

        /// <summary>
        /// The component used to resolve the current user
        /// or the user of a specified IHttpContext based
        /// on the session cookie therein
        /// </summary>
        public IUserResolver UserResolver
        {
            get;
            protected internal set;
        }

        public IHttpContext HttpContext
        {
            get;
            set;
        }

		Database _database;
		public Database Database
		{
			get
			{
				if (_database == null)
				{
					_database = Db.For<ApiKey>();
				}

				return _database;
			}
			set
			{
				_database = value;
			}
		}

        public string GetClientId(IApplicationNameProvider nameProvider)
        {
            return GetClientId(nameProvider.GetApplicationName());
        }

        public static string GetClientId(string applicationName)
        {
            return "{0}:{1}"._Format(applicationName, applicationName.Sha1());
        }

        public string GetApplicationApiKey(string applicationClientId, int index)
        {
            ApiKeyCollection keys = ApiKey.Where(c => c.ClientId == applicationClientId, Order.By<ApiKeyColumns>(c => c.CreatedAt, SortOrder.Descending), Database);
            return keys[index].SharedSecret;
        }

        public ApplicationCreateResult CreateApplication(string applicationName)
        {
            return CreateApplication(HttpContext, UserResolver, applicationName, Database);
        }

        public static ApplicationCreateResult CreateApplication(IHttpContext context, IUserResolver userResolver, string applicationName, Database database = null)
        {
            ApplicationCreateResult result = new ApplicationCreateResult();
            try
            {
                Application app = Application.OneWhere(c => c.Name == applicationName, database);
                if (app != null)
                {
                    result.Status = ApplicationCreateStatus.NameInUse;
                }
                else
                {
                    string createdBy = userResolver.GetUser(context);
                    if (string.IsNullOrEmpty(createdBy))
                    {
                        createdBy = userResolver.GetCurrentUser();
                        if (string.IsNullOrEmpty(createdBy))
                        {
                            throw new UnableToResolveUserException(userResolver);
                        }
                    }

                    app = new Application
                    {
                        Name = applicationName
                    };
                    app.Save(database);
                    AddKey(app, userResolver, context);

                    result.Application = app;
                    result.Status = ApplicationCreateStatus.Success;
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.Status = ApplicationCreateStatus.Error;
            }

            return result;
        }

        public Application GetApplication(string applicationClientId)
        {
            string[] split = applicationClientId.DelimitSplit(":");
            if (split.Length != 2)
            {
                throw new ArgumentException("The specified applicationClientId is not valid: {0}"._Format(applicationClientId));
            }

            return Application.OneWhere(c => c.Name == split[0], Database);
        }

        public ApiKey AddKey(string applicationClientId)
        {
            return AddKey(GetApplication(applicationClientId), UserResolver, HttpContext, Database);
        }

        public static ApiKey AddKey(Application app, IUserResolver userResolver, IHttpContext context, Database database = null)
        {
            ApiKey key = app.ApiKeysByApplicationId.AddNew();
            key.ClientId = GetClientId(app.Name);
            key.Disabled = false;
            key.SharedSecret = ServiceProxySystem.GenerateId();
            key.CreatedBy = userResolver.GetUser(context);
            key.CreatedAt = new Instant();
            key.Save(database);
            return key;
        }
    }
}
