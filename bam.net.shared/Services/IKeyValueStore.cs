using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Services
{
    public interface IKeyValueStore
    {
        string Get(string key);
        bool Set(string key, string value);
    }
}
