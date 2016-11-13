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
using Bam.Net.Data;
using System.Runtime.Serialization;

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
        [Exclude]
        public object Clone()
        {
            Session clone = new Session();
            clone.CopyProperties(this);
            return clone;
        }

        public static Session Init(IHttpContext context)
        {
            Session session = Get(context);
            if (session.UserId != User.Anonymous.Id)
            {
                User user = session.UserOfUserId ?? User.Anonymous;
                context.User = new DaoPrincipal(user, user.IsAuthenticated);
            }

            return session;
        }

        public static Session Get(IHttpContext context, Database db = null)
        {
            Args.ThrowIfNull(context, "context");
            
            return Get(context.Request, context.Response, db);    
        }

        static Dictionary<string, Session> _sessionCache = new Dictionary<string, Session>();

        public static Session Get(IRequest request, IResponse response, Database db = null)
        {
            Cookie sessionIdCookie = request.Cookies[CookieName];
            Session session = null;
            string identifier = string.Empty;
            if (sessionIdCookie != null)
            {
                identifier = sessionIdCookie.Value;
                if (_sessionCache.ContainsKey(identifier))
                {
                    session = _sessionCache[identifier];
                }
                if(session == null)
                {
                    session = OneWhere(c => c.Identifier == identifier, db);
                }
            }

            if (session == null)
            {
                session = Create(response, identifier, true, db);
            }
            else
            {
                session.LastActivity = DateTime.UtcNow;
                session.SaveAsync(db);
            }
            _sessionCache.Set(identifier, session);
            return session;
        }

        public void End(Database db = null, ILogger logger = null) // TODO: Write unit test
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

                Identifier = Identifier + "-Ended";
                IsActive = false;
                SaveAsync(db);
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

        public static Session Get(string userName, bool isActive = true, Database db = null)
        {
            User user = User.GetByUserNameOrDie(userName, db);
            return Get(user, isActive, db);
        }

        /// <summary>
        /// Get a Session instance for the specified userName, null will 
        /// be returned if it doesn't exist
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="isActive"></param>
        /// <returns></returns>
        public static Session Get(User user, bool isActive = true, Database db = null)
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

                session.SaveAsync(db);            
            }

            return session;
        }

        internal static Session Create(IResponse response, string identifier = "", bool isActive = true, Database db = null)
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
            session.Save(db);

            Cookie existing = response.Cookies[CookieName];
            if(existing == null)
            {
                existing = new Cookie(CookieName, session.Identifier);
                response.Cookies.Add(existing);
            }
            else
            {
                response.Cookies[CookieName].Value = session.Identifier;
            }
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
                SaveAsync();
            }
        }

        /// <summary>
        /// Get the object of type T for the specified key
        /// using the setter to set it if it hasn't already
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="setter"></param>
        /// <returns></returns>
        public T Get<T>(string key, Func<T> setter = null)
        {
            object value = this[key];
            if (value != null)
            {
                return (T)value;
            }
            else if (value == null && setter != null)
            {
                value = setter();
                this[key] = value;
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
                    if (state.ValueType.EndsWith(".json"))
                    {
                        return Encoding.UTF8.GetString(state.Value).FromJson(Type.GetType(state.ValueType.Truncate(".json".Length)));
                    }
                    else
                    {
                        return state.Value.FromBinaryBytes();
                    }
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
                    try
                    {
                        state.Value = val.ToBinaryBytes();
                    }
                    catch (SerializationException ex)
                    {
                        Log.Default.AddEntry("Session::Error serializing object of type {0}, using Json instead", LogEventType.Warning, ex, val.GetType().Name);
                        state.ValueType = $"{state.ValueType}.json";
                        state.Value = Encoding.UTF8.GetBytes(val.ToJson());
                    }
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
