using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net;

namespace Bam.Net.Application
{
    [Proxy("glooTestSvc")]
    public class GlooTestService
    {
        public GlooMonkey GetMonkey(string name)
        {
            return new GlooMonkey
            {
                Name = name,
                Birthday = DateTime.UtcNow.Subtract(TimeSpan.FromDays(365 * RandomNumber.Between(10, 150))),
                HasTail = RandomHelper.Bool()
            };            
        }
    }
}
