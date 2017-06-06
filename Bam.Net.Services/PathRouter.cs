using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Services
{
    public class PathRouter
    {
        public PathRouter(string pathName)
        {
            PathName = pathName;
        }
        public string PathName { get; set; }

    }
}
