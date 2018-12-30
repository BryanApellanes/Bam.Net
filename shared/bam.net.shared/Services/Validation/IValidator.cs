using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Services.Catalog.Data;

namespace Bam.Net.Services.Validation
{
    public interface IValidator
    {
        ValidationServiceResult Validate(string objectCuid);
    }
}
