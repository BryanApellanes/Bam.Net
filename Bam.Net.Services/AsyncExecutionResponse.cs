﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Services.AsyncCallback.Data;

namespace Bam.Net.Services
{
    public class AsyncExecutionResponse: AsyncExecutionResponseData
    {
        public object Result { get; set; }
    }
}
