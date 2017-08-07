/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.ServiceProxy;
using Bam.Net.Automation.TestReporting.Data;

namespace Bam.Net.Automation.TestReporting
{
	public class GetSuiteDefinitionResponse: TestReportResponse
    {
        public TestSuiteDefinition SuiteDefinition
        {
            get
            {
                return DataTo<TestSuiteDefinition>();
            }
        }
	}
}
