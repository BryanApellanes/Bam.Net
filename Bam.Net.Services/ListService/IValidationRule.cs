using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Services.ListService
{
    public interface IValidationRule
    {
        bool IsValid(IValidatable validatable);
    }
}
