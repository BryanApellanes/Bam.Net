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
    public class RoleResolvers : IEnumerable<IRoleResolver>, IRequiresHttpContext, IRoleResolver
    {
        List<IRoleResolver> _resolvers;
        public RoleResolvers()
        {
            this._resolvers = new List<IRoleResolver>();
            this.AddResolver(new DefaultRoleResolver());
        }

        static RoleResolvers _default;
        static object _defaultLock = new object();
        public static RoleResolvers Default
        {
            get
            {
                return _defaultLock.DoubleCheckLock(ref _default, () => new RoleResolvers());
            }
        }
        [Exclude]
        public object Clone()
        {
            RoleResolvers clone = new RoleResolvers();
            clone.CopyProperties(this);
            clone._resolvers = new List<IRoleResolver>();
            _resolvers.Each(r => clone._resolvers.Add((IRoleResolver)r.Clone()));
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

        public IRoleResolver this[int i]
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

        public void AddResolver(IRoleResolver resolver)
        {
            _resolvers.Add(resolver);
        }

        public void RemoveResolver(IRoleResolver resolver)
        {
            _resolvers.Remove(resolver);
        }

        public IEnumerator<IRoleResolver> GetEnumerator()
        {
            return _resolvers.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _resolvers.GetEnumerator();
        }

        public bool IsInRole(IUserResolver userResolver, string roleName)
        {
            bool? result = false;
            this.Each(rr =>
            {
                result = rr.IsInRole(userResolver, roleName);
                return !result.Value; // stop the "Each" loop if they are in the role
            });

            return result.Value;
        }

        public string[] GetRoles(IUserResolver userResolver)
        {
            List<string> roles = new List<string>();
            this.Each(rr =>
            {
                roles.AddRange(rr.GetRoles(userResolver));
            });

            return roles.ToArray();
        }
    }
}
