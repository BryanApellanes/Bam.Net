using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data.Repositories
{
    public interface IObjectWriter: IObjectPersisterDirectoryProvider
    {
        void Enqueue(Type type, object data);
        void Write(Type type, object data);
        void WriteProperty(PropertyInfo prop, object value);
        bool Delete(object data, Type type = null);

        event EventHandler WriteObjectFailed;
        event EventHandler WriteObjectPropertiesFailed;
        event EventHandler DeleteFailed;
    }
}
