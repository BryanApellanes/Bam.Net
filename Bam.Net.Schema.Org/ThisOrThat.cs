/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bam.Net.Schema.Org
{
    public class ThisOrThat<L, R>: DataType 
        where L: DataType 
        where R: DataType
    {
        public L GetLeft()
        {
            return Value<L>();
        }

        public R GetRight()
        {
            return Value<R>();
        }
    }

    public class ThisOrThat<L, M, R> : ThisOrThat<L, R>
        where L : DataType
        where M : DataType
        where R : DataType
    {
        public M GetMiddle()
        {
            return Value<M>();
        }
    }

}
