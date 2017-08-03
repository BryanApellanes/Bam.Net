using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Testing
{
    public interface ITestResultCollector
    {
        FileInfo[] Setup(string testDirectory);
    }
}
