using Bam.Net.CommandLine;
using Bam.Net.Testing.Unit;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bam.Net.Testing.Specification
{
    [AttributeUsage(AttributeTargets.Method)]
    public class SpecTestAttribute: ConsoleActionAttribute
    {
    }
}
