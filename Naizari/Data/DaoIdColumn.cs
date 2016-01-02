/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;

namespace Naizari.Data
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class DaoIdColumn : DaoColumn
    {
        public DaoIdColumn()
            : base("", -1)
        { }

        public DaoIdColumn(string columnName, int maxLength)
            : base(columnName, maxLength)
        { }
    }
}
