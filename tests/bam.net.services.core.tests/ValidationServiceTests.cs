using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Services.Validation;
using Bam.Net.Services.Validation.Data;
using Bam.Net.Testing;
using Bam.Net.Testing.Unit;

namespace Bam.Net.Services.Tests
{
    public class TestValidationEvaluator : IValidationRuleEvaluator
    {
        public string ShouldBe { get; set; }
        public bool Evaluate(object instance)
        {
            return instance.Property("TestProperty").Equals(ShouldBe);
        }
    }
    [Serializable]
    public partial class ValidationServiceTests : CommandLineTool
    {
        [UnitTest]
        public void CanEvaluateValidationRule()
        {
            ValidationServiceRule rule = new ValidationServiceRule();
            string someRandomValue = 16.RandomLetters();
            rule.Properties = new List<EvaluatorProperty>
            {
                new EvaluatorProperty{Name = "ShouldBe", Value = someRandomValue}
            };
            rule.TypeName = typeof(TestValidationEvaluator).AssemblyQualifiedName;

            Expect.IsTrue(rule.Execute(new { TestProperty = someRandomValue }).Success);
            Expect.IsFalse(rule.Execute(new { TestProperty = "not valid" }).Success);
        }
    }
}
