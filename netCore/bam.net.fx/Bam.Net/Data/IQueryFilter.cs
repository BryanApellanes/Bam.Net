/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections;
using System.Collections.Generic;

namespace Bam.Net.Data
{
    public interface IQueryFilter<C>: IQueryFilter where C : IFilterToken, new()
    {
    }

    public interface IQueryFilter: IHasFilters, IHasParameterInfos
    {
        string Parse();
        string Parse(int? number);
    }
}
