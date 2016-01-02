/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;

namespace Naizari.Data
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public class DaoPrimaryKeyColumn: DaoColumn
    {
        public DaoPrimaryKeyColumn() { }
        public DaoPrimaryKeyColumn(string columnName, int maxLength)
            : base(columnName, maxLength)
        {
            
        }

      
    }
}
