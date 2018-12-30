using System;
using System.Collections.Generic;
using System.Text;

namespace Bam.Net.Testing.Specification
{
    public class FeatureSetupFailedException: SpecTestFailedException
    {
        public FeatureSetupFailedException(string description) : base($"Feature ({description}") { }
    }
}
