using Bam.Net.CoreServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bam.Net.Testing.Specification
{
    public class ItContext
    {   
        Queue<AssertionAction> _shouldQueue;
        public ItContext()
        {
            _shouldQueue = new Queue<AssertionAction>();
            Assertions = new List<Assertion>();
        }

        public object AssertionTarget { get; set; }
        protected internal List<Assertion> Assertions { get; set; }
        protected ServiceRegistry SpecificationTestRegistry { get; set; }

        public ItContext It(object target)
        {
            AssertionTarget = target;
            return this;            
        }

        public ItContext Should(Func<ItContext, object> func)
        {
            _shouldQueue.Enqueue(new AssertionFunc<object>
            {
                ShouldDescription = "should return",
                Func = func
            });
            return this;
        }

        public ItContext Should(string description, Action action)
        {
            _shouldQueue.Enqueue(new AssertionAction
            {
                ShouldDescription = description,
                Action = action
            });
            return this;
        }

        public ItContext Return<T>()
        {
            AssertionFunc<T> func = (AssertionFunc<T>)_shouldQueue.Dequeue();
            func.Execute(this);
            Assertions.Add(func);
            return this;
        }

        public ItContext WithoutThrowing()
        {
            return NotThrow();
        }

        public ItContext NotThrow()
        {
            AssertionAction action = _shouldQueue.Dequeue();
            Assertions.Add(action.Execute());
            return this;
        }

        public ItContext IsA<T>()
        {
            string target = AssertionTarget == null ? "[null]" : AssertionTarget.ToString();            
            if (AssertionTarget is T)
            {
                AddSuccess($"{target} is a {typeof(T).Name}");
            }
            else
            {
                AddFailure($"{target} was NOT a{typeof(T).Name}");
            }
            return this;
        }

        public ItContext Is(object compareTo)
        {
            return Be(compareTo);
        }

        public ItContext IsEqualTo(object compareTo, string successMessage = null)
        {
            string target = AssertionTarget == null ? "[null]" : AssertionTarget.ToString();
            string compare = compareTo == null ? "[null]" : compareTo.ToString();
            if (AssertionTarget.Equals(compareTo))
            {
                AddSuccess(successMessage ?? $"{target} equals {compare}");
            }
            else
            {
                AddFailure($"{target} did not equal {compare}");
            }
            return this;
        }

        public ItContext IsTrue(string message = null)
        {
            if(((bool)(AssertionTarget)) == true)
            {
                AddSuccess(message ?? $"{AssertionTarget.ToString()} was true");
            }
            else
            {
                AddFailure(message ?? $"{AssertionTarget.ToString()} was true");
            }
            return this;
        }

        public ItContext Be(object compareTo)
        {
            bool passed = AssertionTarget == compareTo;
            if (passed)
            {
                AddSuccess($"{AssertionTarget.ToString()} is {compareTo.ToString()}");
            }
            else
            {
                AddFailure($"{AssertionTarget.ToString()} is NOT {compareTo.ToString()}");
            }
            return this;
        }

        
        protected void AddSuccess(string message)
        {
            Assertions.Add(new Assertion
            {
                Passed = true,
                SuccessMessage = message
            });
        }

        protected void AddFailure(string message)
        {
            Assertions.Add(new Assertion
            {
                Passed = false,
                FailureMessage = message
            });
        }
    }
}
