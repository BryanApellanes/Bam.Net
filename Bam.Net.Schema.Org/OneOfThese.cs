using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Schema.Org.DataTypes
{
    public class OneOfThese<T1, T2> : DataType
       where T1 : DataType
       where T2 : DataType
    {
        public T1 Type1()
        {
            return Value<T1>();
        }

        public T2 Type2()
        {
            return Value<T2>();
        }
    }

    public class OneOfThese<T1, T2, T3> : OneOfThese<T1, T2>
        where T1 : DataType
        where T2 : DataType
        where T3 : DataType
    {
        public T3 Type3()
        {            
            return Value<T3>();
        }
    }

    public class OneOfThese<T1, T2, T3, T4>: OneOfThese<T1, T2, T3>
                where T1 : DataType
        where T2 : DataType
        where T3 : DataType
        where T4 : DataType
    {
        public T4 Type4()
        {
            return Value<T4>();
        }
    }

    public class OneOfThese<T1, T2, T3, T4, T5>: OneOfThese<T1, T2, T3, T4>
                      where T1 : DataType
        where T2 : DataType
        where T3 : DataType
        where T4 : DataType
        where T5: DataType
    {
        public T5 Type5()
        {
            return Value<T5>();
        }
    }
}
