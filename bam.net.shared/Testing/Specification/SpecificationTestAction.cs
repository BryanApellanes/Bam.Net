using System;
using System.Collections.Generic;
using System.Text;

namespace Bam.Net.Testing.Specification
{
    public abstract class SpecificationTestAction<T>: ISpecificationTestAction<T>
    {
        public string Description { get; set; }
        public Action Action { get; set; }

        public abstract bool TryAction();
        public abstract bool TryAction(Action<T, Exception> exceptionHandler);

        public bool TryAction(T actionContainer)
        {
            return TryAction(actionContainer, (f, x) => { });
        }

        public bool TryAction(T container, Action<T, Exception> exceptionHandler)
        {
            try
            {
                Action();
                return true;
            }
            catch (Exception ex)
            {
                exceptionHandler(container, ex);
                return false;
            }
        }
    }
}
