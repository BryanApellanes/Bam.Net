using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Presentation
{
    public interface IPersistenceModelProvider
    {
        PersistenceModel GetPersistenceModel(string applicationName, string persistenceModelName);
    }
}
