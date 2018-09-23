/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Logging;

namespace Bam.Net.Server
{
    public interface IInitialize<T>: IInitialize
    {
        event Action<T> Initializing;
        event Action<T> Initialized;
    }

    public interface IInitialize: ILoggable
    {
        bool IsInitialized { get; }

        void Initialize();
    }
}
