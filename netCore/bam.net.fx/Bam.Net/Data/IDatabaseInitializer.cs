/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using Bam.Net.Incubation;

namespace Bam.Net.Data
{
    public interface IDatabaseInitializer
    {
        DatabaseInitializationResult Initialize(string connectionName);
        void Ignore(params Type[] types);
        void Ignore(params string[] connectionNames);
    }
}
