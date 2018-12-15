/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.ServiceProxy;
using Bam.Net.Automation.Testing.Data;

namespace Bam.Net.Automation.Testing
{
	public class SaveTestExecutionResponse : TestReportResponse
    {
        public TestExecution TestExecution
        {
            get
            {
                return DataTo<TestExecution>();
            }
        }
    }
}
