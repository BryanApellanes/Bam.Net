using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Services.ValidationService.Data
{
    public class ValidationRule
    {
        public string Description { get; set; }
        public string TypeName { get; set; }
        public string Validation { get; set; } // compilable code

        public ValidationResult Execute(object instance)
        {
            throw new NotImplementedException();
        }
    }
}
