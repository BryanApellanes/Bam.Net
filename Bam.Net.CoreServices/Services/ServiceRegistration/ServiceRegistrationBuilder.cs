using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.CoreServices.ServiceRegistration
{
    public class ServiceRegistrationBuilder
    {
        List<Type> _forTypes;
        List<Type> _useTypes;
        public ServiceRegistrationBuilder()
        {
            _forTypes = new List<Type>();
            _useTypes = new List<Type>();
        }
        
        public ServiceRegistrationBuilder For(Type type)
        {
            _forTypes.Add(type);
            return this;
        }

        public ServiceRegistrationBuilder Use(Type type)
        {
            _useTypes.Add(type);
            return this;
        }
        
        public CoreServices.ServiceRegistry Build()
        {
            Args.ThrowIf(_forTypes.Count != _useTypes.Count, "Type count mismatch: for types ({0}) use types({1})", _forTypes.Count, _useTypes.Count);
            CoreServices.ServiceRegistry reg = new CoreServices.ServiceRegistry();
            for(int i = 0; i < _forTypes.Count; i++)
            {
                reg.Set(_forTypes[i], _useTypes[i]);
            }
            return reg;
        }
    }
}
