/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.ExceptionHandling
{
    public interface IArbitrateExceptions
    {
        ExceptionArbiter ExceptionArbiter { get; set; }
    }
}
