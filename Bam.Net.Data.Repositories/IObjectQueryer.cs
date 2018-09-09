using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data.Repositories
{
    public interface IObjectQueryer
    {
        T[] QueryProperty<T>(string propertyName, object value);
        T[] QueryProperty<T>(string propertyName, Func<object, bool> predicate);
        T[] Query<T>(Func<T, bool> predicate);
        object[] Query(Type type, Func<object, bool> predicate);
    }
}
