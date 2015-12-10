/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.CommandLine;
using Bam.Net.Testing;
using Bam.Net.Net;
using Bam.Net.Net.Dns;

namespace Bam.Net.Net.Tests
{
    [Serializable]
    public class UnitTests: CommandLineTestInterface
    {
        [UnitTest]
        public void LookupMxRecordTest()
        {
            MXRecord[] mxRecords = DnsClient.LookupMXRecord("google.com");
            if (mxRecords.Length > 0)
            {
                mxRecords.Each(mx =>
                {
                    OutLine(mx.HostName);
                });
            }
        }
    }
}
