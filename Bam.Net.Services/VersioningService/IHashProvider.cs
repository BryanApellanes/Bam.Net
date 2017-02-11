using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Services.VersioningService
{
    public interface IHashProvider
    {
        string GetHash(params Type[] types);
    }
}
