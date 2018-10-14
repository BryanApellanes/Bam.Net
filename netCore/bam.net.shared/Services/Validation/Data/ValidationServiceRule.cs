using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.Repositories;

namespace Bam.Net.Services.Validation.Data
{
    public class ValidationServiceRule: RepoData
    {
        public string Description { get; set; }
        public virtual List<EvaluatorProperty> Properties { get; set; }
        /// <summary>
        /// The resolvable type name of what can be validated by this rule
        /// </summary>
        public string TypeName { get; set; }

        public ValidationServiceResult Execute(object instance)
        {
            Type processorType = Type.GetType(TypeName);
            if(processorType == null)
            {
                return new ValidationServiceResult { ValidationRuleCuid = Cuid, Message = $"Unable to resolve TypeName {TypeName}", Success = false };
            }
            if (!processorType.ImplementsInterface<IValidationRuleEvaluator>())
            {
                return new ValidationServiceResult { ValidationRuleCuid = Cuid, Message = $"Specified TypeName does not implement IValidationRuleProcessor", Success = false };
            }

            IValidationRuleEvaluator evaluatorInstance = processorType.Construct<IValidationRuleEvaluator>();
            foreach(EvaluatorProperty prop in Properties)
            {
                evaluatorInstance.Property(prop.Name, prop.Value);
            }
            return new ValidationServiceResult { Success = evaluatorInstance.Evaluate(instance) };
        }
    }
}
