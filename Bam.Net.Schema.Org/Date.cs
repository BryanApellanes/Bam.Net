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

        public DateTime Value { get; set; }

        public override string ToString()
        {
            return Value.ToString("s");
        }
    }
}
