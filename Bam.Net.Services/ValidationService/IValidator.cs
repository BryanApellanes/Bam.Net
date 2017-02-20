using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Services.ListService.Data;

namespace Bam.Net.Services.ValidationService
{
    public interface IValidator
    {
        ValidationResult Validate(string objectCuid);
    }
}
