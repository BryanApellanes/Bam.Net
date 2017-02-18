using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Services.VersioningService.Data;

namespace Bam.Net.Services.VersioningService
{
    public abstract class VersioningService : IVersioningService
    {
        public VersioningService()
        {
            VersionedTypes = new HashSet<Type>();
        }
        public IHashProvider HashProvider { get; set; }

        public Bam.Net.Services.VersioningService.Data.Version GetVersion(params Type[] objects)
        {
            throw new NotImplementedException();
        }

        protected virtual HashSet<Type> VersionedTypes { get; set; }
    }
}
