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
    public class BamTestRunProcessWorker: ProcessWorker, ITestWorker
    {
        private const string CommandLineFormat = "opencover.console.exe -register:user -target:bamtestrunner.exe -targetargs:\"/dir:{TestDirectory} /search:{SearchPattern}\" -filter:{Filter} -output:{CoverageOutputFileName}.xml";
        public BamTestRunProcessWorker()
            : base()
        {
            Init();
        }

        public BamTestRunProcessWorker(string name)
            : base(name)
        {
            Init();
        }

        private void Init()
        {
            TestDirectory = ".\\Tests";
            SearchPattern = "*.Tests.dll";
            Filter = "+[*]*";
            CoverageOutputFileName = "coverage";
        }

        string _testDirectory;
        public string TestDirectory
        {
            get
            {
                return _testDirectory;
            }
            set
            {
                _testDirectory = value;
                SetCommandLine();
            }
        }
        string _searchPattern;
        public string SearchPattern
        {
            get
            {
                return _searchPattern;
            }
            set
            {
                _searchPattern = value;
                SetCommandLine();
            }
        }

        string _filter;
        public string Filter
        {
            get
            {
                return _filter;
            }
            set
            {
                _filter = value;
                SetCommandLine();
            }
        }

        string _coverageOutputFileName;
        public string CoverageOutputFileName
        {
            get
            {
                return _coverageOutputFileName;
            }
            set
            {
                _coverageOutputFileName = value;
                SetCommandLine();
            }
        }

        public void SetCommandLine()
        {
            CommandLine = CommandLineFormat.NamedFormat(this);
        }

        public override string[] RequiredProperties
        {
            get
            {
                List<string> results = new List<string>();
                results.AddRange(base.RequiredProperties);
                results.AddRange(new string[] { "TestDir", "SearchPattern", "Filter", "CoverageOutputFileName" });
                return results.ToArray();
            }
        }
    }
}
