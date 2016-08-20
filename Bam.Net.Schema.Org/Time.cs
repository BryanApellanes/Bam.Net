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
    public class Time: DataType
    {
        public Time()
        {
            this.Name = "Time";
        }

        public Time(DateTime value)
        {
            Value<DateTime>(value);
        }

        public static implicit operator DateTime(Time time)
        {
            return time.Value<DateTime>();
        }

        public static implicit operator Time(DateTime value)
        {
            return new Time(value);
        }
        
        public override string ToString()
        {
            return Value<DateTime>().ToString("s");
        }
    }
}
