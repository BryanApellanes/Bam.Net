﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Testing;

namespace Bam.Net.Caching.Tests
{
    [Serializable]
    public class CacheManagerTests : CommandLineTestInterface
    {
        [UnitTest]
        public void GetCacheFailedShouldNotFire()
        {
            CacheManager cacheMan = new CacheManager();
            Type missing = typeof(object);
            bool? fired = false;
            cacheMan.GetCacheFailed += (o, args) =>
            {
                fired = true;
            };

            Cache cache = cacheMan.CacheFor<object>();
            Expect.IsFalse(fired.Value);
            Expect.IsNotNull(cache);
        }
    }
}
