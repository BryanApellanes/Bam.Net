/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Services.DistributedService
{
    [AttributeUsage(AttributeTargets.Property)]
    public class RepositoryKey: Attribute
    {
    }
}
