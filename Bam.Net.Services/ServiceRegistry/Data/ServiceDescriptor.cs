﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.Repositories;
using Bam.Net.Services.Distributed.Files;

namespace Bam.Net.Services.ServiceRegistry.Data
{
    [Serializable]
    public class ServiceDescriptor: AuditRepoData
    {
        public ServiceDescriptor()
        {
        }

        public ServiceDescriptor(Type forType, Type useType)
        {
            ForType = forType.FullName;
            ForAssembly = forType.Assembly.FullName;
            UseType = useType.FullName;
            UseAssembly = useType.Assembly.FullName;
        }

        string _description;
        public string Description
        {
            get
            {
                return _description ?? $"For<{ForType}>().Use<{UseType}>()";
            }
            set
            {
                _description = value;
            }
        }
        public int SequenceNumber { get; set; }
        public string ForType { get; set; }
        public string ForAssembly { get; set; }
        public string UseType { get; set; }
        public string UseAssembly { get; set; }

        public List<ServiceRegistryDescriptor> ServiceRegistry { get; set; }
    }
}
