/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Schema.Org.DataTypes
{
    public class Date: DataType
    {
        public Date()
        {
            this.Name = "Date";
        }

        public Date(DateTime value)
            : this()
        {
            Value<DateTime>(value);
        }
        
        public override string ToString()
        {
            return Value<DateTime>().ToString("s");
        }
    }
}
