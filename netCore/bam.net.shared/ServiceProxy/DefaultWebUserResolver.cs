/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.ServiceProxy
{
    public class DefaultWebUserResolver: IUserResolver
    {
        static DefaultWebUserResolver()
        {
            Instance = new DefaultWebUserResolver();
        }
        [Exclude]
        public object Clone()
        {
            DefaultWebUserResolver clone = new DefaultWebUserResolver();
            clone.CopyProperties(this);
            return clone;
        }

        public static IUserResolver Instance
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the current web user as reported
        /// by HttpContext.Current.User.Identity
        /// </summary>
        /// <returns></returns>
        public string GetCurrentUser()
        {
            return UserUtil.GetCurrentWebUserName();
        }

        public string GetUser(IHttpContext context)
        {
            return GetUserFromContext(context);
        }

        public static string GetUserFromContext(IHttpContext context)
        {
            if (context != null &&
                context.User != null &&
                context.User.Identity != null)
            {
                return context.User.Identity.Name;
            }

            return string.Empty;
        }

        public IHttpContext HttpContext
        {
            get;
            set;
        }
    }
}
