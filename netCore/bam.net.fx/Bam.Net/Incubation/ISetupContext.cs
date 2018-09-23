/*
	Copyright © Bryan Apellanes 2015  
*/
using System;

namespace Bam.Net.Incubation
{
    public interface ISetupContext
    {
        T Construct<T>(params object[] ctorParams);
        T Construct<T>(params Type[] ctorParamTypes);
        object Get(Type type);
        T Get<T>();
        T Get<T>(params object[] ctorParams);
        T Get<T>(params Type[] ctorParamTypes);
        void Set<T>(T instance);
        object this[Type type] { get; set; }
    }
}
