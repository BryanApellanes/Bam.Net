/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Automation.ContinuousIntegration
{
    public interface ITestWorker
    {
        string SearchPattern { get; set; }
        string Filter { get; set; }
        string TestDirectory { get; set; }

        string CoverageOutputFileName { get; set; }
    }
}
