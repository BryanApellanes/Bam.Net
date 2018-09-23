/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Bam.Net.Data
{
    public abstract class ConnectionStringResolvers
    {
        static List<IConnectionStringResolver> _resolvers;
        static ConnectionStringResolvers()
        {
            _resolvers = new List<IConnectionStringResolver>();
            _resolvers.Add(DefaultConnectionStringResolver.Instance);
        }

        public static void AddResolver(IConnectionStringResolver resolver)
        {
            if (!_resolvers.Contains(resolver))
            {
                _resolvers.Add(resolver);
            }
        }

        public static void Remove(IConnectionStringResolver resolver)
        {
            _resolvers.Remove(resolver);
        }

        public static void Clear()
        {
            _resolvers.Clear();
        }

        public static ConnectionStringSettings Resolve(string connectionName)
        {
            ConnectionStringSettings settings = null;
            foreach (IConnectionStringResolver resolver in _resolvers)
            {
                settings = resolver.Resolve(connectionName);
                if (settings != null)
                {
                    break;
                }
            }
            return settings;
        }

        public static ConnectionStringResolveResult TryResolve(string connectionName)
        {
            try
            {
                return new ConnectionStringResolveResult(Resolve(connectionName));
            }
            catch (Exception ex)
            {
                return new ConnectionStringResolveResult(null, ex);
            }
        }
    }

}
