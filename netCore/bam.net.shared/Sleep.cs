using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net
{
    public static class Sleep
    {
        public static void Until(Func<bool> checkCondition, int sleep = 300)
        {
            Exec.SleepUntil(checkCondition, sleep);
        }
    }
}
