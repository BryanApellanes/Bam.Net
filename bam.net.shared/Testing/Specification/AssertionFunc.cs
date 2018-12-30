using System;
using System.Collections.Generic;
using System.Text;

namespace Bam.Net.Testing.Specification
{
    public class AssertionFunc<T>: AssertionAction
    {
        public Func<ItContext, T> Func { get; set; }
        public T Result { get; set; }
        public virtual AssertionAction Execute(ItContext context)
        {
            try
            {
                Result = Func(context);
            }
            catch (Exception ex)
            {
                Passed = false;
                FailureMessage = ex.Message;
            }
            return this;
        }
    }
}
