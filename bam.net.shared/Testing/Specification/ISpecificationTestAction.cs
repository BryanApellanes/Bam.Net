using System;
using System.Collections.Generic;
using System.Text;

namespace Bam.Net.Testing.Specification
{
    public interface ISpecificationTestAction<T>
    {
        string Description { get; set; }
        Action Action { get; set; }
        bool TryAction(T context);
        bool TryAction(T context, Action<T, Exception> exceptionHandler);
    }
}
