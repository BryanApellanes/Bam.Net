﻿using Bam.Net.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.CoreServices.WebHooks
{
    [Serializable]
    public class WebHookSubscriber: RepoData
    {
        public virtual WebHookDescriptor Descriptor { get; set; }
        public string Url { get; set; }
    }
}
