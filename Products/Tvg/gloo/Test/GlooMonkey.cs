using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Application
{
    public class GlooMonkey
    {
        public GlooMonkey() { }
        public GlooMonkey(string name)
        {
            Name = name;
            Birthday = DateTime.UtcNow.Subtract(TimeSpan.FromDays(365 * RandomNumber.Between(10, 150)));
            HasTail = RandomHelper.Bool();
        }
        public string Name { get; set; }
        public bool HasTail { get; set; }
        public DateTime Birthday { get; set; }
    }
}
