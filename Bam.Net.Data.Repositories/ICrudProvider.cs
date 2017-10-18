using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Bam.Net.Data.Repositories
{
    public interface ICrudProvider
    {
        object Create(object toCreate);
        bool Delete(object toDelete);
        object Save(object toSave);
        IEnumerable SaveCollection(IEnumerable values);
        object Update(object toUpdate);
        object Retrieve(string typeIdentifier, string instanceId);
    }
}
