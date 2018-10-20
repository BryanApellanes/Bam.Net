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
    public class UserResolvers: IEnumerable<IUserResolver>, IRequiresHttpContext, IUserResolver
    {
        List<IUserResolver> _resolvers;
        public UserResolvers()
        {
            this._resolvers = new List<IUserResolver>();
            this.AddResolver(new DefaultWebUserResolver());
        }

        static UserResolvers _default;
        static object _defaultLock = new object();
        public static UserResolvers Default
        {
            get
            {
                return _defaultLock.DoubleCheckLock(ref _default, () => new UserResolvers());
            }
        }
        [Exclude]
        public object Clone()
        {
            UserResolvers clone = new UserResolvers();
            clone.CopyProperties(clone);
            clone._resolvers = new List<IUserResolver>();
            _resolvers.Each(r => clone._resolvers.Add((IUserResolver)r.Clone()));
            return clone;
        }

        public int Count
        {
            get
            {
                return _resolvers.Count;
            }            
        }

        public IHttpContext HttpContext
        {
            get;
            set;
        }

        public IUserResolver this[int i]
        {
            get
            {
                return _resolvers[i];
            }
        }

        public void Clear()
        {
            _resolvers.Clear();
        }

        public string GetCurrentUser()
        {
            return GetUser(HttpContext);
        }

        public string GetUser(IHttpContext context)
        {
            string result = string.Empty;
            this.Each(ur =>
            {
                result = ur.GetUser(context);
                return string.IsNullOrEmpty(result); // breaks the "Each" loop if result is not blank
            });

            return result;
        }

        public void InsertResolver(int index, IUserResolver resolver)
        {
            _resolvers.Insert(index, resolver);
        }

        public void AddResolver(IUserResolver resolver)
        {
            _resolvers.Add(resolver);
        }

        public void RemoveResolver(IUserResolver resolver)
        {
            _resolvers.Remove(resolver);
        }

        public IEnumerator<IUserResolver> GetEnumerator()
        {
            return _resolvers.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _resolvers.GetEnumerator();
        }
    }
}
