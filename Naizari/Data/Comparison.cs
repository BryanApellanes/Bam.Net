/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;

namespace Naizari.Data
{
    public enum Comparison
    {
        Invalid,
        Equals,
        NotEqualTo,
        StartsWith,
        EndsWith,
        Contains,
        DoesntStartWith,
        DoesntEndWith,
        DoesntContain,
        GreaterThan,
        GreaterThanOrEqualTo,
        LessThan,
        LessThanOrEqualTo,
    }
}
