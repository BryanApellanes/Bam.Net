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
    public class Boolean: DataType
    {
        public Boolean()
        {
            this.Name = "Boolean";
        }

        public Boolean(bool value)
            : this()
        {
            Value<bool>(value);
        }

        public static implicit operator bool(Boolean boolean)
        {
            return boolean.Value<bool>();
        }

        public static implicit operator Boolean(bool boolean)
        {
            return new Boolean(boolean);
        }        
    }
}
