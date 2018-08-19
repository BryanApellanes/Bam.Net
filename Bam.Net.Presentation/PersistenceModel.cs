using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Presentation
{
    public class PersistenceModel
    {
        public string ApplicationName { get; set; }
        public string Name { get; set; }
        public HashSet<Type> Types { get; set; }
    }
}
