using Bam.Net.Testing;
using System;
using Bam.Net.Application.Verbs;

namespace Bam.Net.UserAccounts.Tests
{
    [Serializable]
    public partial class Program : CommandLineTool
    {
        static void Main(string[] args)
        {
            ExecuteMainOrInteractive(args);
        }
    }
}
