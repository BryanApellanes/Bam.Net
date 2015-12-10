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
    public class Boolean: DataType
    {
        public Boolean()
        {
            this.Name = "Boolean";
        }

        public Boolean(bool value)
            : this()
        {
            this.Value = value;
        }

        public static implicit operator bool(Boolean boolean)
        {
            return boolean.Value;
        }

        public static implicit operator Boolean(bool boolean)
        {
            return new Boolean(boolean);
        }

        public bool Value { get; set; }
    }
}
