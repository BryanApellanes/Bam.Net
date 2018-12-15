using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace masuco
{
    [Serializable]
    public class MasucoConfig
    {
        public string Source { get; set; }
        public string Destination { get; set; }
    }
}
