﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.CoreServices.Data.Dao.Repository;

namespace Bam.Net.CoreServices
{
    public abstract class CoreProxyableService: ProxyableService
    {
        public CoreRegistryRepository CoreRegistryRepository { get; set; }
        public IApplicationNameProvider ApplicationNameProvider { get; set; }
        public override string ApplicationName
        {
            get
            {
                if(ApplicationNameProvider != null && ApplicationNameProvider != this)
                {
                    return ApplicationNameProvider.GetApplicationName();
                }
                return base.ApplicationName;
            }
        }
    }
}