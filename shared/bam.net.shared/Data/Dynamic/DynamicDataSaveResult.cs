using Bam.Net.Data.Dynamic.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bam.Net.Data.Dynamic
{
    public class DynamicDataSaveResult
    {
        public DynamicTypeDescriptor DynamicTypeDescriptor { get; set; }
        public DataInstance DataInstance { get; set; }
    }
}
