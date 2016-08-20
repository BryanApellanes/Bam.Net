/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bam.Net.Schema.Org.DataTypes
{
    public class Integer: Number
    {
        public Integer()
        {
            this.Name = "Integer";
        }

        public Integer(int value)
            : this()
        {
            Value<int>(value);
        }
    }
}
