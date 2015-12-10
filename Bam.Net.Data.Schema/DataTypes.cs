/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bam.Net.Data.Schema
{
    public enum DataTypes
    {
        Default,
        Boolean,
        Int,
        Long,
        Decimal,
        String,
        /// <summary>
        /// The field will be generated as a byte array (byte[])
        /// </summary>
        ByteArray,
        DateTime
    }
}
