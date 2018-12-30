using System;
using System.Collections.Generic;
using System.Text;

namespace Bam.Net.Testing.Specification
{
    public class ScenarioSetupAction
    {
        public string Description { get; set; }
        public Action Action { get; set; }
    }
}
