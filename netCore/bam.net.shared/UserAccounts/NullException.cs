using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.UserAccounts
{
    /// <summary>
    /// Not actually an exception
    /// </summary>
    public class NullException: Exception
    {
        public NullException() : base(string.Empty) { }
    }
}
