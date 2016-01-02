/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.Threading;

namespace Naizari.Helpers
{
    /// <summary>
    /// A thread safe utility for setting and getting application
    /// and session level singletons.
    /// </summary>
    public static class SingletonHelper
    {
        private class ImplementationProviders
        {
            Dictionary<Type, bool> locks;
            Dictionary<Type, object> providers;
            public ImplementationProviders()
            {
                providers = new Dictionary<Type, object>();
                locks = new Dictionary<Type, bool>();
            }

            public void Clear()
            {
                providers.Clear();
            }

            public T GetProvider<T>()
            {
                return GetProvider<T>(default(T));
            }

            public T GetProvider<T>(T setToIfNotFound)
            {
                return GetProvider<T>(setToIfNotFound, false);
            }

            public T GetProvider<T>(T setToIfNotFound, bool allowOverWrite)
            {
                if (providers.ContainsKey(typeof(T)))
                    return (T)providers[typeof(T)];
                else
                {
                    if (setToIfNotFound != null)
                    {
                        this.SetProvider<T>(setToIfNotFound, allowOverWrite, false);
                        return setToIfNotFound;
                    }
                }
                
                return default(T);
            }

            public void SetProvider<T>(object provider)
            {
                SetProvider<T>(provider, false);
            }

            public void SetProvider<T>(object provider, bool allowOverwrite)
            {
                SetProvider<T>(provider, allowOverwrite, true);
            }

            object setProviderLock = new object();
            public void SetProvider<T>(object provider, bool allowOverwrite, bool throwOnReset)
            {
                ExceptionHelper.ThrowIfIsNotOfType<T>(provider, "provider");

                lock (setProviderLock)
                {
                    if (locks.ContainsKey(typeof(T)))
                    {
                        if (locks[typeof(T)])
                        {
                            providers[typeof(T)] = provider;
                        }

                        if (!locks[typeof(T)] && throwOnReset)
                        {
                            ExceptionHelper.Throw<ProviderAlreadySetException>("The provider of type {0} has already been set.", typeof(T).Name);
                        }
                    }
                    else
                    {
                        if (providers.ContainsKey(typeof(T)))
                            providers[typeof(T)] = provider;
                        else
                            providers.Add(typeof(T), provider);

                        if (locks.ContainsKey(typeof(T)))
                            locks[typeof(T)] = allowOverwrite;
                        else
                            locks.Add(typeof(T), allowOverwrite);
                    }
                }
            }

            
        }

        private static volatile Dictionary<string, Dictionary<Type, object>> sessionSingletons;
        private static volatile Dictionary<Type, object> applicationSingletons;
        private static volatile Dictionary<string, DateTime> sessionDeathTimes;

        static SingletonHelper()
        {
            Initialize();            
        }

        static void ApplicationDisposed(object sender, EventArgs e)
        {
            //sessionSingletons = null;
            //applicationSingletons = null;
            //sessionDeathTimes = null;
            //GC.Collect();// this isn't really necessary but you never know with IIS
        }

        private static void Initialize()
        {
            if (sessionSingletons == null)
                sessionSingletons = new Dictionary<string, Dictionary<Type, object>>();
            if (applicationSingletons == null)
                applicationSingletons = new Dictionary<Type, object>();
            if (sessionDeathTimes == null)
                sessionDeathTimes = new Dictionary<string, DateTime>();

        }

        private static void EnsureWebApp()
        {
            if(HttpContext.Current == null)
                throw new InvalidOperationException("HttpContext.Current was null.");
        }

        private static void EnsureSession()
        {
            if (HttpContext.Current != null && HttpContext.Current.Session == null)
                throw new InvalidOperationException("Session state is not available.  Make sure session centered operations are performed post session state load.");
        }

        static object sessionProviderLock = new object();
        /// <summary>
        /// Sets the session provider of Type T to the specified concrete instance provider.
        /// The generic argument T "should" be an interface so different implementations
        /// can be set and used for testing.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="provider"></param>
        public static void SetSessionProvider<T>(object provider)
        {
            SetSessionProvider<T>(provider, false);
        }

        /// <summary>
        /// Sets the session provider of Type T to the specified concrete instance provider.
        /// The generic argument T "should" be an interface so different implementations
        /// can be set and used for testing.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="provider"></param>
        public static void SetSessionProvider<T>(object provider, bool allowReset)
        {
            lock (sessionProviderLock)
            {
                ImplementationProviders providers = GetSessionSingleton<ImplementationProviders>();
                providers.SetProvider<T>(provider, allowReset);
            }
        }

        /// <summary>
        /// Gets a provider of the specified type T for the current session.
        /// </summary>
        /// <typeparam name="T">The type implemented by the object to return.  This can be an interface an abstract
        /// class or any type in the inheritance hierarchy of the stored object.</typeparam>
        /// <returns></returns>
        public static T GetSessionProvider<T>()
        {
            return GetSessionProvider<T>(default(T));
        }

        /// <summary>
        /// Gets a provider of the specified type T for the current session.  If it hasn't been 
        /// set yet it will be set to the instance provided.  Once set it cannot be set again.
        /// </summary>
        public static T GetSessionProvider<T>(object setToIfNotFound)
        {
            return GetSessionProvider<T>(setToIfNotFound, false);
        }

        /// <summary>
        /// Gets a provider of the specified type T for the current session.  If it hasn't been 
        /// set yet it will be set to the instance provided.  Once set it can only be set again
        /// if allowOverWrite is true.
        /// </summary>
        /// <typeparam name="T">The type implemented by the object to return.  This can be an interface an abstract
        /// class or any type in the inheritance hierarchy of the stored object.</typeparam>
        /// <param name="setToIfNotFound">The instance to set the session provider to if it has not already been set.</param>
        /// <returns></returns>
        public static T GetSessionProvider<T>(object setToIfNotFound, bool allowOverWrite)
        {
            lock (sessionProviderLock)
            {
                ImplementationProviders providers = GetSessionSingleton<ImplementationProviders>();
                return providers.GetProvider<T>((T)setToIfNotFound, allowOverWrite);
            }
        }

        /// <summary>
        /// Set a singleton instance provider of the generic type T.
        /// Can only be called once per type T.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="provider"></param>
        public static void SetApplicationProvider<T>(object provider)
        {
            SetApplicationProvider<T>(provider, false);
        }

        /// <summary>
        /// Set a singleton instance provider of the generic type T.
        /// Can only be called again if allowOverwrite is true.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="provider"></param>
        /// <param name="allowOverwrite"></param>
        public static void SetApplicationProvider<T>(object provider, bool allowOverwrite)
        {
            ImplementationProviders providers = GetApplicationSingleton<ImplementationProviders>();
            providers.SetProvider<T>(provider, allowOverwrite);
        }

        public static bool ApplicationProviderIsSet<T>()
        {
            return GetApplicationProvider<T>() != null;
        }

        /// <summary>
        /// Get a singleton instance provider of the generic type T.  If not 
        /// currently set it will be set to the object provided.  Once set
        /// the application provider of the specified type cannot be set to a different
        /// instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetApplicationProvider<T>()
        {
            return GetApplicationProvider<T>(default(T));
        }

        /// <summary>
        /// Get a singleton instance provider of the generic type T.  If not 
        /// currently set it will be set to the object provided.  Once set
        /// the application provider of the specified type cannot be set to a different
        /// instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="setToIfNotFound"></param>
        /// <returns></returns>
        public static T GetApplicationProvider<T>(object setToIfNotFound)
        {
            ImplementationProviders providers = GetApplicationSingleton<ImplementationProviders>();
            return providers.GetProvider<T>((T)setToIfNotFound);
        }

        /// <summary>
        /// Gets a singleton of the specified generic type.  If the singleton
        /// hasn't been set a new instance will be created using the default constructor.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetApplicationSingleton<T>() where T : new()
        {
            lock (applicationSingletons)
            {
                if (!applicationSingletons.ContainsKey(typeof(T)))
                    applicationSingletons.Add(typeof(T), new T());
            }
            return (T)applicationSingletons[typeof(T)];
        }

        /// <summary>
        /// Gets a session singleton of the specified type T. 
        /// Will be instantiated as necessary.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        public static T GetSessionSingleton<T>() where T : new()
        {
            string ignore;
            return GetSessionSingleton<T>(out ignore);
        }

        static object sessionSingletonLock = new object();

        static string staticSessionId = Guid.NewGuid().ToString(); 
        /// <summary>
        /// Gets a session singleton of the specified type T. 
        /// Will be instantiated as necessary.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        public static T GetSessionSingleton<T>(out string sessionId) where T : new()
        {
            sessionId = staticSessionId; // make one up if we have to. winforms, standalone exe

            if (HttpContext.Current != null && 
                HttpContext.Current.Request != null &&
                HttpContext.Current.Request.Cookies != null && 
                HttpContext.Current.Request.Cookies["ASP.NET_SessionId"] != null)
            {
                sessionId = HttpContext.Current.Request.Cookies["ASP.NET_SessionId"].Value;
            }

            if (HttpContext.Current != null && HttpContext.Current.Session != null)
            {
                sessionId = HttpContext.Current.Session.SessionID; // get it from here if it exists
            }

            lock (sessionSingletonLock)
            {
                if (!sessionSingletons.ContainsKey(sessionId))
                {
                    lock (sessionSingletons)
                    {
                        if (!sessionSingletons.ContainsKey(sessionId))
                            sessionSingletons.Add(sessionId, new Dictionary<Type, object>());
                    }
                }

                if (!sessionSingletons[sessionId].ContainsKey(typeof(T)))
                {
                    lock (sessionSingletons[sessionId])
                    {
                        if (!sessionSingletons[sessionId].ContainsKey(typeof(T)))
                            sessionSingletons[sessionId].Add(typeof(T), new T());
                    }
                }
            }
            object retVal = sessionSingletons[sessionId][typeof(T)];
            return (T)retVal;
        }

        public static void AbandonSession()
        {
            if (sessionSingletons.ContainsKey(staticSessionId))
            {
                sessionSingletons.Remove(staticSessionId);
            }

            if (HttpContext.Current != null && HttpContext.Current.Session != null)
            {
                sessionSingletons.Remove(HttpContext.Current.Session.SessionID);
            }

            if (HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies["ASP.NET_SessionId"] != null)
            {
                string sessionIdCookie = HttpContext.Current.Request.Cookies["ASP.NET_SessionId"].Value;
                if (sessionSingletons.ContainsKey(sessionIdCookie))
                {
                    sessionSingletons.Remove(sessionIdCookie);
                }
            }
        }
    }
}
