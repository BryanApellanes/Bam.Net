using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Services.Catalog.Data
{
    public class ItemCustomizationDefinition
    {
        public string Name { get; set; }
        
        public byte[] Definition { get; set; } // NOT Implemented: Assembly.Load(Definition).GetType("ItemCustomization")... Invoke("Execute", ItemDefinition)
    }
}
