﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Automation
{
    public interface IWorkerTypeProvider
    {
        Type[] GetWorkerTypes();
    }
}
