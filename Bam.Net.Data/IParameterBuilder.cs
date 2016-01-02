/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Data
{
    public interface IParameterBuilder
    {
        DbParameter[] GetParameters(IHasFilters filter);        
    }
}
