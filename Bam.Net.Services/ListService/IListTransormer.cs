using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Services.ListService.Data;

namespace Bam.Net.Services.ListService
{
    public interface IListTransormer
    {
        TransformCheckResult WillTransform(ListDefinition list);
    }
}
