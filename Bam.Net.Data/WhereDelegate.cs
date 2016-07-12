/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bam.Net.Data;
using System.Data;
using System.Data.Common;

namespace Bam.Net.Data
{
    public delegate QueryFilter WhereDelegate<C>(C where) where C : IQueryFilter, IFilterToken, new();
}
