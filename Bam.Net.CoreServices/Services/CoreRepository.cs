using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.Repositories;

namespace Bam.Net.CoreServices.Services
{
    /// <summary>
    /// Extender to DaoRepository for purposes
    /// of dependency injection in CoreRegistry
    /// </summary>
    public class CoreRepository: DaoRepository
    {
        public CoreRepository() { }
    }
}
