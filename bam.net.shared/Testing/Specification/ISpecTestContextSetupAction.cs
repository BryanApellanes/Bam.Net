using System;
using System.Collections.Generic;
using System.Text;

namespace Bam.Net.Testing.Specification
{
    public interface ISpecTestContextSetupAction<T>
    {
        string Description { get; set; }
        Action SetupAction { get; set; }
        bool TrySetup(T context);
        bool TrySetup(T context, Action<T, Exception> exceptionHandler);
    }
}
