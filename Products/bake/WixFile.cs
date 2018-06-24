using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Application
{
    public class WixFile
    {
        public string Id { get; set; }
        public string Source { get; set; }

        string _keyPath;
        public string KeyPath
        {
            get { return _keyPath.IsAffirmative() ? "yes" : "no"; }
            set { _keyPath = value; }
        }
    }
}
