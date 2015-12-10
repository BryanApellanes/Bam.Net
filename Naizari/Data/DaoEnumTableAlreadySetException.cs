/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Naizari.Data
{
    public class DaoEnumTableAlreadySetException<T, EnumType>: Exception where T: DaoObject, new()
    {
        public DaoEnumTableAlreadySetException()
            : base(string.Format("The table type {0} has already been set to an enum table of {1}", typeof(T).Name, typeof(EnumType).Name))
        {
        }
    }
}
