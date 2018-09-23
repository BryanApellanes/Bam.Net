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
    public class Number: DataType
    {
        public Number()
        {
            this.Name = "Number";
        }

        public Number(int value)
            : this()
        {
            Value<int>(value);
        }

        public Number(long value)
            : this()
        {
            Value<long>(value);
        }

        public Number(float value)
            : this()
        {
            Value<float>(value);
        }

        public static implicit operator int(Number number)
        {
            return number.Value<int>();
        }

        public static implicit operator long(Number number)
        {
            return number.Value<long>();
        }

        public static implicit operator float(Number number)
        {
            return number.Value<float>();
        }

        public static implicit operator Number(float f)
        {
            return new Number(f);
        }

        public static implicit operator Number(int i)
        {
            return new Number(i);
        }

        public static implicit operator Number(long l)
        {
            return new Number(l);
        }
    }
}
