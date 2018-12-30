using System;
using System.Collections.Generic;
using System.Text;

namespace Bam.Net.Testing.Specification
{
    public class AssertionAction : Assertion
    {
        public string ShouldDescription { get; set; }
        public Action Action { get; set; }
        public virtual AssertionAction Execute()
        {
            try
            {
                Action();
                Passed = true;
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
