using System.Collections;

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
