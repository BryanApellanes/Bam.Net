using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Configuration;

namespace Bam.Net
{
    public static class ApplicationNameProvider
    {
        static IApplicationNameProvider _defaultApplicationNameProvider;
        static object _defaultLock = new object();
        public static IApplicationNameProvider Default
        {
            get
            {
                return _defaultLock.DoubleCheckLock(ref _defaultApplicationNameProvider, () => DefaultConfigurationApplicationNameProvider.Instance);
            }
            set
            {
                _defaultApplicationNameProvider = value;
            }
        }
    }
}
