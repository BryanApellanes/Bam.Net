using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Services
{
    public class Valuespace
    {
        public Valuespace(string start, string end)
        {
            Start = start;
            End = end;
        }

        public string Start { get; set; }
        public string End { get; set; }
    }
}
