using System;
using System.Collections.Generic;
using System.Text;

namespace Bam.Net.Testing.Specification
{
    public abstract class SpecTestContextSetupAction<T>: ISpecTestContextSetupAction<T>
    {
        public string Description { get; set; }
        public Action SetupAction { get; set; }

        public abstract bool TrySetup();
        public abstract bool TrySetup(Action<T, Exception> exceptionHandler);

        public bool TrySetup(T actionContainer)
        {
            return TrySetup(actionContainer, (f, x) => { });
        }

        public bool TrySetup(T container, Action<T, Exception> exceptionHandler)
        {
            try
            {
                SetupAction();
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
