/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Bam.Net.Data
{
    public interface IHasDataRow
    {
        Database Database { get; set; }
        DataRow DataRow { get; set; }
    }
}
