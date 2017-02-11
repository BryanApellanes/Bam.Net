using System;
using System.Collections;
using System.Collections.Generic;

namespace Bam.Net.Data.Repositories
{
    public interface IBaseCrudProvider
    {
        object Create(object toCreate);
        bool Delete(object toDelete);
        IEnumerable<object> Query(Type type, Dictionary<string, object> queryParameters);
        object Retrieve(Type objectType, string identifier);
        object Save(object toSave);
        IEnumerable SaveCollection(IEnumerable values);
        object Update(object toUpdate);
    }
}