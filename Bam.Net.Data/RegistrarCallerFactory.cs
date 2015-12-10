/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Logging;

namespace Bam.Net.Data
{
    public class RegistrarCallerFactory
    {
        public bool TryCreateRegistrarCaller(string assemblyQualifiedName, out IRegistrarCaller result)
        {
            bool success = false;
            result = null;
            try
            {
                result = CreateRegistrarCaller(assemblyQualifiedName);
                success = result != null;
            }
            catch (Exception ex)
            {
                Log.AddEntry("An error occurred trying to create RegistrarCaller: {0}", ex, ex.Message);
                success = false;
            }

            return success;
        }

        public IRegistrarCaller CreateRegistrarCaller(string assemblyQualifiedName)
        {
            Type returnType = Type.GetType(assemblyQualifiedName);
            IRegistrarCaller result = null;
            if (returnType != null)
            {
                result = returnType.Construct<IRegistrarCaller>();
            }

            return result;
        }
    }
}
