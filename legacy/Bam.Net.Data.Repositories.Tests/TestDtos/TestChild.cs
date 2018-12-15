using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data.Repositories.Tests.TestDtos
{

    [Serializable]
    public class TestChild
    {
        // missing foreign key to parent
        public string Uuid { get; set; }
    }
}
