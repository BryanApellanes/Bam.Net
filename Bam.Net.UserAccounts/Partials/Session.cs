/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Bam.Net.Encryption;
using Bam.Net.ServiceProxy;
using System.Web.Mvc;
using Org.BouncyCastle.Security;
using Bam.Net.Logging;

namespace Bam.Net.UserAccounts.Data
{
    [Proxy("session")]
    public partial class Session: IRequiresHttpContext
    {
        public static string CookieName
        {
            get
            {
                return "SESS";
            }
        }

        public static Session Init(IHttpContext context)
        {
            Session session = Get(context);
            if (session.UserId != User.Anonymous.Id)
            {
                context.User = new DaoPrincipal(session.UserOfUserId, session.UserOfUserId.IsAuthenticated);
            }

            return session;
        }

        public static Session Get(IHttpContext context)
        {
            Args.ThrowIfNull(context, "context");

            return Get(context.Request, context.Response);    
        }

        public static Session Get(IRequest request, IResponse response)
        {
            Cookie sessionIdCookie = request.Cookies[CookieName];
            Session session = null;
            string identifier = string.Empty;
            if (sessionIdCookie != null)
            {
                identifier = sessionIdCookie.Value;
                session = Session.OneWhere(c => c.Identifier == identifier);
            }

            if (session == null)
            {
                session = Create(response, identifier);
            }
            else
            {
                session.LastActivity = DateTime.UtcNow;
                session.Save();
            }
            return session;
        }

        public void End(ILogger logger = null) // TODO: Write unit test
        {
            try
            {
                if (logger == null)
                {
                    logger = Log.Default;
                }

                User user = UserOfUserId;
                if (user == null)
                {
                    user = User.Anonymous;
                }
                logger.AddEntry("Ending session ({0}) for user ({1})", this.Id.ToString(), user.UserName);

                this.Identifier = this.Identifier + "-Ended";
                this.IsActive = false;
                this.Save();
            }
            catch (Exception ex)
            {
                logger.AddEntry("Exception occurred ending session ({0})", ex, this.Id.ToString());
            }
        }

        public IHttpContext HttpContext
        {
            get;
            set;
        }

        public static Session Get(string userName, bool isActive = true)
        {
            User user = User.GetByUserNameOrDie(userName);
            return Get(user, isActive);
        }

        /// <summary>
        /// Get a Session instance for the specified userName, null will 
        /// be returned if it doesn't exist
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="isActive"></param>
        /// <returns></returns>
        public static Session Get(User user, bool isActive = true)
        {
            Session session = user.SessionsByUserId.FirstOrDefault();
            DateTime now = DateTime.UtcNow;

            if(session != null)
            {
                if (isActive)
                {
                    session.Touch(false); // save:false because save is called below
                }
                else
                {
                    session.IsActive = false;
                }

                session.Save();            
            }

            return session;
        }

        internal static Session Create(IResponse response, string identifier = "", bool isActive = true)
        {
            if (string.IsNullOrEmpty(identifier))
            {
                identifier = GenId();
            }

            Session session = new Session();
            session.Identifier = identifier;
            DateTime now = DateTime.UtcNow;
            session.CreationDate = now;
            session.LastActivity = now;
            session.IsActive = isActive;
            session.UserId = User.Anonymous.Id;
            session.Save();

            Cookie cookie = new Cookie(CookieName, session.Identifier);
            response.Cookies.Add(cookie);
            return session;
        }

        
        protected internal static string GenId()
        {
            SecureRandom random = new SecureRandom();
            return random.GenerateSeed(64).ToBase64().Sha256();
        }
        /// <summary>
        /// Updates the LastActivity property and sets IsActive to true
        /// </summary>
        /// <param name="save"></param>
        public void Touch(bool save = true)
        {
            DateTime now = DateTime.UtcNow;
            LastActivity = now;
            IsActive = true;

            if (save)
            {
                Save();
            }
        }

        public T Get<T>(string key)
        {
            object value = this[key];
            if (value != null)
            {
                return (T)value;
            }

            return default(T);
        }

        public void Set(string key, object value)
        {
            this[key] = value;
        }

        public object this[string key] 
        {
            get
            {
                SessionState state = this.SessionStatesBySessionId.Where(ss => ss.Name == key).FirstOrDefault();
                if (state != null)
                {
                    return state.Value.FromBinaryBytes();
                }

                return null;
            }
            set
            {
                SessionState state = this.SessionStatesBySessionId.Where(ss => ss.Name == key).FirstOrDefault();
                if (state == null)
                {
                    state = this.SessionStatesBySessionId.AddNew();
                    state.Name = key;
                }

                object val = value;
                if (val != null)
                {
                    state.ValueType = val.GetType().AssemblyQualifiedName;
                    state.Value = val.ToBinaryBytes();
                }
                else
                {
                    state.ValueType = string.Empty;
                    state.Value = null;
                }

                state.Save();
            }
        }
    }
}
