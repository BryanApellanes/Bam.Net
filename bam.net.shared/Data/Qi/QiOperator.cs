/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bam.Net.Data.Qi
{
    public enum QiOperator
    {
        Invalid,
        Equals,
        NotEqualTo,
        GreaterThan,
        LessThan,
        StartsWith,
        DoesntStartWith,
        EndsWith,
        DoesntEndWith,
        Contains,
        DoesntContain,
        OpenParen,
        CloseParen,
        AND,
        OR
    }
}
