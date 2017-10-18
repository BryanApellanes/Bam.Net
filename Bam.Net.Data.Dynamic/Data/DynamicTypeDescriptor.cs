﻿using Bam.Net.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data.Dynamic.Data
{
    [Serializable]
    public class DynamicTypeDescriptor: RepoData
    {
        public DynamicTypeDescriptor()
        { }
        public long DynamicNamespaceDescriptorId { get; set; }
        public virtual DynamicNamespaceDescriptor DynamicNamespaceDescriptor { get; set; }
        public string TypeName { get; set; }
        public virtual List<DynamicTypePropertyDescriptor> Properties { get; set; }
    }
}
