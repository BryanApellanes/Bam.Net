using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.Repositories;

namespace Bam.Net.Services.Validation.Data
{
    public class EvaluatorProperty: RepoData
    {
        public long ValidationRuleId { get; set; }
        public ValidationServiceRule ValidationRule { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
