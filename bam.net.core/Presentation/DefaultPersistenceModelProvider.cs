using System;
using System.Collections.Generic;
using System.Text;

namespace Bam.Net.Presentation
{
    public class DefaultPersistenceModelProvider : IPersistenceModelProvider
    {
        public PersistenceModel GetPersistenceModel(string applicationName, string persistenceModelName)
        {
            return new PersistenceModel();
        }
    }
}
