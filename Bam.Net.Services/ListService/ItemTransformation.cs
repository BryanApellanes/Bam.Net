using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Services.ListService.Data;

namespace Bam.Net.Services.ListService
{
    public class ItemTransformation
    {
        public ItemDefinition OldState { get; set; }
        public ItemDefinition NewState { get; set; }
    }
}
