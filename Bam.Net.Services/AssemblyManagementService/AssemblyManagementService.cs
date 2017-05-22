using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Services.AssemblyManagementService.Data;

namespace Bam.Net.Services.AssemblyManagementService
{
    public abstract class AssemblyManagementService : IAssemblyManagementService
    {
        public AssemblyManagementService()
        {
        }

        public Bam.Net.Services.AssemblyManagementService.Data.AssemblyRegistration GetVersion(params Type[] objects)
        {
            throw new NotImplementedException();
        }        
    }
}
