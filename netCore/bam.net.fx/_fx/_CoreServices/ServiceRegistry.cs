﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.Repositories;

namespace Bam.Net.CoreServices
{
    public partial class ServiceRegistry
    {
        static ServiceRegistry()
        {
            Default = new ServiceRegistry { Name = "Default" };
            Default.Set<IObjectPersister>(ObjectPersister.Default);
            Default.Set<IRepository>(new ObjectRepository());
        }
    }
}
