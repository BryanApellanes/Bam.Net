/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Schema.Org
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
            this.Value = value;
        }

        public static implicit operator DateTime(Date date)
        {
            return date.Value;
        }

        public static implicit operator Date(DateTime date)
        {
            return new Date(date);
        }

        public DateTime Value { get; set; }

        public override string ToString()
        {
            return Value.ToString("s");
        }
    }
}
