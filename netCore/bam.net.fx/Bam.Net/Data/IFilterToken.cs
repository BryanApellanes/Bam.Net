/*
	Copyright © Bryan Apellanes 2015  
*/
using System;

namespace Bam.Net.Data
{
    public interface IFilterToken
    {
        string Operator { get; set; }
        string ToString();
    }
}
