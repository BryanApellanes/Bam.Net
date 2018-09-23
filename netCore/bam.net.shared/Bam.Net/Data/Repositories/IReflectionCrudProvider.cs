using System;
using System.Collections;
using System.Collections.Generic;

namespace Bam.Net.Data.Repositories
{
    public interface IReflectionCrudProvider: ICrudProvider
    {
        IEnumerable<object> Query(Type type, Dictionary<string, object> queryParameters);
        object Retrieve(Type objectType, string identifier);
    }
}