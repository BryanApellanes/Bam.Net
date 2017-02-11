using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Services.ListService.Data;

namespace Bam.Net.Services.ListService
{
    public class ListValidator : IValidator, IListTransormer
    {
        public ValidationResult Validate(string listCuid)
        {
            throw new NotImplementedException();
        }

        public TransformCheckResult WillTransform(ListDefinition list)
        {
            throw new NotImplementedException();
        }
    }
}
