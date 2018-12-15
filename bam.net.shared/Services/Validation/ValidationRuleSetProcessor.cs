using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Services.Validation.Data;

namespace Bam.Net.Services
{
	public class ValidationRuleSetProcessor<T>: ValidationRuleSetProcessor
    {
		public ValidationRuleSetProcessor()
        {
            Type = typeof(T);
        }
		public Type Type { get; set; }
    }
    public class ValidationRuleSetProcessor
    {
        public ValidationRuleSetProcessor() { }

		public List<ValidationServiceRule> ValidationRules { get; set; }

        public List<ValidationServiceResult> Validate(object toBeValidated)
        {
            throw new NotImplementedException();
        }
    }
}
