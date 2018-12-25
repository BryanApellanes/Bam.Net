using System;
using System.Collections.Generic;
using System.Text;

namespace Bam.Net.Testing.Specification
{
    public class ThenAction
    {
        public string Description { get; set; }
        public Action<ThenDelegate> Action { get; set; }
    }
}
